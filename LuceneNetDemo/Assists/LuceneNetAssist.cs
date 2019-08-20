using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;
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
        static LuceneNetAssist()
        {
            try
            {
                System.IO.Directory.CreateDirectory(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SearchIndex"));
            }
            finally
            {
            }
        }

        /// <summary>
        /// 目录
        /// </summary>
        /// <remarks>RAMDirectory: 内存索引；FSDirectory: 文件索引；</remarks>
        private readonly Lucene.Net.Store.Directory Directory = FSDirectory.Open(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SearchIndex"));

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
        /// 分词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> Segment(string text)
        {
            List<string> results = new List<string>(100);

            using (StringReader reader = new StringReader(text))
            {
                using (TokenStream stream = this.Analyzer.TokenStream(text, reader))
                {
                    ITermAttribute termAttribute;

                    while (stream.IncrementToken())
                    {
                        termAttribute = stream.GetAttribute<ITermAttribute>();
                        results.Add(termAttribute.Term);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <param name="indexModels"></param>
        public void InitIndices(List<IIndexModel> indexModels)
        {
            // true: 覆写索引；false: 追加索引；
            using (IndexWriter writer = new IndexWriter(this.Directory, this.Analyzer, true, IndexWriter.MaxFieldLength.LIMITED))
            {
                indexModels.ForEach(index =>
                {
                    try
                    {
                        var document = new Document();

                        // 在写入索引时必是不分词，否则是模糊搜索和删除，会出现混乱  
                        document.Add(new Field(nameof(IIndexModel.Index), index.Index, Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field(nameof(IIndexModel.Description), index.Description, Field.Store.YES, Field.Index.ANALYZED));

                        writer.AddDocument(document);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"创建索引失败：{ex.Message}");
                    }
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
            //查询字段  
            string[] fileds = { nameof(IIndexModel.Index), nameof(IIndexModel.Description) };
            QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fileds, this.Analyzer);
            Query query = parser.Parse(keyword);
            IndexSearcher searcher = new IndexSearcher(this.Directory, true);
            TopDocs docs = searcher.Search(query, 50);
            return docs.ScoreDocs
                .Select(hit =>
                {
                    var doc = searcher.Doc(hit.Doc);
                    var index = doc.Get(nameof(IIndexModel.Index));
                    var description = doc.Get(nameof(IIndexModel.Description));

                    return new IndexModel<string>()
                    {
                        Index = index,
                        Description = description,
                    } as IIndexModel;
                })
                .ToList();
        }
    }
}
