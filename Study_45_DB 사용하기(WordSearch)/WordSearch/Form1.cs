using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        System.Data.OleDb.OleDbConnection _conn;

        public Form1()
        {
            InitializeComponent();
        }


        private void DBConnect(string strDBPath)
        {
            // DataAdapter는 자동으로 Connection을
            // 핸들링한다. conn.Open() 불필요.
            string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={strDBPath}";
            _conn = new OleDbConnection(connStr);

            string Query = $@"SELECT * 
                              FROM Voca";

            QueryExeCute(Query);
        }

        private void QueryExeCute(string Query)
        {
            // Fill 전달 전에 DataSet객체 생성
            DataSet ds = new DataSet();

            
            System.Data.OleDb.OleDbDataAdapter adp = new OleDbDataAdapter(Query, _conn);
            adp.Fill(ds);

            // 가져온 데이타를 DataGridView 컨트롤에 바인딩
            dgData.DataSource = ds.Tables[0];
        }


        /// <summary>
        /// 16 강에서 사용 한 OpenFileDialog 사용 ^^;;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDBLoad_Click(object sender, EventArgs e)
        {
            string strFilePath = string.Empty;

            OFDialog.InitialDirectory = Application.StartupPath;   //프로그램 실행 파일 위치
            OFDialog.FileName = "*.mdb";
            OFDialog.Filter = "db files (*.mdb)|*.mdb|All files (*.*)|*.*";

            if (OFDialog.ShowDialog() == DialogResult.OK)
            {
                tboxPath.Text = OFDialog.FileName;
                DBConnect(tboxPath.Text);
            }
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            QueryExeCute(tboxQuery.Text);
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            Button obtn = (Button)sender;

            String strQuery = null;

            switch (obtn.Name)
            {
                case "btnEx1":
                    strQuery = $@"SELECT * 
FROM Voca";
                    break;
                case "btnEx2":
                    strQuery = $@"SELECT*
FROM Voca
WHERE 단어 LIKE ""%a%""";
                    break;
                case "btnEx3":
                    strQuery = $@"SELECT*
FROM Voca
ORDER BY ID DESC";
                    break;
                default:
                    break;
            }

            tboxQuery.Text = strQuery;
        }

    }
}
