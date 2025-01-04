namespace _24_DelegatePizzaOrder
{
    partial class frmPizza
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lboxMake = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lboxMake
            // 
            this.lboxMake.FormattingEnabled = true;
            this.lboxMake.ItemHeight = 12;
            this.lboxMake.Location = new System.Drawing.Point(39, 12);
            this.lboxMake.Name = "lboxMake";
            this.lboxMake.Size = new System.Drawing.Size(553, 256);
            this.lboxMake.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(471, 318);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 47);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "창 닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPizza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 398);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lboxMake);
            this.Name = "frmPizza";
            this.Text = "frmPizza";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lboxMake;
        private System.Windows.Forms.Button btnClose;
    }
}