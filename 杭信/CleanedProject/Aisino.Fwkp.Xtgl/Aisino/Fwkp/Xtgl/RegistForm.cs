namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Plugin.Core.WebService;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Xtgl.Properties;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class RegistForm : DockForm
    {
        private AisinoBTN btnAdd;
        private AisinoBTN btnExport;
        private AisinoBTN btnLoad;
        private AisinoBTN btnRemove;
        private AisinoBTN btnSetupRegFile;
        private AisinoCHK chkBeforeDate;
        private AisinoCHK chkOverdate;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem delToolStripMenuItem;
        private ImageList imageList1;
        private AisinoLBL label6;
        private AisinoLBL lblBBH;
        private AisinoLBL lblKPJH;
        private AisinoLBL lblNSDJH;
        private AisinoLBL lblRJBH;
        private AisinoLBL lblRJXLH;
        private AisinoLBL lblRJZL;
        private AisinoLBL lblSQJZRQ;
        private AisinoLBL lblZCDY;
        private ILog loger = LogUtil.GetLogger<RegistForm>();
        private AisinoLST lstBoxAuthNo;
        private AisinoNUD numUdDays;
        private AisinoNUD numUDMachineNo;
        private AisinoNUD numUdOutTime;
        private TreeNode rootNode;
        private AisinoRTX rtxtInsVersions;
        private AisinoRTX rtxtVersion;
        private AisinoTAB tcRegistInfo;
        private TabPage tpBaseInfo;
        private TabPage tpFileCheck;
        private TabPage tpLoadFiles;
        private TabPage tpRegist;
        private AisinoTVW tvRegVersion;
        private TextBoxRegex txtAuthNo;
        private AisinoTXT txtLoadUri;
        private AisinoTXT txtName;
        private AisinoTXT txtTaxNo;
        private readonly List<VersionInfo> versionList = new List<VersionInfo>();
        private XmlComponentLoader xmlComponentLoader1;

        public RegistForm()
        {
            this.Initialize();
            this.tcRegistInfo.TabPages.Clear();
            base.Load += new EventHandler(this.RegistForm_Load);
            base.Closed += new EventHandler(this.RegistForm_Closed);
            this.numUdDays.Maximum = 99999999M;
            this.numUdDays.MaximumSize = new Size(0x2d, 0x15);
        }

        private void AddRegFileNode(RegFileWrapper file)
        {
            if (file != null)
            {
                TreeNode node;
                if (this.rootNode.Nodes.ContainsKey(file.DisplayName))
                {
                    node = this.rootNode.Nodes[file.DisplayName];
                    node.Tag = file;
                }
                else
                {
                    node = new TreeNode(file.DisplayName) {
                        Name = file.DisplayName,
                        Tag = file
                    };
                    this.rootNode.Nodes.Add(node);
                }
                this.SetNodeImage(node, file.VerFlag);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.lstBoxAuthNo.Items.Contains(this.txtAuthNo.Text.Trim()))
            {
                this.lstBoxAuthNo.Items.Add(this.txtAuthNo.Text.Trim());
            }
            this.txtAuthNo.Text = "";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvRegVersion.SelectedNode;
            if ((selectedNode != null) && (selectedNode.Tag is RegFileWrapper))
            {
                RegFileWrapper tag = (RegFileWrapper) selectedNode.Tag;
                if (RegisterManager.MakeExportedRegFile(tag.VerFlag.ToString(), base.TaxCardInstance))
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    int num = tag.FileName.LastIndexOf(@"\");
                    if ((num > 0) && (num < (tag.FileName.Length - 1)))
                    {
                        string str2 = tag.FileName.Substring(num + 1).Replace(".RFHX", ".TFHX");
                        string sourceFileName = Path.Combine(baseDirectory, str2);
                        SaveFileDialog dialog = new SaveFileDialog {
                            Filter = "注册文件 (*.RFHX;*.TFHX)|*.RFHX;*.TFHX",
                            FileName = str2
                        };
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = dialog.FileName;
                            try
                            {
                                File.Copy(sourceFileName, fileName, true);
                                File.Delete(sourceFileName);
                                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "导出注册文件成功！" });
                            }
                            catch (Exception exception)
                            {
                                this.loger.Error(exception.Message, exception);
                            }
                        }
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "导出注册文件失败。\r\n可能原因：该注册文件已被修改或删除" });
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.txtLoadUri.Text.Trim() == "")
            {
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "下载地址输入不正确" });
                this.txtLoadUri.Select();
            }
            else if (this.lstBoxAuthNo.Items.Count == 0)
            {
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "文件提取号输入不正确" });
                this.txtAuthNo.Select();
            }
            else
            {
                string str = this.txtLoadUri.Text + "/GetRegSrc/GetRegSrc.asmx";
                string str2 = "SimplyGetRegFile";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                List<string> regFiles = new List<string>();
                try
                {
                    foreach (object obj2 in this.lstBoxAuthNo.Items)
                    {
                        object[] objArray = new object[] { base.TaxCardInstance.TaxCode, obj2.ToString() };
                        object obj3 = WebServiceFactory.InvokeWebService(str, str2, objArray);
                        if (obj3 is XmlElement)
                        {
                            string innerXml = ((XmlElement) obj3).InnerXml;
                            string item = this.CreateRegFile(innerXml, baseDirectory);
                            if (item != null)
                            {
                                regFiles.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "下载注册文件异常。\r\n请检查下载地址是否正确，或网络连接是否正常。" });
                    this.loger.Error(exception.Message, exception);
                    return;
                }
                this.SetupNewRegFiles(regFiles);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstBoxAuthNo.SelectedIndices.Count > 0)
            {
                this.lstBoxAuthNo.Items.RemoveAt(this.lstBoxAuthNo.SelectedIndices[0]);
            }
            this.txtAuthNo.Text = "";
        }

        private void btnSetupRegFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "注册文件 (*.RFHX;*.TFHX)|*.RFHX;*.TFHX;|All files (*.*)|*.*",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string[] fileNames = dialog.FileNames;
                List<string> regFiles = new List<string>();
                foreach (string str2 in fileNames)
                {
                    string item = Path.Combine(baseDirectory, str2.Substring(str2.LastIndexOf('\\') + 1));
                    if (str2 == item)
                    {
                        regFiles.Add(item);
                    }
                    else
                    {
                        try
                        {
                            File.Copy(str2, item, true);
                            regFiles.Add(item);
                        }
                        catch (IOException exception)
                        {
                            this.loger.Error(exception.Message, exception);
                        }
                    }
                }
                this.SetupNewRegFiles(regFiles);
            }
        }

        private string CreateRegFile(string innerXml, string dir)
        {
            if (innerXml != null)
            {
                try
                {
                    string str = null;
                    string str2 = null;
                    int count = 0x400;
                    innerXml = string.Format("<root>{0}</root>", innerXml);
                    XmlReader reader = XmlReader.Create(new StringReader(innerXml));
                    if (reader.ReadToFollowing("FileName"))
                    {
                        str = reader.ReadString();
                    }
                    if (reader.ReadToFollowing("FileInfo"))
                    {
                        str2 = reader.ReadString();
                    }
                    if (reader.ReadToFollowing("FileLength"))
                    {
                        count = Convert.ToInt32(reader.ReadString());
                    }
                    if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                    {
                        string path = Path.Combine(dir, str);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        FileStream output = new FileStream(path, FileMode.CreateNew);
                        BinaryWriter writer = new BinaryWriter(output);
                        if ((str2.Length % 4) != 0)
                        {
                            int num2 = 4 - (str2.Length % 4);
                            string str4 = "";
                            for (int i = 0; i < num2; i++)
                            {
                                str4 = str4 + "=";
                            }
                            str2 = str2 + str4;
                        }
                        writer.Write(Convert.FromBase64String(str2), 0, count);
                        writer.Close();
                        output.Close();
                        return path;
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message, exception);
                }
            }
            return null;
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvRegVersion.SelectedNode;
            if ((selectedNode != null) && (DialogResult.Yes == MessageBoxHelper.Show("确定删除此注册文件？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
            {
                RegFileWrapper tag = selectedNode.Tag as RegFileWrapper;
                if (File.Exists(tag.FileName))
                {
                    this.tvRegVersion.Nodes.Remove(selectedNode);
                    File.Delete(tag.FileName);
                    if (!File.Exists(tag.FileName))
                    {
                        MessageBoxHelper.Show("删除注册文件成功，将重启开票软件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        FormMain.ResetForm();
                    }
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

        private string GetBBDY(string dm)
        {
            foreach (VersionInfo info in this.versionList)
            {
                if ((info != null) && (info.Code == dm))
                {
                    return info.Name;
                }
            }
            return "";
        }

        private List<RegFileWrapper> GetRegFileWrappers(RegFileSetupResult fileSetup)
        {
            List<RegFileWrapper> list = new List<RegFileWrapper>();
            if (fileSetup != null)
            {
                if (fileSetup.NormalRegFiles != null)
                {
                    foreach (RegFileInfo info in fileSetup.NormalRegFiles)
                    {
                        RegFileWrapper item = this.NewFileWrapper(info, RegFileType.Normal);
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
                if (fileSetup.OutOfDateRegFiles == null)
                {
                    return list;
                }
                foreach (RegFileInfo info2 in fileSetup.OutOfDateRegFiles)
                {
                    RegFileWrapper wrapper2 = this.NewFileWrapper(info2, RegFileType.OutOfDate);
                    if (wrapper2 != null)
                    {
                        list.Add(wrapper2);
                    }
                }
            }
            return list;
        }

        private string GetVersionName(string fileVer)
        {
            string str = fileVer;
            foreach (VersionInfo info in this.versionList)
            {
                if (info.Sign == fileVer)
                {
                    return info.Name;
                }
            }
            return str;
        }

        private void InitBaseInfo()
        {
            this.txtTaxNo.Text = base.TaxCardInstance.TaxCode;
            this.txtName.Text = base.TaxCardInstance.Corporation;
            this.numUDMachineNo.Maximum = 10240M;
            this.numUDMachineNo.Value = base.TaxCardInstance.Machine;
        }

        private void InitFileCheckInfo(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                RegFileWrapper tag = treeNode.Tag as RegFileWrapper;
                this.lblNSDJH.Text = base.TaxCardInstance.TaxCode;
                this.lblKPJH.Text = base.TaxCardInstance.Machine.ToString();
                if (tag != null)
                {
                    qwe fileContent = tag.FileContent;
                    this.lblRJBH.Text = new string(fileContent.SoftwareID);
                    this.lblRJXLH.Text = fileContent.SerialNo.ToString();
                    string str = new string(fileContent.StopDate);
                    this.lblSQJZRQ.Text = string.Format("{0}年{1}月{2}日", str.Substring(0, 4), str.Substring(4, 2), str.Substring(6, 2));
                    this.lblRJZL.Text = fileContent.SoftwareType.ToString();
                    string str2 = PropertyUtil.GetValue("MAIN_VER");
                    string bBDY = "";
                    string str4 = new string(fileContent.SoftwareID);
                    if (str4.Length == 6)
                    {
                        bBDY = this.GetBBDY(str4.Substring(4, 2));
                    }
                    this.lblZCDY.Text = bBDY;
                    this.lblBBH.Text = str2;
                    this.rtxtVersion.Text = tag.VersionDesc;
                    if (tag.ExportFlag && ((int)base.TaxCardInstance.TaxMode == 2))
                    {
                        this.btnExport.Visible = true;
                    }
                    else
                    {
                        this.btnExport.Visible = false;
                    }
                }
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.tcRegistInfo = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tcRegistInfo");
            this.tpBaseInfo = this.xmlComponentLoader1.GetControlByName<TabPage>("tpBaseInfo");
            this.tpRegist = this.xmlComponentLoader1.GetControlByName<TabPage>("tpRegist");
            this.tpLoadFiles = this.xmlComponentLoader1.GetControlByName<TabPage>("tpLoadFiles");
            this.tpFileCheck = this.xmlComponentLoader1.GetControlByName<TabPage>("tpFileCheck");
            this.numUdDays = this.xmlComponentLoader1.GetControlByName<AisinoNUD>("numUdDays");
            this.numUDMachineNo = this.xmlComponentLoader1.GetControlByName<AisinoNUD>("numUDMachineNo");
            this.numUdOutTime = this.xmlComponentLoader1.GetControlByName<AisinoNUD>("numUdOutTime");
            this.txtName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtName");
            this.txtTaxNo = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtTaxNo");
            this.txtLoadUri = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtLoadUri");
            this.rtxtInsVersions = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxtInsVersions");
            this.rtxtVersion = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxtVersion");
            this.btnSetupRegFile = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnSetupRegFile");
            this.btnLoad = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnLoad");
            this.btnRemove = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnRemove");
            this.btnAdd = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnAdd");
            this.btnExport = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnExport");
            this.chkOverdate = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkOverdate");
            this.chkBeforeDate = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkBeforeDate");
            this.lblBBH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblBBH");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.lblRJZL = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblRJZL");
            this.lblRJXLH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblRJXLH");
            this.lblKPJH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKPJH");
            this.lblZCDY = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblZCDY");
            this.lblSQJZRQ = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblSQJZRQ");
            this.lblRJBH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblRJBH");
            this.lblNSDJH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNSDJH");
            this.tvRegVersion = this.xmlComponentLoader1.GetControlByName<AisinoTVW>("tvRegVersion");
            this.txtAuthNo = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txtAuthNo");
            this.lstBoxAuthNo = this.xmlComponentLoader1.GetControlByName<AisinoLST>("lstBoxAuthNo");
            this.numUDMachineNo.Enabled = false;
            this.txtAuthNo.IsEmpty = false;
            this.txtAuthNo.RegexText = "^[0-9]{0,20}$";
            this.tvRegVersion.ImageList = this.imageList1;
            this.tvRegVersion.ImageIndex = 0;
            this.tvRegVersion.SelectedImageIndex = 0;
            this.chkBeforeDate.Visible = true;
            this.numUdDays.Visible = true;
            this.label6.Visible = true;
            this.chkOverdate.Visible = true;
            this.rtxtInsVersions.Visible = true;
            this.btnSetupRegFile.Visible = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
            this.btnSetupRegFile.Click += new EventHandler(this.btnSetupRegFile_Click);
            this.txtAuthNo.TextChanged += new EventHandler(this.txtAuthNo_TextChanged);
            this.lstBoxAuthNo.SelectedIndexChanged += new EventHandler(this.lstBoxAuthNo_SelectedIndexChanged);
            this.tvRegVersion.AfterSelect += new TreeViewEventHandler(this.tvRegVersion_AfterSelect);
            this.tvRegVersion.MouseClick += new MouseEventHandler(this.tvRegVersion_MouseClick);
            this.delToolStripMenuItem.Click += new EventHandler(this.delToolStripMenuItem_Click);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(RegistForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            this.imageList1 = new ImageList(this.components);
            this.contextMenuStrip = new ContextMenuStrip();
            this.delToolStripMenuItem = new ToolStripMenuItem();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x240, 0x1a0);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Xtgl.RegistForm\Aisino.Fwkp.Xtgl.RegistForm.xml";
            this.imageList1.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FWKP.png");
            this.imageList1.Images.SetKeyName(1, "AP.png");
            this.imageList1.Images.SetKeyName(2, "AS.png");
            this.imageList1.Images.SetKeyName(3, "CB.png");
            this.imageList1.Images.SetKeyName(4, "CT.png");
            this.imageList1.Images.SetKeyName(5, "DK.png");
            this.imageList1.Images.SetKeyName(6, "DR.png");
            this.imageList1.Images.SetKeyName(7, "GF.png");
            this.imageList1.Images.SetKeyName(8, "JI.png");
            this.imageList1.Images.SetKeyName(9, "JP.png");
            this.imageList1.Images.SetKeyName(10, "JS.png");
            this.imageList1.Images.SetKeyName(11, "KG.png");
            this.imageList1.Images.SetKeyName(12, "KP.png");
            this.imageList1.Images.SetKeyName(13, "KT.png");
            this.imageList1.Images.SetKeyName(14, "PB.png");
            this.imageList1.Images.SetKeyName(15, "RZ.png");
            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] { this.delToolStripMenuItem });
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new Size(0x65, 0x1a);
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new Size(100, 0x16);
            this.delToolStripMenuItem.Image = Resources.删除注册文件;
            this.delToolStripMenuItem.Text = "删除注册文件";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x240, 0x1a0);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "RegistForm";
            this.Text = "系统注册";
            base.ResumeLayout(false);
        }

        private void InitLoadInfo()
        {
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.LoadRegFileUri");
            if (string.IsNullOrEmpty(str))
            {
                str = "http://lwzc.aisino.com";
            }
            this.txtLoadUri.Text = str;
            string str2 = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.OutTime", "8");
            this.numUdOutTime.Value = Convert.ToInt32(str2);
            this.lstBoxAuthNo.Items.Clear();
        }

        private void InitRegistInfo()
        {
            string str = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTip", "true");
            string str2 = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.OutOfDateTip", "true");
            this.chkBeforeDate.Checked = Convert.ToBoolean(str);
            this.chkOverdate.Checked = Convert.ToBoolean(str2);
            if (this.chkBeforeDate.Checked)
            {
                string str3 = PropertyUtil.GetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTipDays", "30");
                this.numUdDays.Value = Convert.ToInt32(str3);
            }
            this.ShowSetupFileTip();
        }

        private void InitTreeData()
        {
            this.versionList.AddRange(new RegistInfoDAL().SelectRegistFileName());
            RegFileSetupResult fileSetup = RegisterManager.SetupRegFile(base.TaxCardInstance);
            List<RegFileWrapper> regFileWrappers = this.GetRegFileWrappers(fileSetup);
            this.tvRegVersion.BeginUpdate();
            this.tvRegVersion.Nodes.Clear();
            this.rootNode = new TreeNode();
            this.rootNode.Text = "防伪开票";
            this.rootNode.ImageIndex = 0;
            this.rootNode.SelectedImageIndex = 0;
            this.tvRegVersion.Nodes.Add(this.rootNode);
            foreach (RegFileWrapper wrapper in regFileWrappers)
            {
                this.AddRegFileNode(wrapper);
            }
            this.tvRegVersion.EndUpdate();
            this.tvRegVersion.ExpandAll();
        }

        private void lstBoxAuthNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstBoxAuthNo.SelectedIndices.Count > 0)
            {
                this.txtAuthNo.Text = this.lstBoxAuthNo.SelectedItems[0].ToString();
            }
        }

        private RegFileWrapper NewFileWrapper(RegFileInfo file, RegFileType type)
        {
            RegFileWrapper wrapper = null;
            if ((file != null) && (file.VerFlag != null))
            {
                wrapper = new RegFileWrapper(file) {
                    FileType = type
                };
                foreach (VersionInfo info in this.versionList)
                {
                    if ((info != null) && (info.Sign == file.VerFlag))
                    {
                        wrapper.DisplayName = info.Name;
                        wrapper.VersionDesc = info.Description;
                        wrapper.ExportFlag = info.ExportFlag;
                    }
                }
            }
            return wrapper;
        }

        private void RegistForm_Closed(object sender, EventArgs e)
        {
            this.SaveRegistTempVar();
        }

        private void RegistForm_Load(object sender, EventArgs e)
        {
            this.InitTreeData();
            this.InitBaseInfo();
            this.InitRegistInfo();
            this.InitLoadInfo();
        }

        private void SaveRegistTempVar()
        {
            if (this.chkBeforeDate.Checked)
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTipDays", this.numUdDays.Value.ToString());
            }
            PropertyUtil.SetValue("Aisino.Fwkp.Xtgl.RegistForm.BeforeTip", this.chkBeforeDate.Checked.ToString());
            PropertyUtil.SetValue("Aisino.Fwkp.Xtgl.RegistForm.OutOfDateTip", this.chkOverdate.Checked.ToString());
            if (this.txtLoadUri.Text.Trim() != "")
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Xtgl.RegistForm.LoadRegFileUri", this.txtLoadUri.Text);
            }
            PropertyUtil.SetValue("Aisino.Fwkp.Xtgl.RegistForm.OutTime", this.numUdOutTime.Value.ToString());
        }

        private void SetNodeImage(TreeNode fileNode, VerFlag verFlag)
        {
            switch (verFlag)
            {
                case VerFlag.AP:
                    fileNode.ImageIndex = 1;
                    fileNode.SelectedImageIndex = 1;
                    return;

                case VerFlag.AS:
                    fileNode.ImageIndex = 2;
                    fileNode.SelectedImageIndex = 2;
                    return;

                case VerFlag.CB:
                    fileNode.ImageIndex = 3;
                    fileNode.SelectedImageIndex = 3;
                    return;

                case VerFlag.CT:
                    fileNode.ImageIndex = 4;
                    fileNode.SelectedImageIndex = 4;
                    return;

                case VerFlag.DK:
                    fileNode.ImageIndex = 5;
                    fileNode.SelectedImageIndex = 5;
                    return;

                case VerFlag.DR:
                    fileNode.ImageIndex = 6;
                    fileNode.SelectedImageIndex = 6;
                    return;

                case VerFlag.GF:
                    fileNode.ImageIndex = 7;
                    fileNode.SelectedImageIndex = 7;
                    return;

                case VerFlag.JI:
                    fileNode.ImageIndex = 8;
                    fileNode.SelectedImageIndex = 8;
                    return;

                case VerFlag.JP:
                    fileNode.ImageIndex = 9;
                    fileNode.SelectedImageIndex = 9;
                    return;

                case VerFlag.JS:
                    fileNode.ImageIndex = 10;
                    fileNode.SelectedImageIndex = 10;
                    return;

                case VerFlag.KG:
                    fileNode.ImageIndex = 11;
                    fileNode.SelectedImageIndex = 11;
                    return;

                case VerFlag.KP:
                    fileNode.ImageIndex = 12;
                    fileNode.SelectedImageIndex = 12;
                    return;

                case VerFlag.KT:
                    fileNode.ImageIndex = 13;
                    fileNode.SelectedImageIndex = 13;
                    return;

                case VerFlag.PB:
                    fileNode.ImageIndex = 14;
                    fileNode.SelectedImageIndex = 14;
                    return;

                case VerFlag.RZ:
                    fileNode.ImageIndex = 15;
                    fileNode.SelectedImageIndex = 15;
                    return;

                case VerFlag.ER:
                    fileNode.ImageIndex = 14;
                    fileNode.SelectedImageIndex = 14;
                    return;

                case VerFlag.JC:
                    fileNode.ImageIndex = 15;
                    fileNode.SelectedImageIndex = 15;
                    return;

                case VerFlag.QC:
                    fileNode.ImageIndex = 15;
                    fileNode.SelectedImageIndex = 15;
                    return;
            }
            fileNode.ImageIndex = 15;
            fileNode.SelectedImageIndex = 15;
        }

        private void SetupNewRegFiles(List<string> regFiles)
        {
            RegFileSetupResult fileSetup = RegisterManager.SetupRegFile(regFiles, base.TaxCardInstance);
            List<RegFileWrapper> regFileWrappers = this.GetRegFileWrappers(fileSetup);
            this.tvRegVersion.BeginUpdate();
            foreach (RegFileWrapper wrapper in regFileWrappers)
            {
                this.AddRegFileNode(wrapper);
            }
            this.tvRegVersion.EndUpdate();
            this.tvRegVersion.ExpandAll();
            this.TipSetupResult(fileSetup);
            UpdateVersionInfo(fileSetup);
        }

        private void ShowSetupFileTip()
        {
            this.rtxtInsVersions.Text = "";
            foreach (TreeNode node in this.tvRegVersion.Nodes[0].Nodes)
            {
                RegFileWrapper tag = node.Tag as RegFileWrapper;
                if (tag != null)
                {
                    this.rtxtInsVersions.Text = this.rtxtInsVersions.Text + string.Format("{0} : 安装成功! \n", tag.DisplayName);
                }
            }
        }

        private void TipSetupResult(RegFileSetupResult setupResult)
        {
            if (setupResult != null)
            {
                string str = "";
                if ((setupResult.NormalRegFiles != null) && (setupResult.NormalRegFiles.Count > 0))
                {
                    str = str + string.Format("已成功安装版本 {0}个：\n", setupResult.NormalRegFiles.Count);
                    foreach (RegFileInfo info in setupResult.NormalRegFiles)
                    {
                        if ((info != null) && (info.VerFlag != null))
                        {
                            str = str + this.GetVersionName(info.VerFlag) + "\n";
                        }
                    }
                    str = str + "\n已安装的功能将在系统下次启动之后可用。";
                }
                if ((setupResult.OutOfDateRegFiles != null) && (setupResult.OutOfDateRegFiles.Count > 0))
                {
                    str = str + string.Format("过期版本 {0}个：\n", setupResult.OutOfDateRegFiles.Count);
                    foreach (RegFileInfo info2 in setupResult.OutOfDateRegFiles)
                    {
                        if ((info2 != null) && (info2.VerFlag != null))
                        {
                            str = str + this.GetVersionName(info2.VerFlag) + "\n";
                        }
                    }
                }
                if (str == "")
                {
                    str = "注册文件无效";
                }
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { str });
            }
        }

        private void tvRegVersion_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == this.tvRegVersion.Nodes[0])
            {
                this.tcRegistInfo.TabPages.Remove(this.tpFileCheck);
                this.tcRegistInfo.TabPages.Add(this.tpBaseInfo);
                this.tcRegistInfo.TabPages.Add(this.tpRegist);
                this.tcRegistInfo.TabPages.Add(this.tpLoadFiles);
                this.rtxtVersion.Text = "基本的防伪税控开票功能，对金税设备进行管理；密文发票的开具、打印、查询等功能。";
            }
            else
            {
                if (this.tcRegistInfo.TabPages.ContainsKey("tpBaseInfo"))
                {
                    this.tcRegistInfo.TabPages.Remove(this.tpBaseInfo);
                }
                if (this.tcRegistInfo.TabPages.ContainsKey("tpRegist"))
                {
                    this.tcRegistInfo.TabPages.Remove(this.tpRegist);
                }
                if (this.tcRegistInfo.TabPages.ContainsKey("tpLoadFiles"))
                {
                    this.tcRegistInfo.TabPages.Remove(this.tpLoadFiles);
                }
                if (!this.tcRegistInfo.TabPages.ContainsKey("tpFileCheck"))
                {
                    this.tcRegistInfo.TabPages.Add(this.tpFileCheck);
                }
                this.InitFileCheckInfo(e.Node);
            }
        }

        private void tvRegVersion_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.tvRegVersion.GetNodeAt(e.X, e.Y);
            if (nodeAt != null)
            {
                this.tvRegVersion.SelectedNode = nodeAt;
                this.tvRegVersion.Focus();
                if ((e.Button == MouseButtons.Right) && (nodeAt.Text != "防伪开票"))
                {
                    this.contextMenuStrip.Show(this.tvRegVersion, e.Location, ToolStripDropDownDirection.BelowRight);
                }
            }
        }

        private void txtAuthNo_TextChanged(object sender, EventArgs e)
        {
            if (this.txtAuthNo.Text.Length == 20)
            {
                this.btnAdd.Enabled = true;
            }
            else
            {
                this.btnAdd.Enabled = false;
            }
        }

        private static void UpdateVersionInfo(RegFileSetupResult setupResult)
        {
            if ((setupResult != null) && (setupResult.NormalRegFiles != null))
            {
                RegistInfoDAL odal = new RegistInfoDAL();
                foreach (RegFileInfo info in setupResult.NormalRegFiles)
                {
                    string str = info.FileName;
                    if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(info.VerFlag))
                    {
                        odal.UpdateRegFileName(info.VerFlag, str.Substring(str.LastIndexOf('\\') + 1));
                    }
                }
            }
        }
    }
}

