using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace DataBaseproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(button1_Click);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string apiKey = "여기에_발급받은_API_KEY";
            string ocid = "조회할 캐릭터 ocid";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-nxopen-api-key", apiKey);

                string url = $"https://open.api.nexon.com/maplestorym/v1/character/basic?ocid={ocid}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var character = JsonConvert.DeserializeObject<CharacterBasic>(json);

                    // DataGridView에 바인딩할 리스트 생성
                    var list = new List<CharacterBasic> { character };
                    dataGridView1.DataSource = list;
                }
                else
                {
                    MessageBox.Show("API 호출 실패: " + response.StatusCode);
                }
            }
        }

        public class CharacterBasic
        {
            public string character_name { get; set; }
            public int level { get; set; }
            public string job { get; set; }
            public string world_name { get; set; }
        }
    }
}
