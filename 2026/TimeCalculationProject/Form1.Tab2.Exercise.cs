using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimeCalculationProject
{
	public partial class Form1 : Form
	{
		// ✅ 탭2 전용 필드
		private DateTime? exerciseStartTime;

		private void LoadExerciseGrid()
		{
			const string sql = @"
                SELECT
                    ExerciseDate,
                    ExerciseMinutes,
                    InsertTime,
                    UpdateTime
                FROM dbo.Exercise
                ORDER BY ExerciseDate DESC;
            ";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				DataTable table = new DataTable();
				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					table.Load(reader);
				}

				dgw2exercise.DataSource = table;
				dgw2exercise.AutoResizeColumns();
			}
		}

		private void btn2insertexercise_Click(object sender, EventArgs e)
		{
			DateTime exerciseDate = Date2exercise.Value.Date;

			string minutesText = time2exercise.Text.Trim();
			if (!int.TryParse(minutesText, out int exerciseMinutes) || exerciseMinutes < 0 || exerciseMinutes > 1440)
			{
				MessageBox.Show("운동 시간(분)을 0~1440 사이 숫자로 입력해 주세요.");
				time2exercise.Focus();
				return;
			}

			DateTime insertTime = DateTime.Now;

			const string sql = @"
                DECLARE @NextSeq NUMERIC(18,0);

                SELECT @NextSeq = ISNULL(MAX(seq), 0) + 1
                FROM dbo.Exercise WITH (UPDLOCK, HOLDLOCK)
                WHERE ExerciseDate = @ExerciseDate;

                INSERT INTO dbo.Exercise (ExerciseDate, seq, ExerciseMinutes, InsertTime)
                VALUES (@ExerciseDate, @NextSeq, @ExerciseMinutes, @InsertTime);
            ";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@ExerciseDate", SqlDbType.Date).Value = exerciseDate;
				command.Parameters.Add("@ExerciseMinutes", SqlDbType.Int).Value = exerciseMinutes;
				command.Parameters.Add("@InsertTime", SqlDbType.DateTime2).Value = insertTime;

				connection.Open();
				command.ExecuteNonQuery();
			}

			LoadExerciseGrid();
			time2exercise.Focus();
			MessageBox.Show("운동 기록이 저장(또는 수정)되었습니다.");
		}

		private void Btn2Start_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;

			if (exerciseStartTime.HasValue)
			{
				DialogResult result = MessageBox.Show(
					"이미 시작 시간이 있습니다. 다시 시작하시겠습니까?",
					"확인",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (result != DialogResult.Yes)
					return;
			}

			exerciseStartTime = now;
			txttime2.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		private void Btn2Finish_Click(object sender, EventArgs e)
		{
			if (!exerciseStartTime.HasValue)
			{
				MessageBox.Show("먼저 시작(btn2start)을 눌러주세요.");
				return;
			}

			DateTime startTime = exerciseStartTime.Value;
			DateTime finishTime = DateTime.Now;

			TimeSpan diff = finishTime - startTime;
			if (diff < TimeSpan.Zero)
			{
				MessageBox.Show("시간 계산이 이상합니다.");
				return;
			}

			int exerciseMinutes = (int)Math.Round(diff.TotalMinutes);
			if (exerciseMinutes < 0) exerciseMinutes = 0;
			if (exerciseMinutes > 1440) exerciseMinutes = 1440;

			DateTime exerciseDate = startTime.Date;

			InsertExercise(exerciseDate, exerciseMinutes);

			time2exercise.Text = exerciseMinutes.ToString();
			txttime2.Text = $"시작: {startTime:HH:mm:ss} / 종료: {finishTime:HH:mm:ss} / {exerciseMinutes}분";

			LoadExerciseGrid();
			exerciseStartTime = null;

			MessageBox.Show("운동 기록이 저장(또는 수정)되었습니다.");
		}

		private void InsertExercise(DateTime exerciseDate, int exerciseMinutes)
		{
			DateTime insertTime = DateTime.Now;

			const string sql = @"
                INSERT INTO dbo.Exercise (ExerciseDate, ExerciseMinutes, InsertTime)
                VALUES (@ExerciseDate, @ExerciseMinutes, @InsertTime);
            ";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@ExerciseDate", SqlDbType.Date).Value = exerciseDate;
				command.Parameters.Add("@ExerciseMinutes", SqlDbType.Int).Value = exerciseMinutes;
				command.Parameters.Add("@InsertTime", SqlDbType.DateTime2).Value = insertTime;

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		private void btn2delete_Click(object sender, EventArgs e)
		{
			DataGridViewRow row = dgw2exercise.SelectedRows.Count > 0
				? dgw2exercise.SelectedRows[0]
				: dgw2exercise.CurrentRow;

			if (row == null)
			{
				MessageBox.Show("삭제할 행을 먼저 선택해 주세요.");
				return;
			}

			// ⚠️ 여기 로직은 원본 그대로 옮겼지만, 현재는 ExerciseId가 아니라 ExerciseDate를 읽고 있음.
			object idValue = row.Cells["ExerciseDate"]?.Value;
			if (idValue == null || idValue == DBNull.Value)
			{
				MessageBox.Show("선택한 행에서 ExerciseId를 찾을 수 없습니다.");
				return;
			}

			int exerciseId = Convert.ToInt32(idValue);

			DialogResult result = MessageBox.Show(
				"선택한 운동 기록을 삭제하시겠습니까?",
				"삭제 확인",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			const string sql = "DELETE FROM dbo.Exercise WHERE ExerciseId = @ExerciseId;";

			using (SqlConnection connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add("@ExerciseId", SqlDbType.Int).Value = exerciseId;

				connection.Open();
				int rows = command.ExecuteNonQuery();

				if (rows <= 0)
				{
					MessageBox.Show("삭제할 데이터가 없습니다.");
					return;
				}
			}

			LoadExerciseGrid();
			MessageBox.Show("삭제되었습니다.");
		}
	}
}
