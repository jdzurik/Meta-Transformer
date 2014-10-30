using System;
using System.Collections.Generic;
using System.Text;

namespace XSL.Library
{    
    public class XSLTag
    {
        #region Fields and Properties
        private string[] _validParentTags;

        public string[] ValidParentTags
        {
            get { return _validParentTags; }
            set { _validParentTags = value; }
        }
        
        private string _tagName = string.Empty;

        public string TagName
        {
            get { return _tagName; }
            set { _tagName = value; }
        }

        private string _toolTip = string.Empty;

        public string ToolTip
        {
            get { return _toolTip; }
            set { _toolTip = value; }
        }

        private List<XSLAttribute> _attributes = null;

        public List<XSLAttribute> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        private bool _allowsContent = false;

        public bool AllowsContent
        {
            get { return _allowsContent; }
            set { _allowsContent = value; }
        }

        #endregion Fields and Properties

        #region Constructors
        public XSLTag(string tagName, string validParentTagsCSV, string toolTip)
        {
            this.TagName = tagName;
            this.ToolTip = toolTip;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = true;
        }

        public XSLTag(string tagName, string validParentTagsCSV, string toolTip, 
            bool allowsContent)
        {
            this.TagName = tagName;
            this.ToolTip = toolTip;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = allowsContent;
        }

        public XSLTag(string tagName, string validParentTagsCSV)
        {
            this.TagName = tagName;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = true;
        }

        public XSLTag(string tagName, string validParentTagsCSV, bool allowsContent)
        {
            this.TagName = tagName;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = allowsContent;
        }

        public XSLTag(string tagName)
        {
            this.TagName = tagName;
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = true;
        }

        public XSLTag(string tagName, bool allowsContent)
        {
            this.TagName = tagName;
            this.Attributes = new List<XSLAttribute>();
            this.AllowsContent = allowsContent;
        }

        public XSLTag(string tagName, List<XSLAttribute> attrs)
        {
            this.TagName = tagName;
            this.Attributes = attrs;
            this.AllowsContent = true;
        }

        public XSLTag(string tagName, List<XSLAttribute> attrs, bool allowsContent)
        {
            this.TagName = tagName;
            this.Attributes = attrs;
            this.AllowsContent = allowsContent;
        }

        public XSLTag(string tagName, List<XSLAttribute> attrs, string toolTip)
        {
            this.TagName = tagName;
            this.Attributes = attrs;
            this.ToolTip = toolTip;
            this.AllowsContent = true;
        }

        public XSLTag(string tagName, List<XSLAttribute> attrs, string toolTip, bool allowsContent)
        {
            this.TagName = tagName;
            this.Attributes = attrs;
            this.ToolTip = toolTip;
            this.AllowsContent = allowsContent;
        }

        public XSLTag(string tagName, string validParentTagsCSV, string toolTip,
            List<XSLAttribute> attrs)
        {
            this.TagName = tagName;
            this.ToolTip = toolTip;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = attrs;
        }

        public XSLTag(string tagName, string validParentTagsCSV, string toolTip,
            List<XSLAttribute> attrs, bool allowsContent)
        {
            this.TagName = tagName;
            this.ToolTip = toolTip;
            this.ValidParentTags = validParentTagsCSV.Split(",".ToCharArray());
            this.Attributes = attrs;
            this.AllowsContent = allowsContent;
        }
        #endregion Constructors

        public override string ToString()
        {
            return string.Format("{0}: {1}", base.ToString(), _tagName);
        }
    } // end XSLTag

    public class XSLAttribute
    {
        #region Fields and Properties
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private bool _isDefault = false;

        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }
        private bool _isMandatory = false;

        public bool IsMandatory
        {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }

        private string _toolTip = string.Empty;

        public string ToolTip
        {
            get { return _toolTip; }
            set { _toolTip = value; }
        }

        private string[] _fixedValues;

        public string[] FixedValues
        {
            get { return _fixedValues; }
        }
        #endregion Fields and Properties

        #region Constructors
        public XSLAttribute(string name, bool isDefault, bool isMandatory,
            string fixedValuesCSV, string toolTip)
        {
            _name = name;
            _isDefault = isDefault;
            _isMandatory = isMandatory;
            _fixedValues = fixedValuesCSV.Split(",".ToCharArray());
            _toolTip = toolTip;
        }

        public XSLAttribute(string name, bool isDefault, bool isMandatory,
            string toolTip)
        {
            _name = name;
            _isDefault = isDefault;
            _isMandatory = isMandatory;
            _toolTip = toolTip;
        }

        public XSLAttribute(string name, bool isDefault, bool isMandatory)
        {
            _name = name;
            _isDefault = isDefault;
            _isMandatory = isMandatory;
        }

