using System.Diagnostics;
using System.IO;

namespace LuceneNetDemo.IndexModels
{
    /// <summary>
    /// 索引模型工厂
    /// </summary>
    public static class IndexModelFactory
    {
        /// <summary>
        /// 创建索引模型
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IIndexModel CreateIndexModel(string address)
        {
            string extension = Path.GetExtension(address).ToLower();

            switch (extension)
            {
                case "":
                    {
                        break;
                    }
                default:
                    break;
            }

            return new IndexModel<string>()
            {
                Index = Path.GetFileName(address),
                AttachedEntity = address,
                Description = $"文件 => {address}",
            };
        }
    }
}
