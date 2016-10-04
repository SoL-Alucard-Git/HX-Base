namespace Aisino.Framework.Plugin.Core.Util
{
    using System;

    public class UpLoadCheckState
    {
        private static bool bool_0;
        private static bool bool_1;
        private static bool bool_2;
        private static bool bool_3;
        private static bool bool_4;

        static UpLoadCheckState()
        {
            
        }

        public UpLoadCheckState()
        {
            
        }

        public static bool CheckState()
        {
            if (!bool_0 && !bool_1)
            {
                return bool_2;
            }
            return true;
        }

        public static void SetDataMigrationState(bool bool_5)
        {
            bool_2 = bool_5;
        }

        public static void SetFpxfState(bool bool_5)
        {
            bool_0 = bool_5;
        }

        public static void SetWenBenState(bool bool_5)
        {
            bool_1 = bool_5;
        }

        public static bool ShouGongShangChuan
        {
            get
            {
                return bool_3;
            }
            set
            {
                bool_3 = value;
            }
        }

        public static bool ShouGongXiaZai
        {
            get
            {
                return bool_4;
            }
            set
            {
                bool_4 = value;
            }
        }
    }
}

