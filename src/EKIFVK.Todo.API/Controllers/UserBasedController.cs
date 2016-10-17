using System;
using System.Linq;
using EKIFVK.DeusLegem.CreationSystem.API;
using EKIFVK.Todo.API.Models;
using EKIFVK.Todo.API.Services;
using Microsoft.Extensions.Options;

namespace EKIFVK.Todo.API.Controllers
{
    public class UserBasedController : BaseController
    {
        protected readonly DatabaseContext Database;
        protected readonly IPermissionService Checker;

        public UserBasedController(DatabaseContext database, IPermissionService checker, IOptions<SystemConsts> consts)
            :base(consts)
        {
            Checker = checker;
            Database = database;
        }

        /// <summary>
        /// 根据用户名查找用户实体（用户名无视大小写）
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        protected SystemUser FindUser(string name)
        {
            return Database.SystemUser.FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 获取当前会话的用户的实体
        /// </summary>
        /// <returns></returns>
        protected SystemUser FindUser()
        {
            var token = Checker.FindToken(Request.Headers);
            return Database.SystemUser.FirstOrDefault(e => e.AccessToken == token);
        }

        /// <summary>
        /// 无视大小写比较字符串
        /// </summary>
        /// <param name="name1">字符串1</param>
        /// <param name="name2">字符串2</param>
        /// <returns></returns>
        protected static bool Equals(string name1, string name2)
        {
            return string.Equals(name1, name2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