        public XSLAttribute(string name, string toolTip)
        {
            _name = name;
            _toolTip = toolTip;
            _isDefault = false;
            _isMandatory = false;
        }

        public XSLAttribute(string name)
        {
            _name = name;
            _isDefault = false;
            _isMandatory = false;
        }
        #endregion Constructors
    } // end XSLAttribute

    public class XSLTags
    {
        private List<XSLTag> _XslTagList;

        public XSLTags()
        {
            _XslTagList = GetXSLTags();
        }

        public static List<XSLTag> GetXSLTags()
        {
            List<XSLTag> xslTags = new List<XSLTag>();
            XSLTag curTag = null;
                        
            curTag = new XSLTag("xsl:apply-imports", string.Empty,
                "Works in conjuction w/imported templates to apply 'base class' templates");
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:apply-templates", string.Empty,
                "Defines set of nodes to be processed by selecting appropriate template rules");
            curTag.Attributes.Add(new XSLAttribute("select",
                "Node-set to be processed; if omitted all children of current node"));
            curTag.Attributes.Add(new XSLAttribute("mode",
                "Optional processing mode; only template rules match that have the same mode"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:attribute", "xsl:template,xsl:attribute-set",
                "Outputs attribute name and value to current output destination");
            curTag.Attributes.Add(new XSLAttribute("name", true, true,
                "Mandatory name of attribute to be generated"));
            curTag.Attributes.Add(new XSLAttribute("namespace",
                "Optional namespace URI of generate attribute"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:attribute-set", string.Empty,
                "Top level element used to group and re-use common sets of attributes");
            curTag.Attributes.Add(new XSLAttribute("name", true, true,
                "Mandatory name of the attribute set"));
            curTag.Attributes.Add(new XSLAttribute("use-attribute-sets",
                "Optional whitespace-separated list of qualified names of other attribute sets to include."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:call-template", "xsl:template",
                "Invokes a named template (calls a 'method')");
            curTag.Attributes.Add(new XSLAttribute("name", true, true,
                "Madatory qualified name of the template to be called"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:choose", "xsl:template,xsl:variable,xsl:with-param",
                "Defines a choice between a number of alernatives (i.e. switch)");
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:comment", string.Empty,
                "Writes a <!--comment--> to current output destination");
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:copy", string.Empty,
                "Performs shallow copy of current node (w/namespaces) to output " +
                "destination. For deep copy use xsl:copy-of");
            curTag.Attributes.Add(new XSLAttribute("use-attribute-sets",
                "Optional whitespace-delimited list of qualified attribute sets to " +
                "be applied to generated node if an element."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:copy-of", string.Empty,
                "Performs deep copy of nodeset to current output destination.");
            curTag.Attributes.Add(new XSLAttribute("select", true, true,
                "Mandatory node-set expression to be copied to output destination"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:decimal-format", string.Empty,
                "Defines characters and symbols used when converting numbers into " +
                "strings using format-number(). Must be immediate child of xsl:stylesheet.", false);
            curTag.Attributes.Add(new XSLAttribute("name", "Optional name of format"));
            curTag.Attributes.Add(new XSLAttribute("decimal-separator",
                "Character used to separate integer and fraction; default is '.'"));
            curTag.Attributes.Add(new XSLAttribute("grouping-separator",
                "Character used to separate groups of digits. Default is ','"));
            curTag.Attributes.Add(new XSLAttribute("infinity",
                "String used to represent infinity. Default is 'Infinity'"));
            curTag.Attributes.Add(new XSLAttribute("minus-sign",
                "Character used as minus sign. Default is '-'"));
            curTag.Attributes.Add(new XSLAttribute("NaN",
                "String used to represent Not a Number. Default is 'NaN'"));
            curTag.Attributes.Add(new XSLAttribute("percent",
                "Character used to represent percentage sign. Default is '%'"));
            curTag.Attributes.Add(new XSLAttribute("per-mille",
                "Character used to repfresent per-thousand sign. Default is '‰'"));
            curTag.Attributes.Add(new XSLAttribute("zero-digit",
                "Character used to indicate place where a leading or traling zero " +
                "digit is required even if not significant. Default is '0'"));
            curTag.Attributes.Add(new XSLAttribute("digit",
                "Character used to indicate place where digit will be. Default is '#'"));
            curTag.Attributes.Add(new XSLAttribute("pattern-separator",
                "Character used in format pattern to separate subpattern for positive " +
                "numbers from that of negative. Default is ';'"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:document", string.Empty,
                "Creates a new output file at current location");
            curTag.Attributes.Add(new XSLAttribute("href", true, true,
                "Mandatory relative or absolute URI indicating location where output document " +
                "will be written after serialization"));
            curTag.Attributes.Add(new XSLAttribute("method", false, false, "xml,html,text",
                "Required output format"));
            curTag.Attributes.Add(new XSLAttribute("version", "NMToken defining version of output format"));
            curTag.Attributes.Add(new XSLAttribute("encoding", "Defines character encoding"));
            curTag.Attributes.Add(new XSLAttribute("omit-xml-declaration", false, false, "yes,no",
                "Whether to omit xml declaration"));
            curTag.Attributes.Add(new XSLAttribute("standalone", false, false, "yes,no",
                "Indicates standalone declaration should be included in output and gives its value"));
            curTag.Attributes.Add(new XSLAttribute("doctype-public",
                "Indicates public identifier to be used in DOCTYPE declaration"));
            curTag.Attributes.Add(new XSLAttribute("doctype-system",
                "Indicates system identifier to be used in DOCTYPE declaration"));
            curTag.Attributes.Add(new XSLAttribute("cdata-section-elements",
                "Whitespace-delimited names of elements whose text content is to be output in CDATA sections"));
            curTag.Attributes.Add(new XSLAttribute("indent", false, false, "yes,no",
                "Indicates whether output should be indented to match its hiearchical structure"));
            curTag.Attributes.Add(new XSLAttribute("media-type",
                "Indicates MIME type to be associated w/output file"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:element", string.Empty,
                "Outputs an element node to current output destination");
            curTag.Attributes.Add(new XSLAttribute("name", true, true,
                "Mandatory name of element to be generated"));
            curTag.Attributes.Add(new XSLAttribute("namespace", "Optional namespace URI of generated element"));
            curTag.Attributes.Add(new XSLAttribute("use-attribute-sets",
                "List of named atribute sets to be added to generated element"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:fallback", string.Empty,
                "Processing block executed if no implementation of its parent " +
                "instruction is available; for example, if processed by an earlier " +
                "version processor that does not recognize extensions or from custom 3rd party");
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:for-each", string.Empty,
                "Selects a set of nodes using an XPath expression and performs the " +
                "same processing for each node in the set");
            curTag.Attributes.Add(new XSLAttribute("select", true, true,
                "Mandatory xpath expression of set of nodes to be processed"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:if", string.Empty,
                "Encloses a block that will be executed only if specified test " +
                "condiation evaluates to true. For else-if see xsl:choose");
            curTag.Attributes.Add(new XSLAttribute("test", true, true,
                "Mandatory boolean conditional expression to be tested"));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:import", string.Empty,
                "Top-level element (child of xsl:stylesheet) that imports contents " +
                "of one stylesheet module into another. Often used for resuable " +
                "XSL 'components' that can be inherited and overriden if needed.", false);
            curTag.Attributes.Add(new XSLAttribute("href", true, true, 
                "Mandatory URI of stylesheet to be imported."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:include", string.Empty,
                "Top-level element to include the contents of one stylesheet module " +
                "in current module. Definitions in other modules come in w/same " +
                "import precedence as current module. This is used to divide up " +
                "stylesheets into multiple files, but not get inheritance like " +
                "with xsl:import", false);
            curTag.Attributes.Add(new XSLAttribute("href", true, true, 
                "Mandatory URI of stylesheet to be imported."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:key", string.Empty,
                "A top-level element which declares a named key that can be used " +
                "in the style sheet with the key() function. Does not have to be unique.",
                false);
            curTag.Attributes.Add(new XSLAttribute("name", true, true, 
                "Required. Specifies the name of the key"));
            curTag.Attributes.Add(new XSLAttribute("href", false, true,
                            "Required. Defines the nodes to which the key will be applied."));
            curTag.Attributes.Add(new XSLAttribute("use", false, true,
                            "Required. The value of the key for each of the nodes."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:message",string.Empty,
                "Writes a message to the output. Typically used to report errors. " + 
                "This element can contain almost any other XSL element. ", true);
            curTag.Attributes.Add(new XSLAttribute("terminate", true, false,
                "Optional. 'yes' terminates the processing after the message is written " +
                "to the output. 'no' continues the processing after the message is " +
                "written to the output. Default is 'no'."));
            xslTags.Add(curTag);

            curTag = new XSLTag("xsl:namespace-alias", string.Empty,
                "Used to replace a namespace in the style sheet to a different namespace " +
                "in the output. A top-level element, and must be a child node of " +
                "<xsl:stylesheet> or <xsl:transform>.", false);
            curTag.Attributes.Add(new XSLAttribute("stylesheet-prefix", false, true,
                "Required. Specifies the namespace you wish to change"));
            curTag.Attributes.Add(new XSLAttribute("result-prefix", false, true,
                "Required. Specifies the desired namespace for the output"));
            xslTags.Add(curTag);

            //TODO: attributes etc., cleanup

            xslTags.Add(new XSLTag("xsl:number", "", "determine the integer position of the "
                + "current node in the source. It is also used to format a number.", false));

            xslTags.Add(new XSLTag("xsl:otherwise", "xsl:choose", "specifies a default action for the <xsl:choose> "
                + "element. This action will take place when none of the <xsl:when> conditions apply.", 
                true));

            xslTags.Add(new XSLTag("xsl:output", "", "Defines the format of the output document. "
                + "Must be a child node of <xsl:stylesheet> or <xsl:transform>", false));

            xslTags.Add(new XSLTag("xsl:param", "", "Declares a local or global parameter (if top-level).", true));

            xslTags.Add(new XSLTag("xsl:preserve-space", "", "Specifies elements for which white "
                + "space should be preserved (defaults to preserved).", false));

            xslTags.Add(new XSLTag("xsl:processing-instruction", "", "writes a processing instruction to the output.", true));

            xslTags.Add(new XSLTag("xsl:script", "", "Used at top-level of stylesheet to define implementation "
                + "of extension functions", true));
            
            xslTags.Add(new XSLTag("xsl:sort", "", "Used to sort the output. Must appear within "
                + "<xsl:for-each> or <xsl:apply-templates>", false));

            xslTags.Add(new XSLTag("xsl:strip-space", "", "Top-level element used to define the "
                + "elements for which white space should be removed.", false));

            xslTags.Add(new XSLTag("xsl:stylesheet", "", "Defines root element of style sheet. "
                + "Same as xsl:transform.", true));            

            xslTags.Add(new XSLTag("xsl:template", "", "Top level element containing rules/content to "
            + "apply when specified node is matched via the match attribute criteria.", true));

            xslTags.Add(new XSLTag("xsl:text", "", "Used to write literal text to the output.", 
                true));

            xslTags.Add(new XSLTag("xsl:transform", "", "Defines root element of style sheet. "
                + "Same as xsl:stylesheet.", true));

            xslTags.Add(new XSLTag("xsl:value-of", "", "Used to extract the value of an XML element "
                + "and add it to output stream of the transformation", false));

            xslTags.Add(new XSLTag("xsl:variable", "", "Declares a local or global variable depending "
                + "on scope. Specify value by content of element OR via select attribute.", true));

            xslTags.Add(new XSLTag("xsl:when", "", "Specifies an action for within a <xsl:choose> element. "
                + "When test condition is meet, contained rules are run.", true));

            xslTags.Add(new XSLTag("xsl:with-param", "", "Defines value of a parameter to be passed "
                + "into a template that has the matching xsl:param", false));            


            return xslTags;
        }
    } // end XSLTags

    public class XSLFunctions
    {
        List<string> _xslFunctions;

        public XSLFunctions()
        {
            _xslFunctions = GetXslFunctions();
        }

        public static List<string> GetXslFunctions()
        {
            List<string> xslFunctions = new List<string>();

            //to include the () or not to include the ()
            xslFunctions.Add("boolean");
            xslFunctions.Add("ceil");
            xslFunctions.Add("ceiling");
            xslFunctions.Add("concat");
            xslFunctions.Add("contains");
            xslFunctions.Add("count");
            xslFunctions.Add("current");
            xslFunctions.Add("document");
            xslFunctions.Add("element-available");
            xslFunctions.Add("entity-uri");
            xslFunctions.Add("false");
            xslFunctions.Add("floor");
            xslFunctions.Add("format-number");
            xslFunctions.Add("function-available");
            xslFunctions.Add("generate-id");
            xslFunctions.Add("getLast");
            xslFunctions.Add("getRemainder");
            xslFunctions.Add("id");
            xslFunctions.Add("init");
            xslFunctions.Add("key");
            xslFunctions.Add("lang");
            xslFunctions.Add("last");
            xslFunctions.Add("local-name");
            xslFunctions.Add("name");
            xslFunctions.Add("namespace-uri");
            xslFunctions.Add("normalize-space");
            xslFunctions.Add("not");
            xslFunctions.Add("number");
            xslFunctions.Add("position");
            xslFunctions.Add("refresh");
            xslFunctions.Add("round");
            xslFunctions.Add("starts-with");
            xslFunctions.Add("string");
            xslFunctions.Add("string-length");
            xslFunctions.Add("substring");
            xslFunctions.Add("substring-after");
            xslFunctions.Add("substring-before");
            xslFunctions.Add("sum");
            xslFunctions.Add("system-property");
            xslFunctions.Add("translate");
            xslFunctions.Add("true");
            xslFunctions.Add("unparsed-entity-uri");

            return xslFunctions;
        }
    }
}
