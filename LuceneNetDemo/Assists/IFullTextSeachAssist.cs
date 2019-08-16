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
        void InitIndices(List<IIndexModel> indexModels);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<IIndexModel> Search(string keyword);
    }
}
