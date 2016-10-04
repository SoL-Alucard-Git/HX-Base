namespace ns2
{
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;

    internal class Class19
    {
        private char[] char_0;
        private int int_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;

        public Class19()
        {
            
            this.string_1 = "";
            this.string_2 = "注册成功";
            this.string_3 = "";
            this.string_4 = "";
            this.int_0 = 0x10;
        }

        public string method_0()
        {
            this.string_0 = this.method_9();
            return this.string_0;
        }

        public void method_1(string string_7)
        {
            this.string_0 = string_7;
        }

        public string method_10()
        {
            return this.method_12();
        }

        public void method_11(string string_7)
        {
            this.string_6 = string_7;
        }

        private string method_12()
        {
            char[] chArray = new string(' ', this.int_0).ToCharArray();
            for (int i = 0; i < this.int_0; i++)
            {
                int num2 = this.char_0[i] % '$';
                char ch = (num2 < 10) ? ((char) (0x30 + num2)) : ((char) ((0x41 + num2) - 10));
                switch ((chArray[i] = ch))
                {
                    case 'E':
                        chArray[i] = 'F';
                        break;

                    case 'I':
                        chArray[i] = '1';
                        break;

                    case 'O':
                        chArray[i] = '0';
                        break;

                    case 'Z':
                        chArray[i] = '2';
                        break;
                }
            }
            return new string(chArray);
        }

        public List<string> method_13(string string_7, string string_8, bool bool_0)
        {
            List<string> list2;
            try
            {
                this.method_8(string_7);
                this.string_5 = string_8;
                string str = this.method_7();
                string str2 = this.string_5;
                List<string> list = new List<string>();
                int num = (bool_0 ? 0 : str.Length) + 1;
                for (int i = 0; i < num; i++)
                {
                    this.method_8((i == 0) ? "" : str.Substring(0, i));
                    this.string_5 = str2;
                    list.Add(this.method_0());
                    this.string_5 = "";
                    list.Add(this.method_0());
                }
                this.method_8(str);
                this.string_5 = str2;
                list2 = list;
            }
            catch
            {
                throw;
            }
            return list2;
        }

        private bool method_14()
        {
            byte[] buffer = new byte[this.int_0];
            string str = this.method_2() + this.method_5();
            Class23 class2 = new Class23();
            byte[] buffer2 = CommonTool.StringToBytes(str);
            class2.method_6(buffer2, (uint) buffer2.Length);
            class2.method_7(buffer, (uint) this.int_0);
            char[] chArray = new string('1', this.int_0).ToCharArray();
            for (int i = 0; i < this.int_0; i++)
            {
                chArray[i] = (char) ((buffer[i] % 0xce) + 0x30);
            }
            this.char_0 = chArray;
            return true;
        }

        public string method_2()
        {
            return this.string_1;
        }

        public void method_3(string string_7)
        {
            if (this.string_1 != string_7)
            {
                if (this.method_4().Length > 0)
                {
                    this.string_1 = string_7.PadRight(this.int_0, ' ');
                }
                else
                {
                    this.string_1 = new string(' ', this.int_0);
                }
            }
        }

        private string method_4()
        {
            if (!string.IsNullOrEmpty(this.string_2))
            {
                return "注册成功";
            }
            return "";
        }

        public string method_5()
        {
            return this.string_3;
        }

        public void method_6(string string_7)
        {
            this.string_3 = string_7;
        }

        private string method_7()
        {
            return this.string_4;
        }

        private void method_8(string string_7)
        {
            this.string_4 = string_7;
        }

        private string method_9()
        {
            this.method_3("M" + this.method_7());
            this.method_6(this.string_5);
            this.method_14();
            return this.method_10();
        }
    }
}

