using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudDownloader.Helper;

namespace CloudDownloader.Helper
{
	/// <summary>
	/// 
	/// </summary>
	public static class LogHelper
	{
		#region string
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <param name="cls"></param>
		public static void Log(this string str, string cls)
		{
			var trace = new StackTrace();
			var thisclassname = trace.GetFrame(0).GetMethod().DeclaringType.Name;
			var classname = "";
			for (int i = 1; trace.GetFrame(i) != null; i++)
			{
				classname = trace.GetFrame(i).GetMethod().DeclaringType.Name;
				if (classname != thisclassname) break;
			}
			if (string.IsNullOrEmpty(classname)) classname = thisclassname;
			var path = $"./Logs/{DateTime.Now.ToString("yyyy-MM-dd")}/";
			Directory.CreateDirectory(path);
			path = $"{path}{classname}.log";
			File.AppendAllText(path, $"{DateTime.Now.ToString("HH:mm:ss.fff")} {cls} > {str}{Environment.NewLine}");
			//Console.WriteLine($"[{path}][{cls}]{str}");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		public static void LogForInfomation(this string str) => str.Log("Info");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		public static void LogForDebug(this string str) => str.Log("Debug");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		public static void LogForWarning(this string str) => str.Log("Warning");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		public static void LogForError(this string str) => str.Log("Error");
		#endregion

		#region Exception 这个类型只能保存为Error
		/// <summary>
		/// 
		/// </summary>
		/// <param name="err"></param>
		/// <param name="cls"></param>
		public static void Log(this Exception err, string cls)
		{
			($"{err.Message}\r\n{err.StackTrace}").Log(cls);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="err"></param>
		public static void LogForError(this Exception err) => err.Log("Error");
		#endregion

		#region byte[]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="cls"></param>
		public static void Log(this byte[] data, string cls)
		{
			data.ByteToHexStr().Log(cls);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public static void LogForInfomation(this byte[] data) => data.Log("Info");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public static void LogForDebug(this byte[] data) => data.Log("Debug");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public static void LogForWarning(this byte[] data) => data.Log("Warning");

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public static void LogForError(this byte[] data) => data.Log("Error");
		#endregion
	}//End Class
}