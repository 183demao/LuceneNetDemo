using System;
using System.Diagnostics;
using Lucene.Net.Documents;

namespace LuceneNetDemo.IndexModels
{
    public enum IndexTypes
    {
        Common = 0,
        Exe = 1,
        Dll = 2,
        Image = 3,
    }

    public class IndexModel<TEntity> : IIndexModel<TEntity>
    {
        /// <summary>
        /// 索引
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public TEntity AttachedEntity { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 索引类型
        /// </summary>
        public virtual IndexTypes IndexType { get; set; } = IndexTypes.Common;

        /// <summary>
        /// 定位
        /// </summary>
        /// <returns></returns>
        public virtual void Locate()
        {
            Debug.Print($"定位索引：{this.ToString()}");
        }

        /// <summary>
        /// 创建文档
        /// </summary>
        /// <returns></returns>
        public virtual Document CreateDocument()
        {
            var document = new Document();

            document.Add(new Field(nameof(IIndexModel.Index), this.Index, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field(nameof(IIndexModel.Description), this.Description, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(nameof(IIndexModel.IndexType), this.IndexType.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(nameof(IIndexModel.Path), this.Path, Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        public virtual void FromDocument(Document document)
        {
            this.Index = document.Get(nameof(IIndexModel.Index));
            this.Description = document.Get(nameof(IIndexModel.Description));
            this.Path = document.Get(nameof(IIndexModel.Path));
            this.IndexType = Enum.TryParse<IndexTypes>(document.Get(nameof(IIndexModel.IndexType)), out var result) ? result : IndexTypes.Common;
        }

        public override string ToString()
            => $"{this.Index} : {this.AttachedEntity?.ToString()}";
    }
}
