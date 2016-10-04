namespace RegMakeFile
{
    using System;

    public class RegFileInfo
    {
        private string errCode;
        private qwe fileContent;
        private DateTime fileModifyDate;
        private string fileName;

        public RegFileInfo(string fileName, qwe fileContent, DateTime fileModifyDate)
        {
            this.fileName = fileName;
            this.fileContent = fileContent;
            this.fileModifyDate = fileModifyDate;
        }

        public bool CheckedOk
        {
            get
            {
                return this.errCode.Equals("0000");
            }
        }

        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
            internal set
            {
                this.errCode = value;
            }
        }

        public qwe FileContent
        {
            get
            {
                return this.fileContent;
            }
        }

        public DateTime FileModifyDate
        {
            get
            {
                return this.fileModifyDate;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        public string SoftFlag
        {
            get
            {
                if (this.errCode.Equals("910101"))
                {
                    return null;
                }
                string str = new string(this.fileContent.SoftwareID);
                return (str.Substring(0, 2) + str.Substring(4, 2));
            }
        }

        public string VerFlag
        {
            get
            {
                if (this.errCode.Equals("910101"))
                {
                    return null;
                }
                return new string(this.fileContent.SoftwareID).Substring(0, 2);
            }
        }
    }
}

