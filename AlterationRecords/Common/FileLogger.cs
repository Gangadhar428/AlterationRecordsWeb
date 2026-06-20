using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AlterationRecords
{
    public class FileLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public static void WriteEntry(string entry)
        {
            entry += Environment.NewLine;
            entry += "------------------------------------------------------------------------";
            entry += Environment.NewLine;
            try
            {
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
                string sPath = System.IO.Path.GetDirectoryName(ass.CodeBase);
                sPath = sPath.Replace(@"file:\", "");
                sPath = System.IO.Path.Combine(sPath, "Logs");
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                string sFileName = string.Format(@"{0}\Sonabend_{1}.Log", sPath, DateTime.Today.ToString("MMddyyyy"));
                FileStream fs = null;
                try
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                    fs.Write(System.Text.Encoding.ASCII.GetBytes(entry), 0, entry.Length);
                    fs.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }
    }
}