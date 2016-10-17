using EKIFVK.Todo.API.Models;
using Microsoft.AspNetCore.Http;

namespace EKIFVK.Todo.API.Services
{
    /// <summary>
    /// 权限验证服务
    /// </summary>
    public interface IPermissionService
    {
        string FindToken(IHeaderDictionary header);
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <param name="ip">用户IP</param>
        /// <param name="permissionId">要验证的权限的ID</param>
        /// <param name="autoUpdateTime">是否更新用户活动时间</param>
        /// <returns></returns>
        PermissionService.VerifyResult Verify(string token, string ip = "", int permissionId = -1, bool autoUpdateTime = true);
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <param name="ip">用户IP</param>
        /// <param name="permissionId">要验证的权限的ID</param>
        /// <param name="autoUpdateTime">是否更新用户活动时间</param>
        /// <returns></returns>
        PermissionService.VerifyResult Verify(string token, string ip = "", string permissionId = "", bool autoUpdateTime = true);
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="ip">用户IP</param>
        /// <param name="permissionId">要验证的权限的ID</param>
        /// <param name="autoUpdateTime">是否更新用户活动时间</param>
        /// <returns></returns>
        PermissionService.VerifyResult Verify(SystemUser user, string ip = "", string permissionId = "", bool autoUpdateTime = true);
        /// <summary>
        /// 更新用户活动时间
        /// </summary>
        /// <param name="token">用户Token</param>
        void UpdateAccessTime(string token);
        /// <summary>
        /// 更新用户活动时间
        /// </summary>
        /// <param name="user">用户实体</param>
        void UpdateAccessTime(SystemUser user);
        /// <summary>
        /// 转换权限ID
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        string ToIdentifer(int id);
        /// <summary>
        /// 转换权限ID
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        int ToIdentifer(string id);
        /// <summary>
        /// 将权限验证结果转换为字符串
        /// </summary>
        /// <param name="result">要转换的权限验证结果</param>
        /// <returns></returns>
        string ToString(PermissionService.VerifyResult result);
    }
}