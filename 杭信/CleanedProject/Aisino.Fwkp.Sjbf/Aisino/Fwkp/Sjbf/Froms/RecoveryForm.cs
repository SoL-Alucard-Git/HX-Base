namespace Aisino.Fwkp.Sjbf.Froms
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Sjbf;
    using Aisino.Fwkp.Sjbf.Common;
    using ICSharpCode.SharpZipLib.Zip;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class RecoveryForm : Form
    {
        private ILog _Loger = LogUtil.GetLogger<RecoveryForm>();
        private CustomStyleDataGrid aisinoDataGrid1;
        private string BackupPath = PropertyUtil.GetValue("数据备份路径");
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private IContainer components;
        private DataGridViewTextBoxColumn CZY;
        private Label label1;
        private Label label2;
        private Label label3;
        private DataGridViewTextBoxColumn LJ;
        private DataGridViewTextBoxColumn LX;
        private DataGridViewTextBoxColumn machine;
        private DataGridViewTextBoxColumn MC;
        private Panel panel1;
        private DataGridViewTextBoxColumn SJ;
        private StatusStrip statusStrip1;
        private string sysTempDir = Path.GetTempPath();
        private DataGridViewTextBoxColumn TaxCode;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnDBFile;
        private ToolStripButton toolStripBtnDBPath;
        private ToolStripButton toolStripExit;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridViewTextBoxColumn Version;

        public RecoveryForm()
        {
            this.InitializeComponent();
        }

        private void AddRowsToDataGrid(DataGridViewRow row)
        {
            if (this.aisinoDataGrid1.InvokeRequired)
            {
                AddRowsToDataGridCallBack method = new AddRowsToDataGridCallBack(this.AddRowsToDataGrid);
                this.aisinoDataGrid1.Invoke(method, new object[] { row });
            }
            else
            {
                this.aisinoDataGrid1.Rows.Add(row);
            }
        }

        private void aisinoDataGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string inFileName = this.aisinoDataGrid1.CurrentRow.Cells["LJ"].Value.ToString();
                    if (!this.IsMatchVTM(AES_Crypt_File.DecryptFile(inFileName)))
                    {
                        MessageBoxHelper.Show("所选备份文件的税号，开票机号或版本号与当前软件不匹配，请重新选择!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (!string.IsNullOrEmpty(inFileName))
                    {
                        if (DialogResult.Yes == MessageBoxHelper.Show("恢复到选定版本数据库？(此操作不可恢复)\n" + inFileName + "\n确定后重启。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            PropertyUtil.SetValue("恢复数据库路径", inFileName);
                            PropertyUtil.Save();
                            FormMain.ResetForm();
                        }
                        else
                        {
                            PropertyUtil.SetValue("恢复数据库路径", "");
                            PropertyUtil.Save();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this._Loger.Error(exception.ToString());
                    ExceptionHandler.HandleError(exception);
                }
            }
        }

        private void CreateDataSource(string Path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(Path);
                int length = info.GetFiles("*.zip").Length;
                this.ReportProgress(0, 0, 0);
                int validNum = 0;
                int num3 = 0;
                FileInfo[] files = info.GetFiles("*.zip");
                Array.Sort(files, new FileSort(FileOrder.LastWriteTime, FileAsc.Desc));
                foreach (FileInfo info2 in files)
                {
                    string zipFileName = AES_Crypt_File.DecryptFile(info2.FullName);
                    if ("" == zipFileName)
                    {
                        this._Loger.Error("解密文件错误");
                        this.ReportProgress(validNum, ++num3, length);
                    }
                    else
                    {
                        string path = this.sysTempDir + info2.Name.Replace("FWSK.zip", "备份说明.txt");
                        DataGridViewRow row = new DataGridViewRow();
                        FastZip zip = new FastZip();
                        try
                        {
                            zip.ExtractZip(zipFileName, this.sysTempDir, ".txt");
                        }
                        catch (Exception exception)
                        {
                            this._Loger.Error("解压缩文件错误:" + info2.FullName + exception.Message);
                            if (File.Exists(zipFileName))
                            {
                                File.Delete(zipFileName);
                            }
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            this.ReportProgress(validNum, ++num3, length);
                            goto Label_0456;
                        }
                        if (File.Exists(path))
                        {
                            DataGridViewTextBoxCell cell;
                            if (!this.IsMatchVTM(path))
                            {
                                if (File.Exists(zipFileName))
                                {
                                    File.Delete(zipFileName);
                                }
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                                this.ReportProgress(validNum, ++num3, length);
                                goto Label_0456;
                            }
                            string[] strArray = File.ReadAllLines(path);
                            for (int i = 0; i < 7; i++)
                            {
                                string str3 = "";
                                if (i < strArray.Length)
                                {
                                    str3 = strArray[i];
                                }
                                else
                                {
                                    str3 = "";
                                }
                                if (i == 0)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("数据备份时间"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1).Replace('-', '/');
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 1)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("数据备份满足条件"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1);
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 2)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("操作员"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1);
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 3)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("数据库备份名称"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1).Replace("cc3268.dll", "FWSK");
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 4)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("软件版本号"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1);
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 5)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("纳税人识别号"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1);
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                                if (i == 6)
                                {
                                    cell = new DataGridViewTextBoxCell();
                                    if (str3.StartsWith("开票机号"))
                                    {
                                        cell.Value = str3.Substring(str3.IndexOf(':') + 1);
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                    row.Cells.Add(cell);
                                }
                            }
                            cell = new DataGridViewTextBoxCell {
                                Value = info2.FullName
                            };
                            row.Cells.Add(cell);
                            this.AddRowsToDataGrid(row);
                            if (File.Exists(zipFileName))
                            {
                                File.Delete(zipFileName);
                            }
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            validNum++;
                        }
                        this.ReportProgress(validNum, ++num3, length);
                    Label_0456:;
                    }
                }
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
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

        private void DoWork(object obj)
        {
            string str = (string) obj;
            if (!string.IsNullOrEmpty(str))
            {
                this.CreateDataSource(str);
                this.WorkCompleted();
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.aisinoDataGrid1 = new CustomStyleDataGrid();
            this.SJ = new DataGridViewTextBoxColumn();
            this.LX = new DataGridViewTextBoxColumn();
            this.CZY = new DataGridViewTextBoxColumn();
            this.MC = new DataGridViewTextBoxColumn();
            this.Version = new DataGridViewTextBoxColumn();
            this.TaxCode = new DataGridViewTextBoxColumn();
            this.machine = new DataGridViewTextBoxColumn();
            this.LJ = new DataGridViewTextBoxColumn();
            this.label1 = new Label();
            this.label2 = new Label();
            this.panel1 = new Panel();
            this.label3 = new Label();
            this.toolStrip1 = new ToolStrip();
            this.toolStripExit = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripBtnDBFile = new ToolStripButton();
            this.toolStripBtnDBPath = new ToolStripButton();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripProgressBar1 = new ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)this.aisinoDataGrid1).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.aisinoDataGrid1.AborCellPainting=false;
            this.aisinoDataGrid1.AllowColumnHeadersVisible=true;
            this.aisinoDataGrid1.AllowUserToAddRows = false;
            this.aisinoDataGrid1.AllowUserToResizeRows=false;
            style.BackColor = Color.FromArgb(240, 250, 0xff);
            this.aisinoDataGrid1.AlternatingRowsDefaultCellStyle = style;
            this.aisinoDataGrid1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.aisinoDataGrid1.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.aisinoDataGrid1.BorderStyle = BorderStyle.None;
            this.aisinoDataGrid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.aisinoDataGrid1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("微软雅黑", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.aisinoDataGrid1.ColumnHeadersDefaultCellStyle = style2;
            this.aisinoDataGrid1.ColumnHeadersHeight=0;
            this.aisinoDataGrid1.Columns.AddRange(new DataGridViewColumn[] { this.SJ, this.LX, this.CZY, this.MC, this.Version, this.TaxCode, this.machine, this.LJ });
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("宋体", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.aisinoDataGrid1.DefaultCellStyle = style3;
            this.aisinoDataGrid1.EnableHeadersVisualStyles = false;
            this.aisinoDataGrid1.GridStyle=0;
            this.aisinoDataGrid1.Location = new Point(0, 0x68);
            this.aisinoDataGrid1.Name = "aisinoDataGrid1";
            this.aisinoDataGrid1.RightToLeft = RightToLeft.No;
            this.aisinoDataGrid1.RowHeadersVisible = false;
            this.aisinoDataGrid1.RowTemplate.Height = 30;
            this.aisinoDataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.aisinoDataGrid1.Size = new Size(780, 0x16b);
            this.aisinoDataGrid1.TabIndex = 0;
            this.aisinoDataGrid1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.aisinoDataGrid1_CellMouseDoubleClick);
            this.SJ.HeaderText = "备份时间";
            this.SJ.Name = "SJ";
            this.LX.HeaderText = "备份类型";
            this.LX.Name = "LX";
            this.CZY.HeaderText = "操作员";
            this.CZY.Name = "CZY";
            this.MC.HeaderText = "备份名称";
            this.MC.Name = "MC";
            this.Version.HeaderText = "版本号";
            this.Version.Name = "Version";
            this.TaxCode.HeaderText = "税号";
            this.TaxCode.Name = "TaxCode";
            this.machine.HeaderText = "开票机号";
            this.machine.Name = "machine";
            this.LJ.HeaderText = "备份路径";
            this.LJ.Name = "LJ";
            this.label1.BackColor = Color.FromArgb(240, 240, 240);
            this.label1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(2, 0x4e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x310, 0x17);
            this.label1.TabIndex = 2;
            this.label1.Text = "与此版本匹配的备份文件列表（双击某一版本进行数据库恢复操作）";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x71, 0x13);
            this.label2.TabIndex = 2;
            this.label2.Text = "备份数据库目录：";
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new Point(0, 0x27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(780, 0x24);
            this.panel1.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.ForeColor = Color.Blue;
            this.label3.Location = new Point(0x70, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3d, 0x13);
            this.label3.TabIndex = 3;
            this.label3.Text = "我是路径";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripExit, this.toolStripSeparator1, this.toolStripBtnDBFile, this.toolStripBtnDBPath });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(780, 0x24);
            this.toolStrip1.TabIndex = 0x4d;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripExit.Image = Resource.退出;
            this.toolStripExit.ImageTransparentColor = Color.Magenta;
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new Size(0x34, 0x21);
            this.toolStripExit.Text = "退出";
            this.toolStripExit.Click += new EventHandler(this.toolStripExit_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x24);
            this.toolStripBtnDBFile.Image = Resource.文件;
            this.toolStripBtnDBFile.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnDBFile.Name = "toolStripBtnDBFile";
            this.toolStripBtnDBFile.Size = new Size(0x4c, 0x21);
            this.toolStripBtnDBFile.Text = "查找文件";
            this.toolStripBtnDBFile.Click += new EventHandler(this.toolStripBtnDBFile_Click);
            this.toolStripBtnDBPath.Image = Resource.目录;
            this.toolStripBtnDBPath.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnDBPath.Name = "toolStripBtnDBPath";
            this.toolStripBtnDBPath.Size = new Size(0x4c, 0x21);
            this.toolStripBtnDBPath.Text = "查找目录";
            this.toolStripBtnDBPath.Click += new EventHandler(this.toolStripBtnDBPath_Click);
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripProgressBar1 });
            this.statusStrip1.Location = new Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(780, 0x19);
            this.statusStrip1.TabIndex = 0x4e;
            this.statusStrip1.Text = "statusStrip1";
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(0x35, 20);
            this.toolStripStatusLabel1.Text = "共200条";
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new Size(250, 0x13);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(780, 0x1ef);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.toolStrip1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.aisinoDataGrid1);
            base.Name = "RecoveryForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "数据恢复";
            base.Load += new EventHandler(this.RecoveryForm_Load);
            ((System.ComponentModel.ISupportInitialize)this.aisinoDataGrid1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        private bool IsMatchVTM(string fileName)
        {
            return true;
        }

        private void RecoveryForm_Load(object sender, EventArgs e)
        {
            this.aisinoDataGrid1.MultiSelect = false;
            this.aisinoDataGrid1.AborCellPainting=true;
            this.aisinoDataGrid1.ReadOnly = true;
            this.aisinoDataGrid1.AllowUserToDeleteRows = false;
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.toolStrip1.Height = 0x24;
            this.label2.Text = "备份数据库目录：";
            this.label3.Text = this.BackupPath;
            this.toolStripBtnDBPath.Enabled = false;
            this.toolStripBtnDBFile.Enabled = false;
            new Thread(new ParameterizedThreadStart(this.DoWork)) { IsBackground = true }.Start(this.BackupPath);
        }

        private void ReportProgress(int validNum, int curPos, int maxValue)
        {
            if (this.toolStrip1.InvokeRequired)
            {
                ReportProgressCallBack method = new ReportProgressCallBack(this.ReportProgress);
                this.aisinoDataGrid1.Invoke(method, new object[] { validNum, curPos, maxValue });
            }
            else
            {
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Maximum = maxValue;
                this.toolStripProgressBar1.Value = curPos;
                if (this.toolStripProgressBar1.Maximum == curPos)
                {
                    this.toolStripStatusLabel1.Text = "符合" + validNum + "条记录";
                    this.toolStripProgressBar1.Visible = false;
                }
                else
                {
                    this.toolStripStatusLabel1.Text = string.Concat(new object[] { "符合", validNum, "条记录/正在扫描第", curPos, "条记录" });
                }
            }
        }

        private void toolStripBtnDBFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "压缩文件(*.zip)|*.zip"
            };
            if (!string.IsNullOrEmpty(this.BackupPath))
            {
                dialog.InitialDirectory = this.BackupPath;
            }
            dialog.Multiselect = false;
            if (DialogResult.OK == dialog.ShowDialog())
            {
                string fileName = dialog.FileName;
                this.label2.Text = "备份数据库路径：";
                this.label3.Text = fileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (!this.IsMatchVTM(AES_Crypt_File.DecryptFile(fileName)))
                    {
                        MessageBoxHelper.Show("所选备份文件的税号，开票机号或版本号与当前软件不匹配，请重新选择!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (DialogResult.Yes == MessageBoxHelper.Show("恢复到选定版本数据库？(此操作不可恢复)\n" + fileName + "\n确定后重启。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        PropertyUtil.SetValue("恢复数据库路径", fileName);
                        PropertyUtil.Save();
                        FormMain.ResetForm();
                    }
                }
            }
        }

        private void toolStripBtnDBPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = "选择包含备份数据库的目录"
            };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                string selectedPath = dialog.SelectedPath;
                this.label2.Text = "备份数据库目录：";
                this.label3.Text = selectedPath;
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    this.aisinoDataGrid1.Rows.Clear();
                    this.toolStripBtnDBPath.Enabled = false;
                    this.toolStripBtnDBFile.Enabled = false;
                    new Thread(new ParameterizedThreadStart(this.DoWork)) { IsBackground = true }.Start(selectedPath);
                }
            }
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void WorkCompleted()
        {
            if (this.aisinoDataGrid1.InvokeRequired || this.toolStrip1.InvokeRequired)
            {
                WorkCompletedCallBack method = new WorkCompletedCallBack(this.WorkCompleted);
                this.aisinoDataGrid1.Invoke(method, new object[0]);
            }
            else
            {
                for (int i = 0; i < this.aisinoDataGrid1.ColumnCount; i++)
                {
                    this.aisinoDataGrid1.AutoResizeColumn(i);
                }
                this.toolStripBtnDBPath.Enabled = true;
                this.toolStripBtnDBFile.Enabled = true;
            }
        }

        private delegate void AddRowsToDataGridCallBack(DataGridViewRow row);

        private delegate void ReportProgressCallBack(int invalidNum, int curPos, int maxValue);

        private delegate void WorkCompletedCallBack();
    }
}

