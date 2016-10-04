namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.HomePage.AisinoDock;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class InfoDock : IDock
    {
        private static IDock _dock;
        private IContainer components;
        private AisinoLBL lab_dz;
        private AisinoLBL lab_gs;
        private AisinoLBL lab_yhzh;
        private AisinoLBL labe_dh;
        private AisinoLBL label4;
        private AisinoLBL label6;
        private AisinoLBL label8;

        public InfoDock()
        {
            this.InitializeComponent();
        }

        public InfoDock(PageControl page) : base(page)
        {
            this.InitializeComponent();
        }

        public static IDock CreateDock(PageControl page)
        {
            if ((_dock == null) || _dock.IsDisposed)
            {
                _dock = new InfoDock(page);
            }
            return _dock;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void init()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            string str = card.get_Corporation();
            string str2 = card.get_Address();
            string str3 = card.get_Telephone();
            string str4 = card.get_BankAccount();
            SetAttribute method = new SetAttribute(this.OnSetAttribute);
            List<string> list = new List<string> {
                str,
                str2,
                str3,
                str4
            };
            base.BeginInvoke(method, new object[] { list });
        }

        private void InitializeComponent()
        {
            this.lab_gs = new AisinoLBL();
            this.lab_dz = new AisinoLBL();
            this.label4 = new AisinoLBL();
            this.labe_dh = new AisinoLBL();
            this.label6 = new AisinoLBL();
            this.lab_yhzh = new AisinoLBL();
            this.label8 = new AisinoLBL();
            base.SuspendLayout();
            this.lab_gs.AutoSize = true;
            this.lab_gs.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_gs.Location = new Point(0x13, 0x24);
            this.lab_gs.Name = "lab_gs";
            this.lab_gs.Size = new Size(12, 12);
            this.lab_gs.TabIndex = 2;
            this.lab_gs.Text = "0";
            this.lab_dz.AutoSize = true;
            this.lab_dz.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_dz.Location = new Point(0x67, 0x3a);
            this.lab_dz.Name = "lab_dz";
            this.lab_dz.Size = new Size(11, 12);
            this.lab_dz.TabIndex = 4;
            this.lab_dz.Text = "0";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.Location = new Point(0x13, 0x3a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "营业地址:";
            this.labe_dh.AutoSize = true;
            this.labe_dh.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.labe_dh.Location = new Point(0x67, 0x4f);
            this.labe_dh.Name = "labe_dh";
            this.labe_dh.Size = new Size(11, 12);
            this.labe_dh.TabIndex = 6;
            this.labe_dh.Text = "0";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label6.Location = new Point(0x13, 0x4f);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3b, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "电话号码:";
            this.lab_yhzh.AutoSize = true;
            this.lab_yhzh.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_yhzh.Location = new Point(0x67, 100);
            this.lab_yhzh.Name = "lab_yhzh";
            this.lab_yhzh.Size = new Size(11, 12);
            this.lab_yhzh.TabIndex = 8;
            this.lab_yhzh.Text = "0";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label8.Location = new Point(0x13, 100);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3b, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "银行账号:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.Controls.Add(this.lab_yhzh);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.labe_dh);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.lab_dz);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.lab_gs);
            base.Name = "InfoDock";
            base.Size = new Size(0x1ac, 0x139);
            base.Title = "企业信息";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.renew();
        }

        public void OnSetAttribute(List<string> e)
        {
            this.lab_gs.Text = e[0];
            this.lab_dz.Text = e[1];
            this.labe_dh.Text = e[2];
            this.lab_yhzh.Text = e[3];
        }

        private void renew()
        {
            new Thread(new ThreadStart(this.init)).Start();
        }

        public delegate void SetAttribute(List<string> e);
    }
}

