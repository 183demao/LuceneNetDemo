namespace LuceneNetDemo.IndexModels
{
    /// <summary>
    /// 索引模型接口
    /// </summary>
    public interface IIndexModel
    {
        /// <summary>
        /// 索引
        /// </summary>
        string Index { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// 定位
        /// </summary>
        /// <returns></returns>
        void Locate();
    }

    /// <summary>
    /// 索引模型接口
    /// </summary>
    public interface IIndexModel<TEntity> : IIndexModel
    {
        /// <summary>
        /// 附加数据
        /// </summary>
        TEntity AttachedEntity { get; set; }
    }
}
