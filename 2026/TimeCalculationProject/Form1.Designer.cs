namespace TimeCalculationProject
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
			this.lbl1birth = new System.Windows.Forms.Label();
			this.lbl1live40age = new System.Windows.Forms.Label();
			this.lbl1live30age = new System.Windows.Forms.Label();
			this.lbl1live60age = new System.Windows.Forms.Label();
			this.lbl1live65age = new System.Windows.Forms.Label();
			this.lbl1time = new System.Windows.Forms.Label();
			this.txtbox1live30 = new System.Windows.Forms.TextBox();
			this.txtbox1live40 = new System.Windows.Forms.TextBox();
			this.txtbox1live60 = new System.Windows.Forms.TextBox();
			this.txtbox1live65 = new System.Windows.Forms.TextBox();
			this.Panellive = new System.Windows.Forms.TableLayoutPanel();
			this.Panelawake = new System.Windows.Forms.TableLayoutPanel();
			this.lbl1awake30 = new System.Windows.Forms.Label();
			this.lbl1awake40 = new System.Windows.Forms.Label();
			this.lbl1awake60 = new System.Windows.Forms.Label();
			this.lbl1awake65 = new System.Windows.Forms.Label();
			this.txtbox1awake30 = new System.Windows.Forms.TextBox();
			this.txtbox1awake40 = new System.Windows.Forms.TextBox();
			this.txtbox1awake60 = new System.Windows.Forms.TextBox();
			this.txtbox1awake65 = new System.Windows.Forms.TextBox();
			this.Panelsleep = new System.Windows.Forms.TableLayoutPanel();
			this.lbl1sleep30 = new System.Windows.Forms.Label();
			this.lbl1sleep40 = new System.Windows.Forms.Label();
			this.lbl1sleep60 = new System.Windows.Forms.Label();
			this.lbl1sleep65 = new System.Windows.Forms.Label();
			this.txtbox1sleep30 = new System.Windows.Forms.TextBox();
			this.txtbox1sleep40 = new System.Windows.Forms.TextBox();
			this.txtbox1sleep60 = new System.Windows.Forms.TextBox();
			this.txtbox1sleep65 = new System.Windows.Forms.TextBox();
			this.lbl1awake = new System.Windows.Forms.Label();
			this.lbl1sleep = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btn1update = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btn2delete = new System.Windows.Forms.Button();
			this.txttime2 = new System.Windows.Forms.TextBox();
			this.btn2finish = new System.Windows.Forms.Button();
			this.btn2start = new System.Windows.Forms.Button();
			this.btn2insertexercise = new System.Windows.Forms.Button();
			this.time2exercise = new System.Windows.Forms.NumericUpDown();
			this.Date2exercise = new System.Windows.Forms.DateTimePicker();
			this.dgw2exercise = new System.Windows.Forms.DataGridView();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btn3deletemergency = new System.Windows.Forms.Button();
			this.btn3insertemergency = new System.Windows.Forms.Button();
			this.txt3emergency = new System.Windows.Forms.TextBox();
			this.lbl3emergency = new System.Windows.Forms.Label();
			this.dgw3emergency = new System.Windows.Forms.DataGridView();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.btn4deletecontinue = new System.Windows.Forms.Button();
			this.btn4insertcontinue = new System.Windows.Forms.Button();
			this.txt4continune = new System.Windows.Forms.TextBox();
			this.dgw4continue = new System.Windows.Forms.DataGridView();
			this.lbl4continue = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.btn5deletemind = new System.Windows.Forms.Button();
			this.btn5insertmind = new System.Windows.Forms.Button();
			this.txt5Mind = new System.Windows.Forms.TextBox();
			this.lbl5mind = new System.Windows.Forms.Label();
			this.dgw5Mind = new System.Windows.Forms.DataGridView();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.btn61130 = new System.Windows.Forms.Button();
			this.btn61120 = new System.Windows.Forms.Button();
			this.btn61110 = new System.Windows.Forms.Button();
			this.lbl6sleep = new System.Windows.Forms.Label();
			this.btn611 = new System.Windows.Forms.Button();
			this.txt6time = new System.Windows.Forms.TextBox();
			this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
			this.Panellive.SuspendLayout();
			this.Panelawake.SuspendLayout();
			this.Panelsleep.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.time2exercise)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgw2exercise)).BeginInit();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw3emergency)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw4continue)).BeginInit();
			this.tabPage5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw5Mind)).BeginInit();
			this.tabPage6.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl1birth
			// 
			this.lbl1birth.AutoSize = true;
			this.lbl1birth.Location = new System.Drawing.Point(6, 17);
			this.lbl1birth.Name = "lbl1birth";
			this.lbl1birth.Size = new System.Drawing.Size(118, 15);
			this.lbl1birth.TabIndex = 1;
			this.lbl1birth.Text = "1997년 8월 30일";
			// 
			// lbl1live40age
			// 
			this.lbl1live40age.AutoSize = true;
			this.lbl1live40age.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1live40age.Location = new System.Drawing.Point(3, 36);
			this.lbl1live40age.Name = "lbl1live40age";
			this.lbl1live40age.Size = new System.Drawing.Size(52, 36);
			this.lbl1live40age.TabIndex = 2;
			this.lbl1live40age.Text = "40살";
			// 
			// lbl1live30age
			// 
			this.lbl1live30age.AutoSize = true;
			this.lbl1live30age.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1live30age.Location = new System.Drawing.Point(3, 0);
			this.lbl1live30age.Name = "lbl1live30age";
			this.lbl1live30age.Size = new System.Drawing.Size(52, 36);
			this.lbl1live30age.TabIndex = 3;
			this.lbl1live30age.Text = "30살";
			// 
			// lbl1live60age
			// 
			this.lbl1live60age.AutoSize = true;
			this.lbl1live60age.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1live60age.Location = new System.Drawing.Point(3, 72);
			this.lbl1live60age.Name = "lbl1live60age";
			this.lbl1live60age.Size = new System.Drawing.Size(52, 36);
			this.lbl1live60age.TabIndex = 2;
			this.lbl1live60age.Text = "60살";
			// 
			// lbl1live65age
			// 
			this.lbl1live65age.AutoSize = true;
			this.lbl1live65age.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1live65age.Location = new System.Drawing.Point(3, 108);
			this.lbl1live65age.Name = "lbl1live65age";
			this.lbl1live65age.Size = new System.Drawing.Size(52, 37);
			this.lbl1live65age.TabIndex = 4;
			this.lbl1live65age.Text = "65살";
			// 
			// lbl1time
			// 
			this.lbl1time.AutoSize = true;
			this.lbl1time.Location = new System.Drawing.Point(550, 47);
			this.lbl1time.Name = "lbl1time";
			this.lbl1time.Size = new System.Drawing.Size(87, 15);
			this.lbl1time.TabIndex = 6;
			this.lbl1time.Text = "태어난 날짜";
			// 
			// txtbox1live30
			// 
			this.txtbox1live30.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1live30.Location = new System.Drawing.Point(61, 3);
			this.txtbox1live30.Name = "txtbox1live30";
			this.txtbox1live30.Size = new System.Drawing.Size(430, 25);
			this.txtbox1live30.TabIndex = 8;
			// 
			// txtbox1live40
			// 
			this.txtbox1live40.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1live40.Location = new System.Drawing.Point(61, 39);
			this.txtbox1live40.Name = "txtbox1live40";
			this.txtbox1live40.Size = new System.Drawing.Size(430, 25);
			this.txtbox1live40.TabIndex = 8;
			// 
			// txtbox1live60
			// 
			this.txtbox1live60.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1live60.Location = new System.Drawing.Point(61, 75);
			this.txtbox1live60.Name = "txtbox1live60";
			this.txtbox1live60.Size = new System.Drawing.Size(430, 25);
			this.txtbox1live60.TabIndex = 8;
			// 
			// txtbox1live65
			// 
			this.txtbox1live65.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1live65.Location = new System.Drawing.Point(61, 111);
			this.txtbox1live65.Name = "txtbox1live65";
			this.txtbox1live65.Size = new System.Drawing.Size(430, 25);
			this.txtbox1live65.TabIndex = 8;
			// 
			// Panellive
			// 
			this.Panellive.ColumnCount = 2;
			this.Panellive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.88475F));
			this.Panellive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.11525F));
			this.Panellive.Controls.Add(this.lbl1live30age, 0, 0);
			this.Panellive.Controls.Add(this.lbl1live40age, 0, 1);
			this.Panellive.Controls.Add(this.lbl1live60age, 0, 2);
			this.Panellive.Controls.Add(this.lbl1live65age, 0, 3);
			this.Panellive.Controls.Add(this.txtbox1live30, 1, 0);
			this.Panellive.Controls.Add(this.txtbox1live40, 1, 1);
			this.Panellive.Controls.Add(this.txtbox1live60, 1, 2);
			this.Panellive.Controls.Add(this.txtbox1live65, 1, 3);
			this.Panellive.Location = new System.Drawing.Point(9, 47);
			this.Panellive.Name = "Panellive";
			this.Panellive.RowCount = 4;
			this.Panellive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panellive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panellive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panellive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panellive.Size = new System.Drawing.Size(494, 145);
			this.Panellive.TabIndex = 10;
			// 
			// Panelawake
			// 
			this.Panelawake.ColumnCount = 2;
			this.Panelawake.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.88475F));
			this.Panelawake.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.11525F));
			this.Panelawake.Controls.Add(this.lbl1awake30, 0, 0);
			this.Panelawake.Controls.Add(this.lbl1awake40, 0, 1);
			this.Panelawake.Controls.Add(this.lbl1awake60, 0, 2);
			this.Panelawake.Controls.Add(this.lbl1awake65, 0, 3);
			this.Panelawake.Controls.Add(this.txtbox1awake30, 1, 0);
			this.Panelawake.Controls.Add(this.txtbox1awake40, 1, 1);
			this.Panelawake.Controls.Add(this.txtbox1awake60, 1, 2);
			this.Panelawake.Controls.Add(this.txtbox1awake65, 1, 3);
			this.Panelawake.Location = new System.Drawing.Point(10, 244);
			this.Panelawake.Name = "Panelawake";
			this.Panelawake.RowCount = 4;
			this.Panelawake.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelawake.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelawake.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelawake.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelawake.Size = new System.Drawing.Size(482, 158);
			this.Panelawake.TabIndex = 11;
			// 
			// lbl1awake30
			// 
			this.lbl1awake30.AutoSize = true;
			this.lbl1awake30.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1awake30.Location = new System.Drawing.Point(3, 0);
			this.lbl1awake30.Name = "lbl1awake30";
			this.lbl1awake30.Size = new System.Drawing.Size(51, 39);
			this.lbl1awake30.TabIndex = 3;
			this.lbl1awake30.Text = "30살";
			// 
			// lbl1awake40
			// 
			this.lbl1awake40.AutoSize = true;
			this.lbl1awake40.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1awake40.Location = new System.Drawing.Point(3, 39);
			this.lbl1awake40.Name = "lbl1awake40";
			this.lbl1awake40.Size = new System.Drawing.Size(51, 39);
			this.lbl1awake40.TabIndex = 2;
			this.lbl1awake40.Text = "40살";
			// 
			// lbl1awake60
			// 
			this.lbl1awake60.AutoSize = true;
			this.lbl1awake60.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1awake60.Location = new System.Drawing.Point(3, 78);
			this.lbl1awake60.Name = "lbl1awake60";
			this.lbl1awake60.Size = new System.Drawing.Size(51, 39);
			this.lbl1awake60.TabIndex = 2;
			this.lbl1awake60.Text = "60살";
			// 
			// lbl1awake65
			// 
			this.lbl1awake65.AutoSize = true;
			this.lbl1awake65.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1awake65.Location = new System.Drawing.Point(3, 117);
			this.lbl1awake65.Name = "lbl1awake65";
			this.lbl1awake65.Size = new System.Drawing.Size(51, 41);
			this.lbl1awake65.TabIndex = 4;
			this.lbl1awake65.Text = "65살";
			// 
			// txtbox1awake30
			// 
			this.txtbox1awake30.AcceptsReturn = true;
			this.txtbox1awake30.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1awake30.Location = new System.Drawing.Point(60, 3);
			this.txtbox1awake30.Name = "txtbox1awake30";
			this.txtbox1awake30.Size = new System.Drawing.Size(419, 25);
			this.txtbox1awake30.TabIndex = 8;
			// 
			// txtbox1awake40
			// 
			this.txtbox1awake40.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1awake40.Location = new System.Drawing.Point(60, 42);
			this.txtbox1awake40.Name = "txtbox1awake40";
			this.txtbox1awake40.Size = new System.Drawing.Size(419, 25);
			this.txtbox1awake40.TabIndex = 8;
			// 
			// txtbox1awake60
			// 
			this.txtbox1awake60.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1awake60.Location = new System.Drawing.Point(60, 81);
			this.txtbox1awake60.Name = "txtbox1awake60";
			this.txtbox1awake60.Size = new System.Drawing.Size(419, 25);
			this.txtbox1awake60.TabIndex = 8;
			// 
			// txtbox1awake65
			// 
			this.txtbox1awake65.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1awake65.Location = new System.Drawing.Point(60, 120);
			this.txtbox1awake65.Name = "txtbox1awake65";
			this.txtbox1awake65.Size = new System.Drawing.Size(419, 25);
			this.txtbox1awake65.TabIndex = 9;
			// 
			// Panelsleep
			// 
			this.Panelsleep.ColumnCount = 2;
			this.Panelsleep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.88475F));
			this.Panelsleep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.11525F));
			this.Panelsleep.Controls.Add(this.lbl1sleep30, 0, 0);
			this.Panelsleep.Controls.Add(this.lbl1sleep40, 0, 1);
			this.Panelsleep.Controls.Add(this.lbl1sleep60, 0, 2);
			this.Panelsleep.Controls.Add(this.lbl1sleep65, 0, 3);
			this.Panelsleep.Controls.Add(this.txtbox1sleep30, 1, 0);
			this.Panelsleep.Controls.Add(this.txtbox1sleep40, 1, 1);
			this.Panelsleep.Controls.Add(this.txtbox1sleep60, 1, 2);
			this.Panelsleep.Controls.Add(this.txtbox1sleep65, 1, 3);
			this.Panelsleep.Location = new System.Drawing.Point(10, 460);
			this.Panelsleep.Name = "Panelsleep";
			this.Panelsleep.RowCount = 4;
			this.Panelsleep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelsleep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelsleep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelsleep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.Panelsleep.Size = new System.Drawing.Size(482, 158);
			this.Panelsleep.TabIndex = 12;
			// 
			// lbl1sleep30
			// 
			this.lbl1sleep30.AutoSize = true;
			this.lbl1sleep30.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1sleep30.Location = new System.Drawing.Point(3, 0);
			this.lbl1sleep30.Name = "lbl1sleep30";
			this.lbl1sleep30.Size = new System.Drawing.Size(51, 39);
			this.lbl1sleep30.TabIndex = 3;
			this.lbl1sleep30.Text = "30살";
			// 
			// lbl1sleep40
			// 
			this.lbl1sleep40.AutoSize = true;
			this.lbl1sleep40.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1sleep40.Location = new System.Drawing.Point(3, 39);
			this.lbl1sleep40.Name = "lbl1sleep40";
			this.lbl1sleep40.Size = new System.Drawing.Size(51, 39);
			this.lbl1sleep40.TabIndex = 2;
			this.lbl1sleep40.Text = "40살";
			// 
			// lbl1sleep60
			// 
			this.lbl1sleep60.AutoSize = true;
			this.lbl1sleep60.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1sleep60.Location = new System.Drawing.Point(3, 78);
			this.lbl1sleep60.Name = "lbl1sleep60";
			this.lbl1sleep60.Size = new System.Drawing.Size(51, 39);
			this.lbl1sleep60.TabIndex = 2;
			this.lbl1sleep60.Text = "60살";
			// 
			// lbl1sleep65
			// 
			this.lbl1sleep65.AutoSize = true;
			this.lbl1sleep65.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl1sleep65.Location = new System.Drawing.Point(3, 117);
			this.lbl1sleep65.Name = "lbl1sleep65";
			this.lbl1sleep65.Size = new System.Drawing.Size(51, 41);
			this.lbl1sleep65.TabIndex = 4;
			this.lbl1sleep65.Text = "65살";
			// 
			// txtbox1sleep30
			// 
			this.txtbox1sleep30.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1sleep30.Location = new System.Drawing.Point(60, 3);
			this.txtbox1sleep30.Name = "txtbox1sleep30";
			this.txtbox1sleep30.Size = new System.Drawing.Size(419, 25);
			this.txtbox1sleep30.TabIndex = 8;
			// 
			// txtbox1sleep40
			// 
			this.txtbox1sleep40.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1sleep40.Location = new System.Drawing.Point(60, 42);
			this.txtbox1sleep40.Name = "txtbox1sleep40";
			this.txtbox1sleep40.Size = new System.Drawing.Size(419, 25);
			this.txtbox1sleep40.TabIndex = 8;
			// 
			// txtbox1sleep60
			// 
			this.txtbox1sleep60.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1sleep60.Location = new System.Drawing.Point(60, 81);
			this.txtbox1sleep60.Name = "txtbox1sleep60";
			this.txtbox1sleep60.Size = new System.Drawing.Size(419, 25);
			this.txtbox1sleep60.TabIndex = 8;
			// 
			// txtbox1sleep65
			// 
			this.txtbox1sleep65.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbox1sleep65.Location = new System.Drawing.Point(60, 120);
			this.txtbox1sleep65.Name = "txtbox1sleep65";
			this.txtbox1sleep65.Size = new System.Drawing.Size(419, 25);
			this.txtbox1sleep65.TabIndex = 8;
			// 
			// lbl1awake
			// 
			this.lbl1awake.AutoSize = true;
			this.lbl1awake.Location = new System.Drawing.Point(13, 206);
			this.lbl1awake.Name = "lbl1awake";
			this.lbl1awake.Size = new System.Drawing.Size(57, 15);
			this.lbl1awake.TabIndex = 13;
			this.lbl1awake.Text = "<의식>";
			// 
			// lbl1sleep
			// 
			this.lbl1sleep.AutoSize = true;
			this.lbl1sleep.Location = new System.Drawing.Point(6, 424);
			this.lbl1sleep.Name = "lbl1sleep";
			this.lbl1sleep.Size = new System.Drawing.Size(42, 15);
			this.lbl1sleep.TabIndex = 14;
			this.lbl1sleep.Text = "<잠>";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage4);
			this.tabControl.Controls.Add(this.tabPage5);
			this.tabControl.Controls.Add(this.tabPage6);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1194, 824);
			this.tabControl.TabIndex = 15;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btn1update);
			this.tabPage1.Controls.Add(this.lbl1birth);
			this.tabPage1.Controls.Add(this.lbl1time);
			this.tabPage1.Controls.Add(this.lbl1sleep);
			this.tabPage1.Controls.Add(this.lbl1awake);
			this.tabPage1.Controls.Add(this.Panellive);
			this.tabPage1.Controls.Add(this.Panelawake);
			this.tabPage1.Controls.Add(this.Panelsleep);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1186, 795);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "시간";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btn1update
			// 
			this.btn1update.Location = new System.Drawing.Point(553, 245);
			this.btn1update.Name = "btn1update";
			this.btn1update.Size = new System.Drawing.Size(174, 66);
			this.btn1update.TabIndex = 15;
			this.btn1update.Text = "업데이트";
			this.btn1update.UseVisualStyleBackColor = true;
			this.btn1update.Click += new System.EventHandler(this.btn1update_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btn2delete);
			this.tabPage2.Controls.Add(this.txttime2);
			this.tabPage2.Controls.Add(this.btn2finish);
			this.tabPage2.Controls.Add(this.btn2start);
			this.tabPage2.Controls.Add(this.btn2insertexercise);
			this.tabPage2.Controls.Add(this.time2exercise);
			this.tabPage2.Controls.Add(this.Date2exercise);
			this.tabPage2.Controls.Add(this.dgw2exercise);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1186, 795);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "운동";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// btn2delete
			// 
			this.btn2delete.Location = new System.Drawing.Point(959, 113);
			this.btn2delete.Name = "btn2delete";
			this.btn2delete.Size = new System.Drawing.Size(173, 73);
			this.btn2delete.TabIndex = 7;
			this.btn2delete.Text = "행삭제";
			this.btn2delete.UseVisualStyleBackColor = true;
			this.btn2delete.Click += new System.EventHandler(this.btn2delete_Click);
			// 
			// txttime2
			// 
			this.txttime2.Location = new System.Drawing.Point(752, 222);
			this.txttime2.Name = "txttime2";
			this.txttime2.Size = new System.Drawing.Size(345, 25);
			this.txttime2.TabIndex = 6;
			// 
			// btn2finish
			// 
			this.btn2finish.Location = new System.Drawing.Point(931, 279);
			this.btn2finish.Name = "btn2finish";
			this.btn2finish.Size = new System.Drawing.Size(166, 107);
			this.btn2finish.TabIndex = 5;
			this.btn2finish.Text = "종료";
			this.btn2finish.UseVisualStyleBackColor = true;
			// 
			// btn2start
			// 
			this.btn2start.Location = new System.Drawing.Point(752, 279);
			this.btn2start.Name = "btn2start";
			this.btn2start.Size = new System.Drawing.Size(166, 107);
			this.btn2start.TabIndex = 4;
			this.btn2start.Text = "시작";
			this.btn2start.UseVisualStyleBackColor = true;
			// 
			// btn2insertexercise
			// 
			this.btn2insertexercise.Location = new System.Drawing.Point(752, 113);
			this.btn2insertexercise.Name = "btn2insertexercise";
			this.btn2insertexercise.Size = new System.Drawing.Size(200, 73);
			this.btn2insertexercise.TabIndex = 3;
			this.btn2insertexercise.Text = "시간등록";
			this.btn2insertexercise.UseVisualStyleBackColor = true;
			this.btn2insertexercise.Click += new System.EventHandler(this.btn2insertexercise_Click);
			// 
			// time2exercise
			// 
			this.time2exercise.Location = new System.Drawing.Point(977, 42);
			this.time2exercise.Name = "time2exercise";
			this.time2exercise.Size = new System.Drawing.Size(120, 25);
			this.time2exercise.TabIndex = 2;
			// 
			// Date2exercise
			// 
			this.Date2exercise.Location = new System.Drawing.Point(752, 42);
			this.Date2exercise.Name = "Date2exercise";
			this.Date2exercise.Size = new System.Drawing.Size(200, 25);
			this.Date2exercise.TabIndex = 1;
			// 
			// dgw2exercise
			// 
			this.dgw2exercise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw2exercise.Location = new System.Drawing.Point(18, 17);
			this.dgw2exercise.Name = "dgw2exercise";
			this.dgw2exercise.RowHeadersWidth = 51;
			this.dgw2exercise.RowTemplate.Height = 27;
			this.dgw2exercise.Size = new System.Drawing.Size(679, 396);
			this.dgw2exercise.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.btn3deletemergency);
			this.tabPage3.Controls.Add(this.btn3insertemergency);
			this.tabPage3.Controls.Add(this.txt3emergency);
			this.tabPage3.Controls.Add(this.lbl3emergency);
			this.tabPage3.Controls.Add(this.dgw3emergency);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1186, 795);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "급한거";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// btn3deletemergency
			// 
			this.btn3deletemergency.Location = new System.Drawing.Point(936, 306);
			this.btn3deletemergency.Name = "btn3deletemergency";
			this.btn3deletemergency.Size = new System.Drawing.Size(201, 59);
			this.btn3deletemergency.TabIndex = 10;
			this.btn3deletemergency.Text = "삭제";
			this.btn3deletemergency.UseVisualStyleBackColor = true;
			// 
			// btn3insertemergency
			// 
			this.btn3insertemergency.Location = new System.Drawing.Point(936, 221);
			this.btn3insertemergency.Name = "btn3insertemergency";
			this.btn3insertemergency.Size = new System.Drawing.Size(201, 59);
			this.btn3insertemergency.TabIndex = 8;
			this.btn3insertemergency.Text = "등록";
			this.btn3insertemergency.UseVisualStyleBackColor = true;
			// 
			// txt3emergency
			// 
			this.txt3emergency.Location = new System.Drawing.Point(936, 37);
			this.txt3emergency.Multiline = true;
			this.txt3emergency.Name = "txt3emergency";
			this.txt3emergency.Size = new System.Drawing.Size(242, 132);
			this.txt3emergency.TabIndex = 7;
			// 
			// lbl3emergency
			// 
			this.lbl3emergency.AutoSize = true;
			this.lbl3emergency.Location = new System.Drawing.Point(16, 19);
			this.lbl3emergency.Name = "lbl3emergency";
			this.lbl3emergency.Size = new System.Drawing.Size(52, 15);
			this.lbl3emergency.TabIndex = 3;
			this.lbl3emergency.Text = "급한거";
			// 
			// dgw3emergency
			// 
			this.dgw3emergency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw3emergency.Location = new System.Drawing.Point(19, 37);
			this.dgw3emergency.Name = "dgw3emergency";
			this.dgw3emergency.RowHeadersWidth = 51;
			this.dgw3emergency.RowTemplate.Height = 27;
			this.dgw3emergency.Size = new System.Drawing.Size(894, 807);
			this.dgw3emergency.TabIndex = 1;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.btn4deletecontinue);
			this.tabPage4.Controls.Add(this.btn4insertcontinue);
			this.tabPage4.Controls.Add(this.txt4continune);
			this.tabPage4.Controls.Add(this.dgw4continue);
			this.tabPage4.Controls.Add(this.lbl4continue);
			this.tabPage4.Location = new System.Drawing.Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(1186, 795);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "계속꾸준히";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// btn4deletecontinue
			// 
			this.btn4deletecontinue.Location = new System.Drawing.Point(953, 299);
			this.btn4deletecontinue.Name = "btn4deletecontinue";
			this.btn4deletecontinue.Size = new System.Drawing.Size(201, 59);
			this.btn4deletecontinue.TabIndex = 14;
			this.btn4deletecontinue.Text = "삭제";
			this.btn4deletecontinue.UseVisualStyleBackColor = true;
			// 
			// btn4insertcontinue
			// 
			this.btn4insertcontinue.Location = new System.Drawing.Point(953, 223);
			this.btn4insertcontinue.Name = "btn4insertcontinue";
			this.btn4insertcontinue.Size = new System.Drawing.Size(201, 59);
			this.btn4insertcontinue.TabIndex = 13;
			this.btn4insertcontinue.Text = "등록";
			this.btn4insertcontinue.UseVisualStyleBackColor = true;
			// 
			// txt4continune
			// 
			this.txt4continune.Location = new System.Drawing.Point(953, 56);
			this.txt4continune.Multiline = true;
			this.txt4continune.Name = "txt4continune";
			this.txt4continune.Size = new System.Drawing.Size(201, 132);
			this.txt4continune.TabIndex = 12;
			// 
			// dgw4continue
			// 
			this.dgw4continue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw4continue.Location = new System.Drawing.Point(27, 56);
			this.dgw4continue.Name = "dgw4continue";
			this.dgw4continue.RowHeadersWidth = 51;
			this.dgw4continue.RowTemplate.Height = 27;
			this.dgw4continue.Size = new System.Drawing.Size(894, 807);
			this.dgw4continue.TabIndex = 10;
			// 
			// lbl4continue
			// 
			this.lbl4continue.AutoSize = true;
			this.lbl4continue.Location = new System.Drawing.Point(24, 27);
			this.lbl4continue.Name = "lbl4continue";
			this.lbl4continue.Size = new System.Drawing.Size(157, 15);
			this.lbl4continue.TabIndex = 11;
			this.lbl4continue.Text = "계속꾸준히해야하는거";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.btn5deletemind);
			this.tabPage5.Controls.Add(this.btn5insertmind);
			this.tabPage5.Controls.Add(this.txt5Mind);
			this.tabPage5.Controls.Add(this.lbl5mind);
			this.tabPage5.Controls.Add(this.dgw5Mind);
			this.tabPage5.Location = new System.Drawing.Point(4, 25);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(1186, 795);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "마인드";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// btn5deletemind
			// 
			this.btn5deletemind.Location = new System.Drawing.Point(945, 308);
			this.btn5deletemind.Name = "btn5deletemind";
			this.btn5deletemind.Size = new System.Drawing.Size(201, 59);
			this.btn5deletemind.TabIndex = 15;
			this.btn5deletemind.Text = "삭제";
			this.btn5deletemind.UseVisualStyleBackColor = true;
			// 
			// btn5insertmind
			// 
			this.btn5insertmind.Location = new System.Drawing.Point(945, 231);
			this.btn5insertmind.Name = "btn5insertmind";
			this.btn5insertmind.Size = new System.Drawing.Size(201, 59);
			this.btn5insertmind.TabIndex = 13;
			this.btn5insertmind.Text = "등록";
			this.btn5insertmind.UseVisualStyleBackColor = true;
			// 
			// txt5Mind
			// 
			this.txt5Mind.Location = new System.Drawing.Point(945, 61);
			this.txt5Mind.Multiline = true;
			this.txt5Mind.Name = "txt5Mind";
			this.txt5Mind.Size = new System.Drawing.Size(201, 132);
			this.txt5Mind.TabIndex = 12;
			// 
			// lbl5mind
			// 
			this.lbl5mind.AutoSize = true;
			this.lbl5mind.Location = new System.Drawing.Point(22, 26);
			this.lbl5mind.Name = "lbl5mind";
			this.lbl5mind.Size = new System.Drawing.Size(52, 15);
			this.lbl5mind.TabIndex = 11;
			this.lbl5mind.Text = "마인드";
			// 
			// dgw5Mind
			// 
			this.dgw5Mind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw5Mind.Location = new System.Drawing.Point(25, 44);
			this.dgw5Mind.Name = "dgw5Mind";
			this.dgw5Mind.RowHeadersWidth = 51;
			this.dgw5Mind.RowTemplate.Height = 27;
			this.dgw5Mind.Size = new System.Drawing.Size(894, 807);
			this.dgw5Mind.TabIndex = 10;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.btn61130);
			this.tabPage6.Controls.Add(this.btn61120);
			this.tabPage6.Controls.Add(this.btn61110);
			this.tabPage6.Controls.Add(this.lbl6sleep);
			this.tabPage6.Controls.Add(this.btn611);
			this.tabPage6.Controls.Add(this.txt6time);
			this.tabPage6.Controls.Add(this.dateTimePicker6);
			this.tabPage6.Location = new System.Drawing.Point(4, 25);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(1186, 795);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "자야하는시간설정";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// btn61130
			// 
			this.btn61130.Location = new System.Drawing.Point(390, 104);
			this.btn61130.Name = "btn61130";
			this.btn61130.Size = new System.Drawing.Size(98, 38);
			this.btn61130.TabIndex = 6;
			this.btn61130.Text = "11시30분";
			this.btn61130.UseVisualStyleBackColor = true;
			// 
			// btn61120
			// 
			this.btn61120.Location = new System.Drawing.Point(286, 104);
			this.btn61120.Name = "btn61120";
			this.btn61120.Size = new System.Drawing.Size(98, 38);
			this.btn61120.TabIndex = 5;
			this.btn61120.Text = "11시20분";
			this.btn61120.UseVisualStyleBackColor = true;
			// 
			// btn61110
			// 
			this.btn61110.Location = new System.Drawing.Point(182, 104);
			this.btn61110.Name = "btn61110";
			this.btn61110.Size = new System.Drawing.Size(98, 38);
			this.btn61110.TabIndex = 4;
			this.btn61110.Text = "11시10분";
			this.btn61110.UseVisualStyleBackColor = true;
			// 
			// lbl6sleep
			// 
			this.lbl6sleep.AutoSize = true;
			this.lbl6sleep.Location = new System.Drawing.Point(75, 53);
			this.lbl6sleep.Name = "lbl6sleep";
			this.lbl6sleep.Size = new System.Drawing.Size(97, 15);
			this.lbl6sleep.TabIndex = 2;
			this.lbl6sleep.Text = "자야할시간 : ";
			// 
			// btn611
			// 
			this.btn611.Location = new System.Drawing.Point(78, 104);
			this.btn611.Name = "btn611";
			this.btn611.Size = new System.Drawing.Size(98, 38);
			this.btn611.TabIndex = 3;
			this.btn611.Text = "11시";
			this.btn611.UseVisualStyleBackColor = true;
			// 
			// txt6time
			// 
			this.txt6time.Location = new System.Drawing.Point(78, 165);
			this.txt6time.Name = "txt6time";
			this.txt6time.Size = new System.Drawing.Size(433, 25);
			this.txt6time.TabIndex = 1;
			// 
			// dateTimePicker6
			// 
			this.dateTimePicker6.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker6.Location = new System.Drawing.Point(182, 46);
			this.dateTimePicker6.Name = "dateTimePicker6";
			this.dateTimePicker6.Size = new System.Drawing.Size(232, 25);
			this.dateTimePicker6.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1194, 824);
			this.Controls.Add(this.tabControl);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Panellive.ResumeLayout(false);
			this.Panellive.PerformLayout();
			this.Panelawake.ResumeLayout(false);
			this.Panelawake.PerformLayout();
			this.Panelsleep.ResumeLayout(false);
			this.Panelsleep.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.time2exercise)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgw2exercise)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw3emergency)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw4continue)).EndInit();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgw5Mind)).EndInit();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl1birth;
        private System.Windows.Forms.Label lbl1live40age;
        private System.Windows.Forms.Label lbl1live30age;
        private System.Windows.Forms.Label lbl1live60age;
        private System.Windows.Forms.Label lbl1live65age;
        private System.Windows.Forms.Label lbl1time;
        private System.Windows.Forms.TextBox txtbox1live30;
        private System.Windows.Forms.TextBox txtbox1live40;
        private System.Windows.Forms.TextBox txtbox1live60;
        private System.Windows.Forms.TextBox txtbox1live65;
        private System.Windows.Forms.TableLayoutPanel Panellive;
        private System.Windows.Forms.TableLayoutPanel Panelawake;
        private System.Windows.Forms.Label lbl1awake30;
        private System.Windows.Forms.Label lbl1awake40;
        private System.Windows.Forms.TextBox txtbox1awake40;
        private System.Windows.Forms.Label lbl1awake60;
        private System.Windows.Forms.TextBox txtbox1awake30;
        private System.Windows.Forms.Label lbl1awake65;
        private System.Windows.Forms.TextBox txtbox1awake60;
        private System.Windows.Forms.TableLayoutPanel Panelsleep;
        private System.Windows.Forms.Label lbl1sleep30;
        private System.Windows.Forms.Label lbl1sleep40;
        private System.Windows.Forms.TextBox txtbox1sleep40;
        private System.Windows.Forms.Label lbl1sleep60;
        private System.Windows.Forms.TextBox txtbox1sleep65;
        private System.Windows.Forms.Label lbl1sleep65;
        private System.Windows.Forms.TextBox txtbox1sleep60;
        private System.Windows.Forms.TextBox txtbox1sleep30;
        private System.Windows.Forms.TextBox txtbox1awake65;
        private System.Windows.Forms.Label lbl1awake;
        private System.Windows.Forms.Label lbl1sleep;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgw3emergency;
        private System.Windows.Forms.Label lbl3emergency;
        private System.Windows.Forms.Button btn3insertemergency;
        private System.Windows.Forms.TextBox txt3emergency;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btn4insertcontinue;
        private System.Windows.Forms.TextBox txt4continune;
        private System.Windows.Forms.Label lbl4continue;
        private System.Windows.Forms.DataGridView dgw4continue;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btn5insertmind;
        private System.Windows.Forms.TextBox txt5Mind;
        private System.Windows.Forms.Label lbl5mind;
        private System.Windows.Forms.DataGridView dgw5Mind;
        private System.Windows.Forms.Button btn3deletemergency;
        private System.Windows.Forms.Button btn5deletemind;
        private System.Windows.Forms.DataGridView dgw2exercise;
        private System.Windows.Forms.Button btn4deletecontinue;
        private System.Windows.Forms.Button btn2insertexercise;
        private System.Windows.Forms.NumericUpDown time2exercise;
        private System.Windows.Forms.DateTimePicker Date2exercise;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.TextBox txt6time;
        private System.Windows.Forms.Label lbl6sleep;
        private System.Windows.Forms.Button btn61120;
        private System.Windows.Forms.Button btn61110;
        private System.Windows.Forms.Button btn611;
        private System.Windows.Forms.Button btn61130;
        private System.Windows.Forms.Button btn2finish;
        private System.Windows.Forms.Button btn2start;
        private System.Windows.Forms.TextBox txttime2;
        private System.Windows.Forms.Button btn2delete;
        private System.Windows.Forms.Button btn1update;
    }
}

