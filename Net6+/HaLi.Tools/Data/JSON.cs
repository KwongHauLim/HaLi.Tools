using Newtonsoft.Json;

namespace HaLi.Tools.Data;

public static class JSON
{
    /// <summary>
    /// 将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">要序列化的对象</param>
    /// <returns>JSON字符串</returns>
    public static string S(object obj)
        => JsonConvert.SerializeObject(obj);

    /// <summary>
    /// 将JSON字符串反序列化为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <returns>反序列化后的对象</returns>
    public static T D<T>(string json)
        => JsonConvert.DeserializeObject<T>(json);

    /// <summary>
    /// 将对象序列化为JSON并写入文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="obj">要序列化的对象</param>
    public static void FS(string path, object obj)
        => File.WriteAllText(path, S(obj));

    /// <summary>
    /// 从文件中读取JSON字符串并反序列化为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="path">文件路径</param>
    /// <returns>反序列化后的对象</returns>
    public static T FD<T>(string path)
        => D<T>(File.ReadAllText(path));
}
