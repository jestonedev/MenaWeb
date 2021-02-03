﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenaWeb.ReportServices
{
    public class ReportService
    {
        private readonly string sqlDriver;
        private readonly string connString;
        protected readonly string activityManagerPath;
        private readonly string attachmentsPath;

        public ReportService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            sqlDriver = config.GetValue<string>("SqlDriver");
            connString = httpContextAccessor.HttpContext.User.FindFirst("connString").Value;
            activityManagerPath = config.GetValue<string>("ActivityManagerPath");
            attachmentsPath = config.GetValue<string>("AttachmentsPath");
        }

        protected string GenerateReport(Dictionary<string, object> arguments, string config)
        {
            var logStr = new StringBuilder();
            try
            {
                var configXml = activityManagerPath + "templates\\" + config + ".xml";
                var configParts = config.Split('\\');
                var fileNameReport = configParts[configParts.Length - 1] + Guid.NewGuid().ToString() + ".docx";
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileNameReport);
                arguments.Add("config", configXml);
                arguments.Add("destFileName", destFileName);
                arguments.Add("force-move-to", destFileName);
                arguments.Add("connectionString", "Driver={" + sqlDriver + "};" + connString);

                using (var p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.FileName = activityManagerPath + "ActivityManager.exe";
                    p.StartInfo.Arguments = GetArguments(arguments);
                    logStr.Append("<dl>\n<dt>Arguments\n<dd>" + p.StartInfo.Arguments + "\n");
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                }

                return fileNameReport;
            }
            catch (Exception ex)
            {
                logStr.Append("<dl>\n<dt>Error\n<dd>" + ex.Message + "\n</dl>");
                throw new Exception(logStr.ToString());
            }
        }

        protected string GenerateMultiFileReport(Dictionary<string, object> arguments, string config)
        {
            var logStr = new StringBuilder();
            try
            {
                var configXml = activityManagerPath + "templates\\" + config + ".xml";
                var configParts = config.Split('\\');
                var directoryName = configParts[configParts.Length - 1] + Guid.NewGuid().ToString();
                var destDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", directoryName);
                Directory.CreateDirectory(destDirectory);

                arguments.Add("config", configXml);
                arguments.Add("destDirectoryName", destDirectory);
                arguments.Add("connectionString", "Driver={" + sqlDriver + "};" + connString);
                using (var p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.FileName = activityManagerPath + "ActivityManager.exe";
                    p.StartInfo.Arguments = GetArguments(arguments);
                    logStr.Append("<dl>\n<dt>Arguments\n<dd>" + p.StartInfo.Arguments + "\n");
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                }

                var destZipFile = destDirectory + ".zip";
                ZipFile.CreateFromDirectory(destDirectory, destZipFile);
                Directory.Delete(destDirectory, true);
                return destZipFile;
            }
            catch (Exception ex)
            {
                logStr.Append("<dl>\n<dt>Error\n<dd>" + ex.Message + "\n</dl>");
                throw new Exception(logStr.ToString());
            }
        }

        private static string GetArguments(Dictionary<string, object> arguments)
        {
            var argumentsString = "";
            foreach (var argument in arguments)
                argumentsString += string.Format(CultureInfo.InvariantCulture, "{0}=\"{1}\" ",
                    argument.Key.Replace("\"", "\\\""),
                    argument.Value == null ? "" : argument.Value.ToString().Replace("\"", "\\\""));
            return argumentsString;
        }

        public byte[] DownloadFile(string fileName)
        {
            try
            {
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
                var file = File.ReadAllBytes(destFileName);
                File.Delete(destFileName);
                return file;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}