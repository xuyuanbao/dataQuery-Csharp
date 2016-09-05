/******************************************
 *  junjie
 *  2014-09-28
******************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;


namespace MyFiles
{
    public static class ClassXml
    {
        public static XmlDocument g_XmlDoc = new XmlDocument();
        private const string m_sXmlRoot = "ZWGPTS";


        /// <summary>
        /// 读取EXCEL报表模板的配置文件
        /// </summary>
        /// <param name="sXmlFilePath"></param>
        /// <returns></returns>
        public static bool XmlGetTemplateInfo(string sXmlFilePath)
        {
            try
            {
                g_XmlDoc.Load(sXmlFilePath);
                XmlNode xRootNode = g_XmlDoc.SelectSingleNode(m_sXmlRoot);
                XmlNodeList xFatherNodeList = xRootNode.ChildNodes;
                ClassExcel.g_stTemplateInfo = new ClassExcel.stWorkbook();
                ClassExcel.g_stTemplateInfo.stSheet = new ClassExcel.stWorksheet[xFatherNodeList.Count];
                int i = 0;
                foreach (XmlNode xFatherNode in xFatherNodeList)
                {
                    XmlNodeList xChildNodeList = xFatherNode.ChildNodes;
                    ClassExcel.g_stTemplateInfo.stSheet[i].strSheetName = xFatherNode.Name;
                    ClassExcel.g_stTemplateInfo.stSheet[i].stRange = new ClassExcel.stRangeValue[xChildNodeList.Count];
                    int j = 0;
                    foreach (XmlNode xChildNode in xChildNodeList)
                    {
                        ClassExcel.g_stTemplateInfo.stSheet[i].stRange[j].strRange = xChildNode.Name;
                        ClassExcel.g_stTemplateInfo.stSheet[i].stRange[j].strValue = xChildNode.InnerText.Trim();
                        j++;
                    }
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 读Xml文件, add by junjie, 20140811
        /// </summary>
        /// <param name="sXmlFilePath"></param>
        /// <param name="sXmlFatherNode"></param>
        /// <param name="sXmlChildNode"></param>
        /// <param name="sDefaultValue"></param>
        /// <returns></returns>
        public static string XmlRead(string sXmlFilePath, string sXmlFatherNode, string sXmlChildNode, string sDefaultValue)
        {
            return XmlReadWrite(false, sXmlFilePath, sXmlFatherNode, sXmlChildNode, null, sDefaultValue);
        }

        /// <summary>
        /// 写Xml文件, add by junjie, 20140811
        /// </summary>
        /// <param name="sXmlFilePath"></param>
        /// <param name="sXmlFatherNode"></param>
        /// <param name="sXmlChildNode"></param>
        /// <param name="sNodeValue"></param>
        /// <returns></returns>
        public static string XmlWrite(string sXmlFilePath, string sXmlFatherNode, string sXmlChildNode, string sNodeValue)
        {
            return XmlReadWrite(true, sXmlFilePath, sXmlFatherNode, sXmlChildNode, sNodeValue, null);
        }


        /// <summary>
        /// 创建Xml文件, add by junjie, 20140811
        /// </summary>
        /// <param name="sXmlPath"></param>
        /// <returns></returns>
        private static bool XmlCreate(string sXmlFilePath)
        {
            try
            {
                XmlTextWriter xtw = new XmlTextWriter(sXmlFilePath, System.Text.Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                xtw.WriteStartDocument(true);
                xtw.WriteStartElement(m_sXmlRoot);
                xtw.WriteEndElement();
                xtw.WriteEndDocument();
                xtw.Flush();
                xtw.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                throw (new Exception("XmlCreate: " + ex.Message));
            }
        }

        /// <summary>
        /// 读写Xml文件, add by junjie, 20140811
        /// </summary>
        /// <param name="bWrite"></param>
        /// <param name="sXmlFilePath"></param>
        /// <param name="sXmlFatherNode"></param>
        /// <param name="sXmlChildNode"></param>
        /// <param name="sNodeValue"></param>
        /// <param name="sDefaultValue"></param>
        /// <returns></returns>
        private static string XmlReadWrite(bool bWrite,string sXmlFilePath, string sXmlFatherNode, string sXmlChildNode, string sNodeValue, string sDefaultValue)
        {
            try
            {
                if (!File.Exists(sXmlFilePath))
                {
                    if (!XmlCreate(sXmlFilePath)) return null;
                }
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(sXmlFilePath);
                XmlNode xRootNode = xDoc.SelectSingleNode(m_sXmlRoot);
                XmlNode xFatherNode = xDoc.SelectSingleNode(m_sXmlRoot + "/" + sXmlFatherNode);
                XmlNode xChildNode = xDoc.SelectSingleNode(m_sXmlRoot + "/" + sXmlFatherNode + "/" + sXmlChildNode);
                string strReturn = "";
                if (xFatherNode == null)
                {
                    xFatherNode = xDoc.CreateElement(sXmlFatherNode);
                    xChildNode = xDoc.CreateElement(sXmlChildNode);
                    if (bWrite)
                    {
                        xChildNode.InnerText = sNodeValue;
                    }
                    else
                    {
                        xChildNode.InnerText = sDefaultValue;
                    }
                    xFatherNode.AppendChild(xChildNode);
                    xRootNode.AppendChild(xFatherNode);
                    strReturn = xChildNode.InnerText;
                }
                else
                {
                    if (xChildNode == null)
                    {
                        xChildNode = xDoc.CreateElement(sXmlChildNode);
                        if (bWrite)
                        {
                            xChildNode.InnerText = sNodeValue;
                        }
                        else
                        {
                            xChildNode.InnerText = sDefaultValue;
                        }
                        xFatherNode.AppendChild(xChildNode);
                        strReturn = xChildNode.InnerText;
                    }
                    else
                    {
                        if (bWrite)
                        {
                            xChildNode.InnerText = sNodeValue;
                            strReturn = xChildNode.InnerText;
                        }
                        else
                        {
                            strReturn = xChildNode.InnerText;
                        }
                    }
                }
                xDoc.Save(sXmlFilePath);
                return strReturn;
            }
            catch (System.Exception ex)
            {
                throw (new Exception("XmlReadWrite: " + ex.Message));
            }
        }


    }
}
