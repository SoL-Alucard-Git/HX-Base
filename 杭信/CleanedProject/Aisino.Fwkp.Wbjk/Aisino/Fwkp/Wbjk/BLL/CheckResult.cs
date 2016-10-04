namespace Aisino.Fwkp.Wbjk.BLL
{
    using System;
    using System.Collections.Generic;

    public class CheckResult
    {
        public bool HasWrong;
        public string Kpzt = "";
        public List<string> listErrorDJ = new List<string>();
        public List<string> listErrorMX = new List<string>();
        public bool NeedCF;
        public string Zfzt = "";

        public void AddErrorDJ(string msg)
        {
            this.AddErrorDJ(msg, 0);
        }

        public void AddErrorDJ(string msg, int typeError)
        {
            this.listErrorDJ.Add(msg);
            if (typeError == 1)
            {
                this.NeedCF = true;
            }
            else if (typeError == 2)
            {
            }
        }

        public void AddErrorMX(string msg)
        {
            this.AddErrorMX(msg, 0);
        }

        public void AddErrorMX(string msg, int typeError)
        {
            this.listErrorMX.Add(msg);
            if (typeError == 1)
            {
                this.NeedCF = true;
            }
            else if (typeError == 2)
            {
            }
        }
    }
}

