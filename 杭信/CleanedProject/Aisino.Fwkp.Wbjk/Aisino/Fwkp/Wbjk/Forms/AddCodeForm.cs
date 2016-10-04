namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AddCodeForm : BaseForm
    {
        private AisinoBTN buttonAdd;
        private AisinoBTN buttonAddAll;
        private AisinoBTN buttonCancel;
        private AisinoBTN buttonDelete;
        private AisinoBTN buttonDeleteAll;
        private AisinoBTN buttonOK;
        private IContainer components;
        private FPCXbll fpcxBLL;
        private string fplx;
        private AisinoLBL labelDest;
        private AisinoLBL labelSrc;
        private AisinoLST listBoxDest;
        private AisinoLST listBoxSrc;
        public ArrayList m_ArrayListDest;
        private ArrayList m_ArrayListSource;
        private bool m_bGoods;
        private SPCXbll spcxBLL;
        private XmlComponentLoader xmlComponentLoader1;

        public AddCodeForm()
        {
            this.fpcxBLL = new FPCXbll();
            this.spcxBLL = new SPCXbll();
            this.m_ArrayListSource = new ArrayList();
            this.m_bGoods = false;
            this.fplx = "";
            this.components = null;
            this.buttonAdd = new AisinoBTN();
            this.buttonAddAll = new AisinoBTN();
            this.buttonDelete = new AisinoBTN();
            this.buttonDeleteAll = new AisinoBTN();
            this.buttonOK = new AisinoBTN();
            this.buttonCancel = new AisinoBTN();
            this.labelSrc = new AisinoLBL();
            this.labelDest = new AisinoLBL();
            this.listBoxSrc = new AisinoLST();
            this.listBoxDest = new AisinoLST();
            this.Initial();
        }

        public AddCodeForm(ArrayList arrayList, bool bGoods, string _fplx)
        {
            this.fpcxBLL = new FPCXbll();
            this.spcxBLL = new SPCXbll();
            this.m_ArrayListSource = new ArrayList();
            this.m_bGoods = false;
            this.fplx = "";
            this.components = null;
            this.buttonAdd = new AisinoBTN();
            this.buttonAddAll = new AisinoBTN();
            this.buttonDelete = new AisinoBTN();
            this.buttonDeleteAll = new AisinoBTN();
            this.buttonOK = new AisinoBTN();
            this.buttonCancel = new AisinoBTN();
            this.labelSrc = new AisinoLBL();
            this.labelDest = new AisinoLBL();
            this.listBoxSrc = new AisinoLST();
            this.listBoxDest = new AisinoLST();
            this.fplx = _fplx;
            this.Initial();
            this.m_ArrayListSource = arrayList;
            this.m_ArrayListDest = new ArrayList();
            this.m_bGoods = bGoods;
        }

        private void AddCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void AddCodeForm_Load(object sender, EventArgs e)
        {
            ArrayList allGoods = new ArrayList();
            if (this.m_bGoods)
            {
                allGoods = this.spcxBLL.GetAllGoods();
            }
            else if (((this.fplx == "a") || (this.fplx == "c")) || (this.fplx == "s"))
            {
                allGoods = this.fpcxBLL.GetAllCustomers();
            }
            else if (this.fplx == "f")
            {
                allGoods = this.fpcxBLL.GetHY_SPFMC();
            }
            else if (this.fplx == "j")
            {
                allGoods = this.fpcxBLL.GetJDC_GHDW();
            }
            int num = 0;
            while (num < allGoods.Count)
            {
                this.listBoxSrc.Items.Add(allGoods[num]);
                num++;
            }
            int count = this.m_ArrayListSource.Count;
            for (num = 0; num < count; num++)
            {
                string str = this.m_ArrayListSource[num].ToString();
                this.listBoxSrc.Items.Remove(str);
                this.listBoxDest.Items.Add(str);
                this.m_ArrayListDest.Add(str);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if ((this.listBoxSrc.Items.Count > 0) && (this.listBoxSrc.SelectedIndex >= 0))
            {
                try
                {
                    int count = this.listBoxSrc.SelectedItems.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string item = this.listBoxSrc.SelectedItem.ToString();
                        this.listBoxDest.Items.Add(item);
                        this.listBoxSrc.Items.Remove(item);
                        this.m_ArrayListDest.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void buttonAddAll_Click(object sender, EventArgs e)
        {
            int count = this.listBoxSrc.Items.Count;
            if (count > 0)
            {
                try
                {
                    int num2;
                    for (num2 = 0; num2 < count; num2++)
                    {
                        this.listBoxSrc.SelectedIndex = num2;
                    }
                    for (num2 = 0; num2 < count; num2++)
                    {
                        string item = this.listBoxSrc.SelectedItem.ToString();
                        this.listBoxDest.Items.Add(item);
                        this.listBoxSrc.Items.Remove(item);
                        this.m_ArrayListDest.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_ArrayListDest.Clear();
                for (int i = 0; i < this.m_ArrayListSource.Count; i++)
                {
                    this.m_ArrayListDest.Add(this.m_ArrayListSource.ToArray()[i]);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            base.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if ((this.listBoxDest.Items.Count > 0) && (this.listBoxDest.SelectedIndex >= 0))
            {
                try
                {
                    int count = this.listBoxDest.SelectedItems.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string item = this.listBoxDest.SelectedItem.ToString();
                        this.listBoxSrc.Items.Add(item);
                        this.listBoxDest.Items.Remove(item);
                        this.m_ArrayListDest.Remove(item);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            int count = this.listBoxDest.Items.Count;
            if (count > 0)
            {
                try
                {
                    int num2;
                    for (num2 = 0; num2 < count; num2++)
                    {
                        this.listBoxDest.SelectedIndex = num2;
                    }
                    for (num2 = 0; num2 < count; num2++)
                    {
                        this.listBoxSrc.Items.Add(this.listBoxDest.SelectedItem.ToString());
                        this.listBoxDest.Items.Remove(this.listBoxDest.SelectedItem.ToString());
                    }
                    this.m_ArrayListDest.Clear();
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MinimizeBox = false;
            base.MaximizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.buttonAdd = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonAdd");
            this.buttonAdd.Click += new EventHandler(this.buttonAdd_Click);
            this.buttonAddAll = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonAddAll");
            this.buttonAddAll.Click += new EventHandler(this.buttonAddAll_Click);
            this.buttonDelete = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonDelete");
            this.buttonDelete.Click += new EventHandler(this.buttonDelete_Click);
            this.buttonDeleteAll = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonDeleteAll");
            this.buttonDeleteAll.Click += new EventHandler(this.buttonDeleteAll_Click);
            this.buttonOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonOK");
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
            this.buttonCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonCancel");
            this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
            this.labelSrc = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelSrc");
            this.labelDest = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("labelDest");
            this.listBoxSrc = this.xmlComponentLoader1.GetControlByName<AisinoLST>("listBoxSrc");
            this.listBoxSrc.SelectionMode = SelectionMode.MultiExtended;
            this.listBoxSrc.DoubleClick += new EventHandler(this.listBoxSrc_DoubleClick);
            this.listBoxDest = this.xmlComponentLoader1.GetControlByName<AisinoLST>("listBoxDest");
            this.listBoxDest.SelectionMode = SelectionMode.MultiExtended;
            this.listBoxDest.DoubleClick += new EventHandler(this.listBoxDest_DoubleClick);
            base.Load += new EventHandler(this.AddCodeForm_Load);
            base.FormClosing += new FormClosingEventHandler(this.AddCodeForm_FormClosing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1b0, 0x178);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.AddCodeForm\Aisino.Fwkp.Wbjk.AddCodeForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b0, 0x178);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "AddCodeForm";
            this.Text = "AddCodeForm";
            base.ResumeLayout(false);
        }

        private void listBoxDest_DoubleClick(object sender, EventArgs e)
        {
            if ((this.listBoxDest.Items.Count > 0) && (this.listBoxDest.SelectedIndex >= 0))
            {
                try
                {
                    string item = this.listBoxDest.SelectedItem.ToString();
                    this.listBoxSrc.Items.Add(item);
                    this.listBoxDest.Items.Remove(item);
                    this.m_ArrayListDest.Remove(item);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
                this.listBoxDest.SelectedIndex = -1;
            }
        }

        private void listBoxSrc_DoubleClick(object sender, EventArgs e)
        {
            if ((this.listBoxSrc.Items.Count > 0) && (this.listBoxSrc.SelectedIndex >= 0))
            {
                try
                {
                    string item = this.listBoxSrc.SelectedItem.ToString();
                    this.listBoxDest.Items.Add(item);
                    this.listBoxSrc.Items.Remove(item);
                    this.m_ArrayListDest.Add(item);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
                this.listBoxSrc.SelectedIndex = -1;
            }
        }

        public string strLabelDest
        {
            get
            {
                return this.labelDest.Text;
            }
            set
            {
                this.labelDest.Text = value;
            }
        }

        public string strLabelSrc
        {
            get
            {
                return this.labelSrc.Text;
            }
            set
            {
                this.labelSrc.Text = value;
            }
        }
    }
}

