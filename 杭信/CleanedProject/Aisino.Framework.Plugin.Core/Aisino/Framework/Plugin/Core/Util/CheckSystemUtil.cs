namespace Aisino.Framework.Plugin.Core.Util
{
    using System;

    public class CheckSystemUtil
    {
        public CheckSystemUtil()
        {
            
        }

        public static string GetOperateName()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            PlatformID platform = oSVersion.Platform;
            int major = oSVersion.Version.Major;
            int minor = oSVersion.Version.Minor;
            string str = string.Empty;
            switch (platform)
            {
                case PlatformID.Win32Windows:
                {
                    if (major != 4)
                    {
                        return str;
                    }
                    int num3 = minor;
                    switch (num3)
                    {
                        case 0:
                            return "Windows95";

                        case 10:
                            return "Windows98";
                    }
                    if (num3 != 90)
                    {
                        return "未知";
                    }
                    return "WindowsMe";
                }
                case PlatformID.Win32NT:
                    switch (major)
                    {
                        case 3:
                            return "WindowsNT3.5";

                        case 4:
                            return "WindowsNT4.0";

                        case 5:
                            if (minor != 0)
                            {
                                switch (minor)
                                {
                                    case 1:
                                        return "WindowsXP";

                                    case 2:
                                        return "Windows2003";
                                }
                                return "未知";
                            }
                            return "Windows2000";

                        case 6:
                            if (minor != 0)
                            {
                                switch (minor)
                                {
                                    case 1:
                                        return "Windows7";

                                    case 2:
                                        return "Windows8";
                                }
                                return "未知";
                            }
                            return "WindowsVista";
                    }
                    str = "未知";
                    break;
            }
            return str;
        }
    }
}

