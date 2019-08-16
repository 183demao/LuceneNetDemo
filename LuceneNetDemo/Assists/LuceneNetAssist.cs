using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.Assists
{
    /// <summary>
    /// Lucene.Net 全文检索引擎
    /// </summary>
    /// <see cref="https://github.com/apache/lucenenet"/>
    /// <seealso cref="http://lucenenet.apache.org/"/>
    /// <seealso cref="https://blog.csdn.net/lovestj/article/details/78687223"/>
    public class LuceneNetAssist : IFullTextSeachAssist
    {
        /// <summary>
        /// 目录
        /// </summary>
        /// <remarks>RAMDirectory: 内存索引；FSDirectory: 文件索引；</remarks>
        private readonly Directory Directory = new RAMDirectory();

        /// <summary>
        /// 分析器
        /// </summary>
        private readonly Analyzer Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

        /// <summary>
        /// 单实例
        /// </summary>
        /// <returns></returns>
        public static LuceneNetAssist Singleton { get; } = new LuceneNetAssist();

        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexModels"></param>
        public void InitIndices(List<IIndexModel> indexModels)
        {
            // true: 覆写索引；false: 追加索引；
            using (IndexWriter writer = new IndexWriter(this.Directory, this.Analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                indexModels.ForEach(index =>
                {
                    var document = new Document();
                    document.Add(new Field(nameof(IIndexModel.Index), index.Index, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field(nameof(IIndexModel.Description), index.Description, Field.Store.YES, Field.Index.ANALYZED));
                    writer.AddDocument(document);
                });

                writer.Optimize();
                writer.Commit();
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<IIndexModel> Search(string keyword)
        {
            List<IIndexModel> results = new List<IIndexModel>();
            IndexSearcher searcher = new IndexSearcher(
                this.Directory,
                true);
            MultiFieldQueryParser parser = new MultiFieldQueryParser(
                Version.LUCENE_30,
                new[] { nameof(IIndexModel.Index), nameof(IIndexModel.Description) },
                this.Analyzer);
            Query query = parser.Parse(keyword);
            var hits = searcher.Search(query, 20);
            searcher.Dispose();

            return default;
        }
    }
}
