namespace Aisino.Fwkp.Fplygl.Form.FPKCGL_3
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class FpKuChunChaXunForm : ParentForm
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<FpKuChunChaXunForm>();

        public FpKuChunChaXunForm()
        {
            try
            {
                this.Initialize();
                base.JH.Visible = false;
                base.strDaYinTitle = "金税设备库存发票查询";
                base.Name = "FpKuChunChaXunForm";
                base.Hide();
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
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "FpKuChunChaXunForm";
        }
    }
}

