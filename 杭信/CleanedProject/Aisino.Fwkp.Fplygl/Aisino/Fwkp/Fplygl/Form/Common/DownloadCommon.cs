namespace Aisino.Fwkp.Fplygl.Form.Common
{
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class DownloadCommon
    {
        public static bool CheckEmpty(byte[] crypt)
        {
            for (int i = 0; i < 10; i++)
            {
                if (crypt[i] != 0xff)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckRepeat(InvVolumeApp locked, InvVolumeApp tarInv)
        {
            return (((locked.TypeCode == tarInv.TypeCode) && (locked.HeadCode == tarInv.HeadCode)) && (locked.Number == tarInv.Number));
        }

        public static string GetDecimalStr(byte[] hash)
        {
            string str = string.Empty;
            foreach (byte num in hash)
            {
                str = str + num.ToString().PadLeft(3, '0');
            }
            return str;
        }

        public static List<InvVolumeApp> GetElectricDownloadVolumes(byte type, int amount, int volumeMaxAmount = 0xffff)
        {
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            int num = amount % volumeMaxAmount;
            int num2 = amount / volumeMaxAmount;
            for (int i = 0; i < num2; i++)
            {
                InvVolumeApp item = new InvVolumeApp {
                    InvType = type,
                    Number = Convert.ToUInt16(volumeMaxAmount)
                };
                list.Add(item);
            }
            if (num != 0)
            {
                InvVolumeApp app2 = new InvVolumeApp {
                    InvType = type,
                    Number = Convert.ToUInt16(num)
                };
                list.Add(app2);
            }
            return list;
        }

        public static InvoiceType Type2Enum(byte type)
        {
            switch (type)
            {
                case 0:
                    return 0;

                case 2:
                    return 2;

                case 11:
                    return 11;

                case 12:
                    return 12;

                case 0x29:
                    return 0x29;

                case 0x33:
                    return 0x33;
            }
            return 1;
        }
    }
}

