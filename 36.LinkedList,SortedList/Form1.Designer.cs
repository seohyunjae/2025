﻿namespace _36.LinkedList_SortedList
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
            this.mcScheduler = new System.Windows.Forms.MonthCalendar();
            this.tboxScheduler = new System.Windows.Forms.TextBox();
            this.btnScheduler = new System.Windows.Forms.Button();
            this.lboxScheduler = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mcScheduler
            // 
            this.mcScheduler.Location = new System.Drawing.Point(18, 18);
            this.mcScheduler.Name = "mcScheduler";
            this.mcScheduler.TabIndex = 0;
            this.mcScheduler.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcScheduler_DateChanged);
            // 
            // tboxScheduler
            // 
            this.tboxScheduler.Location = new System.Drawing.Point(18, 192);
            this.tboxScheduler.Multiline = true;
            this.tboxScheduler.Name = "tboxScheduler";
            this.tboxScheduler.Size = new System.Drawing.Size(220, 133);
            this.tboxScheduler.TabIndex = 1;
            // 
            // btnScheduler
            // 
            this.btnScheduler.Location = new System.Drawing.Point(18, 332);
            this.btnScheduler.Name = "btnScheduler";
            this.btnScheduler.Size = new System.Drawing.Size(220, 32);
            this.btnScheduler.TabIndex = 2;
            this.btnScheduler.Text = "일정 등록 하기";
            this.btnScheduler.UseVisualStyleBackColor = true;
            this.btnScheduler.Click += new System.EventHandler(this.btnScheduler_Click);
            // 
            // lboxScheduler
            // 
            this.lboxScheduler.FormattingEnabled = true;
            this.lboxScheduler.ItemHeight = 12;
            this.lboxScheduler.Location = new System.Drawing.Point(250, 18);
            this.lboxScheduler.Name = "lboxScheduler";
            this.lboxScheduler.Size = new System.Drawing.Size(261, 352);
            this.lboxScheduler.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 397);
            this.Controls.Add(this.lboxScheduler);
            this.Controls.Add(this.btnScheduler);
            this.Controls.Add(this.tboxScheduler);
            this.Controls.Add(this.mcScheduler);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mcScheduler;
        private System.Windows.Forms.TextBox tboxScheduler;
        private System.Windows.Forms.Button btnScheduler;
        private System.Windows.Forms.ListBox lboxScheduler;
    }
}

