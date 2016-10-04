namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BMKH : BMBase<BMKH_Edit, BMKHFenLei, BMKHSelect>
    {
        private IContainer components;
        private BMKHManager khManager = new BMKHManager();

        public BMKH()
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
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "简码");
            item.Add("Property", "JM");
            item.Add("Type", "Text");
            item.Add("Width", "60");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "True");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税号");
            item.Add("Property", "SH");
            item.Add("Type", "Text");
            item.Add("Width", "150");
            item.Add("Align", "MiddleRight");
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
            item.Add("AisinoLBL", "邮件地址");
            item.Add("Property", "YJDZ");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "备注");
            item.Add("Property", "BZ");
            item.Add("Type", "Text");
            item.Add("Width", "100");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "应收科目");
            item.Add("Property", "YSKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区编码");
            item.Add("Property", "DQBM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区名称");
            item.Add("Property", "DQMC");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "地区科目");
            item.Add("Property", "DQKM");
            item.Add("Type", "Text");
            item.Add("Width", "80");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "身份证校验");
            item.Add("Property", "SFZJY");
            item.Add("Type", "Text");
            item.Add("Width", "80");
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
            base.log = LogUtil.GetLogger<BMKH>();
            base.bllManager = this.khManager;
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
                    return this.khManager.ImportDataZC(path);
                }
                if (path.EndsWith(".txt"))
                {
                    return this.khManager.ImportData(path);
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
            base.Name = "BMKH";
            this.Text = "客户编码设置";
            base.treeViewBM1.RootNodeString = "客户编码";
            base.treeViewBM1.ChildText = "增加客户编码";
            base.textBoxWaitKey.ToolTipText = "输入关键字(客户编码,名称,税号,地址电话或银行账号)";
        }
    }
}

