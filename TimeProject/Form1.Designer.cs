namespace TimeProject
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Date = new System.Windows.Forms.DateTimePicker();
			this.Datefrtime = new System.Windows.Forms.DateTimePicker();
			this.Dateendtime = new System.Windows.Forms.DateTimePicker();
			this.cboactivity = new System.Windows.Forms.ComboBox();
			this.buttonrigist = new System.Windows.Forms.Button();
			this.Date1 = new System.Windows.Forms.DateTimePicker();
			this.buttonretrieve = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.remark = new System.Windows.Forms.TextBox();
			this.buttonDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(13, 13);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 27;
			this.dataGridView1.Size = new System.Drawing.Size(1145, 332);
			this.dataGridView1.TabIndex = 0;
			// 
			// Date
			// 
			this.Date.Location = new System.Drawing.Point(13, 381);
			this.Date.Name = "Date";
			this.Date.Size = new System.Drawing.Size(200, 25);
			this.Date.TabIndex = 1;
			// 
			// Datefrtime
			// 
			this.Datefrtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.Datefrtime.Location = new System.Drawing.Point(219, 381);
			this.Datefrtime.Name = "Datefrtime";
			this.Datefrtime.ShowUpDown = true;
			this.Datefrtime.Size = new System.Drawing.Size(200, 25);
			this.Datefrtime.TabIndex = 2;
			// 
			// Dateendtime
			// 
			this.Dateendtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.Dateendtime.Location = new System.Drawing.Point(425, 381);
			this.Dateendtime.Name = "Dateendtime";
			this.Dateendtime.ShowUpDown = true;
			this.Dateendtime.Size = new System.Drawing.Size(200, 25);
			this.Dateendtime.TabIndex = 3;
			// 
			// cboactivity
			// 
			this.cboactivity.FormattingEnabled = true;
			this.cboactivity.Location = new System.Drawing.Point(631, 381);
			this.cboactivity.Name = "cboactivity";
			this.cboactivity.Size = new System.Drawing.Size(121, 23);
			this.cboactivity.TabIndex = 4;
			// 
			// buttonrigist
			// 
			this.buttonrigist.Location = new System.Drawing.Point(758, 381);
			this.buttonrigist.Name = "buttonrigist";
			this.buttonrigist.Size = new System.Drawing.Size(109, 80);
			this.buttonrigist.TabIndex = 6;
			this.buttonrigist.Text = "스케줄 등록";
			this.buttonrigist.UseVisualStyleBackColor = true;
			this.buttonrigist.Click += new System.EventHandler(this.button1_Click);
			// 
			// Date1
			// 
			this.Date1.Location = new System.Drawing.Point(13, 467);
			this.Date1.Name = "Date1";
			this.Date1.Size = new System.Drawing.Size(200, 25);
			this.Date1.TabIndex = 7;
			// 
			// buttonretrieve
			// 
			this.buttonretrieve.Location = new System.Drawing.Point(219, 467);
			this.buttonretrieve.Name = "buttonretrieve";
			this.buttonretrieve.Size = new System.Drawing.Size(109, 31);
			this.buttonretrieve.TabIndex = 8;
			this.buttonretrieve.Text = "조회";
			this.buttonretrieve.UseVisualStyleBackColor = true;
			this.buttonretrieve.Click += new System.EventHandler(this.buttonretrieve_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// remark
			// 
			this.remark.Location = new System.Drawing.Point(13, 413);
			this.remark.Multiline = true;
			this.remark.Name = "remark";
			this.remark.Size = new System.Drawing.Size(739, 48);
			this.remark.TabIndex = 5;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(874, 382);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(101, 79);
			this.buttonDelete.TabIndex = 9;
			this.buttonDelete.Text = "삭제";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1170, 504);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.remark);
			this.Controls.Add(this.buttonretrieve);
			this.Controls.Add(this.Date1);
			this.Controls.Add(this.buttonrigist);
			this.Controls.Add(this.cboactivity);
			this.Controls.Add(this.Dateendtime);
			this.Controls.Add(this.Datefrtime);
			this.Controls.Add(this.Date);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker Date;
        private System.Windows.Forms.DateTimePicker Datefrtime;
        private System.Windows.Forms.DateTimePicker Dateendtime;
        private System.Windows.Forms.ComboBox cboactivity;
        private System.Windows.Forms.Button buttonrigist;
        private System.Windows.Forms.DateTimePicker Date1;
        private System.Windows.Forms.Button buttonretrieve;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox remark;
        private System.Windows.Forms.Button buttonDelete;
    }
}

