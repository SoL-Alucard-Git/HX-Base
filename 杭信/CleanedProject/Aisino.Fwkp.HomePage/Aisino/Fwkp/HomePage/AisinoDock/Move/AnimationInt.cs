namespace Aisino.Fwkp.HomePage.AisinoDock.Move
{
    using System;
    using System.Runtime.CompilerServices;

    public class AnimationInt : Animation
    {
        private int From;
        private int To;
        private int Value;

        public AnimationInt(int From, int To, int SpanTime) : base(SpanTime)
        {
            this.From = From;
            this.To = To;
            this.Value = From;
            this.ChangeValue = (To - From) / base.Num;
        }

        public override object Change()
        {
            this.Value += this.ChangeValue;
            int num = (this.To > this.From) ? this.To : this.From;
            int num2 = (this.To > this.From) ? this.From : this.To;
            if (this.Value > num)
            {
                this.Value = num;
                return this.Value;
            }
            if (this.Value < num2)
            {
                this.Value = num2;
                return this.Value;
            }
            return this.Value;
        }

        public int ChangeValue { get; private set; }
    }
}

