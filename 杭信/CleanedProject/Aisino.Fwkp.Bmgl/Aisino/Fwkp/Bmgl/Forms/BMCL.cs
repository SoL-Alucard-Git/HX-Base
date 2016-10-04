namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BMCL : BMBase<BMCL_Edit, BMCLFenLei, BMCLSelect>
    {
        private BMCLManager clManager = new BMCLManager();
        private IContainer components;

        public BMCL()
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
            item.Add("AisinoLBL", "车辆类型");
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
            item.Add("AisinoLBL", "上级编码");
            item.Add("Property", "SJBM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "厂牌型号");
            item.Add("Property", "CPXH");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "产地");
            item.Add("Property", "CD");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "生产企业名称");
            item.Add("Property", "SCCJMC");
            item.Add("Type", "Text");
            item.Add("Width", "130");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
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
                new BMSPFLManager().ChooseYHZCMCForCL();
            }
            base.log = LogUtil.GetLogger<BMCL>();
            base.bllManager = this.clManager;
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
                    return this.clManager.ImportDataZC(path);
                }
                if (path.EndsWith(".txt"))
                {
                    return this.clManager.ImportData(path);
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
            base.Name = "BMCL";
            this.Text = "车辆编码设置";
            base.treeViewBM1.RootNodeString = "车辆编码";
            base.treeViewBM1.ChildText = "增加车辆编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(车辆编码,名称,厂牌型号,产地,生产厂家名称)";
        }
    }
}

