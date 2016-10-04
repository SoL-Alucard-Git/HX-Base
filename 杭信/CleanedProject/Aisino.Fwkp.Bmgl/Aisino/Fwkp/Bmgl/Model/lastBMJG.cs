namespace Aisino.Fwkp.Bmgl.Model
{
    using System;

    public class lastBMJG
    {
        public string BM;
        public bool IsInvalid;
        public ResultType Result;

        public lastBMJG(string bm, ResultType resultType)
        {
            this.BM = bm;
            this.Result = resultType;
        }

        public lastBMJG(string bm, bool isInvalid)
        {
            this.BM = bm;
            this.IsInvalid = isInvalid;
        }
    }
}

