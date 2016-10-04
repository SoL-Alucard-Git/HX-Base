using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

internal class Class5
{
    private static bool bool_0 = false;
    private static Hashtable hashtable_0 = new Hashtable();

    internal static void lLHifFIsCLsZtjvFfN0i()
    {
        if (!bool_0)
        {
            bool_0 = true;
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Class5.smethod_0);
        }
    }

    private static Assembly smethod_0(object object_0, ResolveEventArgs resolveEventArgs_0)
    {
        lock (hashtable_0)
        {
            string str = resolveEventArgs_0.Name.Trim();
            object obj2 = hashtable_0[str];
            if (obj2 == null)
            {
                try
                {
                    string str2 = smethod_1(str);
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (assembly.GetName().Name.ToUpper() == str2.ToUpper())
                        {
                            return assembly;
                        }
                    }
                }
                catch
                {
                }
            }
            if (obj2 == null)
            {
                try
                {
                    RSACryptoServiceProvider.UseMachineKeyStore = true;
                    string s = smethod_1(str);
                    byte[] bytes = Encoding.Unicode.GetBytes(s);
                    string name = "b0494a1f-4bd3-" + Convert.ToBase64String(Class2.smethod_9(bytes));
                    Stream manifestResourceStream = typeof(Class5).Assembly.GetManifestResourceStream(name);
                    if (manifestResourceStream != null)
                    {
                        try
                        {
                            BinaryReader reader = new BinaryReader(manifestResourceStream) {
                                BaseStream = { Position = 0L }
                            };
                            byte[] buffer = new byte[manifestResourceStream.Length];
                            reader.Read(buffer, 0, buffer.Length);
                            reader.Close();
                            bool flag = false;
                            Assembly assembly2 = null;
                            try
                            {
                                assembly2 = Assembly.Load(buffer);
                            }
                            catch (FileLoadException)
                            {
                                flag = true;
                            }
                            catch (BadImageFormatException)
                            {
                                flag = true;
                            }
                            if (flag)
                            {
                                string path = Path.Combine(Path.Combine(Path.GetTempPath(), name), s + ".dll");
                                if (!File.Exists(path))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                                    FileStream stream2 = new FileStream(path, FileMode.Create, FileAccess.Write);
                                    stream2.Write(buffer, 0, buffer.Length);
                                    stream2.Close();
                                }
                                assembly2 = Assembly.LoadFile(path);
                                hashtable_0.Add(str, assembly2);
                                return assembly2;
                            }
                            hashtable_0.Add(str, assembly2);
                            return assembly2;
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
                return null;
            }
            return (Assembly) obj2;
        }
    }

    private static string smethod_1(string string_0)
    {
        string str = string_0.Trim();
        int index = str.IndexOf(',');
        if (index >= 0)
        {
            str = str.Substring(0, index);
        }
        return str;
    }
}

