namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [Description("输入时正则表达式验证控件")]
    public class TextBoxRegex : AisinoTXT
    {
        protected string _text;
        private bool bool_0;
        private IContainer icontainer_0;
        private int int_0;
        private string string_0;

        public TextBoxRegex()
        {
            
            this.string_0 = string.Empty;
            this.bool_0 = true;
            this.method_0();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this._text = this.Text;
            this.int_0 = base.SelectionStart;
            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.string_0) && ((this.Text != string.Empty) || !this.bool_0)) && !Regex.IsMatch(this.Text, this.string_0))
                {
                    this.Text = this._text;
                    base.SelectionStart = this.int_0;
                }
                base.OnTextChanged(e);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [Category("验证表达式"), DefaultValue(true), Description("是否能够为空，默认为True")]
        public bool IsEmpty
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        [Description("用于验证的正则表达式字符串"), Category("验证表达式")]
        public string RegexText
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
    }
}

