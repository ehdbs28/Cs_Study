using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Memo
{
    public partial class Form1 : Form
    {
        private bool modifyFlag = false;
        private string fileName = "Noname.txt";

        public Form1()
        {
            InitializeComponent();
            this.Text = fileName + "- myNotePad";
        }

        private void TxtMemo_TextChanged(object sender, EventArgs e)
        {
            modifyFlag = true;
        }

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileProcessBeforClose();

            TxtMemo.Text = "";
            modifyFlag = false;
            fileName = "Noname.txt";
        }

        private void FileProcessBeforClose()
        {
            if (modifyFlag)
            {
                DialogResult ans = MessageBox.Show("변경 내용을 저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);

                if(ans == DialogResult.Yes)
                {
                    if(fileName == "Noname.txt")
                    {
                        if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter sw = File.CreateText(saveFileDialog1.FileName);
                            sw.WriteLine(TxtMemo.Text);
                            sw.Close();
                        }
                    }
                    else
                    {
                        StreamWriter sw = File.CreateText(fileName);
                        sw.WriteLine(TxtMemo.Text);
                        sw.Close();
                    }
                }
            }
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileProcessBeforClose();
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            this.Text = fileName + "- myNotePad";

            try
            {
                StreamReader r = File.OpenText(fileName);
                TxtMemo.Text = r.ReadToEnd();
                modifyFlag = false;
                r.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
