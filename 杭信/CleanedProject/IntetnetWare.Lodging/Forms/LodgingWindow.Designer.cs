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
            this.HongZiPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.ChaXunPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChaXun_DataGrid)).BeginInit();
            this.groupChaXun.SuspendLayout();
            this.ChaXun_grpMatchStr.SuspendLayout();
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
            this.tabControl.Controls.Add(this.HongZiPage);
            this.tabControl.Location = new System.Drawing.Point(-1, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1214, 710);
            this.tabControl.TabIndex = 0;
            // 
            // ChaXunPage
            // 
            this.ChaXunPage.Controls.Add(this.ChaXun_DataGrid);
            this.ChaXunPage.Controls.Add(this.groupChaXun);
            this.ChaXunPage.Location = new System.Drawing.Point(4, 25);
            this.ChaXunPage.Name = "ChaXunPage";
            this.ChaXunPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChaXunPage.Size = new System.Drawing.Size(1206, 681);
            this.ChaXunPage.TabIndex = 0;
            this.ChaXunPage.Text = "查询";
            this.ChaXunPage.UseVisualStyleBackColor = true;
            // 
            // ChaXun_DataGrid
            // 
            this.ChaXun_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChaXun_DataGrid.Location = new System.Drawing.Point(9, 146);
            this.ChaXun_DataGrid.Name = "ChaXun_DataGrid";
            this.ChaXun_DataGrid.RowTemplate.Height = 27;
            this.ChaXun_DataGrid.Size = new System.Drawing.Size(1194, 535);
            this.ChaXun_DataGrid.TabIndex = 1;
            // 
            // groupChaXun
            // 
            this.groupChaXun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupChaXun.Size = new System.Drawing.Size(1200, 143);
            this.groupChaXun.TabIndex = 0;
            this.groupChaXun.TabStop = false;
            this.groupChaXun.Text = "查询参数";
            // 
            // ChaXun_grpMatchStr
            // 
            this.ChaXun_grpMatchStr.Controls.Add(this.ChaXun_txtMathStr);
            this.ChaXun_grpMatchStr.Location = new System.Drawing.Point(712, 38);
            this.ChaXun_grpMatchStr.Name = "ChaXun_grpMatchStr";
            this.ChaXun_grpMatchStr.Size = new System.Drawing.Size(200, 51);
            this.ChaXun_grpMatchStr.TabIndex = 8;
            this.ChaXun_grpMatchStr.TabStop = false;
            this.ChaXun_grpMatchStr.Text = "请输入检索关键字...";
            // 
            // ChaXun_txtMathStr
            // 
            this.ChaXun_txtMathStr.Location = new System.Drawing.Point(6, 20);
            this.ChaXun_txtMathStr.Name = "ChaXun_txtMathStr";
            this.ChaXun_txtMathStr.Size = new System.Drawing.Size(188, 25);
            this.ChaXun_txtMathStr.TabIndex = 0;
            // 
            // ChaXun_checkYanQianShiBai
            // 
            this.ChaXun_checkYanQianShiBai.AutoSize = true;
            this.ChaXun_checkYanQianShiBai.Location = new System.Drawing.Point(605, 41);
            this.ChaXun_checkYanQianShiBai.Name = "ChaXun_checkYanQianShiBai";
            this.ChaXun_checkYanQianShiBai.Size = new System.Drawing.Size(89, 19);
            this.ChaXun_checkYanQianShiBai.TabIndex = 7;
            this.ChaXun_checkYanQianShiBai.Text = "验签失败";
            this.ChaXun_checkYanQianShiBai.UseVisualStyleBackColor = true;
            // 
            // ChaXun_checkWeiBaoSong
            // 
            this.ChaXun_checkWeiBaoSong.AutoSize = true;
            this.ChaXun_checkWeiBaoSong.Location = new System.Drawing.Point(508, 41);
            this.ChaXun_checkWeiBaoSong.Name = "ChaXun_checkWeiBaoSong";
            this.ChaXun_checkWeiBaoSong.Size = new System.Drawing.Size(74, 19);
            this.ChaXun_checkWeiBaoSong.TabIndex = 6;
            this.ChaXun_checkWeiBaoSong.Text = "未报送";
            this.ChaXun_checkWeiBaoSong.UseVisualStyleBackColor = true;
            // 
            // ChaXun_BtnGo
            // 
            this.ChaXun_BtnGo.Location = new System.Drawing.Point(978, 46);
            this.ChaXun_BtnGo.Name = "ChaXun_BtnGo";
            this.ChaXun_BtnGo.Size = new System.Drawing.Size(179, 51);
            this.ChaXun_BtnGo.TabIndex = 5;
            this.ChaXun_BtnGo.Text = "查询";
            this.ChaXun_BtnGo.UseVisualStyleBackColor = true;
            this.ChaXun_BtnGo.Click += new System.EventHandler(this.ChaXun_BtnGo_Click);
            // 
            // ChaXun_CmbFpType
            // 
            this.ChaXun_CmbFpType.FormattingEnabled = true;
            this.ChaXun_CmbFpType.Location = new System.Drawing.Point(337, 38);
            this.ChaXun_CmbFpType.Name = "ChaXun_CmbFpType";
            this.ChaXun_CmbFpType.Size = new System.Drawing.Size(143, 23);
            this.ChaXun_CmbFpType.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "月";
            // 
            // ChaXun_CmbMonth
            // 
            this.ChaXun_CmbMonth.FormattingEnabled = true;
            this.ChaXun_CmbMonth.Location = new System.Drawing.Point(188, 38);
            this.ChaXun_CmbMonth.Name = "ChaXun_CmbMonth";
            this.ChaXun_CmbMonth.Size = new System.Drawing.Size(91, 23);
            this.ChaXun_CmbMonth.TabIndex = 2;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(148, 46);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(22, 15);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "年";
            // 
            // ChaXun_CmbYear
            // 
            this.ChaXun_CmbYear.FormattingEnabled = true;
            this.ChaXun_CmbYear.Location = new System.Drawing.Point(6, 39);
            this.ChaXun_CmbYear.Name = "ChaXun_CmbYear";
            this.ChaXun_CmbYear.Size = new System.Drawing.Size(135, 23);
            this.ChaXun_CmbYear.TabIndex = 0;
            // 
            // TianKaiPage
            // 
            this.TianKaiPage.Location = new System.Drawing.Point(4, 25);
            this.TianKaiPage.Name = "TianKaiPage";
            this.TianKaiPage.Padding = new System.Windows.Forms.Padding(3);
            this.TianKaiPage.Size = new System.Drawing.Size(1206, 681);
            this.TianKaiPage.TabIndex = 1;
            this.TianKaiPage.Text = "填开";
            this.TianKaiPage.UseVisualStyleBackColor = true;
            // 
            // ZuoFeiPage
            // 
            this.ZuoFeiPage.Location = new System.Drawing.Point(4, 25);
            this.ZuoFeiPage.Name = "ZuoFeiPage";
            this.ZuoFeiPage.Size = new System.Drawing.Size(1206, 681);
            this.ZuoFeiPage.TabIndex = 2;
            this.ZuoFeiPage.Text = "作废";
            this.ZuoFeiPage.UseVisualStyleBackColor = true;
            // 
            // HongZiPage
            // 
            this.HongZiPage.Location = new System.Drawing.Point(4, 25);
            this.HongZiPage.Name = "HongZiPage";
            this.HongZiPage.Size = new System.Drawing.Size(1206, 681);
            this.HongZiPage.TabIndex = 3;
            this.HongZiPage.Text = "红字";
            this.HongZiPage.UseVisualStyleBackColor = true;
            // 
            // LodgingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 708);
            this.Controls.Add(this.tabControl);
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
    }
}