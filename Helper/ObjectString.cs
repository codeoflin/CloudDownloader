namespace CloudDownloader.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ParseJSON<T>(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            //Newtonsoft.Json.JsonConvert.DeserializeObject(str)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}