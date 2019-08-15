using System.Collections.Generic;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.Assists
{
    /// <summary>
    /// 全文检索助手接口
    /// </summary>
    interface IFullTextSeachAssist
    {
        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexModels"></param>
        void InitIndices(IEnumerable<IIndexModel> indexModels);
    }
}
