using System;
using System.Linq;
using EKIFVK.DeusLegem.CreationSystem.API;
using EKIFVK.Todo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EKIFVK.Todo.API.Services
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly DatabaseContext _database;
        private readonly IOptions<SystemConfig> _config;
        private readonly IOptions<SystemConsts> _consts;

        public PermissionService(DatabaseContext database, IOptions<SystemConfig> config, IOptions<SystemConsts> consts)
        {
            _database = database;
            _config = config;
            _consts = consts;
        }

        private static bool CheckToken(string token)
        {
            return token.ToUpper() == token && token.Length == 36;
        }

        public VerifyResult Verify(string token, string ip = "", int permissionId = -1, bool autoUpdateTime = true)
        {
            return Verify(token, ip, ToIdentifer(permissionId), autoUpdateTime);
        }

        public VerifyResult Verify(string token, string ip = "", string permissionId = "", bool autoUpdateTime = true)
        {
            if (!CheckToken(token)) return VerifyResult.InvalidToken;
            var user = _database.SystemUser.FirstOrDefault(e => e.AccessToken == token);
            return Verify(user, ip, permissionId, autoUpdateTime);
        }

        public VerifyResult Verify(SystemUser user, string ip = "", string permissionId = "", bool autoUpdateTime = true)
        {
            if (user == null) return VerifyResult.EmptyAccount;
            var group = _database.SystemUsergroup.FirstOrDefault(e => e.Id == user.Usergroup);
            if (!user.Enabled || !group.Enabled) return VerifyResult.Denied;
            if (!user.LastActiveTime.HasValue || user.LastActiveTime.Value.AddMinutes(_config.Value.TokenAvaliableTime) < DateTime.Now) return VerifyResult.OutOfTime;
            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(user.LastAccessIp) && user.LastAccessIp != ip) return VerifyResult.Denied;
            if (autoUpdateTime) UpdateAccessTime(user);
            if (string.IsNullOrEmpty(permissionId)) return VerifyResult.Authorized;
            var permissions = group.Permission.Split(' ');
            return permissions.Any(e => e == permissionId) ? VerifyResult.Authorized : VerifyResult.Denied;
        }

        public void UpdateAccessTime(string token)
        {
            if (!CheckToken(token)) return;
            var user = _database.SystemUser.FirstOrDefault(e => e.AccessToken == token);
            if (user == null) return;
            UpdateAccessTime(user);
        }

        public void UpdateAccessTime(SystemUser user)
        {
            user.LastActiveTime = DateTime.Now;
            _database.SaveChanges();
        }

        public string ToIdentifer(int id)
        {
            if (id < 0) return "";
            var permission = _database.SystemPermission.FirstOrDefault(e => e.Id == id);
            return permission?.Name ?? "";
        }

        public int ToIdentifer(string id)
        {
            if (id == "") return -1;
            var permission = _database.SystemPermission.FirstOrDefault(e => e.Name == id);
            return permission?.Id ?? -1;
        }

        public string ToString(VerifyResult result)
        {
            switch (result)
            {
                case VerifyResult.Authorized:
                    return _consts.Value.PERMISSION_AUTHORIZED;
                case VerifyResult.Denied:
                    return _consts.Value.PERMISSION_DENIED;
                case VerifyResult.EmptyAccount:
                    return _consts.Value.PERMISSION_EMPTYACCOUNT;
                case VerifyResult.InvalidToken:
                    return _consts.Value.PERMISSION_INVALIDTOKEN;
                case VerifyResult.OutOfTime:
                    return _consts.Value.PERMISSION_OUTOFTIME;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public string FindToken(IHeaderDictionary header)
        {
            StringValues token;
            return !header.TryGetValue("X-Access-Token", out token) ? "" : token.ToString();
        }

        /// <summary>
        /// 权限检查结果
        /// </summary>
        public enum VerifyResult
        {
            /// <summary>
            /// 授权通过
            /// </summary>
            Authorized,
            /// <summary>
            /// Token超时失效
            /// </summary>
            OutOfTime,
            /// <summary>
            /// Token不合法
            /// </summary>
            InvalidToken,
            /// <summary>
            /// 找不到与Token对应的用户
            /// </summary>
            EmptyAccount,
            /// <summary>
            /// 授权失败
            /// </summary>
            Denied
        }
    }
}
