namespace Aisino.Fwkp.HomePage.AisinoDock.Move
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Animation
    {
        protected int millisecondOne;

        public Animation()
        {
            this.millisecondOne = 8;
        }

        public Animation(int SpanTime)
        {
            this.millisecondOne = 8;
            this.Time = SpanTime;
            int num = SpanTime / this.millisecondOne;
            if ((SpanTime % this.millisecondOne) != 0)
            {
                num++;
            }
            this.Num = num;
        }

        public abstract object Change();

        public int Num { get; protected set; }

        public int Time { get; set; }
    }
}

