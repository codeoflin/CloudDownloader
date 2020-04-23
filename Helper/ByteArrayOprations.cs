using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CloudDownloader.Helper
{
    /// <summary>
    /// 用于byte[]的操作
    /// </summary>
    public static class ByteArrayOprations
    {
        /// <summary>
        /// 字节数组转指针
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
		public static IntPtr ArrayToIntptr(this byte[] source)
        {
            if (source == null) return IntPtr.Zero;
            var da = source;
            var ptr = Marshal.AllocHGlobal(da.Length);
            Marshal.Copy(da, 0, ptr, da.Length);
            return ptr;
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="path"></param>
		public static void ToFile(this byte[] src, string path)
        {
            File.WriteAllBytes(path, src);
        }

        /*
        /// <summary>
        /// 添加到文件尾(还没实现)
        /// </summary>
        /// <param name="src"></param>
        /// <param name="path"></param>
		
		public static void AppendToFile(this byte[] src,string path)
		{
			//File.AppendText(path,src);
		}
		// */

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hs">Example: FF1E12</param>
        /// <returns></returns>	 
        public static byte[] HexStringToBytes(this string hs)
        {
            hs = hs.Replace(" ", "");
            hs = hs.Replace("-", "");
            hs = hs.Replace("_", "");
            if ((hs.Length % 2) != 0) throw new ArgumentException();
            var b = new byte[hs.Length / 2];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < hs.Length / 2; i++) b[i] = Convert.ToByte(hs[i * 2].ToString() + hs[(i * 2) + 1].ToString(), 16);
            //按照指定编码将字节数组变为字符串
            return b;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>	 
        public static string ByteToHexStr(this byte[] bytes)
        {
            string str = "";
            if (bytes == null) return null;
            for (int i = 0; i < bytes.Length; i++) str += bytes[i].ToString("X2");
            return str;
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string str)
        {
            return Convert.FromBase64String(str);
        }

    }//End Class

}