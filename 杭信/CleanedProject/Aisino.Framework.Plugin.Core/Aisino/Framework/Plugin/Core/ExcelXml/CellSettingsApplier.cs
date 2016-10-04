namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class CellSettingsApplier
    {
        internal Styles styles_0;

        internal CellSettingsApplier()
        {
            
        }

        internal CellSettingsApplier(Styles styles_1)
        {
            
            if (styles_1 == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.styles_0 = styles_1;
        }

        internal object method_0(Delegate35 delegate35_0)
        {
            if (this.vmethod_0() == null)
            {
                return delegate35_0(this.styles_0.vmethod_2());
            }
            XmlStyle styles = this.vmethod_0().method_6(this.styles_0.StyleID);
            return delegate35_0(styles);
        }

        internal void method_1(Delegate35 delegate35_0)
        {
            //Styles.Delegate36 delegate2 = null;
            //Delegate35 setDelegate = delegate35_0;
            //if (this.vmethod_0() == null)
            //{
            //    if (delegate2 == null)
            //    {
            //        <>c__DisplayClass2 class2;
            //        delegate2 = new Styles.Delegate36(class2.<SetCellStyleProperty>b__0);
            //    }
            //    this.styles_0.vmethod_1(delegate2);
            //}
            //else
            //{
            //    XmlStyle styles = new XmlStyle(this.vmethod_0().method_6(this.styles_0.StyleID));
            //    setDelegate(styles);
            //    this.styles_0.StyleID = this.vmethod_0().method_8(styles);
            //}
        }

        internal virtual ExcelXmlWorkbook vmethod_0()
        {
            return this.styles_0.vmethod_0();
        }

        internal delegate object Delegate35(IStyle styles);
    }
}

