namespace Aisino.Fwkp.HomePage.AisinoDock.Move
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class StoryBoard
    {
        private Control _control;
        private List<ControlAnimation> list = new List<ControlAnimation>();
        private int MaxIndex;
        private int millisecondOne = 8;
        private Thread thread;
        private static int timeInt;

        public StoryBoard(Control control)
        {
            this._control = control;
        }

        public void Add(ControlAnimation item)
        {
            this.list.Add(item);
            if (item.animation.Num > this.MaxIndex)
            {
                this.MaxIndex = item.animation.Num;
            }
        }

        public void Add(Animation animation, Control control, string name)
        {
            ControlAnimation item = new ControlAnimation {
                animation = animation,
                control = control,
                name = name
            };
            this.Add(item);
        }

        public void Remove(ControlAnimation item)
        {
            this.list.Remove(item);
        }

        private void sta()
        {
            new System.Threading.Timer(new TimerCallback(this.TimerProc)).Change(0, this.millisecondOne);
        }

        public void Start()
        {
            this.thread = new Thread(new ThreadStart(this.sta));
            if (this.thread != null)
            {
                this.thread.Start();
            }
        }

        private void TimerProc(object state)
        {
            if (((this._control == null) || this._control.IsDisposed) || (timeInt > this.MaxIndex))
            {
                ((System.Threading.Timer) state).Dispose();
                timeInt = 0;
            }
            else
            {
                this._control.BeginInvoke(new Sta(this.too));
            }
        }

        private void too()
        {
            foreach (ControlAnimation animation in this.list)
            {
                PropertyInfo property = animation.control.GetType().GetProperty(animation.name);
                if (property != null)
                {
                    property.SetValue(animation.control, animation.animation.Change(), null);
                }
            }
            timeInt++;
        }

        public class ControlAnimation
        {
            public Animation animation { get; set; }

            public Control control { get; set; }

            public string name { get; set; }
        }

        private delegate void Sta();
    }
}

