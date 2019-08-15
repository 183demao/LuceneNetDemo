using System.Diagnostics;

namespace LuceneNetDemo.IndexModels
{
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
        /// 附加数据
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 定位
        /// </summary>
        /// <returns></returns>
        public virtual void Locate()
        {
            Debug.Print($"定位索引：{this.ToString()}");
        }

        public override string ToString()
            => $"{this.Index} : {this.AttachedEntity?.ToString()}";
    }
}
