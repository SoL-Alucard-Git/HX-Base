namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BMFYXM : BMBase<BMFYXM_Edit, BMFYXMFenLei, BMFYXMSelect>
    {
        private IContainer components;
        private BMFYXMManager fyxmManager = new BMFYXMManager();

        public BMFYXM()
        {
            this.InitializeComponent();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "编码");
            item.Add("Property", "BM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
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
            item.Add("HeadAlign", "MiddleLeft");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "简码");
            item.Add("Property", "JM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleRight");
            item.Add("HeadAlign", "MiddleRight");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税收分类编码");
            item.Add("Property", "SPFL");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
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
            item.Add("AisinoLBL", "快捷码");
            item.Add("Property", "KJM");
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
            item.Add("Visible", "False");
            list.Add(item);
            base.aisinoDataGrid1.ColumeHead = list;
            if (Flbm.IsYM())
            {
                new BMSPFLManager().ChooseYHZCMCForFYXM();
            }
            base.log = LogUtil.GetLogger<BMFYXM>();
            base.bllManager = this.fyxmManager;
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
            base.Name = "BMFYXM";
            this.Text = "费用项目编码设置";
            base.treeViewBM1.RootNodeString = "费用项目编码";
            base.treeViewBM1.ChildText = "增加费用项目编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(编码,名称或简码)";
            base.toolImport.Enabled = false;
        }
    }
}

