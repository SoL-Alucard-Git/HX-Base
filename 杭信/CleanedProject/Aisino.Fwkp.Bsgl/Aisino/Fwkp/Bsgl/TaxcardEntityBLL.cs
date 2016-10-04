namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class TaxcardEntityBLL
    {
        private ILog loger = LogUtil.GetLogger<TaxcardEntityBLL>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public int GetCurrentRepPeriod(int invType)
        {
            int num = 0;
            List<int> periodCount = new List<int>();
            try
            {
                periodCount = this.taxCard.GetPeriodCount(invType);
                if (periodCount.Count > 1)
                {
                    num = periodCount[1];
                }
            }
            catch (Exception exception)
            {
                this.loger.Info("获取当前月报税期数失败");
                ExceptionHandler.HandleError(exception);
            }
            return num;
        }

        public List<int> GetMonthCollect(int nAccYear)
        {
            List<int> monthStatPeriod = new List<int>();
            try
            {
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(nAccYear);
                if ((nAccYear == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return monthStatPeriod;
        }

        public DateTime GetReportDateFromInterface()
        {
            try
            {
                return this.taxCard.get_RepDate();
            }
            catch (Exception exception)
            {
                this.loger.Info("从金税设备获取当前开票机报税日期失败");
                ExceptionHandler.HandleError(exception);
                return DateTime.Now;
            }
        }

        public DateTime GetStartDate()
        {
            return DateTime.Now.AddMonths(-13);
        }

        public DateTime GetTaxDate()
        {
            try
            {
                return this.taxCard.GetCardClock();
            }
            catch (Exception exception)
            {
                this.loger.Info("从金税设备获取时间失败");
                ExceptionHandler.HandleError(exception);
                return DateTime.Now;
            }
        }

        public Dictionary<int, List<int>> getYearMonth()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int num = 0;
            int num2 = 0;
            string str = "";
            num = this.taxCard.get_SysYear();
            str = this.taxCard.get_CardEffectDate();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
            }
            for (int i = num2; i <= num; i++)
            {
                List<int> monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthDZ()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string str2 = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if ((info.InvType == 0x33) || (info.InvType == 0x1a))
                {
                    str2 = this.taxCard.GetCSDate(info.InvType)[0];
                }
            }
            if ((str2.Length > 0) && str2.Contains("-"))
            {
                key = int.Parse(str2.Split(new char[] { '-' })[0]);
                num4 = int.Parse(str2.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list4 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list4.Add(j);
            }
            dictionary.Add(key, list4);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthDZFP()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string lastRepDate = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if (info.InvType == 0x33)
                {
                    lastRepDate = info.LastRepDate;
                }
            }
            if ((lastRepDate.Length > 0) && lastRepDate.Contains("-"))
            {
                key = int.Parse(lastRepDate.Split(new char[] { '-' })[0]);
                num4 = int.Parse(lastRepDate.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list3 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list3.Add(j);
            }
            dictionary.Add(key, list3);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthHY()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string lastRepDate = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if (info.InvType == 11)
                {
                    lastRepDate = info.LastRepDate;
                }
            }
            if ((lastRepDate.Length > 0) && lastRepDate.Contains("-"))
            {
                key = int.Parse(lastRepDate.Split(new char[] { '-' })[0]);
                num4 = int.Parse(lastRepDate.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list3 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list3.Add(j);
            }
            dictionary.Add(key, list3);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthJDC()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string lastRepDate = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if (info.InvType == 12)
                {
                    lastRepDate = info.LastRepDate;
                }
            }
            if ((lastRepDate.Length > 0) && lastRepDate.Contains("-"))
            {
                key = int.Parse(lastRepDate.Split(new char[] { '-' })[0]);
                num4 = int.Parse(lastRepDate.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list3 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list3.Add(j);
            }
            dictionary.Add(key, list3);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthJSFP()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string lastRepDate = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if (info.InvType == 0x29)
                {
                    lastRepDate = info.LastRepDate;
                }
            }
            if ((lastRepDate.Length > 0) && lastRepDate.Contains("-"))
            {
                key = int.Parse(lastRepDate.Split(new char[] { '-' })[0]);
                num4 = int.Parse(lastRepDate.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list3 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list3.Add(j);
            }
            dictionary.Add(key, list3);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthPTDZ()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str = "";
            string lastRepDate = "";
            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
            {
                if (info.InvType == 0x33)
                {
                    lastRepDate = info.LastRepDate;
                }
            }
            if ((lastRepDate.Length > 0) && lastRepDate.Contains("-"))
            {
                key = int.Parse(lastRepDate.Split(new char[] { '-' })[0]);
                num4 = int.Parse(lastRepDate.Split(new char[] { '-' })[1]);
            }
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= num4; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list3 = new List<int>();
            for (int j = 1; j <= num4; j++)
            {
                list3.Add(j);
            }
            dictionary.Add(key, list3);
            return dictionary;
        }

        public Dictionary<int, List<int>> getYearMonthZP()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            int key = 0;
            int num2 = 0;
            int num3 = 0;
            int month = 0;
            string str = "";
            key = this.taxCard.get_LastRepDate().Year;
            month = this.taxCard.get_LastRepDate().Month;
            str = this.taxCard.get_CardEffectDate();
            List<int> monthStatPeriod = new List<int>();
            if (!string.IsNullOrEmpty(str) && (str.Length == 6))
            {
                num2 = Convert.ToInt32(str.Substring(0, 4));
                num3 = Convert.ToInt32(str.Substring(4, 2));
            }
            else
            {
                num2 = this.taxCard.get_SysYear();
                num3 = this.taxCard.get_SysMonth();
            }
            if (num2 == key)
            {
                for (int k = num3; k <= month; k++)
                {
                    monthStatPeriod.Add(k);
                }
                dictionary.Add(key, monthStatPeriod);
                return dictionary;
            }
            for (int i = num2; i < key; i++)
            {
                monthStatPeriod = new List<int>();
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(i);
                if ((i == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
                dictionary.Add(i, monthStatPeriod);
            }
            List<int> list2 = new List<int>();
            for (int j = 1; j <= month; j++)
            {
                list2.Add(j);
            }
            dictionary.Add(key, list2);
            return dictionary;
        }

        public bool IsCurrentDate(int nYear, int nMonth)
        {
            bool flag = false;
            try
            {
                if ((nYear == this.taxCard.GetCardClock().Year) && (nMonth == this.taxCard.GetCardClock().Month))
                {
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Info("从金税设备获取金税设备时间失败");
                ExceptionHandler.HandleError(exception);
                flag = false;
            }
            return flag;
        }

        public bool IsLocked()
        {
            try
            {
                TaxStateInfo stateInfo = this.taxCard.GetStateInfo(false);
                if (stateInfo.IsLockReached == 0)
                {
                    this.loger.Debug(stateInfo.IsLockReached);
                    this.loger.Debug("未到锁死期");
                    return false;
                }
                this.loger.Debug("已到锁死期");
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Info("判断金税设备是否锁死失败");
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public bool IsLockedHY()
        {
            bool flag = false;
            try
            {
                foreach (InvTypeInfo info2 in this.taxCard.get_StateInfo().InvTypeInfo)
                {
                    if (info2.InvType == 11)
                    {
                        if (info2.IsLockTime == 0)
                        {
                            this.loger.Debug("未到锁死期");
                            flag = false;
                        }
                        else
                        {
                            this.loger.Debug("已到锁死期");
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info("判断金税设备是否锁死失败");
                ExceptionHandler.HandleError(exception);
                flag = false;
            }
            return flag;
        }

        public bool IsLockedJDC()
        {
            bool flag = false;
            try
            {
                foreach (InvTypeInfo info2 in this.taxCard.get_StateInfo().InvTypeInfo)
                {
                    if (info2.InvType == 12)
                    {
                        if (info2.IsLockTime == 0)
                        {
                            this.loger.Debug("未到锁死期");
                            flag = false;
                        }
                        else
                        {
                            this.loger.Debug("已到锁死期");
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info("判断金税设备是否锁死失败");
                ExceptionHandler.HandleError(exception);
                flag = false;
            }
            return flag;
        }
    }
}

