namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BMSFHR : BMBase<BMSFHR_Edit, BMSFHRFenLei, BMSFHRSelect>
    {
        private IContainer components;
        private BMSFHRManager sfhrManager = new BMSFHRManager();

        public BMSFHR()
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
            item.Add("AisinoLBL", "税号");
            item.Add("Property", "SH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地址电话");
            item.Add("Property", "DZDH");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "银行账号");
            item.Add("Property", "YHZH");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "邮政编码");
            item.Add("Property", "YZBM");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "文件性质");
            item.Add("Property", "WJ");
            item.Add("Type", "Text");
            item.Add("RowStyleField", "WJ");
            item.Add("Visible", "False");
            list.Add(item);
            base.aisinoDataGrid1.ColumeHead = list;
            base.log = LogUtil.GetLogger<BMSFHR>();
            base.bllManager = this.sfhrManager;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.Name = "BMSFHR";
            this.Text = "收/发货人编码设置";
            base.treeViewBM1.RootNodeString = "收/发货人编码";
            base.treeViewBM1.ChildText = "增加收/发货人编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(编码,名称,税号,地址电话,银行账号)";
        }
    }
}

