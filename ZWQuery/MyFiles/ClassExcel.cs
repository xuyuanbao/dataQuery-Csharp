/******************************************
 *  junjie
 *  2014-09-28
******************************************/

//#define MYSQL

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace MyFiles
{
    public static class ClassExcel
    {
        //使用COM组件，定义EXCEL对象
        private static Excel.Application xlExcelApp = null;
        private static Excel.Workbook xlBook = null;
        private static Excel.Worksheet xlSheet = null;
        private static Excel.Range xlRange = null;

        public struct stRangeValue
        {
            public string strRange;
            public string strValue;
        }

        public struct stWorksheet
        {
            public string strSheetName;
            public stRangeValue[] stRange;
        }

        public struct stWorkbook
        {
            public string strBookName;
            public stWorksheet[] stSheet;
        }

        public static stWorkbook g_stTemplateInfo;  //EXCEL模板参数

        /// <summary>
        /// 使用OLEDB操作方式将数据集快速导入指定的EXCEL文件, add by junjie, 20140731
        /// 兼容Excel2003-2010版本, modify by junjie, 20141008
        /// </summary>
        /// <param name="DS"></param>
        /// <param name="strFilePathAndName"></param>
        /// <returns></returns>
        public static bool DatasetToExcel(DataSet DS, string strFilePathAndName)
        {
            strFilePathAndName=strFilePathAndName.Trim();
            if (strFilePathAndName.Equals("") || DS == null)
            {
                return false;
            }
            if (DS.Tables.Count <= 0)
            {
                 return false;
            }

            try
            {
                //使用OLEDB提供程序连接到EXCEL工作簿，将EXCEL文件作为数据源来读写
                //连接字符串：Provider=Microsoft.Jet.OLEDB.4.0;Data Source=FileName.xls;Extended Properties=Excel 8.0;HDR=Yes;IMEX=1;
                //-- Provider: 表示提供程序名称 ( Excel97-2003的提供程序是 Microsoft.Jet.OLEDB.4.0，Excel2007-2010的提供程序是 Microsoft.ACE.OLEDB.12.0)
                //-- Data Source: Excel文件的路径
                //-- Extended Properties: 设置Excel的特殊属性，取值 Excel 5.0 针对Excel97版本，Excel 8.0 针对Excel97以上-2003版本，Excel 12.0 针对Excel2007-2010版本
                //-- HDR=Yes 表示第一行是标题/列名，不做为数据使用，在计算行数时就不包含第一行，系统默认是Yes
                //-- IMEX: 0导入模式（只写），1导出模式（只读），2混合模式（可读写）

                using (OleDbConnection con = new OleDbConnection())
                {
                    string strConnString;
                    try
                    {
                        //针对Excel 97以上-2003版本
                        strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePathAndName + ";Extended Properties=Excel 8.0;";
                        con.ConnectionString = strConnString;
                        con.Open();
                    }
                    catch
                    {
                        //针对Excel 2007-2010版本
                        strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePathAndName + ";Extended Properties=Excel 12.0;";
                        con.ConnectionString = strConnString;
                        con.Open();
                    }
                    for (int tbIndex = 0; tbIndex < DS.Tables.Count; tbIndex++)
                    {
                        StringBuilder strSQL = new StringBuilder();
                        string strTableName = "Sheet" + (tbIndex + 1).ToString();
                        
                        //当文件已存在并选择替换时，防止提示表已存在错误
                        //strSQL.Append("DROP TABLE [" + DS.Tables[tbIndex].TableName + "]");
                        strSQL.Append("DROP TABLE [" + strTableName + "]");
                        OleDbCommand cmd = new OleDbCommand(strSQL.ToString(), con);
                        cmd.CommandText = strSQL.ToString();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch 
                        { }
                        strSQL.Remove(0, strSQL.Length);

                        //创建表
                        //strSQL.Append("CREATE TABLE [" + DS.Tables[tbIndex].TableName + "]");
                        strSQL.Append("CREATE TABLE [" + strTableName + "]");

                        strSQL.Append("(");
                        for (int i = 0; i < DS.Tables[tbIndex].Columns.Count; i++)
                        {
                            strSQL.Append("[" + DS.Tables[tbIndex].Columns[i].ColumnName + "] " + DataTableToExcelDataType(DS.Tables[tbIndex].Columns[i].DataType.Name) + ",");
                        }
                        strSQL = strSQL.Remove(strSQL.Length - 1, 1);
                        strSQL.Append(")");

                        cmd.CommandText = strSQL.ToString();
                        cmd.ExecuteNonQuery();

                        //向表中插入数据
                        for (int i = 0; i < DS.Tables[tbIndex].Rows.Count; i++)
                        {
                            strSQL.Remove(0,strSQL.Length);
                            StringBuilder strField = new StringBuilder();
                            StringBuilder strValue = new StringBuilder();
                            for (int j = 0; j < DS.Tables[tbIndex].Columns.Count;j++ )
                            {
                                if (!DS.Tables[tbIndex].Rows[i][j].ToString().Equals(""))
                                {
                                    strField.Append("[" + DS.Tables[tbIndex].Columns[j].ColumnName.ToString() + "],");
                                    strValue.Append("'" + DS.Tables[tbIndex].Rows[i][j].ToString().Trim() + "',");
                                }
                            }
                            strField = strField.Remove(strField.Length - 1, 1);
                            strValue = strValue.Remove(strValue.Length - 1, 1);
                            //cmd.CommandText = strSQL.Append("insert into [" + DS.Tables[tbIndex].TableName +
                            //    "](").Append(strField.ToString()).Append(")values(").Append(strValue).Append(")").ToString();
                            cmd.CommandText = strSQL.Append("insert into [" + strTableName +
                                "](").Append(strField.ToString()).Append(")values(").Append(strValue).Append(")").ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message, "DatasetToExcel()");
            }
            return false;
        }

        /// <summary>
        /// 输入.NET DataTable的数据类型名称，返回在EXCEL中所对应的数据类型名称, add by junjie, 20140731
        /// </summary>
        /// <param name="strDataTableColumnDataTypeName"></param>
        /// <returns></returns>
        private static string DataTableToExcelDataType(string strDataTableColumnDataTypeName)
        {
            string strExcelDataType = "";
            switch (strDataTableColumnDataTypeName.ToLower())
            {
                case "single": 
                case "double":
                case "decimal":
                case "float":
                    strExcelDataType="float"; 
                    break;
                case "byte": 
                case "int32":
                case "int64":
                case "uint32":
                case "uint64": 
                    strExcelDataType="int"; 
                    break;
                case "datetime":
                case "mysqldatetime":
                    strExcelDataType="datetime"; 
                    break;
                default:
                    strExcelDataType = "memo";  //"nvarchar";  解决数据长度大于256时报错“字符太小而不能接受所要添加的数据的数量”的问题, by junjie, 20141008
                    break;
            }
            return (strExcelDataType);
        }


        public static string[] GetExcelSheetNames(string fileExcel)
        {
            try
            {
                //using (OleDbConnection con = new OleDbConnection())
                //{
                //    string strConnString;
                //    try
                //    {
                //        //针对Excel 97以上-2003版本
                //        strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileExcel + ";Extended Properties=Excel 8.0;";
                //        con.ConnectionString = strConnString;
                //        con.Open();
                //    }
                //    catch
                //    {
                //        //针对Excel 2007-2010版本
                //        strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileExcel + ";Extended Properties=Excel 12.0;";
                //        con.ConnectionString = strConnString;
                //        con.Open();
                //    }
                //    //返回Excel的架构，包括各个Sheet表的名称、类型、创建时间和修改时间等
                //    DataTable dt = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //    con.Close();
                //    string[] strSheetNames = new string[dt.Rows.Count];
                //    for (int i=0;i<dt.Rows .Count ;i++)
                //    {
                //        strSheetNames[i] = dt.Rows[i]["TABLE_NAME"].ToString();
                //    }
                //    return strSheetNames;
                //}

                if (xlExcelApp == null)
                {
                    xlExcelApp = new Excel.Application();
                    xlExcelApp.DisplayAlerts = false;
                    xlExcelApp.Visible = false;
                }
                xlBook = xlExcelApp.Workbooks.Open(fileExcel.Trim(),
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                string[] strSheetNames = new string[xlBook.Worksheets.Count];
                for (int i = 0; i < xlBook.Worksheets.Count; i++) 
                {
                    xlSheet = (Excel.Worksheet)xlBook.Worksheets[1 + i];  //工作表索引从1开始
                    strSheetNames[i] = xlSheet.Name;
                }
                return strSheetNames;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message, "GetExcelSheetName()");
            }
            finally
            {
                CloseExcel();
            }
            return null;
        }

        /// <summary>
        /// 根据模板文件及配置参数生成EXCEL报表
        /// </summary>
        /// <param name="sTemplateFile"></param>
        /// <param name="sNewFile"></param>
        /// <returns></returns>
        public static bool ExportReport(string sTemplateExcel,bool bXmlConfig, string sReportExcel,MyDB.Database DB)
        {
            try
            {
                sReportExcel = GetTemplateExcel(sTemplateExcel, bXmlConfig, sReportExcel);
                if (sReportExcel != null)
                {
                    for (int iSheetIndex = 0; iSheetIndex < g_stTemplateInfo.stSheet.Length; iSheetIndex++)
                    {
                        if (OpenExcel(sReportExcel, g_stTemplateInfo.stSheet[iSheetIndex].strSheetName))
                        {
                            for (int iRangeIndex = 0; iRangeIndex < g_stTemplateInfo.stSheet[iSheetIndex].stRange.Length; iRangeIndex++)
                            {
                                if (!SetRange(g_stTemplateInfo.stSheet[iSheetIndex].stRange[iRangeIndex].strRange,
                                    g_stTemplateInfo.stSheet[iSheetIndex].stRange[iRangeIndex].strValue,DB))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseExcel();
            }
        }


        /// <summary>
        /// 获取EXCEL模板文件，读取同名XML配置文件，成功返回新文件路径，失败返回null
        /// </summary>
        /// <param name="sTemplateFile">EXCEL模板文件路径</param>
        /// <param name="bXmlConfig"> 是否从Xml配置文件获取模板参数</param>
        /// <param name="sReportExcel">报表文件路径</param>
        /// <returns></returns>
        public static string GetTemplateExcel(string sTemplateExcel,bool bXmlConfig, string sReportExcel)
        {
            try
            {
                if (sTemplateExcel == null)
                {
                    MessageBox.Show("未指定Excel模板！");
                    return null;
                }
                sTemplateExcel = sTemplateExcel.Trim();
                if (!File.Exists(sTemplateExcel))
                {
                    MessageBox.Show("Excel模板不存在！\r\n" + sTemplateExcel);
                    return null;
                }
                if (bXmlConfig)
                {
                    string sTemplateXml =  sTemplateExcel.Remove(sTemplateExcel.Length - 3, 3) + "xml";
                    if (!File.Exists(sTemplateXml))
                    {
                        MessageBox.Show("Xml配置文件不存在！\r\n" + sTemplateXml);
                        return null;
                    }
                    if (!ClassXml.XmlGetTemplateInfo(sTemplateXml))
                    {
                        MessageBox.Show("Xml配置文件读取失败！\r\n" + sTemplateXml);
                        return null;
                    }
                }
                if (sReportExcel == null)
                {
                    SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                    SaveFileDialog1.Title = "报表文件保存为";
                    SaveFileDialog1.DefaultExt = "xls";
                    SaveFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                    SaveFileDialog1.FileName = "";
                    if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        sReportExcel = SaveFileDialog1.FileName.Trim();
                    }
                    SaveFileDialog1.Dispose();
                    if (sReportExcel == null)
                    {
                        return null;
                    }
                }
                sReportExcel = sReportExcel.Trim();
                File.Copy(sTemplateExcel, sReportExcel, true);
                return sReportExcel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message + "\r\n" + "GetTemplate( " + sTemplateExcel + ", " + sReportExcel + " )", "GetTemplate()");
                return null;
            }
        }
        
        /// <summary>
        /// 使用COM组件，打开指定的EXCEL工作簿和工作表 （注意：当打开下一个的工作簿时，需要先CloseExcel关闭当前工作簿）
        /// </summary>
        /// <param name="sWorkbook"></param>
        /// <param name="sWorksheet"></param>
        /// <returns></returns>
        public static bool OpenExcel(string sWorkbook, string sWorksheet)
        {
            try
            {
                sWorkbook = sWorkbook.Trim();
                sWorksheet = sWorksheet.Trim();
                if (xlExcelApp == null)
                {
                    xlExcelApp = new Excel.Application();
                    xlExcelApp.DisplayAlerts = false;
                    xlExcelApp.Visible = false;
                }
                if (xlBook == null)
                {
                    xlBook = xlExcelApp.Workbooks.Open(sWorkbook.Trim(),
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                xlSheet = (Excel.Worksheet)xlBook.Worksheets[sWorksheet.Trim()];
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message + "\r\n" + "OpenExcel(" + sWorkbook + "," + sWorksheet + ")", "OpenExcel()");
                CloseExcel();
                return false;
            }
        }

        /// <summary>
        /// 使用COM组件，向当前工作表中的单元格区域写入数据
        /// </summary>
        /// <param name="sRangeString"></param>
        /// <param name="sValueString"></param>
        /// <returns></returns>
        public static bool SetRange(string sRangeString, string sValueString, MyDB.Database DB)
        {
            try
            {
                if (xlSheet == null)
                {
                    return false;
                }
                sRangeString = sRangeString.Trim().ToUpper();
                string[] sA1Cell = sRangeString.Split('-');  //Excel单元格区域引用使用A1样式，冒号用中划线代替
                xlRange = xlSheet.get_Range(sA1Cell[0], sA1Cell[1]);
                sValueString = sValueString.Trim();
                if (sValueString.Length >= 4 && sValueString.Substring(0, 4).Equals("SQL=", StringComparison.OrdinalIgnoreCase))
                {
                    string strSQL = sValueString.Substring(4);
#if(MYSQL)
                    if (MySQLDB.MySQLDB.ExecuteDBDataSet(strSQL))
                    {
                        int row = MySQLDB.MySQLDB.dataSet.Tables[0].Rows.Count;
                        int col = MySQLDB.MySQLDB.dataSet.Tables[0].Columns.Count;
#else
                    if (DB.ExecuteDataSet (strSQL))
                    {
                        int row = DB.dataSet.Tables[0].Rows.Count;
                        int col = DB.dataSet.Tables[0].Columns.Count;
#endif
                        row = (row < xlRange.Rows.Count) ? row : xlRange.Rows.Count;
                        col = (col < xlRange.Columns.Count) ? col : xlRange.Columns.Count;
                        object[,] objValue = new object[row, col];  //二维数组
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
#if(MYSQL)
                                objValue[i, j] = MySQLDB.MySQLDB.dataSet.Tables[0].Rows[i][j];
#else
                                objValue[i, j] = DB.dataSet.Tables[0].Rows[i][j];
#endif
                            }
                        }
                        xlRange.Value2 = objValue;
                    }
                }
                else if (sValueString.Length >= 4 && sValueString.Substring(0, 4).Equals("TXT=", StringComparison.OrdinalIgnoreCase))
                {
                    string strValue = sValueString.Substring(4);
                    xlRange.NumberFormatLocal = "@";  //强制文本类型
                    xlRange.Value2 = strValue;
                }
                else
                {
                    xlRange.Value2 = sValueString;    //EXCEL默认类型
                }
                xlBook.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message + "\r\n" + "SetRange(" + sRangeString + "," + sValueString + ")", "SetRange()");
                return false;
            }
        }


        /// <summary>
        /// 使用COM组件，关闭并释放EXCEL对象
        /// </summary>
        public static void CloseExcel()
        {
            try
            {
                if (xlRange != null)
                {
                    DisposeObject(xlRange);
                }
                if (xlSheet != null)
                {
                    DisposeObject(xlSheet);
                }
                if (xlBook != null)
                {
                    xlBook.Close(false, Type.Missing, Type.Missing);
                    DisposeObject(xlBook);
                }
                if (xlExcelApp != null)
                {
                    xlExcelApp.Workbooks.Close();
                    xlExcelApp.Quit();
                    DisposeObject(xlExcelApp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, ex.Message, "CloseExcel()");
            }
            finally
            {
                xlRange = null;
                xlSheet = null;
                xlBook = null;
                xlExcelApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    try
                    {
                        pro.Kill();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 释放所引用的COM对象
        /// </summary>
        /// <param name="obj"></param>
        private static void DisposeObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch
            { }
            finally
            {
                obj = null;
            }
        }
 

    }
}
