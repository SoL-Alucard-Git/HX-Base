namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class BMSPFL : BMBase<object, object, object>
    {
        private IContainer components;
        private AisinoDataSet dataSet = new AisinoDataSet();
        private BMSPFLManager spflManager = new BMSPFLManager();
        protected ToolStripButton toolHide;
        private ToolStripSeparator toolStripSeparatorUpdate;
        protected ToolStripButton toolUpdate;

        public BMSPFL()
        {
            this.InitializeComponent();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码");
            item.Add("Property", "BM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "合并编码");
            item.Add("Property", "HBBM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "名称");
            item.Add("Property", "MC");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "说明");
            item.Add("Property", "SM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLVStr");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税特殊管理");
            item.Add("Property", "ZZSTSGL");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税政策依据");
            item.Add("Property", "ZZSZCYJ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "增值税特殊内容代码");
            item.Add("Property", "ZZSTSNRDM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税管理");
            item.Add("Property", "XFSGL");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税政策依据");
            item.Add("Property", "XFSZCYJ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "消费税特殊内容代码");
            item.Add("Property", "XFSTSNRDM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "关键字");
            item.Add("Property", "GJZ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "统计局编码");
            item.Add("Property", "TJJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "汇总项");
            item.Add("Property", "HZX");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "汇总项");
            item.Add("Property", "HZXStr");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "是否隐藏");
            item.Add("Property", "ISHIDE");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Visible", "False");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "是否隐藏");
            item.Add("Property", "ISHIDEStr");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "海关进出口商品品目");
            item.Add("Property", "HGJCKSPPM");
            item.Add("Type", "Text");
            item.Add("Width", "200");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码表版本号");
            item.Add("Property", "BMB_BBH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "版本号");
            item.Add("Property", "BBH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "可用状态");
            item.Add("Property", "KYZT");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "启用时间");
            item.Add("Property", "QYSJ");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "过渡期截止时间");
            item.Add("Property", "GDQJZSJ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "更新时间");
            item.Add("Property", "GXSJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "文件性质");
            item.Add("Property", "WJ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "WJ");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            base.aisinoDataGrid1.ColumeHead = list;
            base.aisinoDataGrid1.DataGrid.AllowUserToAddRows = false;
            base.log = LogUtil.GetLogger<BMSPFL>();
            base.bllManager = this.spflManager;
            if (TaxCardFactory.CreateTaxCard().QYLX.ISTDQY)
            {
                this.toolUpdate.Enabled = false;
            }
        }

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (e.CurrentRow.Cells["ISHIDEStr"].Value.ToString().ToString() == "是")
                {
                    this.toolHide.Text = "取消隐藏";
                }
                else
                {
                    this.toolHide.Text = "隐藏";
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
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

        protected override ImportResult ImportMethod(string path)
        {
            ImportResult result;
            ProgressHinter instance = ProgressHinter.GetInstance();
            instance.SetMsg("正在导入" + base.treeViewBM1.RootNodeString + "...");
            instance.StartCycle();
            try
            {
                if (path.EndsWith(".xml"))
                {
                    return this.spflManager.ImportDataSPFL(path);
                }
                result = new ImportResult();
            }
            catch
            {
                throw;
            }
            finally
            {
                instance.CloseCycle();
            }
            return result;
        }

        private void InitializeComponent()
        {
            base.Name = "BMSPFL";
            this.Text = "税收分类编码设置";
            base.treeViewBM1.RootNodeString = "税收分类编码";
            base.treeViewBM1.ChildText = "增加税收分类编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(税收分类编码,名称,说明,税率,增值税特殊管理,关键字)";
            base.toolAdd.Enabled = false;
            base.toolModify.Enabled = false;
            base.toolDel.Enabled = false;
            base.toolExport.Enabled = false;
            base.treeViewBM1.contextMenuStrip1.Enabled = false;
            base.treeViewBM1.AddFLToolStripMenuItem.Visible = false;
            base.treeViewBM1.ModifyFLToolStripMenuItem.Visible = false;
            base.treeViewBM1.UToolStripMenuItem.Visible = false;
            base.treeViewBM1.VToolStripMenuItem.Visible = false;
            base.treeViewBM1.WToolStripMenuItem.Visible = false;
            base.treeViewBM1.toolStripSeparator1.Visible = false;
            base.treeViewBM1.toolStripSeparator3.Visible = false;
            this.toolUpdate = new ToolStripButton();
            this.toolStripSeparatorUpdate = new ToolStripSeparator();
            this.toolUpdate.Image = Resources.导入;
            this.toolUpdate.ImageTransparentColor = Color.Magenta;
            this.toolUpdate.Name = "toolImport";
            this.toolUpdate.Size = new Size(0x34, 0x16);
            this.toolUpdate.Text = "更新";
            base.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripSeparatorUpdate, this.toolUpdate });
            this.toolUpdate.Click += new EventHandler(this.toolUpdate_Click);
            ControlStyleUtil.SetToolStripStyle(base.toolStrip1);
            this.toolHide = new ToolStripButton();
            this.toolStripSeparatorUpdate = new ToolStripSeparator();
            this.toolHide.Image = Resources.修改;
            this.toolHide.ImageTransparentColor = Color.Magenta;
            this.toolHide.Name = "toolImport";
            this.toolHide.Size = new Size(0x34, 0x16);
            this.toolHide.Text = "隐藏";
            base.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripSeparatorUpdate, this.toolHide });
            this.toolHide.Click += new EventHandler(this.toolHide_Click);
            ControlStyleUtil.SetToolStripStyle(base.toolStrip1);
            base.aisinoDataGrid1.DataGridRowClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowClickEvent);
        }

        private void toolHide_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshGridTreeInvoke invoke;
                bool isHide = this.toolHide.Text == "隐藏";
                string bm = base.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                string sjbm = base.aisinoDataGrid1.SelectedRows[0].Cells["SJBM"].Value.ToString();
                string str3 = base.aisinoDataGrid1.SelectedRows[0].Cells["HZXStr"].Value.ToString();
                if (str3 == "是")
                {
                    if (!isHide)
                    {
                        this.spflManager.UpdateSPFLIsHide(bm, isHide);
                        this.spflManager.UpdateHZXIsHideDownToUp(sjbm, isHide);
                        if (DialogResult.Cancel == MessageBoxHelper.Show("您的上级编码的是否隐藏设置为否，下级编码的是否隐藏也会设置为否，确认该设置吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {
                            goto Label_014C;
                        }
                        this.spflManager.UpdateHZXIsHideUpToDown(bm, isHide);
                    }
                    else
                    {
                        this.spflManager.UpdateSPFLIsHide(bm, isHide);
                        this.spflManager.UpdateHZXIsHideUpToDown(bm, isHide);
                    }
                }
                if (!isHide && (str3 == "否"))
                {
                    this.spflManager.UpdateHZXIsHideDownToUp(sjbm, isHide);
                    this.spflManager.UpdateSPFLIsHide(bm, isHide);
                }
                if (isHide && (str3 == "否"))
                {
                    this.spflManager.UpdateSPFLIsHide(bm, isHide);
                }
            Label_014C:
                invoke = new RefreshGridTreeInvoke(this.RefreshGrid);
                base.BeginInvoke(invoke);
            }
            catch (Exception exception)
            {
                base.log.Error("错误使用分类编码表隐藏按钮" + exception.ToString());
            }
        }

        private void toolUpdate_Click(object sender, EventArgs e)
        {
            new SPFLService().UpdateSPFL();
            this.dataSet = this.spflManager.QueryData(this.spflManager.Pagesize, this.spflManager.CurrentPage);
            base.aisinoDataGrid1.DataSource = this.dataSet;
            base.treeViewBM1.TreeLoad();
            base.treeViewBM1.SelectNodeByText(base.treeViewBM1.RootNodeString);
        }

        public delegate void RefreshGridTreeInvoke();
    }
}

