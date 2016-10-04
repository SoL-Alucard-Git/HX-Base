namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;

    public class Common
    {
        protected static ILog log;

        static Common()
        {
            
            log = LogUtil.GetLogger<Common>();
        }

        public Common()
        {
            
        }

        public static Bitmap Base64ToBitMap(string string_0)
        {
            try
            {
                return new Bitmap(new MemoryStream(Convert.FromBase64String(string_0)));
            }
            catch
            {
                return null;
            }
        }

        public static string BitmapToBase64(Bitmap bitmap_0)
        {
            MemoryStream stream = new MemoryStream();
            bitmap_0.Save(stream, ImageFormat.Gif);
            string str = Convert.ToBase64String(stream.ToArray());
            stream.Close();
            return str;
        }

        public static float MillimeterToPx(float float_0, int int_0)
        {
            return (float) (((double) (float_0 * int_0)) / 25.4);
        }

        public static float PxToMillimeter(float float_0, int int_0)
        {
            return (float) ((float_0 * 25.4) / ((double) int_0));
        }

        public static bool ToBool(string string_0)
        {
            bool result = true;
            bool.TryParse(string_0, out result);
            return result;
        }

        public static Color ToColor(string string_0)
        {
            string[] strArray = string_0.Split(new char[] { ',' });
            if (strArray.Length == 3)
            {
                return Color.FromArgb(ToInt(strArray[0]), ToInt(strArray[1]), ToInt(strArray[2]));
            }
            return Color.FromName(string_0);
        }

        public static float ToFloat(string string_0)
        {
            float result = 0f;
            float.TryParse(string_0, out result);
            return result;
        }

        public static Font ToFont(string string_0)
        {
            string[] array = new string[] { string.Empty, string.Empty, string.Empty };
            string_0.Split(new char[] { ',' }).CopyTo(array, 0);
            string familyName = "宋体";
            if (array[0] != string.Empty)
            {
                familyName = array[0];
            }
            float result = 11f;
            if (array[1] != string.Empty)
            {
                float.TryParse(array[1], out result);
            }
            FontStyle regular = FontStyle.Regular;
            foreach (string str3 in array[2].Split(new char[] { '|' }))
            {
                string str2 = str3;
                if (str2 != null)
                {
                    if (str2 == "Bold")
                    {
                        regular |= FontStyle.Bold;
                    }
                    else if (str2 == "Italic")
                    {
                        regular |= FontStyle.Italic;
                    }
                    else if (!(str2 == "Underline"))
                    {
                        if (str2 == "Strikeout")
                        {
                            regular |= FontStyle.Strikeout;
                        }
                    }
                    else
                    {
                        regular |= FontStyle.Underline;
                    }
                }
            }
            return new Font(familyName, result, regular);
        }

        public static int ToInt(string string_0)
        {
            int result = 0;
            int.TryParse(string_0, out result);
            return result;
        }

        public static Margins ToMargin(string string_0)
        {
            string[] strArray = string_0.Split(new char[] { ',' });
            int[] numArray2 = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (strArray.Length > i)
                {
                    numArray2[i] = ToInt(strArray[i]);
                }
            }
            return new Margins(numArray2[0], numArray2[1], numArray2[2], numArray2[3]);
        }

        public static Point ToPoint(string string_0)
        {
            string[] array = new string[] { "0", "0" };
            string_0.Split(new char[] { ',' }).CopyTo(array, 0);
            return new Point(ToInt(array[0]), ToInt(array[1]));
        }

        public static PointF ToPointF(string string_0)
        {
            string[] array = new string[] { "0", "0" };
            string_0.Split(new char[] { ',' }).CopyTo(array, 0);
            return new PointF(ToFloat(array[0]), ToFloat(array[1]));
        }

        public static string[] ZheHang(string string_0, string string_1)
        {
            ReadXml xml = ReadXml.Get();
            if (xml.ZheHang.ContainsKey(string_1))
            {
                PrintZheHangModel model = xml.ZheHang[string_1];
                string configId = model.ConfigId;
                string tempId = model.TempId;
                return ZheHang(string_0, configId, tempId);
            }
            return null;
        }

        public static string[] ZheHang(string string_0, string string_1, string string_2)
        {
            Canvas canvas = new Canvas(ReadXml.Get()[string_1].CanvasPath);
            if (canvas != null)
            {
                AisinoPrintLabel label = canvas[string_2] as AisinoPrintLabel;
                if (label != null)
                {
                    return label.ZheHang(string_0);
                }
            }
            return null;
        }
    }
}

