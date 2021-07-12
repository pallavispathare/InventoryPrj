using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data.Entity.Validation;

namespace Logging
{
    public static class Logging
    {
        // Log method to log the exceptions
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteErrorLog(Exception ex, string fileName = "")
        {
            bool detailsLogging = isDetailedLogging();

            if (detailsLogging == false)
                return;

            StreamWriter sw = null;
            string Error = string.Empty;
            try
            {
                if (ex.GetType().IsAssignableFrom(typeof(DbEntityValidationException)))
                {
                    var innerException = ex as DbEntityValidationException;

                    foreach (var eve in innerException.EntityValidationErrors)
                    {
                        Error = "Entity of type '" + eve.Entry.Entity.GetType().Name + "' in state '" + eve.Entry.State + "' has the following validation errors:";

                        foreach (var ve in eve.ValidationErrors)
                        {
                            Error += "\n- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                        }
                    }

                    Error += "\n" + ex.ToString();
                }

                if (fileName.Trim() != "")
                {
                    string folderPath = createFolderForLogs();
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMdd");
                    sw = new StreamWriter(folderPath + "\\" + fileName + ".txt", true);

                    if (!string.IsNullOrEmpty(Error))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " : " + Error);
                    }
                    else
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " : " + ex.ToString());
                    }

                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Error))
                    {
                        WriteGenericLog(Error, "");
                    }
                    else
                    {
                        WriteGenericLog(ex.ToString(), "");
                    }
                }
            }
            catch
            {

            }
        }

        private static bool isDetailedLogging()
        {
            bool detailedLogging = false;
            try
            {
                if (ConfigurationManager.AppSettings["DetailedLogging"] != null)
                {
                    detailedLogging = Convert.ToBoolean(ConfigurationManager.AppSettings["DetailedLogging"].ToString());
                }
            }
            catch
            {
            }
            return detailedLogging;
        }

        private static string createFolderForLogs()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "\\logs";
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch
            {
            }
            return folderPath;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void WriteGenericLog(string ex, string str)
        {
            bool detailsLogging = isDetailedLogging();

            if (detailsLogging == false)
                return;

            StreamWriter sw = null;
            try
            {
                string fileName = "HealthBridgeLog_" + DateTime.Now.ToString("yyyyMMdd");
                string folderPath = createFolderForLogs();
                sw = new StreamWriter(folderPath + "\\" + fileName + ".txt", true);//AppDomain.CurrentDomain.BaseDirectory

                if (ex == "" && str != "")
                {
                    sw.WriteLine(DateTime.Now.ToString() + " : " + str);
                }
                else if (ex != "" && str == "")
                {
                    sw.WriteLine(DateTime.Now.ToString() + " : " + ex);
                }
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }

        // Log method to log the custom messages
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteMessageLog(string message, string fileName = "")
        {
            bool detailsLogging = isDetailedLogging();

            if (detailsLogging == false)
                return;

            StreamWriter sw = null;
            try
            {
                fileName = fileName + DateTime.Now.ToString("yyyyMMdd");
                if (fileName.Trim() != "")
                {

                    string folderPath = createFolderForLogs();
                    sw = new StreamWriter(folderPath + "\\" + fileName + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + " : " + message);
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    WriteGenericLog("", message);
                }
            }
            catch
            {

            }
        }

        // Log method to log the custom messages
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteTestLog(string message, string fileName = "")
        {
            bool detailsLogging = isDetailedLogging();

            if (detailsLogging == false)
                return;

            StreamWriter sw = null;
            try
            {
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMdd");
                // if (fileName.Trim() != "")
                {
                    string folderPath = createFolderForLogs();
                    sw = new StreamWriter(folderPath + "\\testlog.txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + " : " + message);
                    sw.Flush();
                    sw.Close();
                }
                //  else
                {
                    // WriteGenericLog("", message);
                }
            }
            catch
            {

            }
        }

    }
}
