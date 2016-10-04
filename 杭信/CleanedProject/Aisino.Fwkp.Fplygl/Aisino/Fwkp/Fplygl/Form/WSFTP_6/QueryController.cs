namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using log4net;
    using System;
    using System.Windows.Forms;

    public class QueryController
    {
        private ILog loger = LogUtil.GetLogger<AllocateController>();

        public void RunCommand()
        {
            DialogResult result = new AllocateQueryCondition().ShowDialog();
        }
    }
}

