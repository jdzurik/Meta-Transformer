
using Common.Library.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XSL.Library
{
    public class TransformResults : IDisposable
    {
        private string _output;
        private bool _cancelled;
        private TemporaryFile _tempFile;
        private string _outputFilename = string.Empty;
        private bool _IsDisposed; 
        
        internal TransformResults()
        {
        }

        internal TransformResults(string output, bool cancelled)
        {
            _output = output;
            _cancelled = cancelled;
        }

        public bool Cancelled
        {
            get { return _cancelled; }
            internal set { _cancelled = value; }
        }

        public string Output
        {
            get { return _output; }
            internal set { _output = value; }
        }

        public string OutputFilename
        {
            get { return _outputFilename; }
            private set { _outputFilename = value; }
        }

        public void WriteOutputToFile(string filename)
        {
            // tranformation result retrieved as string either way but if filename is specified...
            if (!string.IsNullOrEmpty(filename))
            {
                // then dump results to specified filename
                File.WriteAllText(filename, _output);
                this.OutputFilename = filename;
            }
        }

        public void WriteOutputToTempFile(string extension)
        {
            _tempFile = new TemporaryFile(_output, extension);
            _outputFilename = _tempFile.Path;
        }

        public void WriteOutputToTempFile()
        {
            WriteOutputToTempFile("txt");
        }

        public override string ToString()
        {
            return this.GetType().Name + " [Output]: " + _output;
        }

        ~TransformResults()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed)
            {
                if (disposing && null != _tempFile)
                {
                    _tempFile.Dispose();
                }

                _IsDisposed = true;                
            }
        }
    }
}
