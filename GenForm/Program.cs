using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GenForm
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        else { 
            Form1 f1 = new Form1();
            f1.OpenGenFile(args[0]);
            f1.RunTransform();
        }
    }
  }
}
