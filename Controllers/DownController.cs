using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Drawing;
using System.Net;
using System.IO;
using CloudDownloader.Helper;

namespace CloudDownloader.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class DownController : ControllerBase
    {
        //private readonly MyDbContext DbContext;
        /// <summary>
        /// 
        /// </summary>
        public DownController()
        {
            //DbContext = dbcontext;
        }

        /// <summary>
        /// 字节单位换算
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string BytesLenToString(Int64 bytes)
        {
            if (bytes < 1024) return $"{bytes}Bytes";
            if (bytes < (Math.Pow(1024, 2))) return $"{(bytes / 1024).ToString("0.00")}KB";
            if (bytes < (Math.Pow(1024, 3))) return $"{(bytes / Math.Pow(1024, 2)).ToString("0.00")}MB";
            if (bytes < (Math.Pow(1024, 4))) return $"{(bytes / Math.Pow(1024, 3)).ToString("0.00")}GB";
            if (bytes < (Math.Pow(1024, 5))) return $"{(bytes / Math.Pow(1024, 4)).ToString("0.00")}TB";
            if (bytes < (Math.Pow(1024, 6))) return $"{(bytes / Math.Pow(1024, 5)).ToString("0.00")}PB";
            if (bytes < (Math.Pow(1024, 7))) return $"{(bytes / Math.Pow(1024, 6)).ToString("0.00")}EB";
            if (bytes < (Math.Pow(1024, 8))) return $"{(bytes / Math.Pow(1024, 7)).ToString("0.00")}ZB";
            if (bytes < (Math.Pow(1024, 9))) return $"{(bytes / Math.Pow(1024, 8)).ToString("0.00")}BB";
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HttpGet(string url)
        {
            Directory.CreateDirectory(".tmp");
            //准备请求
            var myrequest = (HttpWebRequest)WebRequest.Create(url);
            myrequest.Method = "GET";
            var res = myrequest.GetResponse() as HttpWebResponse;
            var size = res.ContentLength;
            var filename = res.Headers["Content-Disposition"];
            if (string.IsNullOrWhiteSpace(filename)) filename = res.ResponseUri.Segments.Last();
            var index = 0;
            var contenttype = res.ContentType;
            var rs = res.GetResponseStream();
            //var f = System.IO.File.Create($".tmp/{DateTime.Now.Ticks}");
            var buff = new byte[size];
            $"开始下载文件:{filename} 长度:{size}".LogForInfomation();
            while (index < size)
            {
                var starttime = DateTime.Now;
                var readlen = rs.Read(buff, index, (int)size - index);
                var timespace = (DateTime.Now - starttime).TotalMilliseconds;
                index += readlen;
                if (timespace > 800) Console.WriteLine($"{filename} Size:{BytesLenToString(size)} Downloading:{((index / (size / 100))).ToString()}% Speed:{BytesLenToString((long)((readlen / timespace * 1000)))}/S");
            }
            $"下载文件完成! {filename}".LogForInfomation();
            Console.WriteLine($"{filename} Done!");
            rs.Close();
            rs.Dispose();
            res.Close();
            res.Dispose();
            myrequest.Abort();

            return base.File(buff, contenttype, filename);
        }
    }//End Class
}
