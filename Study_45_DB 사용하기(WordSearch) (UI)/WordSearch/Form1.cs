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

        }

        #region DB Connection
        private void DBConnect(string strDBPath)
        {
            string connStr = $@"Provider=mircrosoft.ACE.OLEDB.12.0;Data Source = {strDBPath}";

            _conn = new OleDbConnection(connStr);

            string Query = $@"Select + 
                              From Voca";

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
