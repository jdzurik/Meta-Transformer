using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Library.Controls
{
    /// <summary>
    /// Houses/groups settings related to syntax highlighting
    /// </summary>
    [Serializable]
    public class SyntaxSettings
    {
        #region Declarations
        //public
        public string[] Keywords;

        //private
        private string _commentPattern;
        private Color _keywordColor;
        private Color _integerColor;
        private Color _stringColor;
        private Color _commentColor;

        private bool _enableComments;
        private bool _enableIntegers;
        private bool _enableStrings;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the regular expression pattern that selects comments
        /// </summary>        
        public string CommentPattern
        {
            get { return _commentPattern; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        Regex tempRegEx = new Regex(value);
                        _commentPattern = value;
                    }
                    catch (Exception)
                    {
                        //TODO: throw not working and serialization wrong, sets to null control grayed out
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of keywords
        /// </summary>
        [Category("Appearance"),
        Description("Gets or sets the color of keywords")]        
        public Color KeywordColor
        {
            get { return _keywordColor; }
            set { _keywordColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of strings
        /// </summary>
        [Category("Appearance"),
        Description("Gets or sets the color of strings")]
        public Color StringColor
        {
            get { return _stringColor; }
            set { _stringColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of integers
        /// </summary>
        [Category("Appearance"),
        Description("Gets or sets the color of integers")]
        public Color IntegerColor
        {
            get { return _integerColor; }
            set { _integerColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of comments
        /// </summary>
        [Category("Appearance"),
        Description("Gets or sets the color of comments")]
        public Color CommentColor
        {
            get { return _commentColor; }
            set { _commentColor = value; }
        }

        /// <summary>
        /// Gets or sets value indicating if syntax highlighing is enabled for comments
        /// </summary>
        [Category("Behavior"),
        Description("Gets or sets value indicating if syntax highlighing is enabled for comments")
        ]//,
        //DefaultValue(true) //TODO: look at why not serializing when defaulting to true        
        public bool EnableComments
        {
            get { return _enableComments; }
            set { _enableComments = value; }
        }

        private bool ShouldSerializeEnableComments() //wrong sig?
        {
            return true; 
        }

        /// <summary>
        /// Gets or sets value indicating if syntax highlighing is enabled for integers
        /// </summary>
        [Category("Behavior"),
        Description("Gets or sets value indicating if syntax highlighing is enabled for integers")]
        public bool EnableIntegers
        {
            get { return _enableIntegers; }
            set { _enableIntegers = value; }
        }

        /// <summary>
        /// Gets or sets value indicating if syntax highlighing is enabled for strings
        /// </summary>
        [Category("Behavior"),
        Description("Gets or sets value indicating if syntax highlighing is enabled for strings")]
        public bool EnableStrings
        {
            get { return _enableStrings; }
            set { _enableStrings = value; }
        }
        #endregion Public Properties
    }
       
    
}
