using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
            label2.Text = "0";

            using (var oStreamReader = new StreamReader(@"D:\Work\sample_data.csv"))
            {
                using (var oEntities = new SampleEntities())
                {
                    var iCnt = 0;
                    var iCnt2 = 0;

                    var sLine = string.Empty;
                    var oList = new List<TBL_BigData>();
                    while ((sLine = oStreamReader.ReadLine()) != null)
                    {
                        // CSV file has 15 million rows and 2 columns with \t splitter.
                        var oData = sLine.Split('\t');
                        
                        oList.Add(new TBL_BigData() { Data1 = oData[0], Data2 = oData[1] });
                        label1.Text = iCnt.ToString();

                        iCnt++;

                        // Insert every half million temporary to the database.
                        if (0 < iCnt && iCnt % 500000 == 0)
                        {
                            // Use the EFBulkInsert package here.
                            EFBulkInsert.BulkInsertExtension.BulkInsert<TBL_BigData>(oEntities, oList, 500000);
                            oList = new List<TBL_BigData>();

                            iCnt2 += 500000;
                            label2.Text = iCnt2.ToString();
                        }

                        Application.DoEvents();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(var oEntities = new SampleEntities())
            {
                oEntities.Database.ExecuteSqlCommand(@"DROP TABLE dbo.TBL_BigData");
                oEntities.Database.ExecuteSqlCommand(@"
CREATE TABLE [dbo].[TBL_BigData](
	[Data1] [nvarchar](100) NOT NULL,
	[Data2] [nvarchar](100) NOT NULL
)
");
            }
        }
    }
}
