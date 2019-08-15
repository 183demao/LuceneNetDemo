using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuceneNetDemo.IndexModels;

namespace LuceneNetDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.InputTextBox.Focus();
        }

        private async void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            string input = this.InputTextBox.Text;
            var indexModels = (await this.Search(input)).ToArray();

            this.OutputListBox.Items.Clear();
            this.OutputListBox.Items.AddRange(indexModels);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<IEnumerable<IIndexModel>> Search(string input)
        {
            Debug.Print($"搜索关键字：{input}");
            return Enumerable.Empty<IIndexModel>();
        }

        private void OutputListBox_DoubleClick(object sender, EventArgs e)
        {
            if (!(this.OutputListBox.SelectedItem is IIndexModel indexModel))
            {
                return;
            }

            indexModel.Locate();
        }
    }
}
