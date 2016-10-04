namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class Project
    {
        [DataMember(Order=0)]
        public string key1 { get; set; }

        [DataMember(Order=1)]
        public string key2 { get; set; }
    }
}

