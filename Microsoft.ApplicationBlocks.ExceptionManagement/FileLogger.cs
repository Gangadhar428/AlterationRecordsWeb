using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ApplicationBlocks.ExceptionManagement
{
    public class FileLogger
    {
        public static void WriteEntry(string entry, string filetype)
        {
            entry += Environment.NewLine;
            entry += "------------------------------------------------------------------------";
            entry += Environment.NewLine;
            try
            {
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();

                string sPath = "";
                if (ass.CodeBase.IndexOf("bin") > 0)
                    sPath = ass.CodeBase.Substring(0, ass.CodeBase.IndexOf("bin"));
                else if (ass.CodeBase.IndexOf(@"/") > 0)
                    sPath = ass.CodeBase.Substring(0, ass.CodeBase.LastIndexOf(@"/"));
                else
                    sPath = ass.CodeBase;

                sPath = sPath.Replace(@"file:///", "");
                sPath = Path.Combine(sPath, "Logs");
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                string sFileName = string.Format(@"{0}\Choprapsm{2}_{1}.Log", sPath, DateTime.Today.ToString("MMddyyyy"), filetype);
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
                ExceptionManager.Publish(ex); ;
            }

        }
    }
}
