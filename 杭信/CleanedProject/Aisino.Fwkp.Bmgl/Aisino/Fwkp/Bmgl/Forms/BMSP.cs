namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class BMSP : BMBase<BMSP_Edit, BMSPFenLei, BMSPSelect>
    {
        private IContainer components;
        private BLL.BMSPManager spManager = new BLL.BMSPManager();

        public BMSP()
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
            item.Add("Visible", "True");
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
            item.Add("AisinoLBL", "简码");
            item.Add("Property", "JM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "商品税目");
            item.Add("Property", "SPSM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLV");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税率");
            item.Add("Property", "SLVStr");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "规格型号");
            item.Add("Property", "GGXH");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "计量单位");
            item.Add("Property", "JLDW");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "快捷码");
            item.Add("Property", "KJM");
            item.Add("Type", "Text");
            item.Add("Visible", "False");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价");
            item.Add("Property", "DJ");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "单价Double");
            item.Add("Property", "DJStr");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "含税价标志");
            item.Add("Property", "HSJBZSTR");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "销售收入科目");
            item.Add("Property", "XSSRKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "应缴增值税科目");
            item.Add("Property", "YJZZSKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "销售退回科目");
            item.Add("Property", "XSTHKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "中外合作油气田");
            item.Add("Property", "HYSY");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Visible", "False");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "零税率标识");
            item.Add("Property", "LSLVBS");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Visible", "False");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "Hash值");
            item.Add("Property", "XTHASH");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "隐藏标志");
            item.Add("Property", "ISHIDE");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "隐藏标志");
            item.Add("Property", "ISHIDEBOOL");
            item.Add("Type", "ComboBox");
            DataTable table = new DataTable();
            table.Columns.Add("ISHIDE", Type.GetType("System.String"));
            DataRow row = table.NewRow();
            row["ISHIDE"] = "否";
            table.Rows.Add(row);
            DataRow row2 = table.NewRow();
            row2["ISHIDE"] = "是";
            table.Rows.Add(row2);
            base.aisinoDataGrid1.ComboBoxColumnDataSource.Add("IsHideSource", table);
            item.Add("DataSource", "IsHideSource");
            item.Add("DisplayMember", "ISHIDE");
            item.Add("ValueMember", "ISHIDE");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "稀土商品编码");
            item.Add("Property", "XTCODE");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税收分类编码");
            item.Add("Property", "SPFL");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "80");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税收分类名称");
            item.Add("Property", "SPFLMC");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "200");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "享受优惠政策");
            item.Add("Property", "YHZC");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "优惠政策类型");
            item.Add("Property", "YHZCMC");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("Width", "200");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", Flbm.IsYM().ToString());
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "SPZL是啥？");
            item.Add("Property", "SPZL");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "SPSX是啥？");
            item.Add("Property", "SPSX");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
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
            this.ImportXTSPAuto();
            this.InitIsHideCell();
            if (Flbm.IsYM())
            {
                new DAL.BMSPManager().Update_SP();
            }
            base.aisinoDataGrid1.DataGrid.AllowUserToAddRows = false;
            base.log = LogUtil.GetLogger<BMKH>();
            base.bllManager = this.spManager;
        }

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
            object obj2 = e.CurrentRow.Cells["XTHASH"].Value;
            object obj3 = e.CurrentRow.Cells["WJ"].Value;
            if (((obj2 != null) && (obj2.ToString().Trim() != "")) && (obj3.ToString() == "0"))
            {
                base.toolAdd.Enabled = false;
                base.toolDel.Enabled = false;
                base.toolModify.Enabled = false;
            }
            else if ((obj2 != null) && (obj2.ToString().Trim() != ""))
            {
                base.toolAdd.Enabled = false;
                base.toolDel.Enabled = false;
                base.toolModify.Enabled = true;
            }
            else
            {
                base.toolAdd.Enabled = true;
                base.toolDel.Enabled = true;
                base.toolModify.Enabled = true;
            }
        }

        protected override void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                object obj2 = e.CurrentRow.Cells["XTHASH"].Value;
                object obj3 = e.CurrentRow.Cells["WJ"].Value;
                if (((obj2 == null) || (obj2.ToString().Trim() == "")) || (obj3.ToString() != "0"))
                {
                    base.aisinoDataGrid1_DataGridRowDbClickEvent(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name;
                if (base.aisinoDataGrid1.SelectedRows.Count != 1)
                {
                    if (base.treeViewBM1.SelectedNode == null)
                    {
                        MessageManager.ShowMsgBox("INP-235104");
                        return;
                    }
                    name = base.treeViewBM1.SelectedNode.Name;
                }
                else if ("0" == base.aisinoDataGrid1.SelectedRows[0].Cells["WJ"].Value.ToString())
                {
                    name = base.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString();
                }
                else
                {
                    name = base.aisinoDataGrid1.SelectedRows[0].Cells["SJBM"].Value.ToString();
                }
                if (this.IsXTSP(name))
                {
                    MessageManager.ShowMsgBox("INP-235124");
                }
                else
                {
                    base.btnAdd_Click(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.aisinoDataGrid1.SelectedRows.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-235106");
                }
                else if (this.IsXTSP(base.aisinoDataGrid1.SelectedRows[0].Cells["BM"].Value.ToString()))
                {
                    MessageManager.ShowMsgBox("INP-235125");
                }
                else
                {
                    base.btnDel_Click(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void DataGridCellEndEdit(object sender, DataGridRowEventArgs e)
        {
            RefreshGridTreeInvoke invoke;
            if (base.aisinoDataGrid1.CurrentCell.OwningColumn.Name == "ISHIDEBOOL")
            {
                string sjbm = e.CurrentRow.Cells["BM"].Value.ToString();
                string str2 = e.CurrentRow.Cells["XTHASH"].Value.ToString();
                string bm = e.CurrentRow.Cells["SJBM"].Value.ToString();
                bool isHide = base.aisinoDataGrid1.CurrentCell.Value.ToString() == "是";
                if ((str2.Length == 0) && (bm.Length == 0))
                {
                    MessageBoxHelper.Show("不允许修改普通商品上级编码的隐藏标志", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (((str2.Length > 1) && (bm.Length != 0)) && !isHide)
                    {
                        this.spManager.UpdateSPIsHide(bm, isHide);
                    }
                    if (str2.Length == 1)
                    {
                        string hide = isHide ? "1000000000" : "0000000000";
                        if (DialogResult.Cancel == MessageBoxHelper.Show("您的稀土上级编码的隐藏标志发生变化，下级编码的隐藏标志也会随着变化，确认该变化吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {
                            goto Label_013A;
                        }
                        this.spManager.UpdateXTIsHide(sjbm, hide);
                    }
                    this.spManager.UpdateSPIsHide(sjbm, isHide);
                }
            }
        Label_013A:
            invoke = new RefreshGridTreeInvoke(this.RefreshGrid);
            base.BeginInvoke(invoke);
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
            if (path.ToLower().EndsWith(".dat") && this.spManager.IsXTCorp())
            {
                return this.spManager.ImportXTSP(path, true);
            }
            ProgressHinter instance = ProgressHinter.GetInstance();
            instance.SetMsg("正在导入" + base.treeViewBM1.RootNodeString + "...");
            instance.StartCycle();
            try
            {
                if (path.EndsWith(".xml"))
                {
                    return this.spManager.ImportDataZC(path);
                }
                if (path.EndsWith(".txt"))
                {
                    return this.spManager.ImportData(path);
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

        private void ImportXTSPAuto()
        {
            if (this.spManager.IsNeedImportXTSP(""))
            {
                try
                {
                    this.spManager.ImportXTSP("", false);
                }
                catch (Exception exception)
                {
                    base.log.Error(exception.ToString());
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void InitializeComponent()
        {
            base.aisinoDataGrid1.DataGridRowClickEvent += new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowClickEvent);
            base.Name = "BMSP";
            this.Text = "商品编码设置";
            base.treeViewBM1.RootNodeString = "商品编码";
            base.treeViewBM1.ChildText = "增加商品编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(商品编码,名称,简码或规格型号)";
        }

        private void InitIsHideCell()
        {
            base.aisinoDataGrid1.ReadOnly = false;
            foreach (DataGridViewColumn column in base.aisinoDataGrid1.Columns)
            {
                if (column.Name != "ISHIDEBOOL")
                {
                    column.ReadOnly = true;
                }
            }
            base.aisinoDataGrid1.DataGridCellEndEditEvent += new EventHandler<DataGridRowEventArgs>(this.DataGridCellEndEdit);
        }

        private bool IsXTSP(string bm)
        {
            if ((bm == null) || string.Empty.Equals(bm))
            {
                return false;
            }
            BMSPModel model = (BMSPModel) this.spManager.GetModel(bm);
            return ((model.XTHASH != null) && (model.XTHASH != ""));
        }

        protected override void treeViewBM1_onClickAdd(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (this.IsXTSP(e.BmString))
                {
                    MessageManager.ShowMsgBox("INP-235124");
                }
                else
                {
                    base.treeViewBM1_onClickAdd(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void treeViewBM1_onClickAddFenLei(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (this.IsXTSP(e.BmString))
                {
                    MessageBoxHelper.Show("不允许在稀土编码族下添加分类！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    base.treeViewBM1_onClickAddFenLei(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void treeViewBM1_onClickDelete(object sender, EventArgs e)
        {
            try
            {
                if (this.spManager.ContainXTSP(base.treeViewBM1.SelectedNode.Name))
                {
                    MessageBoxHelper.Show("不允许删除含有稀土商品的编码族！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    base.treeViewBM1_onClickDelete(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        protected override void treeViewBM1_onClickModify(object sender, TreeSelectEventArgs e)
        {
            try
            {
                if (this.IsXTSP(e.BmString))
                {
                    MessageBoxHelper.Show("不允许修改稀土族分类！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    base.treeViewBM1_onClickModify(sender, e);
                }
            }
            catch (Exception exception)
            {
                base.log.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        public delegate void RefreshGridTreeInvoke();
    }
}

