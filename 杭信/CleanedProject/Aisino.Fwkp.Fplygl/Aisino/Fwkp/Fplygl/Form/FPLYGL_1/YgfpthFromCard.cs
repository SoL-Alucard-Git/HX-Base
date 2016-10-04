namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class YgfpthFromCard : ParentForm
    {
        private IContainer components;
        private readonly ILYGL_JPXX jpxxDal = BLLFactory.CreateInstant<ILYGL_JPXX>("LYGL_JPXX");
        private ILog loger = LogUtil.GetLogger<YgfpthFromCard>();

        public YgfpthFromCard()
        {
            try
            {
                this.Initialize();
                base.pnlAttention.Visible = false;
                base.lblAttentionHeader.Text = "关于“已购发票退回”：";
                base.lblAttentionDetail.Text = "此操作只有因税务机关原因，造成购买的发票代码或号码有误时方可使用，\n并且执行此操作前须征得主管税务机关同意。否则，造成后果由企业承担！";
                base.lblAttentionHeader.Visible = true;
                base.lblAttentionDetail.Visible = true;
                base.tool_FPTuiHui.Visible = true;
                base.tool_FPTuiHui.Click += new EventHandler(this.tool_FPTuiHui_Click);
                base.customStyleDataGrid1.DoubleClick += new EventHandler(this.tool_FPTuiHui_Click);
                base.strDaYinTitle = "金税设备库存发票退回";
                base.Name = "YgfpthFromCard";
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

        private bool DeterErrorCard()
        {
            try
            {
                int num = base.TaxCardInstance.get_RetCode();
                if (0 < num)
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    base._bError = true;
                    return false;
                }
                if (!this.YiMan_ChunChuCard(base.TaxCardInstance, new TaxStateInfo()))
                {
                    base._bError = true;
                    return false;
                }
                MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                base._bError = true;
                return false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
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

        private bool FPTuiHui()
        {
            try
            {
                if (base._bError)
                {
                    return false;
                }
                DataGridViewRow currentRow = base.customStyleDataGrid1.CurrentRow;
                if ((currentRow == null) || (base.customStyleDataGrid1.RowCount <= 0))
                {
                    return false;
                }
                base.SelectedItems.Clear();
                base.SelectedItems.Add(currentRow.Cells["JH"].Value.ToString().Trim());
                if (base.SelectedItems.Count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-441202");
                    return false;
                }
                if (DialogResult.Yes == MessageManager.ShowMsgBox("INP-441213"))
                {
                    InvInfoReturnMsg msg = new InvInfoReturnMsg();
                    msg.InsertInvVolume(currentRow);
                    InvVolumeApp returnVolume = new InvVolumeApp {
                        InvType = ShareMethods.GetTypeCode(currentRow.Cells["FPZL"].Value.ToString()),
                        TypeCode = currentRow.Cells["LBDM"].Value.ToString(),
                        HeadCode = Convert.ToUInt32(currentRow.Cells["QSHM"].Value.ToString()),
                        Number = Convert.ToUInt16(currentRow.Cells["SYZS"].Value.ToString())
                    };
                    if (DialogResult.Yes == msg.ShowDialog())
                    {
                        this.TuiHuiAction(returnVolume);
                    }
                }
            }
            catch (BaseException exception)
            {
                base._bError = true;
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                base._bError = true;
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
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
            this.Text = "YgfpthFromCard";
        }

        private void tool_FPTuiHui_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPTuiHui();
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

        private void TuiHuiAction(InvVolumeApp returnVolume)
        {
            try
            {
                if (!base._bError)
                {
                    string deviceType = string.Empty;
                    if (1 == base.TaxCardInstance.get_StateInfo().TBRegFlag)
                    {
                        ChooseReturnMedium medium = new ChooseReturnMedium();
                        if (DialogResult.OK != medium.ShowDialog())
                        {
                            return;
                        }
                        deviceType = medium.GetDeviceType();
                    }
                    else
                    {
                        deviceType = "1";
                    }
                    bool flag = base.TaxCardInstance.InvReturn(Convert.ToInt32(base.SelectedItems[0]), deviceType);
                    if (base.TaxCardInstance.get_RetCode() != 0)
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        base._bError = true;
                        base.Close();
                    }
                    else if (flag)
                    {
                        if (0x29 == returnVolume.InvType)
                        {
                            this.jpxxDal.DeleteSingleVolumn(returnVolume);
                        }
                        if (base.TaxCardInstance.get_Machine() == 0)
                        {
                            MessageManager.ShowMsgBox("TCD_9109_");
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("TCD_9110_");
                        }
                        this.QueryIndexChanged(null, null);
                    }
                    else if (!this.DeterErrorCard())
                    {
                    }
                }
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

        public bool YiMan_ChunChuCard(TaxCard taxcard, TaxStateInfo taxStateInfo)
        {
            try
            {
                if (0x238f == taxcard.get_RetCode())
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }
    }
}

