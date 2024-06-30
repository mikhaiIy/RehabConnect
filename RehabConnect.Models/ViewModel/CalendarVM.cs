namespace RehabConnect.Models.ViewModel;

public class CalendarVM
{
    public int id { get; set; } // Schedule Id
    public string? title { get; set; } // Program Name 
    public DateTime start { get; set; } // Date + Start Time
    public DateTime end { get; set; } // Date + Enb Time

    public ExtendedProps ExtendedProps { get; set; }
}

public class ExtendedProps
{
    public string Calendar { get; set; }
    public int Capacity { get; set; }
    
    public int Registered { get; set; }
    
    public string Students { get; set; }
}
