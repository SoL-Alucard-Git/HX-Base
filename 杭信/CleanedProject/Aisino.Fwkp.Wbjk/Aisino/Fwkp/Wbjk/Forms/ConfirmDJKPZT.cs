namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Model;
    using Aisino.Fwkp.Wbjk.Properties;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfirmDJKPZT : BaseForm
    {
        private AisinoBTN btnBack;
        private AisinoBTN btnCancel;
        private AisinoBTN btnGenerate;
        private IContainer components = null;
        private GenerateInvoice generBL = GenerateInvoice.Instance;
        private ImageList imageList1 = new ImageList();
        private List<FPGenerateResult> listGeneratePreview;
        private CustomListView listView1;
        private ILog log = LogUtil.GetLogger<ConfirmDJKPZT>();
        private ToolStripButton toolBtnBack;
        private ToolStripButton toolBtnCancel;
        private ToolStripButton toolBtnGenerate;
        private XmlComponentLoader xmlComponentLoader1;

        public ConfirmDJKPZT(List<FPGenerateResult> listGeneratePreview)
        {
            this.Initialize();
            this.listGeneratePreview = listGeneratePreview;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.listGeneratePreview.Clear();
            base.DialogResult = DialogResult.Retry;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.listGeneratePreview.Clear();
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                UpLoadCheckState.SetWenBenState(true);
                GenerateFPResult result = new GenerateFPResult(this.generBL.GenerateInvlist(this.listGeneratePreview));
                UpLoadCheckState.SetWenBenState(false);
                if (result.ShowDialog() == DialogResult.OK)
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
            }
            finally
            {
                UpLoadCheckState.SetWenBenState(false);
            }
        }

        private void ConfirmDJKPZT_Load(object sender, EventArgs e)
        {
            try
            {
                int num4;
                this.SetImgList();
                if ((this.listGeneratePreview.Count > 0) && (this.listGeneratePreview[0].DJZL == "j"))
                {
                    this.listView1.Columns.Add("图标", 60);
                    this.listView1.Columns.Add("序号", 60);
                    this.listView1.Columns.Add("单据号", 150);
                    this.listView1.Columns.Add("可开票性", 100);
                    this.listView1.Columns.Add("开票种类", 80);
                    this.listView1.Columns.Add("受限原因", 150);
                    this.listView1.Columns.Add("增值税税率或征收率", 120, HorizontalAlignment.Right);
                    this.listView1.Columns.Add("价税合计", 120, HorizontalAlignment.Right);
                    this.listView1.Columns.Add("增值税税额", 120, HorizontalAlignment.Right);
                }
                else
                {
                    this.listView1.Columns.Add("图标", 60);
                    this.listView1.Columns.Add("序号", 60);
                    this.listView1.Columns.Add("单据号", 150);
                    this.listView1.Columns.Add("可开票性", 100);
                    this.listView1.Columns.Add("开票种类", 80);
                    this.listView1.Columns.Add("受限原因", 150);
                    this.listView1.Columns.Add("税率", 60, HorizontalAlignment.Right);
                    this.listView1.Columns.Add("开票金额", 120, HorizontalAlignment.Right);
                    this.listView1.Columns.Add("开票税额", 120, HorizontalAlignment.Right);
                }
                if ((this.listGeneratePreview.Count > 0) && ((this.listGeneratePreview[0].DJZL == "c") || (this.listGeneratePreview[0].DJZL == "s")))
                {
                    this.listView1.Columns.Add("明细税率", 120, HorizontalAlignment.Right);
                }
                this.listView1.SmallImageList = this.imageList1;
                ListViewItem item = null;
                int num = 1;
                foreach (FPGenerateResult result in this.listGeneratePreview)
                {
                    bool flag = false;
                    if ((result.DJZL == "c") || (result.DJZL == "s"))
                    {
                        int count = result.ListXSDJ_MX.Count;
                        double sLV = 0.0;
                        num4 = 0;
                        while (num4 < count)
                        {
                            if (num4 == 0)
                            {
                                sLV = result.ListXSDJ_MX[num4].SLV;
                            }
                            else if (!(sLV == result.ListXSDJ_MX[num4].SLV))
                            {
                                flag = true;
                                break;
                            }
                            num4++;
                        }
                    }
                    string kKPX = result.KKPX;
                    if (kKPX == null)
                    {
                        goto Label_03CD;
                    }
                    if (!(kKPX == "完整开票"))
                    {
                        if (kKPX == "部分开票")
                        {
                            goto Label_03A9;
                        }
                        if (kKPX == "不允许开票")
                        {
                            goto Label_03BB;
                        }
                        goto Label_03CD;
                    }
                    item = new ListViewItem(" ", "OK");
                    goto Label_03DF;
                Label_03A9:
                    item = new ListViewItem(" ", "Part");
                    goto Label_03DF;
                Label_03BB:
                    item = new ListViewItem(" ", "No");
                    goto Label_03DF;
                Label_03CD:
                    item = new ListViewItem(" ", "No");
                Label_03DF:
                    item.Tag = result;
                    item.SubItems.Add(num.ToString());
                    num++;
                    item.SubItems.Add(result.BH);
                    item.SubItems.Add(result.KKPX);
                    if ((result.KPZL == "清单开票") && (result.KPJE < 0.0))
                    {
                        item.SubItems.Add("清单汇总");
                    }
                    else
                    {
                        item.SubItems.Add(result.KPZL);
                    }
                    item.SubItems.Add(result.SXYY);
                    if (!flag)
                    {
                        if (result.SLV == 0.0)
                        {
                            item.SubItems.Add("0%");
                        }
                        else if (((result.DJZL == "s") && (result.SLV == 0.05)) && result.HYSY)
                        {
                            item.SubItems.Add("中外合作油气田");
                        }
                        else if (!((result.SLV != 0.015) || result.HYSY))
                        {
                            item.SubItems.Add(result.SLV.ToString("0.0%"));
                        }
                        else
                        {
                            item.SubItems.Add(result.SLV.ToString("0%"));
                        }
                    }
                    else
                    {
                        item.SubItems.Add("多税率");
                    }
                    if (result.DJZL == "j")
                    {
                        double round = SaleBillCtrl.GetRound((double) (result.KPJE / (1.0 + result.SLV)), 2);
                        double num6 = result.KPJE - round;
                        item.SubItems.Add(result.KPJE.ToString("0.00"));
                        item.SubItems.Add(num6.ToString("0.00"));
                    }
                    else
                    {
                        item.SubItems.Add(result.KPJE.ToString("0.00"));
                        item.SubItems.Add(result.KPSE.ToString("0.00"));
                    }
                    this.listView1.Items.Add(item);
                }
                bool flag2 = true;
                int num7 = 0;
                for (num4 = 0; num4 < this.listGeneratePreview.Count; num4++)
                {
                    if (this.listGeneratePreview[num4].KKPX == "不能开票")
                    {
                        num7++;
                    }
                }
                if (num7 == this.listGeneratePreview.Count)
                {
                    flag2 = false;
                }
                if (flag2)
                {
                    this.btnGenerate.Enabled = true;
                    this.toolBtnGenerate.Enabled = true;
                }
                else
                {
                    this.btnGenerate.Enabled = false;
                    this.toolBtnGenerate.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("超时"))
                {
                    this.log.Error(exception.ToString());
                }
                else
                {
                    HandleException.HandleError(exception);
                }
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
            this.toolBtnBack = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnBack");
            this.toolBtnGenerate = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnGenerate");
            this.toolBtnCancel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolBtnCancel");
            this.btnBack = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnBack");
            this.btnGenerate = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnGenerate");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.listView1 = this.xmlComponentLoader1.GetControlByName<CustomListView>("listView1");
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.toolBtnBack.Click += new EventHandler(this.btnBack_Click);
            this.toolBtnGenerate.Click += new EventHandler(this.btnGenerate_Click);
            this.toolBtnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.listView1.MouseClick += new MouseEventHandler(this.listView1_MouseClick);
            this.listView1.Font = new Font("宋体", 12f);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x37c, 0x236);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.ConfirmDJKPZT\Aisino.Fwkp.Wbjk.ConfirmDJKPZT.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x37c, 0x236);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "ConfirmDJKPZT";
            this.Text = "确认单据开票状态";
            base.Load += new EventHandler(this.ConfirmDJKPZT_Load);
            base.ResumeLayout(false);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem itemAt = this.listView1.GetItemAt(e.X, e.Y);
                if (itemAt.Name != "MX")
                {
                    FPGenerateResult tag = (FPGenerateResult) itemAt.Tag;
                    if (tag.DJZL != "j")
                    {
                        int num2;
                        int index = itemAt.Index + 1;
                        if ((index < this.listView1.Items.Count) && (this.listView1.Items[index].Name == "MX"))
                        {
                            for (num2 = 0; num2 < (tag.ListXSDJ_MX.Count + 1); num2++)
                            {
                                this.listView1.Items.RemoveAt(index);
                            }
                        }
                        else
                        {
                            ListViewItem item = new ListViewItem {
                                ForeColor = Color.Red,
                                Name = "MX",
                                Font = new Font("宋体", 10f)
                            };
                            item.SubItems.Add("序号");
                            item.SubItems.Add("货物名称");
                            item.SubItems.Add("规格型号");
                            item.SubItems.Add("单位");
                            item.SubItems.Add("数量");
                            item.SubItems.Add("单价");
                            item.SubItems.Add("金额");
                            item.SubItems.Add("税额");
                            item.SubItems.Add("税率");
                            this.listView1.Items.Insert(index, item);
                            for (num2 = 0; num2 < tag.ListXSDJ_MX.Count; num2++)
                            {
                                decimal num4;
                                XSDJ_MXModel model = tag.ListXSDJ_MX[num2];
                                ListViewItem item3 = new ListViewItem {
                                    Name = "MX",
                                    ForeColor = Color.Blue,
                                    Font = new Font("宋体", 10f)
                                };
                                int num3 = num2 + 1;
                                item3.SubItems.Add(num3.ToString());
                                item3.SubItems.Add(model.SPMC);
                                item3.SubItems.Add(model.GGXH);
                                item3.SubItems.Add(model.JLDW);
                                string text = (model.SL == 0.0) ? "" : (num4 = (decimal) model.SL).ToString();
                                item3.SubItems.Add(text);
                                string str2 = (model.DJ == 0.0) ? "" : (num4 = (decimal) model.DJ).ToString();
                                item3.SubItems.Add(str2);
                                item3.SubItems.Add(model.JE.ToString("F"));
                                item3.SubItems.Add(model.SE.ToString("F"));
                                string str3 = "";
                                if (model.SLV == 0.0)
                                {
                                    str3 = "0%";
                                }
                                else if (((tag.DJZL == "s") && (model.SLV == 0.05)) && tag.HYSY)
                                {
                                    str3 = "中外合作油气田";
                                }
                                else if (!((model.SLV != 0.015) || tag.HYSY))
                                {
                                    str3 = model.SLV.ToString("0.0%");
                                }
                                else
                                {
                                    str3 = model.SLV.ToString("0%");
                                }
                                item3.SubItems.Add(str3);
                                this.listView1.Items.Insert((index + 1) + num2, item3);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void SetImgList()
        {
            this.imageList1.Images.Add("OK", Resources.OK);
            this.imageList1.Images.Add("No", Resources.NoError);
            this.imageList1.Images.Add("Part", Resources.Part);
        }
    }
}

