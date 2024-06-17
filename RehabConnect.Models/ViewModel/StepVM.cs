using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnect.Models.ViewModel;

public class StepVM
{
    public Step Step { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> RoadmapList { get; set; }
}