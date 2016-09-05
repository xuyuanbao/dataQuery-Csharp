using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;


namespace MyDB
{
    public static class MyXml
    {
        private const string _RootNode = "MyDB"; 

        /// <summary>
        /// 读Xml文件
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
        /// 写Xml文件
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
        /// 创建Xml文件
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
                xtw.WriteStartElement(_RootNode);
                xtw.WriteEndElement();
                xtw.WriteEndDocument();
                xtw.Flush();
                xtw.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 读写Xml文件
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
                XmlNode xRootNode = xDoc.SelectSingleNode(_RootNode);
                XmlNode xFatherNode = xDoc.SelectSingleNode(_RootNode + "/" + sXmlFatherNode);
                XmlNode xChildNode = xDoc.SelectSingleNode(_RootNode + "/" + sXmlFatherNode + "/" + sXmlChildNode);
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
                MessageBox.Show(ex.Message);
                return "";
            }
        }

    }
}
