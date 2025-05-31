using System;
using System.Data;
using System.Windows.Forms;

namespace DataTableTest
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet(); // 학급들에 대한 정보를 가지고 있을 DataSet
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            bool bCheckisTable = false;

            if (ds.Tables.Contains(cboxRegClass.Text))
            {
                bCheckisTable = true;
            }
 
            DataTable dt = null;

            if (!bCheckisTable)
            {
                dt = new DataTable(cboxRegClass.Text);

                DataColumn colName = new DataColumn("NAME", typeof(string));
                DataColumn colSex = new DataColumn("SEX", typeof(string));
                DataColumn colRef = new DataColumn("REF", typeof(string));

                dt.Columns.Add(colName);
                dt.Columns.Add(colSex);
                dt.Columns.Add(colRef);
            }
            else
            {
                dt = ds.Tables[cboxRegClass.Text];      
            }

            //Row 자료를 등록
            DataRow row = dt.NewRow();

            row["NAME"] = tboxRegName.Text;

            if (rdoRegSexMale.Checked)
            {
                row["SEX"] = "남자";
            }
            else if (rdoRegSexFemale.Checked)
            {
                row["SEX"] = "여자";

            }

            row["REF"] = tboxRegRef.Text;

            //생성된 Row를 Table에 등록
            dt.Rows.Add(row);

            if (bCheckisTable)
            {
                ds.Tables.Remove(cboxRegClass.Text);
                ds.Tables.Add(dt);
            }

            ds.Tables.Add(dt);

            cboxViewClass_SelectedIndexChanged(this, null);
        }

        private void cboxViewClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgViewInfo.DataSource = ds.Tables[cboxViewClass.Text];

            foreach (DataGridViewRow oRow in dgViewInfo.Rows) 
            { 
                oRow.HeaderCell.Value = oRow.Index.ToString(); 
            }
            dgViewInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }


        private void btnViewDataDel_Click(object sender, EventArgs e)
        {
            int iSelectRow = dgViewInfo.SelectedRows[0].Index;

            ds.Tables[cboxRegClass.Text].Rows.RemoveAt(iSelectRow);

            cboxViewClass_SelectedIndexChanged(this, null);
        }
    }
}
