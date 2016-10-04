namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class FontSetupUtil
    {
        public FontSetupUtil()
        {
            
        }

        [DllImport("gdi32")]
        public static extern int AddFontResource(string string_0);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int int_0, uint uint_0, int int_1, int int_2);
        public static void SetUpFonts()
        {
            try
            {
                string str = "OCRAEXT_0.TTF";
                string str2 = "OCRAEXT_0";
                string sourceFileName = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), string.Format(@"Config\Fonts\{0}", str));
                string path = Path.Combine(Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System).TrimEnd(new char[] { '\\' })), "Fonts"), str);
                if (!File.Exists(path))
                {
                    File.Copy(sourceFileName, path);
                    AddFontResource(path);
                    SendMessage(0xffff, 0x1d, 0, 0);
                    WriteProfileString("fonts", str2 + "(TrueType)", str);
                }
            }
            catch (Exception)
            {
            }
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int WriteProfileString(string string_0, string string_1, string string_2);
    }
}

