namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;

    public class Condition
    {
        private FieldType fieldType_0;
        private string string_0;
        private string string_1;

        public Condition()
        {
            
        }

        public override string ToString()
        {
            return this.Name;
        }

        public virtual string Expression
        {
            get
            {
                return this.string_1;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public FieldType ValueType
        {
            get
            {
                return this.fieldType_0;
            }
            set
            {
                this.fieldType_0 = value;
            }
        }
    }
}

