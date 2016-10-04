namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class XfjfpfpForm : ParentForm
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<XfjfpfpForm>();

        public XfjfpfpForm()
        {
            try
            {
                this.Initialize();
                base.tool_XuanZe.Visible = true;
                base.tool_XuanZe.Click += new EventHandler(this.tool_XuanZe_Click);
                base.customStyleDataGrid1.DoubleClick += new EventHandler(this.tool_XuanZe_Click);
                base.strDaYinTitle = "金税设备库存发票分配";
                base.Name = "XfjfpfpForm";
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
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

        private bool FenPeiFaPiaoJuan()
        {
            try
            {
                DataGridViewRow currentRow = base.customStyleDataGrid1.CurrentRow;
                if ((currentRow == null) || (base.customStyleDataGrid1.RowCount <= 0))
                {
                    return false;
                }
                base.SelectedItems.Clear();
                base.SelectedItems.Add(currentRow.Cells["JH"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["FPZL"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["LBDM"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["MC"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["QSHM"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["SYZS"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["LGRQ"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["LGZS"].Value.ToString().Trim());
                base.SelectedItems.Add(currentRow.Cells["KPXE"].Value.ToString().Trim());
                if (base.SelectedItems.Count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-441202");
                    return false;
                }
                XfkpjfpfpForm form = new XfkpjfpfpForm(base.SelectedItems);
                if (DialogResult.OK == form.ShowDialog())
                {
                    this.QueryIndexChanged(null, null);
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "XfjfpfpForm";
        }

        private void tool_XuanZe_Click(object sender, EventArgs e)
        {
            try
            {
                this.FenPeiFaPiaoJuan();
            }
            catch (BaseException exception)
            {
                base._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                base._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

