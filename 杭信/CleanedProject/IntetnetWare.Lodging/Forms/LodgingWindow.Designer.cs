namespace InternetWare.Form
{
    partial class LodgingWindow
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ChaXunPage = new System.Windows.Forms.TabPage();
            this.ChaXun_DataGrid = new System.Windows.Forms.DataGridView();
            this.groupChaXun = new System.Windows.Forms.GroupBox();
            this.ChaXun_btnPrint = new System.Windows.Forms.Button();
            this.ChaXun_grpMatchStr = new System.Windows.Forms.GroupBox();
            this.ChaXun_txtMathStr = new System.Windows.Forms.TextBox();
            this.ChaXun_checkYanQianShiBai = new System.Windows.Forms.CheckBox();
            this.ChaXun_checkWeiBaoSong = new System.Windows.Forms.CheckBox();
            this.ChaXun_BtnGo = new System.Windows.Forms.Button();
            this.ChaXun_CmbFpType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChaXun_CmbMonth = new System.Windows.Forms.ComboBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.ChaXun_CmbYear = new System.Windows.Forms.ComboBox();
            this.TianKaiPage = new System.Windows.Forms.TabPage();
            this.ZuoFeiPage = new System.Windows.Forms.TabPage();
            this.ZuoFei_gridView = new System.Windows.Forms.DataGridView();
            this.ZuoFei_paramGroup = new System.Windows.Forms.GroupBox();
            this.ZuoFei_grpSearch = new System.Windows.Forms.GroupBox();
            this.ZuoFei_txtMatch = new System.Windows.Forms.TextBox();
            this.ZuoFei_btnDoService = new System.Windows.Forms.Button();
            this.ZuoFei_btnSearch = new System.Windows.Forms.Button();
            this.ZuoFei_checkYanQianShiBai = new System.Windows.Forms.CheckBox();
            this.WeiKaiPage = new System.Windows.Forms.TabPage();
            this.WeiKai_btn作废未开 = new System.Windows.Forms.Button();
            this.WeiKai_txt要作废发票份数 = new System.Windows.Forms.TextBox();
            this.WeiKai_txt发票起始号码 = new System.Windows.Forms.TextBox();
            this.WeiKai_txt发票份数 = new System.Windows.Forms.TextBox();
            this.WeiKai_txt发票代码 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WeiKai_cmbFpType = new System.Windows.Forms.ComboBox();
            this.HongZiPage = new System.Windows.Forms.TabPage();
            this.HZSells = new System.Windows.Forms.Button();
            this.HZByer_wdk = new System.Windows.Forms.Button();
            this.HZByer_ydk = new System.Windows.Forms.Button();
            this.DaYinPage = new System.Windows.Forms.TabPage();
            this.DaYin_picBox = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DaYin_btnDoPrint = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.发票号码 = new System.Windows.Forms.Label();
            this.DaYin_tbFpdm = new System.Windows.Forms.TextBox();
            this.DaYin_tbFphm = new System.Windows.Forms.TextBox();
            this.DaYin_CmbPrinter = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.ChaXunPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChaXun_DataGrid)).BeginInit();
            this.groupChaXun.SuspendLayout();
            this.ChaXun_grpMatchStr.SuspendLayout();
            this.ZuoFeiPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZuoFei_gridView)).BeginInit();
            this.ZuoFei_paramGroup.SuspendLayout();
            this.ZuoFei_grpSearch.SuspendLayout();
            this.WeiKaiPage.SuspendLayout();
            this.HongZiPage.SuspendLayout();
            this.DaYinPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DaYin_picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.ChaXunPage);
            this.tabControl.Controls.Add(this.TianKaiPage);
            this.tabControl.Controls.Add(this.ZuoFeiPage);
            this.tabControl.Controls.Add(this.WeiKaiPage);
            this.tabControl.Controls.Add(this.HongZiPage);
            this.tabControl.Controls.Add(this.DaYinPage);
            this.tabControl.Location = new System.Drawing.Point(-1, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1583, 852);
            this.tabControl.TabIndex = 0;
            // 
            // ChaXunPage
            // 
            this.ChaXunPage.Controls.Add(this.ChaXun_DataGrid);
            this.ChaXunPage.Controls.Add(this.groupChaXun);
            this.ChaXunPage.Location = new System.Drawing.Point(4, 28);
            this.ChaXunPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXunPage.Name = "ChaXunPage";
            this.ChaXunPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXunPage.Size = new System.Drawing.Size(1575, 820);
            this.ChaXunPage.TabIndex = 0;
            this.ChaXunPage.Text = "查询";
            this.ChaXunPage.UseVisualStyleBackColor = true;
            // 
            // ChaXun_DataGrid
            // 
            this.ChaXun_DataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChaXun_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChaXun_DataGrid.Location = new System.Drawing.Point(10, 175);
            this.ChaXun_DataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_DataGrid.Name = "ChaXun_DataGrid";
            this.ChaXun_DataGrid.RowTemplate.Height = 27;
            this.ChaXun_DataGrid.Size = new System.Drawing.Size(1560, 642);
            this.ChaXun_DataGrid.TabIndex = 1;
            // 
            // groupChaXun
            // 
            this.groupChaXun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupChaXun.Controls.Add(this.ChaXun_btnPrint);
            this.groupChaXun.Controls.Add(this.ChaXun_grpMatchStr);
            this.groupChaXun.Controls.Add(this.ChaXun_checkYanQianShiBai);
            this.groupChaXun.Controls.Add(this.ChaXun_checkWeiBaoSong);
            this.groupChaXun.Controls.Add(this.ChaXun_BtnGo);
            this.groupChaXun.Controls.Add(this.ChaXun_CmbFpType);
            this.groupChaXun.Controls.Add(this.label1);
            this.groupChaXun.Controls.Add(this.ChaXun_CmbMonth);
            this.groupChaXun.Controls.Add(this.lbl1);
            this.groupChaXun.Controls.Add(this.ChaXun_CmbYear);
            this.groupChaXun.Location = new System.Drawing.Point(3, 0);
            this.groupChaXun.Margin = new System.Windows.Forms.Padding(0);
            this.groupChaXun.Name = "groupChaXun";
            this.groupChaXun.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupChaXun.Size = new System.Drawing.Size(1567, 172);
            this.groupChaXun.TabIndex = 0;
            this.groupChaXun.TabStop = false;
            this.groupChaXun.Text = "查询参数";
            // 
            // ChaXun_btnPrint
            // 
            this.ChaXun_btnPrint.Location = new System.Drawing.Point(1200, 49);
            this.ChaXun_btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_btnPrint.Name = "ChaXun_btnPrint";
            this.ChaXun_btnPrint.Size = new System.Drawing.Size(127, 58);
            this.ChaXun_btnPrint.TabIndex = 9;
            this.ChaXun_btnPrint.Text = "打印";
            this.ChaXun_btnPrint.UseVisualStyleBackColor = true;
            this.ChaXun_btnPrint.Click += new System.EventHandler(this.ChaXun_btnPrint_Click);
            // 
            // ChaXun_grpMatchStr
            // 
            this.ChaXun_grpMatchStr.Controls.Add(this.ChaXun_txtMathStr);
            this.ChaXun_grpMatchStr.Location = new System.Drawing.Point(801, 46);
            this.ChaXun_grpMatchStr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_grpMatchStr.Name = "ChaXun_grpMatchStr";
            this.ChaXun_grpMatchStr.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_grpMatchStr.Size = new System.Drawing.Size(225, 61);
            this.ChaXun_grpMatchStr.TabIndex = 8;
            this.ChaXun_grpMatchStr.TabStop = false;
            this.ChaXun_grpMatchStr.Text = "请输入检索关键字...";
            // 
            // ChaXun_txtMathStr
            // 
            this.ChaXun_txtMathStr.Location = new System.Drawing.Point(7, 24);
            this.ChaXun_txtMathStr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_txtMathStr.Name = "ChaXun_txtMathStr";
            this.ChaXun_txtMathStr.Size = new System.Drawing.Size(211, 28);
            this.ChaXun_txtMathStr.TabIndex = 0;
            // 
            // ChaXun_checkYanQianShiBai
            // 
            this.ChaXun_checkYanQianShiBai.AutoSize = true;
            this.ChaXun_checkYanQianShiBai.Location = new System.Drawing.Point(681, 49);
            this.ChaXun_checkYanQianShiBai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_checkYanQianShiBai.Name = "ChaXun_checkYanQianShiBai";
            this.ChaXun_checkYanQianShiBai.Size = new System.Drawing.Size(106, 22);
            this.ChaXun_checkYanQianShiBai.TabIndex = 7;
            this.ChaXun_checkYanQianShiBai.Text = "验签失败";
            this.ChaXun_checkYanQianShiBai.UseVisualStyleBackColor = true;
            // 
            // ChaXun_checkWeiBaoSong
            // 
            this.ChaXun_checkWeiBaoSong.AutoSize = true;
            this.ChaXun_checkWeiBaoSong.Location = new System.Drawing.Point(572, 49);
            this.ChaXun_checkWeiBaoSong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_checkWeiBaoSong.Name = "ChaXun_checkWeiBaoSong";
            this.ChaXun_checkWeiBaoSong.Size = new System.Drawing.Size(88, 22);
            this.ChaXun_checkWeiBaoSong.TabIndex = 6;
            this.ChaXun_checkWeiBaoSong.Text = "未报送";
            this.ChaXun_checkWeiBaoSong.UseVisualStyleBackColor = true;
            // 
            // ChaXun_BtnGo
            // 
            this.ChaXun_BtnGo.Location = new System.Drawing.Point(1044, 49);
            this.ChaXun_BtnGo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_BtnGo.Name = "ChaXun_BtnGo";
            this.ChaXun_BtnGo.Size = new System.Drawing.Size(127, 58);
            this.ChaXun_BtnGo.TabIndex = 5;
            this.ChaXun_BtnGo.Text = "查询";
            this.ChaXun_BtnGo.UseVisualStyleBackColor = true;
            this.ChaXun_BtnGo.Click += new System.EventHandler(this.ChaXun_BtnGo_Click);
            // 
            // ChaXun_CmbFpType
            // 
            this.ChaXun_CmbFpType.FormattingEnabled = true;
            this.ChaXun_CmbFpType.Location = new System.Drawing.Point(379, 46);
            this.ChaXun_CmbFpType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_CmbFpType.Name = "ChaXun_CmbFpType";
            this.ChaXun_CmbFpType.Size = new System.Drawing.Size(160, 26);
            this.ChaXun_CmbFpType.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "月";
            // 
            // ChaXun_CmbMonth
            // 
            this.ChaXun_CmbMonth.FormattingEnabled = true;
            this.ChaXun_CmbMonth.Location = new System.Drawing.Point(212, 46);
            this.ChaXun_CmbMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_CmbMonth.Name = "ChaXun_CmbMonth";
            this.ChaXun_CmbMonth.Size = new System.Drawing.Size(102, 26);
            this.ChaXun_CmbMonth.TabIndex = 2;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(166, 55);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(26, 18);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "年";
            // 
            // ChaXun_CmbYear
            // 
            this.ChaXun_CmbYear.FormattingEnabled = true;
            this.ChaXun_CmbYear.Location = new System.Drawing.Point(7, 47);
            this.ChaXun_CmbYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChaXun_CmbYear.Name = "ChaXun_CmbYear";
            this.ChaXun_CmbYear.Size = new System.Drawing.Size(151, 26);
            this.ChaXun_CmbYear.TabIndex = 0;
            // 
            // TianKaiPage
            // 
            this.TianKaiPage.Location = new System.Drawing.Point(4, 28);
            this.TianKaiPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TianKaiPage.Name = "TianKaiPage";
            this.TianKaiPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TianKaiPage.Size = new System.Drawing.Size(1358, 820);
            this.TianKaiPage.TabIndex = 1;
            this.TianKaiPage.Text = "填开";
            this.TianKaiPage.UseVisualStyleBackColor = true;
            // 
            // ZuoFeiPage
            // 
            this.ZuoFeiPage.Controls.Add(this.ZuoFei_gridView);
            this.ZuoFeiPage.Controls.Add(this.ZuoFei_paramGroup);
            this.ZuoFeiPage.Location = new System.Drawing.Point(4, 28);
            this.ZuoFeiPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ZuoFeiPage.Name = "ZuoFeiPage";
            this.ZuoFeiPage.Size = new System.Drawing.Size(1358, 820);
            this.ZuoFeiPage.TabIndex = 2;
            this.ZuoFeiPage.Text = "已开作废";
            this.ZuoFeiPage.UseVisualStyleBackColor = true;
            // 
            // ZuoFei_gridView
            // 
            this.ZuoFei_gridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZuoFei_gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ZuoFei_gridView.Location = new System.Drawing.Point(4, 130);
            this.ZuoFei_gridView.Name = "ZuoFei_gridView";
            this.ZuoFei_gridView.RowTemplate.Height = 30;
            this.ZuoFei_gridView.Size = new System.Drawing.Size(1351, 687);
            this.ZuoFei_gridView.TabIndex = 1;
            // 
            // ZuoFei_paramGroup
            // 
            this.ZuoFei_paramGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZuoFei_paramGroup.Controls.Add(this.ZuoFei_grpSearch);
            this.ZuoFei_paramGroup.Controls.Add(this.ZuoFei_btnDoService);
            this.ZuoFei_paramGroup.Controls.Add(this.ZuoFei_btnSearch);
            this.ZuoFei_paramGroup.Controls.Add(this.ZuoFei_checkYanQianShiBai);
            this.ZuoFei_paramGroup.Location = new System.Drawing.Point(0, 3);
            this.ZuoFei_paramGroup.Name = "ZuoFei_paramGroup";
            this.ZuoFei_paramGroup.Size = new System.Drawing.Size(1355, 120);
            this.ZuoFei_paramGroup.TabIndex = 0;
            this.ZuoFei_paramGroup.TabStop = false;
            this.ZuoFei_paramGroup.Text = "查询参数";
            // 
            // ZuoFei_grpSearch
            // 
            this.ZuoFei_grpSearch.Controls.Add(this.ZuoFei_txtMatch);
            this.ZuoFei_grpSearch.Location = new System.Drawing.Point(22, 33);
            this.ZuoFei_grpSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ZuoFei_grpSearch.Name = "ZuoFei_grpSearch";
            this.ZuoFei_grpSearch.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ZuoFei_grpSearch.Size = new System.Drawing.Size(225, 61);
            this.ZuoFei_grpSearch.TabIndex = 9;
            this.ZuoFei_grpSearch.TabStop = false;
            this.ZuoFei_grpSearch.Text = "请输入检索关键字...";
            // 
            // ZuoFei_txtMatch
            // 
            this.ZuoFei_txtMatch.Location = new System.Drawing.Point(7, 24);
            this.ZuoFei_txtMatch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ZuoFei_txtMatch.Name = "ZuoFei_txtMatch";
            this.ZuoFei_txtMatch.Size = new System.Drawing.Size(211, 28);
            this.ZuoFei_txtMatch.TabIndex = 0;
            // 
            // ZuoFei_btnDoService
            // 
            this.ZuoFei_btnDoService.Location = new System.Drawing.Point(516, 57);
            this.ZuoFei_btnDoService.Name = "ZuoFei_btnDoService";
            this.ZuoFei_btnDoService.Size = new System.Drawing.Size(101, 37);
            this.ZuoFei_btnDoService.TabIndex = 4;
            this.ZuoFei_btnDoService.Text = "作废选中";
            this.ZuoFei_btnDoService.UseVisualStyleBackColor = true;
            this.ZuoFei_btnDoService.Click += new System.EventHandler(this.ZuoFei_btnDoService_Click);
            // 
            // ZuoFei_btnSearch
            // 
            this.ZuoFei_btnSearch.Location = new System.Drawing.Point(407, 57);
            this.ZuoFei_btnSearch.Name = "ZuoFei_btnSearch";
            this.ZuoFei_btnSearch.Size = new System.Drawing.Size(90, 37);
            this.ZuoFei_btnSearch.TabIndex = 3;
            this.ZuoFei_btnSearch.Text = "查询";
            this.ZuoFei_btnSearch.UseVisualStyleBackColor = true;
            this.ZuoFei_btnSearch.Click += new System.EventHandler(this.ZuoFei_btnSearch_Click);
            // 
            // ZuoFei_checkYanQianShiBai
            // 
            this.ZuoFei_checkYanQianShiBai.AutoSize = true;
            this.ZuoFei_checkYanQianShiBai.Location = new System.Drawing.Point(271, 69);
            this.ZuoFei_checkYanQianShiBai.Name = "ZuoFei_checkYanQianShiBai";
            this.ZuoFei_checkYanQianShiBai.Size = new System.Drawing.Size(106, 22);
            this.ZuoFei_checkYanQianShiBai.TabIndex = 0;
            this.ZuoFei_checkYanQianShiBai.Text = "验签失败";
            this.ZuoFei_checkYanQianShiBai.UseVisualStyleBackColor = true;
            // 
            // WeiKaiPage
            // 
            this.WeiKaiPage.Controls.Add(this.WeiKai_btn作废未开);
            this.WeiKaiPage.Controls.Add(this.WeiKai_txt要作废发票份数);
            this.WeiKaiPage.Controls.Add(this.WeiKai_txt发票起始号码);
            this.WeiKaiPage.Controls.Add(this.WeiKai_txt发票份数);
            this.WeiKaiPage.Controls.Add(this.WeiKai_txt发票代码);
            this.WeiKaiPage.Controls.Add(this.label7);
            this.WeiKaiPage.Controls.Add(this.label6);
            this.WeiKaiPage.Controls.Add(this.label5);
            this.WeiKaiPage.Controls.Add(this.label4);
            this.WeiKaiPage.Controls.Add(this.label2);
            this.WeiKaiPage.Controls.Add(this.WeiKai_cmbFpType);
            this.WeiKaiPage.Location = new System.Drawing.Point(4, 28);
            this.WeiKaiPage.Name = "WeiKaiPage";
            this.WeiKaiPage.Size = new System.Drawing.Size(1358, 820);
            this.WeiKaiPage.TabIndex = 5;
            this.WeiKaiPage.Text = "未开作废";
            this.WeiKaiPage.UseVisualStyleBackColor = true;
            // 
            // WeiKai_btn作废未开
            // 
            this.WeiKai_btn作废未开.Location = new System.Drawing.Point(135, 261);
            this.WeiKai_btn作废未开.Name = "WeiKai_btn作废未开";
            this.WeiKai_btn作废未开.Size = new System.Drawing.Size(185, 39);
            this.WeiKai_btn作废未开.TabIndex = 10;
            this.WeiKai_btn作废未开.Text = "作废未开发票";
            this.WeiKai_btn作废未开.UseVisualStyleBackColor = true;
            this.WeiKai_btn作废未开.Click += new System.EventHandler(this.WeiKai_btn作废未开_Click);
            // 
            // WeiKai_txt要作废发票份数
            // 
            this.WeiKai_txt要作废发票份数.Location = new System.Drawing.Point(219, 213);
            this.WeiKai_txt要作废发票份数.Name = "WeiKai_txt要作废发票份数";
            this.WeiKai_txt要作废发票份数.Size = new System.Drawing.Size(274, 28);
            this.WeiKai_txt要作废发票份数.TabIndex = 9;
            // 
            // WeiKai_txt发票起始号码
            // 
            this.WeiKai_txt发票起始号码.Location = new System.Drawing.Point(219, 164);
            this.WeiKai_txt发票起始号码.Name = "WeiKai_txt发票起始号码";
            this.WeiKai_txt发票起始号码.Size = new System.Drawing.Size(274, 28);
            this.WeiKai_txt发票起始号码.TabIndex = 8;
            // 
            // WeiKai_txt发票份数
            // 
            this.WeiKai_txt发票份数.Location = new System.Drawing.Point(219, 120);
            this.WeiKai_txt发票份数.Name = "WeiKai_txt发票份数";
            this.WeiKai_txt发票份数.Size = new System.Drawing.Size(274, 28);
            this.WeiKai_txt发票份数.TabIndex = 7;
            // 
            // WeiKai_txt发票代码
            // 
            this.WeiKai_txt发票代码.Location = new System.Drawing.Point(219, 68);
            this.WeiKai_txt发票代码.Name = "WeiKai_txt发票代码";
            this.WeiKai_txt发票代码.Size = new System.Drawing.Size(274, 28);
            this.WeiKai_txt发票代码.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 18);
            this.label7.TabIndex = 5;
            this.label7.Text = "作废发票份数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "发票起始号码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "发票份数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "发票代码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "发票种类";
            // 
            // WeiKai_cmbFpType
            // 
            this.WeiKai_cmbFpType.FormattingEnabled = true;
            this.WeiKai_cmbFpType.Location = new System.Drawing.Point(219, 23);
            this.WeiKai_cmbFpType.Name = "WeiKai_cmbFpType";
            this.WeiKai_cmbFpType.Size = new System.Drawing.Size(274, 26);
            this.WeiKai_cmbFpType.TabIndex = 0;
            this.WeiKai_cmbFpType.SelectedIndexChanged += new System.EventHandler(this.WeiKai_cmbFpType_SelectedIndexChanged);
            // 
            // HongZiPage
            // 
            this.HongZiPage.Controls.Add(this.HZSells);
            this.HongZiPage.Controls.Add(this.HZByer_wdk);
            this.HongZiPage.Controls.Add(this.HZByer_ydk);
            this.HongZiPage.Location = new System.Drawing.Point(4, 28);
            this.HongZiPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.HongZiPage.Name = "HongZiPage";
            this.HongZiPage.Size = new System.Drawing.Size(1358, 820);
            this.HongZiPage.TabIndex = 3;
            this.HongZiPage.Text = "红字";
            this.HongZiPage.UseVisualStyleBackColor = true;
            // 
            // HZSells
            // 
            this.HZSells.Location = new System.Drawing.Point(105, 276);
            this.HZSells.Name = "HZSells";
            this.HZSells.Size = new System.Drawing.Size(156, 39);
            this.HZSells.TabIndex = 2;
            this.HZSells.Text = "销售方申请";
            this.HZSells.UseVisualStyleBackColor = true;
            this.HZSells.Click += new System.EventHandler(this.HZSells_Click);
            // 
            // HZByer_wdk
            // 
            this.HZByer_wdk.Location = new System.Drawing.Point(105, 158);
            this.HZByer_wdk.Name = "HZByer_wdk";
            this.HZByer_wdk.Size = new System.Drawing.Size(156, 41);
            this.HZByer_wdk.TabIndex = 1;
            this.HZByer_wdk.Text = "购方申请未抵扣";
            this.HZByer_wdk.UseVisualStyleBackColor = true;
            this.HZByer_wdk.Click += new System.EventHandler(this.HZByer_wdk_Click);
            // 
            // HZByer_ydk
            // 
            this.HZByer_ydk.Location = new System.Drawing.Point(105, 53);
            this.HZByer_ydk.Name = "HZByer_ydk";
            this.HZByer_ydk.Size = new System.Drawing.Size(156, 44);
            this.HZByer_ydk.TabIndex = 0;
            this.HZByer_ydk.Text = "购方申请已抵扣";
            this.HZByer_ydk.UseVisualStyleBackColor = true;
            this.HZByer_ydk.Click += new System.EventHandler(this.HZByer_ydk_Click);
            // 
            // DaYinPage
            // 
            this.DaYinPage.Controls.Add(this.DaYin_picBox);
            this.DaYinPage.Controls.Add(this.label8);
            this.DaYinPage.Controls.Add(this.DaYin_btnDoPrint);
            this.DaYinPage.Controls.Add(this.label3);
            this.DaYinPage.Controls.Add(this.发票号码);
            this.DaYinPage.Controls.Add(this.DaYin_tbFpdm);
            this.DaYinPage.Controls.Add(this.DaYin_tbFphm);
            this.DaYinPage.Controls.Add(this.DaYin_CmbPrinter);
            this.DaYinPage.Location = new System.Drawing.Point(4, 28);
            this.DaYinPage.Name = "DaYinPage";
            this.DaYinPage.Size = new System.Drawing.Size(1358, 820);
            this.DaYinPage.TabIndex = 4;
            this.DaYinPage.Text = "打印";
            this.DaYinPage.UseVisualStyleBackColor = true;
            // 
            // DaYin_picBox
            // 
            this.DaYin_picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DaYin_picBox.Location = new System.Drawing.Point(3, 98);
            this.DaYin_picBox.Name = "DaYin_picBox";
            this.DaYin_picBox.Size = new System.Drawing.Size(1355, 726);
            this.DaYin_picBox.TabIndex = 7;
            this.DaYin_picBox.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(9, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(476, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "请输入发票号码、代码，或在查询功能中选中行后点击打印";
            // 
            // DaYin_btnDoPrint
            // 
            this.DaYin_btnDoPrint.Location = new System.Drawing.Point(1020, 49);
            this.DaYin_btnDoPrint.Name = "DaYin_btnDoPrint";
            this.DaYin_btnDoPrint.Size = new System.Drawing.Size(75, 31);
            this.DaYin_btnDoPrint.TabIndex = 5;
            this.DaYin_btnDoPrint.Text = "打印";
            this.DaYin_btnDoPrint.UseVisualStyleBackColor = true;
            this.DaYin_btnDoPrint.Click += new System.EventHandler(this.DaYin_btnDoPrint_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "发票代码";
            // 
            // 发票号码
            // 
            this.发票号码.AutoSize = true;
            this.发票号码.Location = new System.Drawing.Point(16, 42);
            this.发票号码.Name = "发票号码";
            this.发票号码.Size = new System.Drawing.Size(80, 18);
            this.发票号码.TabIndex = 3;
            this.发票号码.Text = "发票号码";
            // 
            // DaYin_tbFpdm
            // 
            this.DaYin_tbFpdm.Location = new System.Drawing.Point(620, 49);
            this.DaYin_tbFpdm.Name = "DaYin_tbFpdm";
            this.DaYin_tbFpdm.Size = new System.Drawing.Size(331, 28);
            this.DaYin_tbFpdm.TabIndex = 2;
            this.DaYin_tbFpdm.Text = "3100153320";
            // 
            // DaYin_tbFphm
            // 
            this.DaYin_tbFphm.Location = new System.Drawing.Point(122, 39);
            this.DaYin_tbFphm.Name = "DaYin_tbFphm";
            this.DaYin_tbFphm.Size = new System.Drawing.Size(319, 28);
            this.DaYin_tbFphm.TabIndex = 1;
            this.DaYin_tbFphm.Text = "35203341";
            // 
            // DaYin_CmbPrinter
            // 
            this.DaYin_CmbPrinter.FormattingEnabled = true;
            this.DaYin_CmbPrinter.Location = new System.Drawing.Point(520, 11);
            this.DaYin_CmbPrinter.Name = "DaYin_CmbPrinter";
            this.DaYin_CmbPrinter.Size = new System.Drawing.Size(431, 26);
            this.DaYin_CmbPrinter.TabIndex = 0;
            // 
            // LodgingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 850);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LodgingWindow";
            this.Text = "LodgingWindow";
            this.Load += new System.EventHandler(this.LodgingWindow_Load);
            this.tabControl.ResumeLayout(false);
            this.ChaXunPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChaXun_DataGrid)).EndInit();
            this.groupChaXun.ResumeLayout(false);
            this.groupChaXun.PerformLayout();
            this.ChaXun_grpMatchStr.ResumeLayout(false);
            this.ChaXun_grpMatchStr.PerformLayout();
            this.ZuoFeiPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZuoFei_gridView)).EndInit();
            this.ZuoFei_paramGroup.ResumeLayout(false);
            this.ZuoFei_paramGroup.PerformLayout();
            this.ZuoFei_grpSearch.ResumeLayout(false);
            this.ZuoFei_grpSearch.PerformLayout();
            this.WeiKaiPage.ResumeLayout(false);
            this.WeiKaiPage.PerformLayout();
            this.HongZiPage.ResumeLayout(false);
            this.DaYinPage.ResumeLayout(false);
            this.DaYinPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DaYin_picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ChaXunPage;
        private System.Windows.Forms.TabPage TianKaiPage;
        private System.Windows.Forms.GroupBox groupChaXun;
        private System.Windows.Forms.ComboBox ChaXun_CmbYear;
        private System.Windows.Forms.TabPage ZuoFeiPage;
        private System.Windows.Forms.TabPage HongZiPage;
        private System.Windows.Forms.ComboBox ChaXun_CmbFpType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ChaXun_CmbMonth;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button ChaXun_BtnGo;
        private System.Windows.Forms.CheckBox ChaXun_checkWeiBaoSong;
        private System.Windows.Forms.CheckBox ChaXun_checkYanQianShiBai;
        private System.Windows.Forms.GroupBox ChaXun_grpMatchStr;
        private System.Windows.Forms.TextBox ChaXun_txtMathStr;
        private System.Windows.Forms.DataGridView ChaXun_DataGrid;
        private System.Windows.Forms.TabPage DaYinPage;
        private System.Windows.Forms.ComboBox DaYin_CmbPrinter;
        private System.Windows.Forms.TextBox DaYin_tbFpdm;
        private System.Windows.Forms.TextBox DaYin_tbFphm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label 发票号码;
        private System.Windows.Forms.Button DaYin_btnDoPrint;
        private System.Windows.Forms.Button HZByer_ydk;
        private System.Windows.Forms.Button HZByer_wdk;
        private System.Windows.Forms.Button HZSells;
        private System.Windows.Forms.GroupBox ZuoFei_paramGroup;
        private System.Windows.Forms.GroupBox ZuoFei_grpSearch;
        private System.Windows.Forms.TextBox ZuoFei_txtMatch;
        private System.Windows.Forms.Button ZuoFei_btnDoService;
        private System.Windows.Forms.Button ZuoFei_btnSearch;
        private System.Windows.Forms.CheckBox ZuoFei_checkYanQianShiBai;
        private System.Windows.Forms.DataGridView ZuoFei_gridView;
        private System.Windows.Forms.TabPage WeiKaiPage;
        private System.Windows.Forms.TextBox WeiKai_txt要作废发票份数;
        private System.Windows.Forms.TextBox WeiKai_txt发票起始号码;
        private System.Windows.Forms.TextBox WeiKai_txt发票份数;
        private System.Windows.Forms.TextBox WeiKai_txt发票代码;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox WeiKai_cmbFpType;
        private System.Windows.Forms.Button WeiKai_btn作废未开;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ChaXun_btnPrint;
        private System.Windows.Forms.PictureBox DaYin_picBox;
    }
}