using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnect.Models.ViewModel;

public class StepVM
{
    [ValidateNever]
    public Step? Step { get; set; }
    [ValidateNever]
    public Program? Program { get; set; }
    
    [ValidateNever]
    public Roadmap? Roadmap { get; set; }
}