namespace Aisino.Framework.Startup.Login
{
    using System;
    using System.Collections.Generic;

    public interface ILogin
    {
        bool Login(bool bool_0, string string_0);
        bool Login(string string_0, string string_1);

        string Bk1 { get; }

        string Bk2 { get; }

        string Bk3 { get; }

        List<string> Gnqx { get; }

        bool IsAdmin { get; }

        List<string> Jsqx { get; }

        string Yhdm { get; }

        string Yhmc { get; }
    }
}

