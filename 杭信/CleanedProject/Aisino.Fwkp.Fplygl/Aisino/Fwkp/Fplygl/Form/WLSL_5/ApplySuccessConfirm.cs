namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ApplySuccessConfirm : ApplySuccessParent
    {
        private IContainer components;
        private ILog loger = LogUtil.GetLogger<ApplySuccessConfirm>();

        public ApplySuccessConfirm(out bool dataExist)
        {
            this.Initialize();
            this.ShowListWithFilteredQuery();
            if (base.volumeList.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-4412AD");
                dataExist = false;
            }
            else
            {
                base.BindData(base.volumeList);
                dataExist = true;
            }
        }

        private void csdgStatusVolumn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow row = base.csdgStatusVolumn.SelectedRows[0];
            DialogResult result = new ApplyConfirm(row.Cells["SLXH"].Value.ToString(), ApplyCommon.Invtype2CodeMix(row.Cells["FPZL"].Value.ToString())).ShowDialog();
            if (DialogResult.Yes == result)
            {
                MessageManager.ShowMsgBox("INP-4412AI");
                base.volumeList.RemoveAt(base.csdgStatusVolumn.SelectedRows[0].Index);
                base.BindData(base.volumeList);
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
            this.Text = "申领确认";
            base.tool_Confirm.Visible = true;
            base.csdgStatusVolumn.MouseDoubleClick += new MouseEventHandler(this.csdgStatusVolumn_MouseDoubleClick);
        }

        protected override void ShowListWithFilteredQuery()
        {
            QueryCondition qCondition = new QueryCondition {
                startTime = string.Empty,
                endTime = string.Empty,
                invType = string.Empty,
                status = "1"
            };
            List<OneTypeVolumes> queryList = new List<OneTypeVolumes>();
            QueryConfirmCommon.QueryController(qCondition, queryList, true);
            foreach (OneTypeVolumes volumes in queryList)
            {
                if (!ApplyCommon.Invtype2CodeMix(volumes.invType).Equals("026"))
                {
                    base.volumeList.Add(volumes);
                }
            }
        }

        protected override void tool_Confirm_Click(object sender, EventArgs e)
        {
            if (base.csdgStatusVolumn.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-441209", new string[] { "待确认" });
            }
            else
            {
                this.csdgStatusVolumn_MouseDoubleClick(null, null);
            }
        }
    }
}

