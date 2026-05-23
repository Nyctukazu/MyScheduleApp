using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyScheduleApp;

public class CourseSession
{
    public string Subject {get; set;}
    public string startTime {get; set;}
    public string endTime {get; set;}
    public string Faculty {get; set;}
    public string Abbrev {get; set;}
}