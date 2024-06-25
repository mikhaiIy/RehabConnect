using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace RehabConnect.Models.ViewModel;


public class EnrollProgramVM
{
    public IEnumerable<Schedule>? Schedule { get; set; }

    [ValidateNever]
    public StudentProgram? StudentProgram { get; set; }

    public IEnumerable<Step>? StepList { get; set; }

    public int stepId;

    public IEnumerable<Program>? ProgramList { get; set; }

    public string ScheduleDataJson { get; set; }
}