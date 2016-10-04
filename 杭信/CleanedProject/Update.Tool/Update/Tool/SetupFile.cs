namespace Update.Tool
{
    using System;

    public class SetupFile
    {
        public bool bNormal;
        public string Folder;
        public string Kind;
        public string Name;
        public string Ver;

        public SetupFile()
        {
           
            this.Name = string.Empty;
            this.Ver = string.Empty;
            this.Kind = string.Empty;
            this.Folder = string.Empty;
            this.bNormal = false;
        }
    }
}

