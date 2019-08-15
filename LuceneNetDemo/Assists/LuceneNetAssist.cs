using System;
using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.Assists
{
    public class LuceneNetAssist : IFullTextSeachAssist
    {
        /// <summary>
        /// 懒加载实体
        /// </summary>
        protected Lazy<LuceneNetAssist> luceneNetAssist = new Lazy<LuceneNetAssist>();

        /// <summary>
        /// 获取单实例
        /// </summary>
        /// <returns></returns>
        public LuceneNetAssist GetSingleton() => this.luceneNetAssist.Value;

        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexModels"></param>
        public void InitIndices(IEnumerable<IIndexModel> indexModels)
        {
            // RAMDirectory: 内存索引；FSDirectory: 文件索引；
            Directory directory = new RAMDirectory();
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            // true: 覆写索引；false: 追加索引；
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);


        }
    }
}
