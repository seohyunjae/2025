using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BUSAN_API_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
             {
                // ✅ 설정
                string serviceKey = "COchHsnuB3QA6dTgGCwe6eWCIefie6K7gwYUpKeacI0p4KORU20CubNPbadp5ytOwGT8XMngG0TbdCEfWHLS%2Fg%3D%3D"; // URL-인코딩(Encoding)된 키 사용
                int pageSize = 30;     // 페이지당 요청 수 (API 한도 내에서 크게)
                int desiredTotal = 30;  // 지금은 10개만! (전체 조회하려면 int.MaxValue 로)

                var items = await FetchAttractionsAsync(serviceKey, pageSize, desiredTotal);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = items;

                dataGridView1.DataSource = items;

                // 🔤 컬럼 헤더 한글화
                var headers = new Dictionary<string, string>
                {
                    ["UC_SEQ"] = "콘텐츠ID",
                    ["MAIN_TITLE"] = "콘텐츠명",
                    ["GUGUN_NM"] = "구군",
                    ["ADDR1"] = "주소",
                    ["LAT"] = "위도",
                    ["LNG"] = "경도",
                    ["PLACE"] = "여행지",
                    ["TITLE"] = "제목",
                    ["SUBTITLE"] = "부제목",
                    ["CNTCT_TEL"] = "연락처",
                    ["HOMEPAGE_URL"] = "홈페이지",
                    ["TRFC_INFO"] = "교통정보",
                    ["USAGE_DAY"] = "운영일",
                    ["HLDY_INFO"] = "휴무일",
                    ["USAGE_DAY_WEEK_AND_TIME"] = "운영 및 시간",
                    ["USAGE_AMOUNT"] = "이용요금",
                    ["MIDDLE_SIZE_RM1"] = "편의시설",
                    ["MAIN_IMG_NORMAL"] = "이미지URL",
                    ["MAIN_IMG_THUMB"] = "썸네일URL",
                    ["ITEMCNTNTS"] = "상세내용"
                };

                foreach (var kv in headers)
                {
                    if (dataGridView1.Columns[kv.Key] != null)
                        dataGridView1.Columns[kv.Key].HeaderText = kv.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 로딩 실패: " + ex.Message);
            }
        }

        /// <summary>
        /// 페이징으로 전체(또는 desiredTotal 만큼) 조회
        /// </summary>
        private async Task<List<AttractionItem>> FetchAttractionsAsync(string serviceKey, int pageSize, int desiredTotal)
        {
            var result = new List<AttractionItem>();
            int page = 1;
            int totalCount = -1; // 첫 페이지에서 파악

            using (var http = new HttpClient())
            {
                while (true)
                {
                    string url =
                        "http://apis.data.go.kr/6260000/AttractionService/getAttractionKr" +
                        $"?ServiceKey={serviceKey}&pageNo={page}&numOfRows={pageSize}";

                    var resp = await http.GetAsync(url);
                    resp.EnsureSuccessStatusCode();
                    string xml = await resp.Content.ReadAsStringAsync();

                    var doc = new XmlDocument();
                    doc.LoadXml(xml);

                    // 첫 페이지에서 totalCount 확인
                    if (totalCount < 0)
                    {
                        var totalNode = doc.GetElementsByTagName("totalCount");
                        if (totalNode.Count > 0 && int.TryParse(totalNode[0].InnerText, out int t))
                            totalCount = t;
                        else
                            totalCount = 0;
                    }

                    // item 파싱
                    var pageItems = ParseItemsFromXml(doc);
                    result.AddRange(pageItems);

                    // 원하는 개수에 도달하면 자르기
                    if (result.Count >= desiredTotal)
                    {
                        if (result.Count > desiredTotal)
                            result.RemoveRange(desiredTotal, result.Count - desiredTotal);
                        break;
                    }

                    // 다음 페이지로 넘어갈지 결정
                    int fetchedSoFar = page * pageSize;
                    if (fetchedSoFar >= totalCount || pageItems.Count == 0)
                        break;

                    page++;

                    // (선택) 과한 호출 방지 딜레이
                    await Task.Delay(100);
                }
            }

            return result;
        }

        /// <summary>
        /// XML 문서에서 item 노드들을 AttractionItem 리스트로 변환
        /// </summary>
        private List<AttractionItem> ParseItemsFromXml(XmlDocument doc)
        {
            var list = new List<AttractionItem>();
            var items = doc.GetElementsByTagName("item");

            foreach (XmlNode node in items)
            {
                var it = new AttractionItem
                {
                    UC_SEQ = ParseInt(node["UC_SEQ"]?.InnerText),
                    MAIN_TITLE = node["MAIN_TITLE"]?.InnerText ?? "",
                    GUGUN_NM = node["GUGUN_NM"]?.InnerText ?? "",
                    ADDR1 = node["ADDR1"]?.InnerText ?? "",
                    LAT = node["LAT"]?.InnerText ?? "",
                    LNG = node["LNG"]?.InnerText ?? "",
                    CNTCT_TEL = node["CNTCT_TEL"]?.InnerText ?? "",
                    USAGE_AMOUNT = node["USAGE_AMOUNT"]?.InnerText ?? "",
                    SUBTITLE = node["SUBTITLE"]?.InnerText ?? "",
                    TITLE = node["TITLE"]?.InnerText ?? "",
                    PLACE = node["PLACE"]?.InnerText ?? "",
                    HOMEPAGE_URL = node["HOMEPAGE_URL"]?.InnerText ?? "",
                    TRFC_INFO = node["TRFC_INFO"]?.InnerText ?? "",
                    HLDY_INFO = node["HLDY_INFO"]?.InnerText ?? "",
                    USAGE_DAY = node["USAGE_DAY"]?.InnerText ?? "",
                    USAGE_DAY_WEEK_AND_TIME = node["USAGE_DAY_WEEK_AND_TIME"]?.InnerText ?? "",
                    MIDDLE_SIZE_RM1 = node["MIDDLE_SIZE_RM1"]?.InnerText ?? "",
                    MAIN_IMG_NORMAL = node["MAIN_IMG_NORMAL"]?.InnerText ?? "",
                    MAIN_IMG_THUMB = node["MAIN_IMG_THUMB"]?.InnerText ?? "",
                    ITEMCNTNTS = node["ITEMCNTNTS"]?.InnerText ?? ""
                };

                list.Add(it);
            }

            return list;
        }

        private int ParseInt(string s)
        {
            if (int.TryParse(s, out int v)) return v;
            return 0;
        }

    }

    // DTO
    public class AttractionItem
    {
        public int UC_SEQ { get; set; }
        public string MAIN_TITLE { get; set; }
        public string GUGUN_NM { get; set; }
        public string ADDR1 { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string CNTCT_TEL { get; set; }
        public string USAGE_AMOUNT { get; set; }
        public string SUBTITLE { get; set; }
        public string TITLE { get; set; }
        public string PLACE { get; set; }
        public string HOMEPAGE_URL { get; set; }
        public string TRFC_INFO { get; set; }
        public string HLDY_INFO { get; set; }
        public string USAGE_DAY { get; set; }
        public string USAGE_DAY_WEEK_AND_TIME { get; set; }
        public string MIDDLE_SIZE_RM1 { get; set; }
        public string MAIN_IMG_NORMAL { get; set; }
        public string MAIN_IMG_THUMB { get; set; }
        public string ITEMCNTNTS { get; set; }
    }


}
