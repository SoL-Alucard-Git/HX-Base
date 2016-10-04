namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class PrintForm : BaseForm
    {
        private AisinoBTN buttonCancel = new AisinoBTN();
        private AisinoBTN buttonOK = new AisinoBTN();
        private IContainer components;
        private List<AisinoCHK> m_checkBoxList = new List<AisinoCHK>();
        private List<AisinoCHK> m_checkBoxStatusList = new List<AisinoCHK>();
        private List<AisinoGRP> m_groupBoxList = new List<AisinoGRP>();
        private List<InvTypeEntity> m_InvTypeList = new List<InvTypeEntity>();
        private List<ItemEntity> m_ItemsList = new List<ItemEntity>();
        public List<PrintEntity> m_PrintEntityList = new List<PrintEntity>();
        private QueryPrintEntity m_QueryPrintEntity = new QueryPrintEntity();

        public PrintForm(List<InvTypeEntity> _InvTypeList, List<ItemEntity> _ItemsList, QueryPrintEntity _queryPrintEntity)
        {
            this.InitializeComponent();
            this.m_InvTypeList = _InvTypeList;
            this.m_ItemsList = _ItemsList;
            this.m_QueryPrintEntity = _queryPrintEntity;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.m_InvTypeList.Count; i++)
                {
                    for (int j = 0; j < this.m_ItemsList.Count; j++)
                    {
                        this.m_PrintEntityList[i].m_ItemEntity[j].m_bStatus = false;
                    }
                }
                base.Close();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                new Thread(new ThreadStart(this.SerialPrint)).Start();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
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

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f8, 0x170);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PrintForm";
            this.Text = "打印选项";
            base.Load += new EventHandler(this.PrintForm_Load);
            base.ResumeLayout(false);
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                for (int i = 0; i < this.m_InvTypeList.Count; i++)
                {
                    PrintEntity item = new PrintEntity {
                        m_InvTypeEntity = this.m_InvTypeList[i]
                    };
                    for (int k = 0; k < this.m_ItemsList.Count; k++)
                    {
                        item.m_ItemEntity.Add(this.m_ItemsList[k]);
                    }
                    this.m_PrintEntityList.Add(item);
                }
                int num3 = 0;
                int bottom = 0;
                int num5 = 0;
                int right = 0;
                int num7 = 0;
                int num8 = 0;
                int num9 = 0;
                for (int j = 0; j < this.m_InvTypeList.Count; j++)
                {
                    AisinoGRP ogrp = new AisinoGRP {
                        BackColor = Color.FromArgb(0xf2, 0xf4, 0xf5),
                        Text = this.m_InvTypeList[j].m_strInvName,
                        Parent = this,
                        Width = 250,
                        Height = 160
                    };
                    if (j == 0)
                    {
                        num3 = 20;
                        num5 = 5;
                    }
                    else
                    {
                        num5 = right + 10;
                    }
                    if (num5 > base.Width)
                    {
                        num3 = bottom + 10;
                        num5 = 5;
                    }
                    ogrp.Top = num3;
                    ogrp.Left = num5;
                    right = ogrp.Right;
                    bottom = ogrp.Bottom;
                    this.m_groupBoxList.Add(ogrp);
                    for (int m = 0; m < this.m_ItemsList.Count; m++)
                    {
                        AisinoCHK ochk = new AisinoCHK {
                            Text = this.m_ItemsList[m].m_strItemName,
                            Checked = true,
                            Width = 120
                        };
                        if (m == 0)
                        {
                            num7 = 15;
                        }
                        else
                        {
                            num7 = num8 + 2;
                        }
                        num9 = 20;
                        ochk.Top = num7;
                        ochk.Left = num9;
                        num8 = ochk.Bottom;
                        ochk.Parent = ogrp;
                        this.m_checkBoxList.Add(ochk);
                        AisinoCHK ochk2 = new AisinoCHK {
                            Width = 50,
                            Top = ochk.Top,
                            Left = ochk.Right + 50,
                            Enabled = false,
                            Parent = ogrp
                        };
                        this.m_checkBoxStatusList.Add(ochk2);
                    }
                }
                this.buttonOK = new AisinoBTN();
                this.buttonOK.Text = "确定";
                this.buttonOK.Top = bottom + 20;
                this.buttonOK.Left = base.Width / 3;
                this.buttonOK.Parent = this;
                this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
                this.buttonCancel = new AisinoBTN();
                this.buttonCancel.Text = "关闭";
                this.buttonCancel.Top = this.buttonOK.Top;
                this.buttonCancel.Left = this.buttonOK.Right + 20;
                this.buttonCancel.Parent = this;
                this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
                base.Size = new Size(base.Width, this.buttonOK.Bottom + 80);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-251303", new string[] { exception.Message });
            }
        }

        private void SerialPrint()
        {
            try
            {
                this.buttonOK.Enabled = false;
                this.buttonCancel.Enabled = false;
                for (int i = 0; i < this.m_PrintEntityList.Count; i++)
                {
                    for (int k = 0; k < this.m_PrintEntityList[i].m_ItemEntity.Count; k++)
                    {
                        this.m_checkBoxStatusList[(i * this.m_ItemsList.Count) + k].Text = "";
                        this.m_checkBoxStatusList[(i * this.m_ItemsList.Count) + k].Checked = false;
                    }
                }
                bool flag = true;
                for (int j = 0; j < this.m_PrintEntityList.Count; j++)
                {
                    for (int m = 0; m < this.m_PrintEntityList[j].m_ItemEntity.Count; m++)
                    {
                        this.m_QueryPrintEntity.m_invType = this.m_PrintEntityList[j].m_InvTypeEntity.m_invType;
                        string strInvName = this.m_PrintEntityList[j].m_InvTypeEntity.m_strInvName;
                        if (this.m_checkBoxList[(j * this.m_ItemsList.Count) + m].Checked)
                        {
                            this.m_QueryPrintEntity.m_itemAction = this.m_PrintEntityList[j].m_ItemEntity[m].m_ItemType;
                            this.m_QueryPrintEntity.m_strSubItem = strInvName + "统计表  1-0" + ((m + 1)).ToString();
                            if (this.m_QueryPrintEntity.m_nTaxPeriod == 0)
                            {
                                this.m_QueryPrintEntity.m_strItemDetail = this.m_PrintEntityList[j].m_ItemEntity[m].m_strItemName + "(" + this.m_QueryPrintEntity.m_nYear.ToString() + "年" + this.m_QueryPrintEntity.m_nMonth.ToString() + "月)";
                            }
                            else
                            {
                                this.m_QueryPrintEntity.m_strItemDetail = this.m_PrintEntityList[j].m_ItemEntity[m].m_strItemName + "(" + this.m_QueryPrintEntity.m_nYear.ToString() + "年" + this.m_QueryPrintEntity.m_nMonth.ToString() + "月第" + this.m_QueryPrintEntity.m_nTaxPeriod.ToString() + "期)";
                            }
                            if (this.m_PrintEntityList[j].m_ItemEntity[m].m_ItemType == ITEM_ACTION.ITEM_TOTAL)
                            {
                                this.m_QueryPrintEntity.m_strTitle = strInvName + "汇总表";
                                InvStatForm form = new InvStatForm(this.m_QueryPrintEntity);
                                if (form.PrintTable(flag))
                                {
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Text = "完成";
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Checked = true;
                                }
                                else
                                {
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Checked = false;
                                }
                                flag = false;
                            }
                            else
                            {
                                this.m_QueryPrintEntity.m_strTitle = strInvName + "明细表";
                                InvoiceResultForm form2 = new InvoiceResultForm(this.m_QueryPrintEntity);
                                if (form2.PrintTable(flag))
                                {
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Text = "完成";
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Checked = true;
                                }
                                else
                                {
                                    this.m_checkBoxStatusList[(j * this.m_ItemsList.Count) + m].Checked = false;
                                }
                                flag = false;
                            }
                        }
                    }
                }
                this.buttonCancel.Enabled = true;
                this.buttonOK.Enabled = true;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }
    }
}

