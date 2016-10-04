namespace Aisino.Fwkp.Fpkj.Form.SendFP
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class EmailOutFilePrompt : BaseForm
    {
        private List<Dictionary<string, object>> _ListHeadMsg = new List<Dictionary<string, object>>();
        private DataGridViewTextBoxColumn BeiZhuJieZhi;
        private DataGridViewTextBoxColumn BeiZhuSend;
        private IContainer components;
        private DataGridViewTextBoxColumn ContentEmail;
        private CustomStyleDataGrid customStyleDataGrid1;
        private DataGridViewTextBoxColumn FileEmail;
        private DataGridViewTextBoxColumn FilePathJieZhi;
        private DataGridViewTextBoxColumn FromEmail;
        private int[] iColumnWithEmailSend = new int[] { 160, 160, 380, 0xff, 80, 160 };
        private int[] iColumnWithJieZhiOut = new int[] { 140, 380, 260, 80, 160 };
        private ILog loger = LogUtil.GetLogger<EmailOutFilePrompt>();
        private DataGridViewTextBoxColumn OutFileJieZhi;
        public static int OutFileType = 0;
        private DataGridViewTextBoxColumn ReceTaxJieZhi;
        private DataGridViewTextBoxColumn RefaultJieZhi;
        private DataGridViewTextBoxColumn RefaultSend;
        public static string strDyHouZhiJieSend = "发票填开打印后直接进行发送";
        public static string strFPTKSendEmailFangShi = "发票填开邮件发送方式--邮件或者介质";
        private string[] strHead = new string[] { "HeaderText", "Name", "Width" };
        private string[] strHeadTextEmailSend = new string[] { "发送方企业邮箱", "接收方企业邮箱", "邮件正文", "附件", "发送结果", "备注" };
        private string[] strHeadTextJieZhiOut = new string[] { "接收方企业税号", "文件路径", "文件名", "传出结果", "备注" };
        public static string strSendItemQueDing = "每次发送时都进行发送选项设置";
        public static string strServerDeleteEmail = "成功接收邮件后-从邮件服务器删除邮件";
        public static string[] strZhiDuanEmailSend = new string[] { "FromEmail", "ToEmail", "ContentEmail", "FileEmail", "RefaultSend", "BeiZhuSend" };
        public static string[] strZhiDuanJieZhiOut = new string[] { "ReceTaxJieZhi", "FilePathJieZhi", "OutFileJieZhi", "RefaultJieZhi", "BeiZhuJieZhi" };
        private DataGridViewTextBoxColumn ToEmail;
        private XmlComponentLoader xmlComponentLoader1;

        public EmailOutFilePrompt(List<Dictionary<string, object>> listEmailInfo)
        {
            try
            {
                this.Initialize();
                this.InitializeListHeadMsg();
                this.InsertGridColumn();
                this.InsertDataToList(listEmailInfo);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.customStyleDataGrid1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x362, 0x192);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.SendFP.EmailOutFilePrompt\Aisino.Fwkp.Fpkj.Form.SendFP.EmailOutFilePrompt.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x362, 0x192);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MinimizeBox = false;
            base.Name = "EmailOutFilePrompt";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "邮件导出文件提示";
            base.WindowState = FormWindowState.Maximized;
            base.ResumeLayout(false);
        }

        private void InitializeListHeadMsg()
        {
            try
            {
                string[] strHeadTextEmailSend = null;
                string[] strZhiDuanEmailSend = null;
                int[] iColumnWithEmailSend = null;
                this._ListHeadMsg.Clear();
                switch (OutFileType)
                {
                    case 0:
                        strHeadTextEmailSend = this.strHeadTextEmailSend;
                        strZhiDuanEmailSend = EmailOutFilePrompt.strZhiDuanEmailSend;
                        iColumnWithEmailSend = this.iColumnWithEmailSend;
                        break;

                    case 1:
                        strHeadTextEmailSend = this.strHeadTextJieZhiOut;
                        strZhiDuanEmailSend = strZhiDuanJieZhiOut;
                        iColumnWithEmailSend = this.iColumnWithJieZhiOut;
                        break;
                }
                int num = 0;
                int num2 = 0;
                Dictionary<string, object> item = null;
                foreach (int num3 in iColumnWithEmailSend)
                {
                    int num4 = 0;
                    item = new Dictionary<string, object>();
                    item.Add(this.strHead[num4++], strHeadTextEmailSend[num2++]);
                    item.Add(this.strHead[num4++], strZhiDuanEmailSend[num++]);
                    item.Add(this.strHead[num4++], num3);
                    this._ListHeadMsg.Add(item);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void InsertDataToList(List<Dictionary<string, object>> listEmailInfo)
        {
            try
            {
                if (listEmailInfo == null)
                {
                    MessageManager.ShowMsgBox("FPCX-000032");
                }
                else if (listEmailInfo.Count <= 0)
                {
                    MessageManager.ShowMsgBox("FPCX-000032");
                }
                else
                {
                    string[] strZhiDuanEmailSend = null;
                    this.customStyleDataGrid1.Rows.Clear();
                    this.customStyleDataGrid1.DataSource = null;
                    DataTable table = new DataTable();
                    switch (OutFileType)
                    {
                        case 0:
                            strZhiDuanEmailSend = EmailOutFilePrompt.strZhiDuanEmailSend;
                            break;

                        case 1:
                            strZhiDuanEmailSend = strZhiDuanJieZhiOut;
                            break;
                    }
                    for (int i = 0; i < strZhiDuanEmailSend.Length; i++)
                    {
                        table.Columns.Add(strZhiDuanEmailSend[i], typeof(string));
                    }
                    foreach (Dictionary<string, object> dictionary in listEmailInfo)
                    {
                        int num2 = 0;
                        DataRow row = table.NewRow();
                        for (int j = 0; j < strZhiDuanEmailSend.Length; j++)
                        {
                            string str = strZhiDuanEmailSend[num2++];
                            row[str] = (string) dictionary[str];
                        }
                        table.Rows.Add(row);
                    }
                    this.customStyleDataGrid1.DataSource = table;
                    this.customStyleDataGrid1.Refresh();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void InsertGridColumn()
        {
            try
            {
                this.customStyleDataGrid1.Columns.Clear();
                switch (OutFileType)
                {
                    case 0:
                    {
                        this.FromEmail = new DataGridViewTextBoxColumn();
                        this.ToEmail = new DataGridViewTextBoxColumn();
                        this.ContentEmail = new DataGridViewTextBoxColumn();
                        this.FileEmail = new DataGridViewTextBoxColumn();
                        this.RefaultSend = new DataGridViewTextBoxColumn();
                        this.BeiZhuSend = new DataGridViewTextBoxColumn();
                        int num = 0;
                        int index = 0;
                        this.FromEmail.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.FromEmail.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.FromEmail.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.FromEmail.ReadOnly = true;
                        this.FromEmail.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        index = 0;
                        this.ToEmail.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.ToEmail.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.ToEmail.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.ToEmail.ReadOnly = true;
                        this.ToEmail.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        index = 0;
                        this.ContentEmail.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.ContentEmail.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.ContentEmail.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.ContentEmail.ReadOnly = true;
                        this.ContentEmail.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        index = 0;
                        this.FileEmail.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.FileEmail.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.FileEmail.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.FileEmail.ReadOnly = true;
                        this.FileEmail.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        index = 0;
                        this.RefaultSend.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.RefaultSend.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.RefaultSend.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.RefaultSend.ReadOnly = true;
                        this.RefaultSend.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        index = 0;
                        this.BeiZhuSend.HeaderText = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.BeiZhuSend.Name = (string) this._ListHeadMsg[num][this.strHead[index]];
                        this.BeiZhuSend.DataPropertyName = (string) this._ListHeadMsg[num][this.strHead[index++]];
                        this.BeiZhuSend.ReadOnly = true;
                        this.BeiZhuSend.Width = (int) this._ListHeadMsg[num++][this.strHead[index++]];
                        int num3 = 0;
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.FromEmail);
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.ToEmail);
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.ContentEmail);
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.FileEmail);
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.RefaultSend);
                        this.customStyleDataGrid1.Columns.Insert(num3++, this.BeiZhuSend);
                        break;
                    }
                    case 1:
                    {
                        this.ReceTaxJieZhi = new DataGridViewTextBoxColumn();
                        this.FilePathJieZhi = new DataGridViewTextBoxColumn();
                        this.OutFileJieZhi = new DataGridViewTextBoxColumn();
                        this.RefaultJieZhi = new DataGridViewTextBoxColumn();
                        this.BeiZhuJieZhi = new DataGridViewTextBoxColumn();
                        int num4 = 0;
                        int num5 = 0;
                        this.ReceTaxJieZhi.HeaderText = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.ReceTaxJieZhi.Name = (string) this._ListHeadMsg[num4][this.strHead[num5]];
                        this.ReceTaxJieZhi.DataPropertyName = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.ReceTaxJieZhi.ReadOnly = true;
                        this.ReceTaxJieZhi.Width = (int) this._ListHeadMsg[num4++][this.strHead[num5++]];
                        num5 = 0;
                        this.FilePathJieZhi.HeaderText = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.FilePathJieZhi.Name = (string) this._ListHeadMsg[num4][this.strHead[num5]];
                        this.FilePathJieZhi.DataPropertyName = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.FilePathJieZhi.ReadOnly = true;
                        this.FilePathJieZhi.Width = (int) this._ListHeadMsg[num4++][this.strHead[num5++]];
                        num5 = 0;
                        this.OutFileJieZhi.HeaderText = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.OutFileJieZhi.Name = (string) this._ListHeadMsg[num4][this.strHead[num5]];
                        this.OutFileJieZhi.DataPropertyName = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.OutFileJieZhi.ReadOnly = true;
                        this.OutFileJieZhi.Width = (int) this._ListHeadMsg[num4++][this.strHead[num5++]];
                        num5 = 0;
                        this.RefaultJieZhi.HeaderText = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.RefaultJieZhi.Name = (string) this._ListHeadMsg[num4][this.strHead[num5]];
                        this.RefaultJieZhi.DataPropertyName = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.RefaultJieZhi.ReadOnly = true;
                        this.RefaultJieZhi.Width = (int) this._ListHeadMsg[num4++][this.strHead[num5++]];
                        num5 = 0;
                        this.BeiZhuJieZhi.HeaderText = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.BeiZhuJieZhi.Name = (string) this._ListHeadMsg[num4][this.strHead[num5]];
                        this.BeiZhuJieZhi.DataPropertyName = (string) this._ListHeadMsg[num4][this.strHead[num5++]];
                        this.BeiZhuJieZhi.ReadOnly = true;
                        this.BeiZhuJieZhi.Width = (int) this._ListHeadMsg[num4++][this.strHead[num5++]];
                        int num6 = 0;
                        this.customStyleDataGrid1.Columns.Insert(num6++, this.ReceTaxJieZhi);
                        this.customStyleDataGrid1.Columns.Insert(num6++, this.FilePathJieZhi);
                        this.customStyleDataGrid1.Columns.Insert(num6++, this.OutFileJieZhi);
                        this.customStyleDataGrid1.Columns.Insert(num6++, this.RefaultJieZhi);
                        this.customStyleDataGrid1.Columns.Insert(num6++, this.BeiZhuJieZhi);
                        break;
                    }
                }
                for (int i = 0; i < this.customStyleDataGrid1.Columns.Count; i++)
                {
                    this.customStyleDataGrid1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                this.customStyleDataGrid1.AllowUserToAddRows = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetFormTitle(object[] obj)
        {
            try
            {
                OutFileType = int.Parse(obj[0].ToString());
                this.Text = obj[2].ToString();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetFormTitle(string title)
        {
            try
            {
                this.Text = title;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }
    }
}

