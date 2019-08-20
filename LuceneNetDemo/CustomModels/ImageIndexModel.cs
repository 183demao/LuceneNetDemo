using System.Windows.Forms;
using Lucene.Net.Documents;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.CustomModels
{
    public class ImageIndexModel : IndexModel<string>
    {
        public override IndexTypes IndexType { get; set; } = IndexTypes.Image;

        public override void Locate()
        {
            base.Locate();

            MessageBox.Show($"打开图片文件索引：{this.Index}");
        }

        public override Document CreateDocument()
            => base.CreateDocument();

        public override void FromDocument(Document document)
            => base.FromDocument(document);
    }
}