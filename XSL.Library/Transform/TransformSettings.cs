using System;
using System.Collections.Generic;
using System.Text;

namespace XSL.Library
{
    public class TransformSettings
    {
        private bool _enableDebug = true;
        private bool _enableParamPrompting = true;
        private bool _abortOnCancelParams = true;
        private bool _showParamsOnTop = false;
        private Dictionary<string, string> _parameters;        

        public TransformSettings()
        {
        }

        /// <summary>
        /// When <see cref="EnableParamPrompting"/> is true, and user is prompted for stylesheet
        /// parameters, this indicates if the transform should be aborted if user clicks Cancel.
        /// </summary>
        public bool AbortOnCancelParams
        {
            get { return _abortOnCancelParams; }
            set { _abortOnCancelParams = value; }
        }

        /// <summary>
        /// Indicates if XSL debugging is enabled. This is for potential future use.
        /// </summary>
        public bool EnableDebug
        {
            get { return _enableDebug; }
            set { _enableDebug = value; }
        }

        /// <summary>
        /// Indicates whether user will be prompted for any xsl:param inputs found in the
        /// stylesheet.
        /// </summary>
        public bool EnableParamPrompting
        {
            get { return _enableParamPrompting; }
            set { _enableParamPrompting = value; }
        }        

        /// <summary>
        /// When <see cref="EnableParamPrompting"/> is true, specifies whether the xsl parameter
        /// input form will be shown on top or not
        /// </summary>
        public bool ShowParamsOnTop
        {
            get { return _showParamsOnTop; }
            set { _showParamsOnTop = value; }
        }

        /// <summary>
        /// Key value pairs representing params that will be passed in as inputs for the
        /// transform (xsl:param). This can be used regardless of <see cref="EnableParamPrompting"/>,
        /// but if user parameter prompting is enabled, care should be taken to ensure the same
        /// parameter value is not specified both here and by the user.
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }       
    }
}
