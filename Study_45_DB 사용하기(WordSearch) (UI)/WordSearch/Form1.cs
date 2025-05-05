using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        OleDbConnection _conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDBLoad_Click(object sender, EventArgs e)
        {
            string strFilePath = string.Empty;

            OFDialog.InitialDirectory = Application.StartupPath;
            OFDialog.FileName = "*.mdb";
            OFDialog.Filter = "db files (*.mdb)|*.mdb|All files (*.*)|*.*";

            if (OFDialog.ShowDialog() == DialogResult.OK)
            {
                tboxPath.Text = OFDialog.FileName;
                DBConnect(OFDialog.FileName);
            }
        }

        #region DB Connection
        private void DBConnect(string strDBPath)
        {
            string connStr = $@"Provider=mircrosoft.ACE.OLEDB.12.0;Data Source = {strDBPath}";

            _conn = new OleDbConnection(connStr);

            string Query = $@"Select + 
                              From Voca";

            QueryExeCute(Query);
        }

        private void QueryExeCute(string Query)
        {
            DataSet ds = new DataSet();

            OleDbDataAdapter adp = new OleDbDataAdapter(Query, _conn);
            adp.Fill(ds);
            dgData.DataSource = ds.Tables[0];
        }

        #endregion
    }
}
