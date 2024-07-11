using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubDbDownload
{
    public partial class Form1 : Form
    {
        string NodeDbName;
        public List<string> ExtractStringsBetween(string input,string startString,string endString)
        {
            List<string> result = new List<string>();
            int startIndex = 0;
            while(startIndex < input.Length)
            {
                int start = input.IndexOf(startString,startIndex);
                if (start == -1) break;

                start += startString.Length;

                int end = input.IndexOf(endString,start);
                if (end == -1) break;

                string substring = input.Substring(start,end - start);

                result.Add(substring);

                startIndex = end + endString.Length;
            }

            return result;
        }
        public void GetNode()
        {
           string nodeInfo = MyWebRequest.get("https://snapshot.ssc.farm");

            List<string> jars = ExtractStringsBetween(nodeInfo, "tar\">", "<");

            if(jars.Count > 3)
            {
                List<string> FileInfos = ExtractStringsBetween(nodeInfo, "</a> ", "<");

                if(FileInfos.Count > 0)
                {
                    for (int i = jars.Count - 1; i >= 0; i--)
                    {
                        this.listView1.Items.Add(new ListViewItem(new string[] { i.ToString(), jars[i].Trim(), FileInfos[i].Trim() }));
                    }
                    this.listView1.EndUpdate();
                }

            }
        }


        public Form1()
        {
         
            InitializeComponent();
            this.listView1.Columns[0].Width = 40;
            this.listView1.Columns[1].Width = 450;
            this.listView1.Columns[2].Width = 300;

            GetNode();

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        NodeDbName = lv.SubItems[1].Text.Trim();
   
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(NodeDbName))
            {
                System.Diagnostics.Process.Start("https://snapshot.ssc.farm/" + NodeDbName);
            }
          
        }
    }
}
