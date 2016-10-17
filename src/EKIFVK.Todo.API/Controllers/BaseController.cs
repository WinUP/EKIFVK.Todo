using System.Collections;
using EKIFVK.DeusLegem.CreationSystem.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EKIFVK.Todo.API.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IOptions<SystemConsts> Consts;

        public BaseController(IOptions<SystemConsts> consts)
        {
            Consts = consts;
        }

        /// <summary>
        /// 获取经过修饰的JSON Response
        /// </summary>
        /// <param name="statusCode">Response状态码</param>
        /// <param name="message">Response消息</param>
        /// <param name="data">Response数据</param>
        /// <returns></returns>
        protected JsonResult JsonResponse(int statusCode = StatusCodes.Status200OK, string message = null, object data = null)
        {
            Response.StatusCode = statusCode;
            return Json(new Hashtable {{"data", data ?? ""}, {"message", message ?? Consts.Value.SUCCESS}});
        }

        
    }
}
