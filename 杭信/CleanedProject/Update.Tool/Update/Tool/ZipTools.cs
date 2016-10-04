namespace Update.Tool
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;
    using Update.Model;

    public class ZipTools
    {
        public ZipTools()
        {
           
        }

        public static byte[] CompressToZipFile(byte[] param)
        {
            MemoryStream stream = new MemoryStream();
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true);
            stream2.Write(param, 0, param.Length);
            stream2.Close();
            return stream.ToArray();
        }

        public static bool DoZip(string _strFileTobeZipped, string _strZippedName, string _strPassword)
        {
            if (Directory.Exists(_strFileTobeZipped))
            {
                return ZipFileDictory(_strFileTobeZipped, _strZippedName, _strPassword);
            }
            return (File.Exists(_strFileTobeZipped) && ZipFile(_strFileTobeZipped, _strZippedName, _strPassword));
        }

        public static byte[] UnCompressZipFile(byte[] param)
        {
            int num;
            MemoryStream stream = new MemoryStream(param);
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
            MemoryStream stream3 = new MemoryStream();
            byte[] buffer = new byte[0x400];
        Label_002C:
            num = stream2.Read(buffer, 0, buffer.Length);
            if (num > 0)
            {
                stream3.Write(buffer, 0, num);
                goto Label_002C;
            }
            stream2.Close();
            return stream3.ToArray();
        }

        public static void UnZip(string _strToBeUnzipName, string _strUnzipDict, string _strPasswd)
        {
            try
            {
                if (File.Exists(_strToBeUnzipName))
                {
                    ZipEntry entry;
                    if (!Directory.Exists(_strUnzipDict))
                    {
                        Directory.CreateDirectory(_strUnzipDict);
                    }
                    ZipInputStream stream = new ZipInputStream(File.OpenRead(_strToBeUnzipName));
                    while ((entry = stream.GetNextEntry()) != null)
                    {
                        string fileName = Path.GetFileName(entry.Name);
                        string str3 = entry.Name.Replace('/', '\\');
                        if (!(fileName != string.Empty))
                        {
                            continue;
                        }
                        string path = Path.Combine(_strUnzipDict, str3);
                        string str = path.Substring(0, path.LastIndexOf(@"\"));
                        if (!Directory.Exists(str))
                        {
                            Directory.CreateDirectory(str);
                        }
                        FileStream stream2 = File.Create(path);
                        int count = 0x800;
                        byte[] buffer = new byte[0x800];
                        goto Label_00B3;
                    Label_00A8:
                        stream2.Write(buffer, 0, count);
                    Label_00B3:
                        count = stream.Read(buffer, 0, buffer.Length);
                        if (count > 0)
                        {
                            goto Label_00A8;
                        }
                        stream2.Close();
                    }
                    stream.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UnZipDownloadInfo(string FileToUpZip, string ZipedFolder)
        {
            if (File.Exists(FileToUpZip))
            {
                if (!Directory.Exists(ZipedFolder))
                {
                    if (File.Exists(ZipedFolder))
                    {
                        File.Delete(ZipedFolder);
                    }
                    Directory.CreateDirectory(ZipedFolder);
                }
                new FastZip().ExtractZip(FileToUpZip, ZipedFolder, FastZip.Overwrite.Always, null, "", "", true);
            }
        }

        public static bool ZipDownloadInfo(DownloadInfo downInfo, string filePath, string ZipedFile)
        {
            bool flag = true;
            if (downInfo.FileList.Count <= 0)
            {
                return false;
            }
            ICSharpCode.SharpZipLib.Zip.ZipFile file = null;
            try
            {
                file = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(ZipedFile);
                file.BeginUpdate();
                foreach (SoftFileInfo info in downInfo.FileList)
                {
                    string fileName = Path.Combine(filePath, info.RelativePath);
                    file.Add(fileName);
                }
                file.CommitUpdate();
                file.Close();
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return flag;
        }

        public static bool ZipFile(string _strToBeZippedName, string _strZippedName, string _strPasswd)
        {
            if (!File.Exists(_strToBeZippedName))
            {
                throw new FileNotFoundException("文件: " + _strToBeZippedName + " 不存在!");
            }
            FileStream baseOutputStream = null;
            ZipOutputStream stream2 = null;
            ZipEntry entry = null;
            bool flag = true;
            try
            {
                baseOutputStream = File.OpenRead(_strToBeZippedName);
                byte[] buffer = new byte[baseOutputStream.Length];
                baseOutputStream.Read(buffer, 0, buffer.Length);
                baseOutputStream.Close();
                baseOutputStream = File.Create(_strZippedName);
                stream2 = new ZipOutputStream(baseOutputStream) {
                    Password = _strPasswd
                };
                entry = new ZipEntry(Path.GetFileName(_strToBeZippedName));
                stream2.PutNextEntry(entry);
                stream2.SetLevel(6);
                stream2.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (entry != null)
                {
                    entry = null;
                }
                if (stream2 != null)
                {
                    stream2.Finish();
                    stream2.Close();
                }
                if (baseOutputStream != null)
                {
                    baseOutputStream.Close();
                    baseOutputStream = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return flag;
        }

        public static bool ZipFileDictory(string _strToBeZippedDict, string _strZippedName, string _strPasswd)
        {
            try
            {
                if (!Directory.Exists(_strToBeZippedDict))
                {
                    return false;
                }
                if (File.Exists(_strZippedName))
                {
                    string destFileName = _strZippedName + DateTime.Now.ToString("yyyyMMddHHmmss");
                    File.Move(_strZippedName, destFileName);
                }
                string[] files = Directory.GetFiles(_strToBeZippedDict);
                Crc32 crc = new Crc32();
                ZipOutputStream stream = new ZipOutputStream(File.Create(_strZippedName));
                stream.SetLevel(6);
                foreach (string str2 in files)
                {
                    FileStream stream2 = File.OpenRead(str2);
                    byte[] buffer = new byte[stream2.Length];
                    stream2.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(str2) {
                        DateTime = DateTime.Now,
                        Size = stream2.Length
                    };
                    stream.Password = _strPasswd;
                    stream2.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    stream.PutNextEntry(entry);
                    stream.Write(buffer, 0, buffer.Length);
                }
                stream.Finish();
                stream.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

