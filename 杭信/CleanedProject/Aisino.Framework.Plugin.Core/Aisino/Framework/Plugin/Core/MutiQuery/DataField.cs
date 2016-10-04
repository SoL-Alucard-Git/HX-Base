namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataField : IEquatable<DataField>
    {
        [CompilerGenerated]
        private FieldType fieldType_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;

        public DataField(string string_2, string string_3)
        {
            
            this.Name = string_2;
            this.Field = string_3;
        }

        public bool Equals(DataField other)
        {
            if (other == null)
            {
                return false;
            }
            return (((this.Name == other.Name) && (this.Field == other.Field)) && (this.DataType == other.DataType));
        }

        public override string ToString()
        {
            return this.Name;
        }

        public FieldType DataType
        {
            [CompilerGenerated]
            get
            {
                return this.fieldType_0;
            }
            [CompilerGenerated]
            set
            {
                this.fieldType_0 = value;
            }
        }

        public string Field
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

