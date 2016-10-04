namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class CLSIme
    {
        public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
        public const int IME_CMODE_FULLSHAPE = 8;

        public CLSIme()
        {
            
        }

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr intptr_0);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr intptr_0, ref int int_0, ref int int_1);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr intptr_0);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr intptr_0, bool bool_0);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr intptr_0, int int_0);
        public static void SetIme(IntPtr intptr_0)
        {
            smethod_6(intptr_0);
        }

        public static void SetIme(Control control_0)
        {
            smethod_2(control_0);
        }

        public static void SetIme(Form form_0)
        {
            form_0.Paint += Form_0_Paint; 
            //+= new PaintEventHandler(CLSIme.smethod_3);
            form_0.Activated += smethod_0;
            smethod_2(form_0);
            smethod_1(form_0);
        }

        private static void Form_0_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void smethod_0(object control_0, object object_0)
        {
            smethod_2(control_0 as Control);
        }

        private static void smethod_1(Control control_0)
        {
            try
            {
                foreach (Control control in control_0.Controls)
                {
                    if (control is ToolStrip)
                    {
                        ToolStrip strip = control as ToolStrip;
                        strip.BackgroundImage = Class131.smethod_45();
                    }
                    if (control.Controls.Count > 0)
                    {
                        smethod_1(control);
                    }
                }
            }
            catch
            {
            }
        }

        private static void smethod_2(Control control_0)
        {
            control_0.Enter += Control_0_Enter;
            foreach (Control control in control_0.Controls)
            {
                smethod_2(control);
            }
        }

        private static void Control_0_Enter(object sender, EventArgs e)
        {
            smethod_5(sender as Control);
        }

        private static void smethod_4(Control control_0, object object_0)
        {
            smethod_5(control_0);
        }

        private static void smethod_5(Control control_0)
        {
            Control control = control_0;
            smethod_6(control.Handle);
        }

        private static void smethod_6(IntPtr intptr_0)
        {
            IntPtr ptr = ImmGetContext(intptr_0);
            if (ImmGetOpenStatus(ptr))
            {
                int num = 0;
                int num2 = 0;
                if (ImmGetConversionStatus(ptr, ref num, ref num2) && ((num & 8) > 0))
                {
                    ImmSimulateHotKey(intptr_0, 0x11);
                }
            }
        }
    }
}

