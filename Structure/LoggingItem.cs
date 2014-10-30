using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Structure
{
  [Serializable]
  public class LoggingItem
  {
    public Guid GUID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public String Message { get; set; }
    public String Name { get; set; }
    public LoggingEnum LogType { get; set; }
    public TimeSpan TimeElapsed { get; set; }
    public Double TotalItems { get; set; }
    public Double OnItem{ get; set; }
    public Int32 PercentComplete { get; set; }
   

    public LoggingItem()
    {
      GUID = new Guid();
      StartTime = DateTime.Now;
      EndTime = DateTime.Now;
      Message = "";
      Name = "";
      LogType = LoggingEnum.Info;

    }

    public LoggingItem(String pMessage)
    {
      GUID = new Guid();
      StartTime = DateTime.Now;
      EndTime = DateTime.Now;
      Message = pMessage;
      Name = "";
      LogType = LoggingEnum.Info;

    }

    public LoggingItem(String pMessage, String pName)
    {
      GUID = new Guid();
      StartTime = DateTime.Now;
      EndTime = DateTime.Now;
      Message = pMessage;
      Name = pName;
      LogType = LoggingEnum.Info; 

    }

    public LoggingItem(String pMessage, String pName, LoggingEnum pLogType)
    {
      GUID = new Guid();
      StartTime = DateTime.Now;
      EndTime = DateTime.Now;
      Message = pMessage;
      Name = pName;
      LogType = pLogType;

    }

    public LoggingItem(DateTime pStartTime, String pMessage, String pName, LoggingEnum pLogType)
    {
      GUID = new Guid();
      StartTime = pStartTime;
      Message = pMessage;
      Name = pName;
      LogType = pLogType;
    }

    public LoggingItem(Double pTotalItems, String pMessage, String pName, LoggingEnum pLogType)
    {
      GUID = new Guid();
      StartTime = DateTime.Now;
      Message = pMessage;
      Name = pName;
      TotalItems = pTotalItems;
      LogType = pLogType;
    }

    public void CalcTime() 
    {
      if (EndTime != null && StartTime != null )
      {
        TimeElapsed = EndTime.Subtract(StartTime);
      }
    }

    public void CalcPercent() 
    {
      if (OnItem != 0.0 && TotalItems != 0.0)
      {
        PercentComplete = Convert.ToInt32((OnItem / TotalItems) * 100);
      }
    } 


  }



  [Serializable]
  public class LoggingList : List<LoggingItem>
  { 
  }

}
