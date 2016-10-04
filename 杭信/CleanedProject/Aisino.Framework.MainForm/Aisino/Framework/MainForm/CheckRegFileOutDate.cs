namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class CheckRegFileOutDate : BaseForm
    {
        private bool bool_0;
        private bool bool_1;
        private CustomStyleDataGrid customStyleDataGridReserve;
        private DataTable dataTable_0;
        private IContainer icontainer_2;
        private ILog ilog_0;
        private int int_0;
        private List<Dictionary<string, string>> list_0;
        private List<string> list_1;
        private TaxCard taxCard_0;

        public CheckRegFileOutDate()
        {
            
            this.list_0 = new List<Dictionary<string, string>>();
            this.list_1 = new List<string>();
            this.taxCard_0 = TaxCardFactory.CreateTaxCard();
            this.ilog_0 = LogUtil.GetLogger<CheckRegFileOutDate>();
            this.bool_0 = true;
            this.bool_1 = true;
            this.int_0 = 30;
            this.InitializeComponent_1();
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTip", "true");
            string str2 = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.OutOfDateTip", "true");
            string str3 = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTipDays", "30");
            this.bool_0 = Convert.ToBoolean(str);
            this.bool_1 = Convert.ToBoolean(str2);
            this.int_0 = Convert.ToInt32(str3);
        }

        private void CheckRegFileOutDate_Load(object sender, EventArgs e)
        {
            this.method_2();
            base.TopMost = true;
            this.method_5();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_2 != null))
            {
                this.icontainer_2.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Init()
        {
            this.method_3();
            this.method_1();
        }

        private void InitializeComponent_1()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CheckRegFileOutDate));
            this.customStyleDataGridReserve = new CustomStyleDataGrid();
            ((ISupportInitialize) this.customStyleDataGridReserve).BeginInit();
            base.SuspendLayout();
            this.customStyleDataGridReserve.AborCellPainting = false;
            this.customStyleDataGridReserve.AllowColumnHeadersVisible = true;
            this.customStyleDataGridReserve.AllowUserToAddRows = false;
            this.customStyleDataGridReserve.AllowUserToDeleteRows = false;
            this.customStyleDataGridReserve.AllowUserToResizeColumns = false;
            this.customStyleDataGridReserve.AllowUserToResizeRows = false;
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.customStyleDataGridReserve.AlternatingRowsDefaultCellStyle = style;
            this.customStyleDataGridReserve.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.customStyleDataGridReserve.BackgroundColor = SystemColors.ButtonFace;
            this.customStyleDataGridReserve.BorderStyle = BorderStyle.None;
            this.customStyleDataGridReserve.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.customStyleDataGridReserve.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0, 0x90, 0xd9);
            style2.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            style2.ForeColor = Color.White;
            this.customStyleDataGridReserve.ColumnHeadersDefaultCellStyle = style2;
            this.customStyleDataGridReserve.ColumnHeadersHeight = 0;
            this.customStyleDataGridReserve.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(0xee, 250, 0xff);
            style3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style3.ForeColor = Color.FromArgb(0x4d, 0x4c, 0x4a);
            style3.SelectionBackColor = Color.FromArgb(0x73, 0x94, 0xb3);
            style3.SelectionForeColor = Color.White;
            style3.WrapMode = DataGridViewTriState.False;
            this.customStyleDataGridReserve.DefaultCellStyle = style3;
            this.customStyleDataGridReserve.Dock = DockStyle.Fill;
            this.customStyleDataGridReserve.EnableHeadersVisualStyles = false;
            this.customStyleDataGridReserve.GridColor = Color.Gray;
            this.customStyleDataGridReserve.GridStyle = CustomStyle.custom;
            this.customStyleDataGridReserve.Location = new Point(0, 0);
            this.customStyleDataGridReserve.Name = "customStyleDataGridReserve";
            this.customStyleDataGridReserve.ReadOnly = true;
            this.customStyleDataGridReserve.RowHeadersVisible = false;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.customStyleDataGridReserve.RowsDefaultCellStyle = style4;
            this.customStyleDataGridReserve.RowTemplate.Height = 0x17;
            this.customStyleDataGridReserve.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.customStyleDataGridReserve.Size = new Size(320, 0xaf);
            this.customStyleDataGridReserve.TabIndex = 9;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            base.ClientSize = new Size(320, 0xaf);
            base.Controls.Add(this.customStyleDataGridReserve);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "CheckRegFileOutDate";
            this.Text = "注册文件到(过)期提醒";
            base.Load += new EventHandler(this.CheckRegFileOutDate_Load);
            ((ISupportInitialize) this.customStyleDataGridReserve).EndInit();
            base.ResumeLayout(false);
        }

        private void method_1()
        {
            try
            {
                RegFileSetupResult result = RegisterManager.SetupRegFile(this.taxCard_0);
                if ((result != null) && ((result.NormalRegFiles.Count + result.OutOfDateRegFiles.Count) >= 1))
                {
                    DateTime cardClock = this.taxCard_0.GetCardClock();
                    DateTime time2 = new DateTime(cardClock.Year, cardClock.Month, cardClock.Day);
                    foreach (RegFileInfo info in result.NormalRegFiles)
                    {
                        string s = "";
                        foreach (char ch in info.FileContent.StopDate)
                        {
                            s = s + ch;
                        }
                        DateTime time3 = DateTime.ParseExact(s, "yyyyMMdd", null);
                        if (this.bool_1 && (time3 < time2))
                        {
                            this.list_1.Add(this.method_4(info.VerFlag));
                        }
                        else if (this.bool_0 && (time2.AddDays((double) this.int_0) >= time3))
                        {
                            Dictionary<string, string> item = new Dictionary<string, string>();
                            item.Add("MC", this.method_4(info.VerFlag));
                            TimeSpan span = (TimeSpan) (time3 - time2);
                            item.Add("DAYS", span.Days.ToString());
                            this.list_0.Add(item);
                        }
                    }
                    foreach (RegFileInfo info2 in result.OutOfDateRegFiles)
                    {
                        string str2 = "";
                        foreach (char ch2 in info2.FileContent.StopDate)
                        {
                            str2 = str2 + ch2;
                        }
                        DateTime time5 = DateTime.ParseExact(str2, "yyyyMMdd", null);
                        if (this.bool_1 && (time5 < time2))
                        {
                            this.list_1.Add(this.method_4(info2.VerFlag));
                        }
                        else if (this.bool_0 && (DateTime.Now.AddDays((double) this.int_0) >= time5))
                        {
                            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                            dictionary2.Add("MC", this.method_4(info2.VerFlag));
                            TimeSpan span2 = (TimeSpan) (time5 - time2);
                            dictionary2.Add("DAYS", span2.Days.ToString());
                            this.list_0.Add(dictionary2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("classifyRegFile异常：" + exception.ToString());
            }
        }

        private void method_2()
        {
            try
            {
                DataTable table = new DataTable();
                if ((this.list_0.Count + this.list_1.Count) > 0)
                {
                    table.Columns.Add("名称");
                    table.Columns.Add("状态");
                    table.AcceptChanges();
                }
                if (this.bool_0 && (this.list_0.Count > 0))
                {
                    foreach (Dictionary<string, string> dictionary in this.list_0)
                    {
                        DataRow row = table.NewRow();
                        row["名称"] = dictionary["MC"];
                        if (dictionary["DAYS"].ToString() == "0")
                        {
                            row["状态"] = "今天到期";
                        }
                        else
                        {
                            row["状态"] = "距离到期还有" + dictionary["DAYS"].ToString() + "天";
                        }
                        table.Rows.Add(row);
                        table.AcceptChanges();
                    }
                }
                if (this.bool_1 && (this.list_1.Count > 0))
                {
                    foreach (string str in this.list_1)
                    {
                        DataRow row2 = table.NewRow();
                        row2["名称"] = str;
                        row2["状态"] = "已过期";
                        table.Rows.Add(row2);
                        table.AcceptChanges();
                    }
                }
                table.AcceptChanges();
                this.customStyleDataGridReserve.DataSource = table;
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("SetContent异常：" + exception.ToString());
            }
        }

        private void method_3()
        {
            try
            {
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                string str = "SELECT * FROM XTBBXX";
                this.dataTable_0 = baseDAOSQLite.querySQLDataTable(str);
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("LoadReginfoFromDB异常：" + exception.ToString());
            }
        }

        private string method_4(string string_0)
        {
            string str = "";
            try
            {
                DataRow[] rowArray = this.dataTable_0.Select("BBBS='" + string_0 + "'");
                if ((rowArray != null) && (rowArray.Length > 0))
                {
                    str = rowArray[0]["MC"].ToString();
                }
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("GetMC 异常：" + exception.ToString());
            }
            return str;
        }

        private void method_5()
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Size.Width - base.Width) - 10;
            int y = (Screen.PrimaryScreen.WorkingArea.Size.Height - base.Height) - 10;
            base.SetDesktopLocation(x, y);
        }

        public int iTotalRegFiles
        {
            get
            {
                return (this.list_0.Count + this.list_1.Count);
            }
        }
    }
}

