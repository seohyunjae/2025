namespace _24_DelegatePizzaOrder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoDow2 = new System.Windows.Forms.RadioButton();
            this.rdoDow1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoEdge2 = new System.Windows.Forms.RadioButton();
            this.rdoEdge1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numEa = new System.Windows.Forms.NumericUpDown();
            this.cboxTopping3 = new System.Windows.Forms.CheckBox();
            this.cboxTopping2 = new System.Windows.Forms.CheckBox();
            this.cboxTopping1 = new System.Windows.Forms.CheckBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.lboxOrder = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEa)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDow2);
            this.groupBox1.Controls.Add(this.rdoDow1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "도우";
            // 
            // rdoDow2
            // 
            this.rdoDow2.AutoSize = true;
            this.rdoDow2.Location = new System.Drawing.Point(139, 20);
            this.rdoDow2.Name = "rdoDow2";
            this.rdoDow2.Size = new System.Drawing.Size(91, 16);
            this.rdoDow2.TabIndex = 1;
            this.rdoDow2.TabStop = true;
            this.rdoDow2.Text = "씬 (11000원)";
            this.rdoDow2.UseVisualStyleBackColor = true;
            // 
            // rdoDow1
            // 
            this.rdoDow1.AutoSize = true;
            this.rdoDow1.Location = new System.Drawing.Point(6, 20);
            this.rdoDow1.Name = "rdoDow1";
            this.rdoDow1.Size = new System.Drawing.Size(127, 16);
            this.rdoDow1.TabIndex = 0;
            this.rdoDow1.TabStop = true;
            this.rdoDow1.Text = "오리지널 (10000원)";
            this.rdoDow1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoEdge2);
            this.groupBox2.Controls.Add(this.rdoEdge1);
            this.groupBox2.Location = new System.Drawing.Point(12, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "엣지";
            // 
            // rdoEdge2
            // 
            this.rdoEdge2.AutoSize = true;
            this.rdoEdge2.Location = new System.Drawing.Point(133, 20);
            this.rdoEdge2.Name = "rdoEdge2";
            this.rdoEdge2.Size = new System.Drawing.Size(145, 16);
            this.rdoEdge2.TabIndex = 1;
            this.rdoEdge2.TabStop = true;
            this.rdoEdge2.Text = "치즈크러스터 (2000원)";
            this.rdoEdge2.UseVisualStyleBackColor = true;
            // 
            // rdoEdge1
            // 
            this.rdoEdge1.AutoSize = true;
            this.rdoEdge1.Location = new System.Drawing.Point(6, 20);
            this.rdoEdge1.Name = "rdoEdge1";
            this.rdoEdge1.Size = new System.Drawing.Size(121, 16);
            this.rdoEdge1.TabIndex = 0;
            this.rdoEdge1.TabStop = true;
            this.rdoEdge1.Text = "리치골드 (1000원)";
            this.rdoEdge1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numEa);
            this.groupBox3.Controls.Add(this.cboxTopping3);
            this.groupBox3.Controls.Add(this.cboxTopping2);
            this.groupBox3.Controls.Add(this.cboxTopping1);
            this.groupBox3.Location = new System.Drawing.Point(12, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 96);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "토핑";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "(EA)";
            // 
            // numEa
            // 
            this.numEa.Location = new System.Drawing.Point(202, 59);
            this.numEa.Name = "numEa";
            this.numEa.Size = new System.Drawing.Size(39, 21);
            this.numEa.TabIndex = 3;
            // 
            // cboxTopping3
            // 
            this.cboxTopping3.AutoSize = true;
            this.cboxTopping3.Location = new System.Drawing.Point(6, 64);
            this.cboxTopping3.Name = "cboxTopping3";
            this.cboxTopping3.Size = new System.Drawing.Size(116, 16);
            this.cboxTopping3.TabIndex = 2;
            this.cboxTopping3.Text = "치즈 (1ea 300원)";
            this.cboxTopping3.UseVisualStyleBackColor = true;
            // 
            // cboxTopping2
            // 
            this.cboxTopping2.AutoSize = true;
            this.cboxTopping2.Location = new System.Drawing.Point(6, 42);
            this.cboxTopping2.Name = "cboxTopping2";
            this.cboxTopping2.Size = new System.Drawing.Size(116, 16);
            this.cboxTopping2.TabIndex = 1;
            this.cboxTopping2.Text = "감자 (1ea 200원)";
            this.cboxTopping2.UseVisualStyleBackColor = true;
            // 
            // cboxTopping1
            // 
            this.cboxTopping1.AutoSize = true;
            this.cboxTopping1.Location = new System.Drawing.Point(6, 20);
            this.cboxTopping1.Name = "cboxTopping1";
            this.cboxTopping1.Size = new System.Drawing.Size(128, 16);
            this.cboxTopping1.TabIndex = 0;
            this.cboxTopping1.Text = "소세지 (1ea 500원)";
            this.cboxTopping1.UseVisualStyleBackColor = true;
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(226, 224);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(75, 23);
            this.btnOrder.TabIndex = 4;
            this.btnOrder.Text = "주문하기";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // lboxOrder
            // 
            this.lboxOrder.FormattingEnabled = true;
            this.lboxOrder.ItemHeight = 12;
            this.lboxOrder.Location = new System.Drawing.Point(12, 253);
            this.lboxOrder.Name = "lboxOrder";
            this.lboxOrder.Size = new System.Drawing.Size(367, 160);
            this.lboxOrder.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 421);
            this.Controls.Add(this.lboxOrder);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoDow2;
        private System.Windows.Forms.RadioButton rdoDow1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoEdge2;
        private System.Windows.Forms.RadioButton rdoEdge1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numEa;
        private System.Windows.Forms.CheckBox cboxTopping3;
        private System.Windows.Forms.CheckBox cboxTopping2;
        private System.Windows.Forms.CheckBox cboxTopping1;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.ListBox lboxOrder;
    }
}

