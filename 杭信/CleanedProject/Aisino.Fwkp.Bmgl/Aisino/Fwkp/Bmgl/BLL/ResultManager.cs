namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.IBLL;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;

    internal sealed class ResultManager : IResultManager
    {
        internal void AddResultTableColumns(ImportResult importResult)
        {
            importResult.DtResult.Columns.Add("TableName");
            importResult.DtResult.Columns.Add("CodeBM");
            importResult.DtResult.Columns.Add("NameMC");
            for (int i = 0; i < importResult.Total; i++)
            {
                importResult.DtResult.Rows[i]["TableName"] = "客户编码.DB";
                importResult.DtResult.Rows[i]["CodeBM"] = "编码";
                importResult.DtResult.Rows[i]["NameMC"] = "名称";
            }
        }

        public string SaveTxt(string pathFile, ImportResult IResult)
        {
            string path = pathFile.Remove(pathFile.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable dtResult = IResult.DtResult;
            string str2 = ",";
            if (dtResult.Rows.Count <= 0)
            {
                return "没有客户";
            }
            StringBuilder builder = new StringBuilder();
            using (StreamWriter writer = new StreamWriter(pathFile, false, ToolUtil.GetEncoding()))
            {
                writer.WriteLine(" 报告种类：编码导入");
                writer.WriteLine(" 来源于：文本文件");
                writer.WriteLine("");
                writer.WriteLine("传入结果(按记录条数统计)：");
                writer.WriteLine("正确传入：" + IResult.Correct.ToString());
                writer.WriteLine("无效记录：" + IResult.Invalid.ToString());
                writer.WriteLine("重复记录：" + IResult.Duplicated.ToString());
                writer.WriteLine("传入失败：" + IResult.Failed.ToString());
                writer.WriteLine("总计：" + IResult.Total.ToString());
                writer.WriteLine("");
                writer.WriteLine("详细资料：");
                writer.WriteLine(" 表名,字段1名称,编码,字段2名称,名称,传入结果,原因:");
                writer.WriteLine("");
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    builder.Remove(0, builder.Length);
                    builder.Append(" ");
                    builder.Append(IResult.ImportTable);
                    builder.Append(str2);
                    builder.Append("编码");
                    builder.Append(str2);
                    builder.Append("\"");
                    builder.Append(dtResult.Rows[i]["Code"]);
                    builder.Append("\"");
                    builder.Append(str2);
                    builder.Append("名称");
                    builder.Append(str2);
                    builder.Append("\"");
                    builder.Append(dtResult.Rows[i]["Name"]);
                    builder.Append("\"");
                    builder.Append(str2);
                    builder.Append(dtResult.Rows[i]["Result"]);
                    builder.Append(str2);
                    builder.Append(dtResult.Rows[i]["Reason"]);
                    builder.Append(str2);
                    writer.WriteLine(builder.ToString());
                }
            }
            return "0";
        }
    }
}

