namespace Aisino.Fwkp.Sjbf.Common
{
    using System;
    using System.Collections;
    using System.IO;

    internal class FileSort : IComparer
    {
        private FileAsc _fileasc;
        private FileOrder _fileorder;

        public FileSort() : this(FileOrder.Name, FileAsc.Asc)
        {
        }

        public FileSort(FileOrder fileorder) : this(fileorder, FileAsc.Asc)
        {
        }

        public FileSort(FileOrder fileorder, FileAsc fileasc)
        {
            this._fileorder = fileorder;
            this._fileasc = fileasc;
        }

        public int Compare(object x, object y)
        {
            FileInfo info = x as FileInfo;
            FileInfo info2 = y as FileInfo;
            if ((info == null) || (info2 == null))
            {
                throw new ArgumentException("参数不是FileInfo类实例.");
            }
            if (this._fileasc == FileAsc.Desc)
            {
                FileInfo info3 = info;
                info = info2;
                info2 = info3;
            }
            switch (this._fileorder)
            {
                case FileOrder.Name:
                    return info.Name.CompareTo(info2.Name);

                case FileOrder.Length:
                    return info.Length.CompareTo(info2.Length);

                case FileOrder.Extension:
                    return info.Extension.CompareTo(info2.Extension);

                case FileOrder.CreationTime:
                    return info.CreationTime.CompareTo(info2.CreationTime);

                case FileOrder.LastAccessTime:
                    return info.LastAccessTime.CompareTo(info2.LastAccessTime);

                case FileOrder.LastWriteTime:
                    return info.LastWriteTime.CompareTo(info2.LastWriteTime);
            }
            return 0;
        }
    }
}

