namespace ns2
{
    using Aisino.FTaxBase;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class Class21
    {
        private static Class21 class21_0;
        private CTaxCardMode ctaxCardMode_0;
        private int int_0;
        private string string_0;
        private string string_1;
        private string string_2;

        private Class21(string string_3)
        {
            
            this.string_0 = Path.GetTempPath() + "Tmp1AF.tmp";
            this.string_1 = "AddedRealTax.dll";
            this.string_2 = "";
            this.ctaxCardMode_0 = CTaxCardMode.tcmHave;
            this.string_1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), this.string_1);
            this.string_2 = string_3;
        }

        [DllImport("Kernel32")]
        private static extern int FreeLibrary(int int_1);
        [DllImport("Kernel32")]
        private static extern IntPtr GetProcAddress(int int_1, string string_3);
        [DllImport("Kernel32")]
        private static extern int LoadLibrary(string string_3);
        public CTaxCardMode method_0()
        {
            return this.ctaxCardMode_0;
        }

        public void method_1(CTaxCardMode ctaxCardMode_1)
        {
            this.ctaxCardMode_0 = ctaxCardMode_1;
        }

        internal bool method_2()
        {
            this.string_0 = "";
            string str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            Random random = new Random(DateTime.Now.Millisecond);
            this.string_0 = Path.Combine(Path.GetTempPath(), str + random.Next().ToString() + ".tmp");
            this.method_8();
            this.int_0 = LoadLibrary(this.string_0);
            if (this.int_0 == 0)
            {
                Class20.smethod_2("加载DLL库不正确，检查路径下dll是否存在？" + this.string_0);
                return false;
            }
            return true;
        }

        internal void method_3()
        {
            this.int_0 = FreeLibrary(this.int_0);
            this.method_9();
        }

        internal int method_4(int int_1, string string_3, int int_2)
        {
            IntPtr procAddress = GetProcAddress(this.int_0, "OpenTax");
            if (procAddress == IntPtr.Zero)
            {
                Class20.smethod_7("库未加载");
            }
            Delegate6 delegateForFunctionPointer = (Delegate6) Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Delegate6));
            if (delegateForFunctionPointer == null)
            {
                Class20.smethod_7("调用函数委托不能是空");
            }
            return delegateForFunctionPointer(int_1, string_3, int_2);
        }

        internal int method_5(int int_1)
        {
            IntPtr procAddress = GetProcAddress(this.int_0, "OpenTax");
            if (procAddress == IntPtr.Zero)
            {
                Class20.smethod_7("库未加载");
            }
            Delegate7 delegateForFunctionPointer = (Delegate7) Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Delegate7));
            if (delegateForFunctionPointer == null)
            {
                Class20.smethod_7("调用函数委托不能是空");
            }
            return delegateForFunctionPointer(int_1);
        }

        internal int method_6()
        {
            IntPtr procAddress = GetProcAddress(this.int_0, "CloseTax");
            if (procAddress == IntPtr.Zero)
            {
                Class20.smethod_7("库未加载");
            }
            Delegate8 delegateForFunctionPointer = (Delegate8) Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Delegate8));
            if (delegateForFunctionPointer == null)
            {
                Class20.smethod_7("调用函数委托不能是空");
            }
            int num = delegateForFunctionPointer();
            this.method_3();
            return num;
        }

        internal int method_7(byte[] byte_0, out IntPtr intptr_0)
        {
            IntPtr procAddress = GetProcAddress(this.int_0, "CallTax");
            if (procAddress == IntPtr.Zero)
            {
                if (this.ctaxCardMode_0 == CTaxCardMode.tcmHave)
                {
                    Class20.smethod_7("库未加载");
                }
                else if (this.ctaxCardMode_0 == CTaxCardMode.tcmNone)
                {
                    Class20.smethod_7("无卡模式下用户不能调用金税设备功能");
                }
            }
            Delegate9 delegateForFunctionPointer = (Delegate9) Marshal.GetDelegateForFunctionPointer(procAddress, typeof(Delegate9));
            if (delegateForFunctionPointer == null)
            {
                Class20.smethod_7("调用函数委托不能是空");
            }
            return delegateForFunctionPointer(byte_0, out intptr_0);
        }

        private void method_8()
        {
            string str = this.string_2;
            if (this.string_2.Length < 15)
            {
                for (int i = 0; i < (15 - this.string_2.Length); i++)
                {
                    str = str + "0";
                }
            }
            new TestIdea(str).FileCrypto(1, this.string_1, this.string_0, 0x100);
        }

        private void method_9()
        {
            if (File.Exists(this.string_0))
            {
                File.Delete(this.string_0);
            }
        }

        internal static Class21 smethod_0(string string_3)
        {
            if (class21_0 == null)
            {
                class21_0 = new Class21(string_3);
            }
            return class21_0;
        }

        private delegate int Delegate6(int Address, string taxCode, int machine);

        private delegate int Delegate7(int Address);

        private delegate int Delegate8();

        private delegate int Delegate9(byte[] in_str, out IntPtr pValue);
    }
}

