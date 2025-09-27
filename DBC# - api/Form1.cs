using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBC_
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            this.button1.Click += button_Click;
            this.textBox1.KeyDown += textBox1_KeyDown;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadCrimeDataFromApiAsync();
        }

        private async void button_Click(object sender, EventArgs e)
        {
            await LoadCrimeDataFromApiAsync();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Click(button1, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private async Task LoadCrimeDataFromApiAsync()
        {
            string serviceKey = "COchHsnuB3QA6dTgGCwe6eWCIefie6K7gwYUpKeacI0p4KORU20CubNPbadp5ytOwGT8XMngG0TbdCEfWHLS%2Fg%3D%3D";
            string url = $"https://api.odcloud.kr/api/3074462/v1/uddi:efafd73f-3310-48f8-9f56-bddc1c51f3ba_201910221541"
                + $"?page=1&perPage=100&serviceKey={serviceKey}";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                string raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"요청 실패: {response.StatusCode}\n내용: {raw}");
                    return;
                }

                // 디버깅용: JSON 구조 확인
                MessageBox.Show(raw);

                var root = JsonConvert.DeserializeObject<CrimeApiResponse>(raw);

                if (root != null && root.data != null && root.data.Count > 0)
                {
                    dataGridView1.DataSource = root.data;
                }
                else
                {
                    MessageBox.Show("데이터가 없거나 구조가 맞지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API 호출 또는 파싱 중 오류: " + ex.Message);
            }
        }
    }

    public class CrimeApiResponse
    {
        [JsonProperty("currentCount")]
        public int CurrentCount { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("matchCount")]
        public int MatchCount { get; set; }

        [JsonProperty("data")]
        public List<CrimeRecord> data { get; set; }
    }

    public class CrimeRecord
    {
        [JsonProperty("경찰청")]
        public string 경찰청 { get; set; }

        [JsonProperty("거주지")]
        public string 거주지 { get; set; }

        [JsonProperty("강간")]
        public string 강간 { get; set; }

        [JsonProperty("강제추행")]
        public string 강제추행 { get; set; }

        [JsonProperty("살인")]
        public string 살인 { get; set; }

        [JsonProperty("절도")]
        public string 절도 { get; set; }

        [JsonProperty("폭력")]
        public string 폭력 { get; set; }

        [JsonProperty("특별법범")]
        public string 특별법범 { get; set; }

        [JsonProperty("기타형법범")]
        public string 기타형법범 { get; set; }

        // 필요하면 다른 필드도 계속 string으로 선언
    }



}
