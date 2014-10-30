using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Windows.Forms;

namespace XSL.Library
{   
    public static class TransformUtility
    {
        public static TransformResults Transform(FileInfo xmlFile, string xsl,
            TransformSettings settings)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile.FullName);
            return Transform(xmlDoc, new XPathDocument(new StringReader(xsl)),
                settings);
        }

        public static TransformResults Transform(FileInfo xmlFile, string xsl)
        {
            return Transform(xmlFile, xsl, new TransformSettings());
        }

        public static TransformResults Transform(string xml, string xsl)
        {
            return Transform(xml, xsl, new TransformSettings());
        }

        public static TransformResults Transform(string xml, string xsl,
            TransformSettings settings)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            return Transform(xmlDoc, new XPathDocument(new StringReader(xsl)),
                settings);
        }

        public static TransformResults Transform(XmlDocument xmlDoc, string xsl,
            TransformSettings settings)
        {
            return Transform(xmlDoc, new XPathDocument(new StringReader(xsl)),
                settings);
        }

        public static TransformResults Transform(XmlDocument xmlDoc, string xsl)
        {
            return Transform(xmlDoc, xsl, new TransformSettings());
        }

        public static TransformResults Transform(XmlDocument xmlDoc, XPathDocument xslDoc, 
            TransformSettings settings)
        {
            TransformResults results = new TransformResults();

            // collect any user-supplied or programatically-supplied arguments for transformation
            XsltArgumentList xsltArgs = GetXslTransformArgs(xslDoc, xmlDoc, settings);

            // even if no args, should not be null. for now, use null for cancel-check
            results.Cancelled = (null == xsltArgs);

            // exit if user clicked Cancel button on params form (if shown)
            if (results.Cancelled) return results;

            // create compiled transformation object and load it with our XSL document
            XslCompiledTransform transform = new XslCompiledTransform(settings.EnableDebug);
            transform.Load(xslDoc);

            // setup a buffer to hold results in memory
            MemoryStream resultBuffer = new MemoryStream();
            // and create a writer to write to the results buffer
            StreamWriter resultWriter = new StreamWriter(resultBuffer);

            try
            {
                // run transformation w/specified xml document, args and text writer
                transform.Transform(xmlDoc, xsltArgs, resultWriter);

                // writer filled result buffer; get buffer contents as string
                results.Output = Encoding.UTF8.GetString(resultBuffer.ToArray());                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                ;
            }

            return results;
        }

        #region Private Methods
        private static XsltArgumentList GetXslTransformArgs(XPathDocument xslDoc,
            XmlDocument xmlDoc, TransformSettings settings)
        {
            // do not initialize to null as null is used to indicate cancel
            XsltArgumentList xsltArgs = new XsltArgumentList();

            if (settings.EnableParamPrompting)
            {
                using (XSLTemplateParams paramsForm = new XSLTemplateParams(xmlDoc, xslDoc))
                {
                    // if there were any xsl params found, then show form to get values
                    if (paramsForm.TemplateArgCount > 0)
                    {
                        paramsForm.TopMost = settings.ShowParamsOnTop;
                        paramsForm.ShowDialog();

                        if (paramsForm.DialogResult != DialogResult.Cancel)
                        {
                            xsltArgs = (paramsForm.TemplateArgCount > 0) ? paramsForm.XsltArgs
                                : new XsltArgumentList();                            
                        }
                        else
                        {
                            // currently used to indicate user clicked Cancel
                            xsltArgs = null;
                        }
                    }
                }
            }

            // if any xsl params were programmatically specified, add them in
            if (null != settings.Parameters && settings.Parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in settings.Parameters)
                {
                    // ensure parameter is not already there
                    if (null == xsltArgs.GetParam(item.Key, string.Empty))
                    {
                        xsltArgs.AddParam(item.Key, string.Empty, item.Value);
                    }
                }
            }

            return xsltArgs;
        }
        #endregion Private Methods
    }
}
