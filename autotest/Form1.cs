using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace autotest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
        }

        private string filePath = "";


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                richTextBox1.Text = OFD.FileName;
                filePath = OFD.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = application.Workbooks.Open(Filename: @filePath);
                Worksheet worksheet1 = workbook.Worksheets.get_Item("Sheet1");
                application.Visible = false;
                Range range = worksheet1.UsedRange;
                String data = "";

                for (int i = 1; i <= range.Rows.Count; ++i)
                {
                    for (int j = 1; j <= range.Columns.Count; ++j)
                    {
                        data += ((range.Cells[i, j] as Range).Value2.ToString() + " ");
                    }
                    data += "\n";
                }


                richTextBox2.Text = data;

                DeleteObject(worksheet1);
                DeleteObject(workbook);
                application.Quit();
                DeleteObject(application);
            }
        }

        private void DeleteObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("메모리 할당을 해제하는 중 문제가 발생하였습니다." + ex.ToString(), "경고!");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
