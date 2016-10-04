namespace Aisino.Fwkp.Bmgl.Model
{
    using System;
    using System.Collections.Generic;

    public class DataResult
    {
        private int _failCount;
        private Dictionary<object, string> _failItems;
        private SuccessFlags _result;
        private int _successCount;
        private Dictionary<object, string> _successItems;
        private int _totalCount;

        public int FailCount
        {
            get
            {
                return this._failCount;
            }
            set
            {
                this._failCount = value;
            }
        }

        public Dictionary<object, string> FailItems
        {
            get
            {
                return this._failItems;
            }
            set
            {
                this._failItems = value;
            }
        }

        public SuccessFlags Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

        public int SuccessCount
        {
            get
            {
                return this._successCount;
            }
            set
            {
                this._successCount = value;
            }
        }

        public Dictionary<object, string> SuccessItems
        {
            get
            {
                return this._successItems;
            }
            set
            {
                this._successItems = value;
            }
        }

        public int TotalCount
        {
            get
            {
                return this._totalCount;
            }
            set
            {
                this._totalCount = value;
                if (this._totalCount == this._successCount)
                {
                    this._result = SuccessFlags.AllSuccess;
                }
                else if (this._totalCount == this._failCount)
                {
                    this._result = SuccessFlags.NoneSuccess;
                }
                else
                {
                    this._result = SuccessFlags.PartialSuccess;
                }
            }
        }
    }
}

