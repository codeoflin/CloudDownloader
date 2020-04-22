using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using CloudDownloader.Helper;
namespace CloudDownloader.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        //private readonly MyDbContext DbContext;
        /// <summary>
        /// 
        /// </summary>
        public TestController()
        {
            //DbContext = dbcontext;
        }

        /// <summary>
        /// 字符串链接测试
        /// </summary>
        /// <param name="code"></param>
        /// <param name="aa"></param>
        /// <returns></returns>
        [HttpGet]
        public string queryTest(string code, int aa)
        {
            //DbContext.Order.Add(new Modules.Order() { Code = code });
            //DbContext.SaveChanges();
            return code + aa.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string queryTestA()
        {
            //DbContext.Order.Add(new Modules.Order(){Code=code});
            //DbContext.SaveChanges();
            return "456";
        }

        /// <summary>
        /// 查询日期时间返回值对象
        /// </summary>
        public class GetTimeResponse
        {
            /// <summary>
            /// 查询结果
            /// </summary>
            /// <value>0:成功 -1:失败</value>
            public int Code { get; set; } = 0;
            /// <summary>
            /// 日期时间
            /// </summary>
            /// <value></value>
            public DateTime Time { get; set; } = DateTime.Now;
        }

        /// <summary>
        /// 查询日期时间
        /// </summary>
        /// <returns>查询接口返回对象</returns>
        [HttpGet]
        public GetTimeResponse GetTime()
        {
            return new GetTimeResponse();
        }
    }//End Class
}
