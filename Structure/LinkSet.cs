using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
  public class LinkSet : BaseSet
  {
    public Guid TableSetID { get; set; }
    public Guid ColumnSetID { get; set; }
    public Boolean IsSingle { get; set; }
    public Boolean LazyLoad { get; set; }
    public Boolean OverrideProperty { get; set; }
    
  }
}
