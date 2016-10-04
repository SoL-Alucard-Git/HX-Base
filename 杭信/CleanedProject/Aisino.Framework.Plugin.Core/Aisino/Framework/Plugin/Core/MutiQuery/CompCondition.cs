namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;

    public class CompCondition : Condition
    {
        private Condition condition_0;
        private Condition condition_1;
        private LogicOperator logicOperator_0;
        private string string_2;

        public CompCondition()
        {
            
        }

        public override string Expression
        {
            get
            {
                this.string_2 = "";
                if (((this.condition_0 != null) && !string.IsNullOrEmpty(this.condition_0.Expression)) && ((this.condition_1 != null) && !string.IsNullOrEmpty(this.condition_1.Expression)))
                {
                    this.string_2 = string.Format("({0} {1} {2})", this.condition_0.Expression, EnumHelper.GetDescription((ArithOperator)logicOperator_0), this.condition_1.Expression);
                }
                return this.string_2;
            }
        }

        public Condition LeftCondition
        {
            get
            {
                return this.condition_0;
            }
            set
            {
                this.condition_0 = value;
            }
        }

        public LogicOperator Operator
        {
            get
            {
                return this.logicOperator_0;
            }
            set
            {
                this.logicOperator_0 = value;
            }
        }

        public Condition RightCondition
        {
            get
            {
                return this.condition_1;
            }
            set
            {
                this.condition_1 = value;
            }
        }
    }
}

