using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Library.IO
{
    public class TemporaryFile : IDisposable
    {
		private bool _IsDisposed; 
        private bool _Keep; 
        private string _Path;

		public TemporaryFile()
        {
            _Path = WriteToTempFile();
        }

        public TemporaryFile(string data)
        {
            _Path = WriteToTempFile(data);
        }

        public TemporaryFile(string data, string extension)
        { 
            _Path = WriteToTempFile(data, extension); 
        }

		public string Path 
        { 
            get { return _Path; } 
        }

		public bool Keep 
        { 
            get { return _Keep; } 
            set { _Keep = value; } 
        }

		~TemporaryFile() 
        { 
            Dispose(false); 
        }

		public void Dispose() 
        { 
            Dispose(false); 
            GC.SuppressFinalize(this); 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed)
            {
                _IsDisposed = true;

                if (!_Keep) 
                { 
                    TryDelete(); 
                }
            }
        }

        private void TryDelete() 
        { 
            try 
            { 
                File.Delete(_Path); 
            } 
            catch (IOException) 
            { 
            } 
            catch (UnauthorizedAccessException) 
            { 
            }
        }
        
        public static string WriteToTempFile()
        {
            return WriteToTempFile(null, ".tmp");
        }

        public static string WriteToTempFile(string data)
        {
            return WriteToTempFile(data, ".tmp");
        }

        public static string WriteToTempFile(string data, string extension)
        {
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            //  Writes text to a temporary file and returns path
            string strFilename = System.IO.Path.GetTempFileName();
            FileInfo fi = new FileInfo(strFilename);
            string destFilename = fi.FullName;
            if (fi.Extension.ToLower() != extension.ToLower())
            {
                destFilename = System.IO.Path.ChangeExtension(fi.FullName, extension);
                fi.MoveTo(destFilename);
            }

            if (!string.IsNullOrEmpty(data))
            {
                FileStream objFS = new FileStream(destFilename, FileMode.Append, FileAccess.Write);
                //  Opens stream and begins writing
                StreamWriter Writer = new StreamWriter(objFS);
                Writer.BaseStream.Seek(0, SeekOrigin.End);
                Writer.WriteLine(data);
                Writer.Flush();
                //  Closes and returns temp path
                Writer.Close();
            }

            return destFilename;
        }
    }
}
