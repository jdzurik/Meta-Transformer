using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace XSL.Library
{
    public static class RegularlyExpressYourself
    {
        #region Public Static Readonly Regular Expression Pattern Strings Yadayada
        /// <summary>
        /// Matches xsl or xml tags or html w/namespace:qualifiers
        /// </summary>
        public static string XslTagPattern
        {
            get
            {
                //XmlTag Regex modified from html tag version

                //changed "<tagname>\w+" to "<tagname>\w+(:\w+)?"
                //similar change in attribute name; both to allow namespace qualifiers

                //some html-based safety nets could be dumped for xml use but

                //<(?<endTag>/)?(?<tagname>\w+(:\w+)?)((\s+(?<attName>\w+(:\w+)?)(\s*=\s*(?:"(?<attVal>[^"]*)"|'(?<attVal>[^']*)'|(?<attVal>[^'">\s]+)))?)+\s*|\s*)(?<completeTag>/)?>

                return 
                    @"<"
                    +    @"(?<endTag>/)?"    //Captures the / if this is an end tag.
                    +    @"(?<tagname>\w+(:\w+)?)"    //Captures TagName (changed)
                    +    @"("                //Groups tag contents
                    +        @"(\s+"            //Groups attributes
                    +           @"(?<attName>\w+(:\w+)?)"  //Attribute name
                    +            @"("                //groups =value portion.
                    +                @"\s*=\s*"            // = 
                    +                @"(?:"        //Groups attribute "value" portion.
                    +                    @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
                    +                    @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
                    +                    @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
                    +                @")"
                    +            @")?"        //end optional att value portion.
                    +        @")+\s*"        //One or more attribute pairs
                    +        @"|\s*"            //Some white space.
                    +    @")"
                    + @"(?<completeTag>/)?>"; //Captures the "/" if this is a complete tag.
            }
        }

        /// <summary>
        /// Matches all currently known xsl tags
        /// </summary>
        public static string KnownXslTagsPattern
        {
            get
            {
                // hope you don't mind scrolling b/c i'm too lazy to format
                string pattern = 
                "<(?<endTag>/)?(?<tagname>(xsl:apply-imports|xsl:apply-templates|xsl:attribute|xsl:attribute-set|xsl:call-template|xsl:choose|xsl:comment|xsl:copy|xsl:copy-of|xsl:decimal-format|xsl:document|xsl:element|xsl:fallback|xsl:for-each|xsl:if|xsl:import|xsl:include|xsl:key|xsl:message|xsl:namespace-alias|xsl:number|xsl:otherwise|xsl:output|xsl:param|xsl:preserve-space|xsl:processing-instruction|xsl:script|xsl:sort|xsl:strip-space|xsl:stylesheet|xsl:template|xsl:text|xsl:transform|xsl:value-of|xsl:variable|xsl:when|xsl:with-param)?)((\\s+(?<attName>\\w+(:\\w+)?)(\\s*=\\s*(?:\"(?<attVal>[^\"]*)\"|'(?<attVal>[^']*)'|(?<attVal>[^'\">\\s]+)))?)+\\s*|\\s*)(?<completeTag>/)?>";
                return pattern;
            }
        }

        public static string KnownXslFunctionsPattern
        {
            get
            {
                //assume context is already in that of an attribute value
                //so no need to include tags or attribute patterns just simple list
                //just match func( or func (
                //hope you don't mind scrolling b/c i'm too lazy to format
                return @"boolean(\s)?(?:\()|ceil(\s)?(?:\()|ceiling(\s)?(?:\()|concat(\s)?(?:\()|contains(\s)?(?:\()|count(\s)?(?:\()|current(\s)?(?:\()|document(\s)?(?:\()|element-available(\s)?(?:\()|entity-uri(\s)?(?:\()|false(\s)?(?:\()|floor(\s)?(?:\()|format-number(\s)?(?:\()|function-available(\s)?(?:\()|generate-id(\s)?(?:\()|getLast(\s)?(?:\()|getRemainder(\s)?(?:\()|id(\s)?(?:\()|init(\s)?(?:\()|key(\s)?(?:\()|lang(\s)?(?:\()|last(\s)?(?:\()|local-name(\s)?(?:\()|name(\s)?(?:\()|namespace-uri(\s)?(?:\()|normalize-space(\s)?(?:\()|not(\s)?(?:\()|number(\s)?(?:\()|position(\s)?(?:\()|refresh(\s)?(?:\()|round(\s)?(?:\()|starts-with(\s)?(?:\()|string(\s)?(?:\()|string-length(\s)?(?:\()|substring(\s)?(?:\()|substring-after(\s)?(?:\()|substring-before(\s)?(?:\()|sum(\s)?(?:\()|system-property(\s)?(?:\()|translate(\s)?(?:\()|true(\s)?(?:\()|unparsed-entity-uri(\s)?(?:\()";
            }
        }

        /// <summary>
        /// Returns pattern for attribute name="value" pairs
        /// </summary>
        public static string AttributeNameValuePattern
        {
            get
            {
                return "(?<1>\\w+)\\s*=\\s*(?:\"(?<2>[^\"]*)\"|(?<2>\\S+))";
            }
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Creates an assembly with regular expressions compiled for performance
        /// and encapsulation reasons
        /// </summary>
        /// <remarks>
        /// Somewhat hard-codes expressions in a way but should only be here
        /// once solidified and easy enough to regenerate assembly and copy over
        /// </remarks>
        public static void GenerateRegExAssembly()
        {
            const string NAMESPACE = "XSL.Library.RegularExpressions";

            RegexCompilationInfo[] compInfo = 
            {
                new RegexCompilationInfo
                (
                    XslTagPattern
                    , RegexOptions.IgnoreCase
                    , "XslTagRegex"
                    , NAMESPACE
                    , true
                ),
                // Matches known XSL tags
                new RegexCompilationInfo
                (
                    KnownXslTagsPattern
                    , RegexOptions.None
                    , "KnownXslTagsRegex"
                    , NAMESPACE, true
                ),
                // Matches attribute name="value" pairs
                new RegexCompilationInfo
                (
                    AttributeNameValuePattern
                    , RegexOptions.None
                    , "AttributeNameValueRegex"
                    , NAMESPACE, true
                ),
                // Matches attribute name="value" pairs
                new RegexCompilationInfo
                (
                    KnownXslFunctionsPattern
                    , RegexOptions.None
                    , "KnownXslFunctionsRegex"
                    , NAMESPACE, true
                )
                ,
                // Matches double words.
                new RegexCompilationInfo
                (
                    @"\b(\w+)\s+\1\b"
                    , RegexOptions.None
                    , "DoubleWordRegex"
                    , NAMESPACE, true
                )
            };
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = NAMESPACE;
            assemblyName.Version = new Version("1.0.0.0");

            /*
            //TODO: fool w/getting this to output somewhere better later
            string curCodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string newAssemblyCodeBase = curCodeBase;
            int pos = curCodeBase.LastIndexOf(@"/");
            if (pos > -1)
            {
                newAssemblyCodeBase = curCodeBase.Substring(0, pos) + "/" + NAMESPACE + ".dll";
            }
            assemblyName.CodeBase = newAssemblyCodeBase;
            */

            //may have to go to below location to grab generated assembly:
            //C:\Program Files\Microsoft Visual Studio 8\Common7\IDE
            Regex.CompileToAssembly(compInfo, assemblyName);
        }
        #endregion
    }
}
