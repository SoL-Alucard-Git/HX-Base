namespace Aisino.Framework.Plugin.Core.WebService
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using Microsoft.CSharp;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Web.Services.Description;
    using System.Xml.Serialization;

    public class WebServiceFactory
    {
        private Assembly assembly_0;
        private static Dictionary<string, Dictionary<string, Assembly>> dictionary_0;
        private static Dictionary<string, Assembly> dictionary_1;
        private static ILog ilog_0;

        static WebServiceFactory()
        {
            
            dictionary_0 = new Dictionary<string, Dictionary<string, Assembly>>();
            dictionary_1 = new Dictionary<string, Assembly>();
            ilog_0 = LogUtil.GetLogger<WebServiceFactory>();
        }

        private WebServiceFactory()
        {
            
        }

        public static WebServiceFactory InitClientProxy(string string_0)
        {
            try
            {
                WebServiceFactory factory = new WebServiceFactory();
                return new WebServiceFactory { assembly_0 = factory.method_0(string_0) };
            }
            catch (Exception exception)
            {
                ilog_0.Error("初始化WebService客户端代理异常", exception);
                return null;
            }
        }

        public T InvokeMethod<T>(string string_0, string string_1, object[] object_0)
        {
            Type type = this.assembly_0.GetType("Aisino.Framework." + string_0);
            object obj2 = Activator.CreateInstance(type);
            return (T) type.GetMethod(string_1).Invoke(obj2, object_0);
        }

        public static object InvokeWebService(string string_0, string string_1, params object[] args)
        {
            object obj4;
            string name = "Aisino.WebService.DynamicWebCalling";
            Stream stream = null;
            WebClient client = null;
            CSharpCodeProvider provider = null;
            try
            {
                if (!dictionary_0.ContainsKey(string_0))
                {
                    client = new WebClient();
                    stream = client.OpenRead(string_0 + "?WSDL");
                    ServiceDescription serviceDescription = ServiceDescription.Read(stream);
                    string key = serviceDescription.Services[0].Name;
                    ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                    importer.AddServiceDescription(serviceDescription, "", "");
                    CodeNamespace namespace2 = new CodeNamespace(name);
                    CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                    codeCompileUnit.Namespaces.Add(namespace2);
                    importer.Import(namespace2, codeCompileUnit);
                    CompilerParameters options = new CompilerParameters {
                        GenerateExecutable = false,
                        GenerateInMemory = true
                    };
                    options.ReferencedAssemblies.Add("System.dll");
                    options.ReferencedAssemblies.Add("System.XML.dll");
                    options.ReferencedAssemblies.Add("System.Web.Services.dll");
                    options.ReferencedAssemblies.Add("System.Data.dll");
                    provider = new CSharpCodeProvider();
                    CompilerResults results = provider.CompileAssemblyFromDom(options, new CodeCompileUnit[] { codeCompileUnit });
                    if (results.Errors.HasErrors)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (CompilerError error in results.Errors)
                        {
                            builder.Append(error.ToString());
                            builder.Append(Environment.NewLine);
                        }
                        throw new Exception(builder.ToString());
                    }
                    Assembly compiledAssembly = results.CompiledAssembly;
                    Dictionary<string, Assembly> dictionary = new Dictionary<string, Assembly>();
                    dictionary.Add(key, compiledAssembly);
                    dictionary_0.Add(string_0, dictionary);
                }
                Dictionary<string, Assembly> dictionary2 = dictionary_0[string_0];
                Assembly assembly2 = null;
                string str3 = null;
                foreach (KeyValuePair<string, Assembly> pair in dictionary2)
                {
                    str3 = pair.Key;
                    assembly2 = pair.Value;
                }
                Type type = assembly2.GetType(name + "." + str3, true, true);
                object obj2 = Activator.CreateInstance(type);
                obj4 = type.GetMethod(string_1).Invoke(obj2, args);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message, new Exception(exception.InnerException.StackTrace));
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                if (client != null)
                {
                    client.Dispose();
                }
                if (provider != null)
                {
                    provider.Dispose();
                }
            }
            return obj4;
        }

        private Assembly method_0(string string_0)
        {
            string hashStr = MD5_Crypt.GetHashStr(string_0);
            hashStr = hashStr.Substring(0, hashStr.IndexOf("=")) + ".dll";
            Assembly assembly = null;
            if (!dictionary_1.TryGetValue(hashStr, out assembly))
            {
                if (!System.IO.File.Exists(hashStr))
                {
                    WebClient client = new WebClient();
                    ServiceDescription serviceDescription = ServiceDescription.Read(client.OpenRead(string_0));
                    ServiceDescriptionImporter importer = new ServiceDescriptionImporter {
                        ProtocolName = "Soap",
                        Style = ServiceDescriptionImportStyle.Client,
                        CodeGenerationOptions = CodeGenerationOptions.GenerateNewAsync | CodeGenerationOptions.GenerateProperties
                    };
                    importer.AddServiceDescription(serviceDescription, null, null);
                    CodeNamespace namespace2 = new CodeNamespace("Aisino.Framework");
                    CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                    codeCompileUnit.Namespaces.Add(namespace2);
                    importer.Import(namespace2, codeCompileUnit);
                    CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                    CompilerParameters options = new CompilerParameters {
                        GenerateExecutable = false,
                        OutputAssembly = hashStr
                    };
                    options.ReferencedAssemblies.Add("System.dll");
                    options.ReferencedAssemblies.Add("System.XML.dll");
                    options.ReferencedAssemblies.Add("System.Web.Services.dll");
                    options.ReferencedAssemblies.Add("System.Data.dll");
                    if (provider.CompileAssemblyFromDom(options, new CodeCompileUnit[] { codeCompileUnit }).Errors.HasErrors)
                    {
                        throw new Exception("客户端代理编译失败");
                    }
                }
                assembly = Assembly.LoadFrom(hashStr);
                dictionary_1[hashStr] = assembly;
            }
            return assembly;
        }

        private static string smethod_0(string string_0)
        {
            string[] strArray = string_0.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].Split(new char[] { '.' })[0];
        }
    }
}

