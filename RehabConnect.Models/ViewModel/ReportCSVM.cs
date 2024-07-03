using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel;

public class ReportCSVM
{
    
    public List<TableData> TableDatas { get; set; }
    
    public int SessionCount { get; set; }
    public int StudentCount { get; set; } 
    public int ReportCount { get; set; }
    
    public int ConfirmedReport { get; set; }
}

public class TableData
{
    public List<ReportList> ReportList { get; set; }
    public List<StudentDetail> StudentDetail { get; set; }
}

public class ReportList
{
    [ValidateNever]
    public List<Report> Report { get; set; }
}

public class StudentDetail
{
    [ValidateNever]
    public Student Student { get; set; }
    [ValidateNever]
    public Program Program { get; set; }
    [ValidateNever]
    public Step Step { get; set; }
    [ValidateNever]
    public Roadmap Roadmap { get; set; }
    [ValidateNever]
    public StudentStatus Status { get; set; }
}