using System;
using System.Data;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stock
{
	public partial class Form1 : Form
	{
		private ClientWebSocket webSocket;
		private CancellationTokenSource cts;

		// dataGridView1에 바인딩할 테이블 (시간 + JSON 문자열)
		private DataTable messageTable = new DataTable();

		// 실제 발급받은 Access Token으로 바꿔야 함
		private const string AccessToken = "여기에_본인_ACCESS_TOKEN_문자열";

		// 모의투자 WebSocket 주소 (실전은 api.kiwoom.com으로 변경)
		private const string SocketUrl = "wss://mockapi.kiwoom.com:10000/api/dostk/websocket";
		// 실전일 때:
		// private const string SocketUrl = "wss://api.kiwoom.com:10000/api/dostk/websocket";

		public Form1()
		{
			InitializeComponent();

			// 폼 닫힐 때 WebSocket 정리
			this.FormClosing += Form1_FormClosing;

			// dataGridView1에 표시할 컬럼 설정
			messageTable.Columns.Add("Time", typeof(string));
			messageTable.Columns.Add("Json", typeof(string));
			dataGridView1.DataSource = messageTable;

			// 보기 조금 편하게
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			// 이미 연결되어 있으면 중복 연결 방지
			if (webSocket != null && webSocket.State == WebSocketState.Open)
			{
				MessageBox.Show("이미 WebSocket에 연결되어 있습니다.");
				return;
			}

			await ConnectAndStartAsync();
		}

		/// <summary>
		/// WebSocket 연결 + LOGIN + REG까지 처리
		/// </summary>
		private async Task ConnectAndStartAsync()
		{
			try
			{
				cts = new CancellationTokenSource();
				webSocket = new ClientWebSocket();

				var uri = new Uri(SocketUrl);
				await webSocket.ConnectAsync(uri, cts.Token);

				// 1) LOGIN 패킷 전송
				var loginObj = new
				{
					trnm = "LOGIN",
					token = AccessToken
				};

				string loginJson = JsonConvert.SerializeObject(loginObj);
				await SendAsync(loginJson);

				AddMessageToGrid("LOGIN 전송", loginJson);

				// 2) 수신 루프를 백그라운드에서 돌리기
				_ = Task.Run(ReceiveLoopAsync);

				// 3) REG 패킷 전송 (문서 예시대로 주문체결 00 등록)
				// 00(주문체결)은 item이 빈 문자열이면 계좌 기준으로 들어옴
				var regObj = new
				{
					trnm = "REG",
					grp_no = "1",
					refresh = "1",
					data = new[]
					{
						new {
							item = new [] { "" },   // 종목코드 없음(계좌 전체)
                            type = new [] { "00" }  // TR명: 00 = 주문체결
                        }
					}
				};

				string regJson = JsonConvert.SerializeObject(regObj);
				await SendAsync(regJson);

				AddMessageToGrid("REG 전송", regJson);

				MessageBox.Show("WebSocket 연결 및 REG 요청 완료");
			}
			catch (Exception ex)
			{
				MessageBox.Show("연결 중 오류: " + ex.Message);
			}
		}

		/// <summary>
		/// WebSocket으로 JSON 문자열 전송
		/// </summary>
		private async Task SendAsync(string json)
		{
			if (webSocket == null || webSocket.State != WebSocketState.Open)
				return;

			byte[] buffer = Encoding.UTF8.GetBytes(json);
			var segment = new ArraySegment<byte>(buffer);

			await webSocket.SendAsync(
				segment,
				WebSocketMessageType.Text,
				true,              // 이 메시지가 하나의 완성된 메시지
				cts.Token);
		}

		/// <summary>
		/// 서버에서 오는 메시지를 계속 받는 루프
		/// </summary>
		private async Task ReceiveLoopAsync()
		{
			var buffer = new byte[8192];

			try
			{
				while (webSocket != null && webSocket.State == WebSocketState.Open)
				{
					var segment = new ArraySegment<byte>(buffer);
					WebSocketReceiveResult result =
						await webSocket.ReceiveAsync(segment, cts.Token);

					if (result.MessageType == WebSocketMessageType.Close)
					{
						await webSocket.CloseAsync(
							WebSocketCloseStatus.NormalClosure,
							"Closed by client",
							CancellationToken.None);
						break;
					}

					// 수신된 실제 길이만큼 문자열로 변환
					string json = Encoding.UTF8.GetString(buffer, 0, result.Count);

					// PING 처리 (문서 예시처럼 PING이면 그대로 다시 보내야 함)
					try
					{
						var obj = JObject.Parse(json);
						string trnm = (string)obj["trnm"];

						if (trnm == "PING")
						{
							// 서버가 보낸 PING을 그대로 다시 보내야 연결 유지
							await SendAsync(json);
							continue; // 그리드에는 안 찍음 (원하면 찍어도 됨)
						}
					}
					catch
					{
						// JSON이 아닐 수도 있으니 파싱 오류는 무시
					}

					// 나머지 메시지들은 그리드에 출력
					AddMessageToGrid("RECV", json);
				}
			}
			catch (Exception ex)
			{
				// UI 스레드에서 메시지 띄우기
				if (!IsDisposed)
				{
					BeginInvoke(new Action(() =>
					{
						MessageBox.Show("수신 중 오류: " + ex.Message);
					}));
				}
			}
		}

		/// <summary>
		/// dataGridView1에 한 줄 추가 (시간 + JSON)
		/// </summary>
		private void AddMessageToGrid(string prefix, string json)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => AddMessageToGrid(prefix, json)));
				return;
			}

			string time = DateTime.Now.ToString("HH:mm:ss");
			string rowText = $"[{prefix}] {json}";
			messageTable.Rows.Add(time, rowText);
		}

		/// <summary>
		/// 폼 닫힐 때 WebSocket 정리
		/// </summary>
		private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (webSocket != null && webSocket.State == WebSocketState.Open)
				{
					cts?.Cancel();
					await webSocket.CloseAsync(
						WebSocketCloseStatus.NormalClosure,
						"Form closing",
						CancellationToken.None);
				}
			}
			catch
			{
				// 무시
			}
		}
	}
}
