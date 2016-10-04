namespace Aisino.Fwkp.HomePage.AisinoDock.Move
{
    using System;
    using System.Drawing;

    public class AnimationPoint : Animation
    {
        private Point From;
        private Point To;
        private Point Value;
        private AnimationInt x;
        private AnimationInt y;

        public AnimationPoint(Point From, Point To, int SpanTime) : base(SpanTime)
        {
            this.From = From;
            this.To = To;
            this.Value = From;
            this.x = new AnimationInt(From.X, To.X, SpanTime);
            base.Num = this.x.Num;
            this.y = new AnimationInt(From.Y, To.Y, SpanTime);
            if (this.y.Num > base.Num)
            {
                base.Num = this.y.Num;
            }
        }

        public override object Change()
        {
            return new Point((int) this.x.Change(), (int) this.y.Change());
        }
    }
}

