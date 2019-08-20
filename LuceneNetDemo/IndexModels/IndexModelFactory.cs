using System;
using System.IO;
using Lucene.Net.Documents;
using LuceneNetDemo.CustomModels;

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
            IIndexModel indexModel;

            switch (extension)
            {
                case ".exe":
                    {
                        indexModel = new EXEIndexModel() { AttachedEntity = address };
                        break;
                    }
                case ".dll":
                    {
                        indexModel = new DLLIndexModel() { AttachedEntity = address };
                        break;
                    }
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".ico":
                    {
                        indexModel = new ImageIndexModel() { AttachedEntity = address };
                        break;
                    }
                default:
                    {
                        indexModel = new CommonIndexModel() { AttachedEntity = address };
                        break;
                    }
            }

            indexModel.Index = Path.GetFileName(address);
            indexModel.Description = Path.GetFileName(address);

            return indexModel;
        }

        /// <summary>
        /// 创建索引模型
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IIndexModel CreateIndexModel(Document document)
        {
            var field = document.Get(nameof(IIndexModel.IndexType));
            IndexTypes indexType = Enum.TryParse(field, out indexType) ? indexType : IndexTypes.Common;
            IIndexModel indexModel;

            switch (indexType)
            {
                case IndexTypes.Exe:
                    {
                        indexModel = new EXEIndexModel();
                        break;
                    }
                case IndexTypes.Dll:
                    {
                        indexModel = new DLLIndexModel();
                        break;
                    }
                case IndexTypes.Image:
                    {
                        indexModel = new ImageIndexModel();
                        break;
                    }
                case IndexTypes.Common:
                default:
                    {
                        indexModel = new CommonIndexModel();
                        break;
                    }
            }

            indexModel.FromDocument(document);
            return indexModel;
        }
    }
}
