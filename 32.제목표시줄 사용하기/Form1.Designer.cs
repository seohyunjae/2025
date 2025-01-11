namespace _32.제목표시줄_사용하기
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gboxControl = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSizeCheck = new System.Windows.Forms.Button();
            this.lboxResult = new System.Windows.Forms.ListBox();
            this.gboxControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxControl
            // 
            this.gboxControl.Controls.Add(this.label3);
            this.gboxControl.Controls.Add(this.label2);
            this.gboxControl.Controls.Add(this.label1);
            this.gboxControl.Controls.Add(this.button3);
            this.gboxControl.Controls.Add(this.button2);
            this.gboxControl.Controls.Add(this.button1);
            this.gboxControl.Location = new System.Drawing.Point(29, 30);
            this.gboxControl.Name = "gboxControl";
            this.gboxControl.Size = new System.Drawing.Size(614, 212);
            this.gboxControl.TabIndex = 0;
            this.gboxControl.TabStop = false;
            this.gboxControl.Text = "Controlbox";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.IndianRed;
            this.label3.Location = new System.Drawing.Point(398, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.IndianRed;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(236, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 83);
            this.label2.TabIndex = 1;
            this.label2.Text = "label1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(72, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(273, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 78);
            this.button3.TabIndex = 0;
            this.button3.Text = "button1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(175, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 44);
            this.button2.TabIndex = 0;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 74);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSizeCheck
            // 
            this.btnSizeCheck.Location = new System.Drawing.Point(536, 258);
            this.btnSizeCheck.Name = "btnSizeCheck";
            this.btnSizeCheck.Size = new System.Drawing.Size(107, 45);
            this.btnSizeCheck.TabIndex = 1;
            this.btnSizeCheck.Text = "Button size 체크";
            this.btnSizeCheck.UseVisualStyleBackColor = true;
            this.btnSizeCheck.Click += new System.EventHandler(this.btnSizeCheck_Click);
            // 
            // lboxResult
            // 
            this.lboxResult.FormattingEnabled = true;
            this.lboxResult.ItemHeight = 12;
            this.lboxResult.Location = new System.Drawing.Point(29, 321);
            this.lboxResult.Name = "lboxResult";
            this.lboxResult.Size = new System.Drawing.Size(614, 148);
            this.lboxResult.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 491);
            this.Controls.Add(this.lboxResult);
            this.Controls.Add(this.btnSizeCheck);
            this.Controls.Add(this.gboxControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Title";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gboxControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxControl;
        private System.Windows.Forms.Button btnSizeCheck;
        private System.Windows.Forms.ListBox lboxResult;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

