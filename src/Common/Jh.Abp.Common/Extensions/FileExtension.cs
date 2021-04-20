using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.Common
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public static class FileExtension
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>成功删除数量</returns>
        public static int FileDelete(this string path)
        {
            var file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 读取String文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> ReadFileAsync(this string path)
        {
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static string ReadFile(this string path)
        {
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 创建目录 自动检测没有创建的目录
        /// </summary>
        /// <param name="directory">new FileInfo(path).Directory</param>
        /// <returns></returns>
        public static bool CreateDirectoryInfo(this DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                var parent= CreateDirectoryInfo(directory.Parent);
                if (parent)
                {//当父级存在的时候再创建子级
                    directory.Create();
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将字符串保存到文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SaveFile(this string fileFullName, string content)
        {
            string dir = Path.GetDirectoryName(fileFullName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(fileFullName, content);
            return fileFullName;
        }
    }
}
