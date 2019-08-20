using System.Windows.Forms;
using Lucene.Net.Documents;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.CustomModels
{
    public class CommonIndexModel : IndexModel<string>
    {
        public override void Locate()
        {
            base.Locate();

            MessageBox.Show($"使用通用方法打开索引：{this.Index}");
        }

        public override Document CreateDocument()
            => base.CreateDocument();

        public override void FromDocument(Document document)
            => base.FromDocument(document);
    }
}
