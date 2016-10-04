namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using System;

    public class InvSplitPara
    {
        public bool above7ForbiddenInv = false;
        public bool above7Generlist = false;
        public bool above7Split = false;
        public bool below7ForbiddenInv = false;
        public bool below7Generlist = false;
        public bool below7Split = false;
        private InvType invType = InvType.Special;
        public bool ShowSetForm = false;

        public void GetInvSplitPara(InvType invType)
        {
            this.invType = invType;
            if (invType == InvType.Special)
            {
                this.below7ForbiddenInv = Convert.ToBoolean(PropertyUtil.GetValue("below7ForbiddenInv", "false"));
                this.below7Split = Convert.ToBoolean(PropertyUtil.GetValue("below7Split", "true"));
                this.below7Generlist = Convert.ToBoolean(PropertyUtil.GetValue("below7Generlist", "false"));
                this.above7ForbiddenInv = Convert.ToBoolean(PropertyUtil.GetValue("above7ForbiddenInv", "false"));
                this.above7Split = Convert.ToBoolean(PropertyUtil.GetValue("above7Split", "true"));
                this.above7Generlist = Convert.ToBoolean(PropertyUtil.GetValue("above7Generlist", "false"));
                this.ShowSetForm = Convert.ToBoolean(PropertyUtil.GetValue("ShowSetForm", "true"));
            }
            else if (invType == InvType.Common)
            {
                this.below7ForbiddenInv = Convert.ToBoolean(PropertyUtil.GetValue("below7ForbiddenInvCommon", "false"));
                this.below7Split = Convert.ToBoolean(PropertyUtil.GetValue("below7SplitCommon", "true"));
                this.below7Generlist = Convert.ToBoolean(PropertyUtil.GetValue("below7GenerlistCommon", "false"));
                this.above7ForbiddenInv = Convert.ToBoolean(PropertyUtil.GetValue("above7ForbiddenInvCommon", "false"));
                this.above7Split = Convert.ToBoolean(PropertyUtil.GetValue("above7SplitCommon", "false"));
                this.above7Generlist = Convert.ToBoolean(PropertyUtil.GetValue("above7GenerlistCommon", "true"));
                this.ShowSetForm = Convert.ToBoolean(PropertyUtil.GetValue("ShowSetFormCommon", "true"));
            }
        }

        public void GetInvSplitPara(string invTypeStr)
        {
            InvType invType = CommonTool.GetInvType(invTypeStr);
            this.GetInvSplitPara(invType);
        }

        public void SetInvSplitPara()
        {
            if (this.invType == InvType.Special)
            {
                PropertyUtil.SetValue("below7ForbiddenInv", this.below7ForbiddenInv.ToString(), true);
                PropertyUtil.SetValue("below7Split", this.below7Split.ToString(), true);
                PropertyUtil.SetValue("below7Generlist", this.below7Generlist.ToString(), true);
                PropertyUtil.SetValue("above7ForbiddenInv", this.above7ForbiddenInv.ToString(), true);
                PropertyUtil.SetValue("above7Split", this.above7Split.ToString(), true);
                PropertyUtil.SetValue("above7Generlist", this.above7Generlist.ToString(), true);
                PropertyUtil.SetValue("ShowSetForm", this.ShowSetForm.ToString(), true);
            }
            else if (this.invType == InvType.Common)
            {
                PropertyUtil.SetValue("below7ForbiddenInvCommon", this.below7ForbiddenInv.ToString(), true);
                PropertyUtil.SetValue("below7SplitCommon", this.below7Split.ToString(), true);
                PropertyUtil.SetValue("below7GenerlistCommon", this.below7Generlist.ToString(), true);
                PropertyUtil.SetValue("above7ForbiddenInvCommon", this.above7ForbiddenInv.ToString(), true);
                PropertyUtil.SetValue("above7SplitCommon", this.above7Split.ToString(), true);
                PropertyUtil.SetValue("above7GenerlistCommon", this.above7Generlist.ToString(), true);
                PropertyUtil.SetValue("ShowSetFormCommon", this.ShowSetForm.ToString(), true);
            }
        }
    }
}

