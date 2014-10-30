using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace Structure
{

  public enum LoggingEnum
  {
    Error,
    Info,
    Progress,
    Duration
  }

  public enum GenEnum
  {
    All,
    Individual
  }

  public enum TableSetType
  {
    Table,
    View,
    Object
  }

  public enum ServerSetType
  {
    MSSQL = 0,
    Oracle = 1,
    XML = 2,
    JSON = 3
  }

  public enum ServerSetAuthType
  {
    User,
    Network
  }

}
