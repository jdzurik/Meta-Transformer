using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    [Serializable]
    public class SchemaMap : BaseSet
    {
        public string XmlSchemaPath { get; set; }
        public string GenObjectType { get; set; }
    }
}


