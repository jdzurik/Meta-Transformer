
#region Libraries Used
using Common.Library.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
#endregion Libraries Used

namespace XSL.Library
{    
    /// <summary>
    /// Houses/groups settings related to XSL syntax highlighting
    /// </summary>
    [Serializable]
    public class XslSyntaxSettings : SyntaxSettings
    {
        private Color _xslTagColor = Color.Blue;
        private Color _characterEntityColor = Color.DarkViolet;
        private Color _attributeNameColor = Color.Crimson;
        private Color _attributeValueColor = Color.Green;
        private Color _xslFunctionsColor = Color.Tomato;

        /// <summary>
        /// Gets or sets syntax color for valid XSL tag names
        /// </summary>
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets syntax color for valid XSL tag names"),
        DefaultValue(typeof(Color), "Blue")]
        public Color XslTagColor
        {
            get { return _xslTagColor; }
            set
            {
                _xslTagColor = value;
            }
        }        

        /// <summary>
        /// Gets or sets syntax color for character entities (&#10; &amp; etc.)
        /// </summary>
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets syntax color for character entities (&#10; &amp; etc.)"),
        DefaultValue(typeof(Color), "DarkViolet")]
        public Color CharacterEntityColor
        {
            get { return _characterEntityColor; }
            set { _characterEntityColor = value; }
        }        

        /// <summary>
        /// Gets or sets syntax color for attribute names
        /// </summary>
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets syntax color for attribute names"),
        DefaultValue(typeof(Color), "Crimson")]
        public Color AttributeNameColor
        {
            get { return _attributeNameColor; }
            set { _attributeNameColor = value; }
        }        

        /// <summary>
        /// Gets or sets syntax color for attribute values
        /// </summary>
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets syntax color for attribute values"),
        DefaultValue(typeof(Color), "Green")]
        public Color AttributeValueColor
        {
            get { return _attributeValueColor; }
            set { _attributeValueColor = value; }
        }
        
        /// <summary>
        /// Gets or sets syntax color for valid XSL functions
        /// </summary>
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets syntax color for valid XSL functions"),
        DefaultValue(typeof(Color), "Tomato")]
        public Color XslFunctionsColor
        {
            get { return _xslFunctionsColor; }
            set { _xslFunctionsColor = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool EnableIntegers
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool EnableStrings
        {
            get { return false; }
        }
    }
}
