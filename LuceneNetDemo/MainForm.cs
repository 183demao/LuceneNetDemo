using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LuceneNetDemo.Assists;
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

            this.InitApplication();
        }

        private void InitApplication()
        {
            var indices = Directory.GetFiles(@"E:\DEVP\5.xIR_1.1\4.Tags\文档管理\综合版\综合版071", "*.txt", SearchOption.AllDirectories)
                .Select(file => IndexModelFactory.CreateIndexModel(file))
                .ToList();

            LuceneNetAssist.Singleton.InitIndices(indices);
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            string input = this.InputTextBox.Text;
            var indexModels = this.Search(input).ToArray();

            this.Text = $"关键字：{input}，共 {indexModels.Length} 个结果";
            this.OutputListBox.Items.Clear();
            this.OutputListBox.Items.AddRange(indexModels);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IEnumerable<IIndexModel> Search(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Enumerable.Empty<IIndexModel>();
            }

            Debug.Print($"搜索关键字：{input}");

            var results = LuceneNetAssist.Singleton.Search(input);
            return results;
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
