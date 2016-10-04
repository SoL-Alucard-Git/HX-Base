namespace Aisino.Fwkp.Bsgl
{
    using Aisino.FTaxBase;
    using System;

    public class TaxStateWrapper
    {
        private int mKPJH;
        private TaxStateInfo mTaxState;

        public TaxStateWrapper(TaxStateInfo taxState, int iTaxCardMachine)
        {
            this.mTaxState = taxState;
            this.mKPJH = iTaxCardMachine;
        }

        public string BottomNo
        {
            get
            {
                return this.mTaxState.DriverVersion;
            }
        }

        public string BSPBSCGBZ
        {
            get
            {
                if (this.mTaxState.TBRepDone == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string BSPBSZL
        {
            get
            {
                if (this.mTaxState.TBRepInfo == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string BSPGRFP
        {
            get
            {
                if (this.mTaxState.TBBuyInv == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public int BSPKPJH
        {
            get
            {
                return this.mTaxState.TBCardNo;
            }
        }

        public int BSPRL
        {
            get
            {
                return this.mTaxState.TBCapacity;
            }
        }

        public string BSPTHFP
        {
            get
            {
                if (this.mTaxState.TBRetInv == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string DriverNo
        {
            get
            {
                return (this.mTaxState.MajorVersion.ToString() + "." + this.mTaxState.MinorVersion.ToString("D2"));
            }
        }

        public int FKPJSM
        {
            get
            {
                return this.mTaxState.MachineNumber;
            }
        }

        public string HYICBuyInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICBuyInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string HYICRepDone
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepDone != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string HYICRepInfo
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepInfo != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string HYICRetInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRetInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string HYLastRepDate
        {
            get
            {
                string lastRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        lastRepDate = this.mTaxState.InvTypeInfo[i].LastRepDate;
                    }
                }
                return lastRepDate;
            }
        }

        public string HYLockedDate
        {
            get
            {
                string lockedDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        lockedDate = this.mTaxState.InvTypeInfo[i].LockedDate;
                    }
                }
                return lockedDate;
            }
        }

        public string HYNextRepDate
        {
            get
            {
                string nextRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        nextRepDate = this.mTaxState.InvTypeInfo[i].NextRepDate;
                    }
                }
                return nextRepDate;
            }
        }

        public string HZFWAuth
        {
            get
            {
                if (this.mTaxState.CompanyType == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string ICBSCGBZ
        {
            get
            {
                if (this.mTaxState.ICRepDone == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string ICBSZL
        {
            get
            {
                if (this.mTaxState.ICRepInfo == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string ICFPLYC
        {
            get
            {
                if (this.mTaxState.ICInvSegm == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string ICFXSQXX
        {
            get
            {
                if (this.mTaxState.ICAuthInfo == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string ICGRFP
        {
            get
            {
                if (this.mTaxState.ICBuyInv == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public int ICKPJH
        {
            get
            {
                return this.mKPJH;
            }
        }

        public int ICRL
        {
            get
            {
                return this.mTaxState.ICCapacity;
            }
        }

        public string ICTHFP
        {
            get
            {
                if (this.mTaxState.ICRetInv == 0)
                {
                    return "无";
                }
                return "有";
            }
        }

        public string IsCSQ
        {
            get
            {
                if (this.mTaxState.IsRepReached == 0)
                {
                    return "未到抄税期";
                }
                return "已到抄税期";
            }
        }

        public string IsFPYW
        {
            get
            {
                if (this.mTaxState.IsInvEmpty == 0)
                {
                    return "有可用发票";
                }
                return "无可用发票";
            }
        }

        public string IsHYCSQ
        {
            get
            {
                ushort isRepTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        isRepTime = this.mTaxState.InvTypeInfo[i].IsRepTime;
                    }
                }
                if (isRepTime == 0)
                {
                    return "未到抄税期";
                }
                return "已到抄税期";
            }
        }

        public string IsHYSSQ
        {
            get
            {
                ushort isLockTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 11)
                    {
                        isLockTime = this.mTaxState.InvTypeInfo[i].IsLockTime;
                    }
                }
                if (isLockTime == 0)
                {
                    return "未到锁死期";
                }
                return "已到锁死期";
            }
        }

        public string IsJDCCSQ
        {
            get
            {
                ushort isRepTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        isRepTime = this.mTaxState.InvTypeInfo[i].IsRepTime;
                    }
                }
                if (isRepTime == 0)
                {
                    return "未到抄税期";
                }
                return "已到抄税期";
            }
        }

        public string IsJDCSSQ
        {
            get
            {
                ushort isLockTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        isLockTime = this.mTaxState.InvTypeInfo[i].IsLockTime;
                    }
                }
                if (isLockTime == 0)
                {
                    return "未到锁死期";
                }
                return "已到锁死期";
            }
        }

        public string IsJSFPCSQ
        {
            get
            {
                ushort isRepTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        isRepTime = this.mTaxState.InvTypeInfo[i].IsRepTime;
                    }
                }
                if (isRepTime == 0)
                {
                    return "未到抄税期";
                }
                return "已到抄税期";
            }
        }

        public string IsJSFPSSQ
        {
            get
            {
                ushort isLockTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        isLockTime = this.mTaxState.InvTypeInfo[i].IsLockTime;
                    }
                }
                if (isLockTime == 0)
                {
                    return "未到锁死期";
                }
                return "已到锁死期";
            }
        }

        public string IsPTDZCSQ
        {
            get
            {
                ushort isRepTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        isRepTime = this.mTaxState.InvTypeInfo[i].IsRepTime;
                    }
                }
                if (isRepTime == 0)
                {
                    return "未到抄税期";
                }
                return "已到抄税期";
            }
        }

        public string IsPTDZSSQ
        {
            get
            {
                ushort isLockTime = 0;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        isLockTime = this.mTaxState.InvTypeInfo[i].IsLockTime;
                    }
                }
                if (isLockTime == 0)
                {
                    return "未到锁死期";
                }
                return "已到锁死期";
            }
        }

        public string IsSSQ
        {
            get
            {
                if (this.mTaxState.IsLockReached == 0)
                {
                    return "未到锁死期";
                }
                return "已到锁死期";
            }
        }

        public bool IsTBEnable
        {
            get
            {
                if (this.mTaxState.IsTBEnable == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public string JDCICBuyInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICBuyInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JDCICRepDone
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepDone != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JDCICRepInfo
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepInfo != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JDCICRetInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRetInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JDCLastRepDate
        {
            get
            {
                string lastRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        lastRepDate = this.mTaxState.InvTypeInfo[i].LastRepDate;
                    }
                }
                return lastRepDate;
            }
        }

        public string JDCLockedDate
        {
            get
            {
                string lockedDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        lockedDate = this.mTaxState.InvTypeInfo[i].LockedDate;
                    }
                }
                return lockedDate;
            }
        }

        public string JDCNextRepDate
        {
            get
            {
                string nextRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 12)
                    {
                        nextRepDate = this.mTaxState.InvTypeInfo[i].NextRepDate;
                    }
                }
                return nextRepDate;
            }
        }

        public string JSFPICBuyInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICBuyInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JSFPICRepDone
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepDone != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JSFPICRepInfo
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepInfo != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JSFPICRetInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRetInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string JSFPLastRepDate
        {
            get
            {
                string lastRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        lastRepDate = this.mTaxState.InvTypeInfo[i].LastRepDate;
                    }
                }
                return lastRepDate;
            }
        }

        public string JSFPLockedDate
        {
            get
            {
                string lockedDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        lockedDate = this.mTaxState.InvTypeInfo[i].LockedDate;
                    }
                }
                return lockedDate;
            }
        }

        public string JSFPNextRepDate
        {
            get
            {
                string nextRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x29)
                    {
                        nextRepDate = this.mTaxState.InvTypeInfo[i].NextRepDate;
                    }
                }
                return nextRepDate;
            }
        }

        public string KPJLX
        {
            get
            {
                if (this.mTaxState.IsMainMachine != 0)
                {
                    return "主开票机";
                }
                return "分开票机";
            }
        }

        public int LockedDays
        {
            get
            {
                return this.mTaxState.LockedDays;
            }
        }

        public string NSDJH
        {
            get
            {
                return this.mTaxState.TaxCode;
            }
        }

        public string PTDZICBuyInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICBuyInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string PTDZICRepDone
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepDone != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string PTDZICRepInfo
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRepInfo != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string PTDZICRetInv
        {
            get
            {
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        if (this.mTaxState.InvTypeInfo[i].ICRetInv != 0)
                        {
                            return "有";
                        }
                        return "无";
                    }
                }
                return "-";
            }
        }

        public string PTDZLastRepDate
        {
            get
            {
                string lastRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        lastRepDate = this.mTaxState.InvTypeInfo[i].LastRepDate;
                    }
                }
                return lastRepDate;
            }
        }

        public string PTDZLockedDate
        {
            get
            {
                string lockedDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        lockedDate = this.mTaxState.InvTypeInfo[i].LockedDate;
                    }
                }
                return lockedDate;
            }
        }

        public string PTDZNextRepDate
        {
            get
            {
                string nextRepDate = null;
                for (int i = 0; i < this.mTaxState.InvTypeInfo.Count; i++)
                {
                    if (this.mTaxState.InvTypeInfo[i].InvType == 0x33)
                    {
                        nextRepDate = this.mTaxState.InvTypeInfo[i].NextRepDate;
                    }
                }
                return nextRepDate;
            }
        }

        public string XTAuth
        {
            get
            {
                string companyAuth = this.mTaxState.CompanyAuth;
                if (companyAuth == "0000000000")
                {
                    return null;
                }
                return companyAuth;
            }
        }
    }
}

