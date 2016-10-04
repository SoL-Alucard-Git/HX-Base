namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class Parameter
    {
        [CompilerGenerated]
        private object object_0;
        [CompilerGenerated]
        private Aisino.Framework.Plugin.Core.ExcelXml.ParameterType parameterType_0;

        internal Parameter(Formula formula_0)
        {
            
            this.ParameterType = Aisino.Framework.Plugin.Core.ExcelXml.ParameterType.Formula;
            this.Value = formula_0;
        }

        internal Parameter(Range range_0)
        {
            
            this.ParameterType = Aisino.Framework.Plugin.Core.ExcelXml.ParameterType.Range;
            this.Value = range_0;
        }

        internal Parameter(char char_0)
        {
            
            this.ParameterType = Aisino.Framework.Plugin.Core.ExcelXml.ParameterType.String;
            this.Value = char_0;
        }

        internal Parameter(string string_0)
        {
            
            this.ParameterType = Aisino.Framework.Plugin.Core.ExcelXml.ParameterType.String;
            this.Value = string_0;
        }

        public Aisino.Framework.Plugin.Core.ExcelXml.ParameterType ParameterType
        {
            [CompilerGenerated]
            get
            {
                return this.parameterType_0;
            }
            [CompilerGenerated]
            private set
            {
                this.parameterType_0 = value;
            }
        }

        public object Value
        {
            [CompilerGenerated]
            get
            {
                return this.object_0;
            }
            [CompilerGenerated]
            private set
            {
                this.object_0 = value;
            }
        }
    }
}

