using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyFiles
{
    public class ClassCsv
    {
        public static string GetCurrentDirectoryX()
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
            else
            {
                return System.IO.Directory.GetCurrentDirectory();
            }
        }

        //参数strHeaders、strValues都是数组字符串
        //GPTS命令的基本概念之"数组字符串"：在组态命令中，用字符串的形式来表示一个数组，数组元素按其索引顺序排列，数组元素之间以逗号间隔。
        //数组和数组字符串之间可以用静态方法Command.ArrayToString()和Command.StringToArray()相互转换。
        //
        public static bool WriteCSV(string strFolder, string strFileName, string strHeaders, string strValues)
        {
            try
            {
                string sFolderPath = GetCurrentDirectoryX() + "\\" + strFolder;
                if (Directory.Exists(sFolderPath) == false)
                {
                    Directory.CreateDirectory(sFolderPath);
                }
                string sFilePath = sFolderPath + "\\" + strFileName + ".csv";
                using (StreamWriter sw = new StreamWriter(sFilePath, true, Encoding.UTF8))
                {
                    if (File.Exists(sFilePath) == false)
                    {
                        sw.WriteLine(strHeaders);
                    }
                    sw.WriteLine(strValues);
                    sw.Flush();
                    sw.Close();
                    return true;
                }
            }
            catch 
            { }
            return false;
        }

    }
}
