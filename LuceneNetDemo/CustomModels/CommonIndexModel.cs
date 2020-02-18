using System.Diagnostics;
using Lucene.Net.Documents;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo.CustomModels
{
    public class CommonIndexModel : IndexModel<string>
    {
        public override void Locate()
        {
            Process.Start("notepad.exe", this.Path);
        }

        public override Document CreateDocument()
        {
            var document = base.CreateDocument();
            document.Add(new Field(nameof(this.AttachedEntity), this.AttachedEntity, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }

        public override void FromDocument(Document document)
        {
            base.FromDocument(document);
            this.AttachedEntity = document.Get(nameof(this.AttachedEntity));
        }
    }
}
