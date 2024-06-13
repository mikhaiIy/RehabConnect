
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnect.Models.ViewModel
{
    public class RoadmapVM
    {
        public Roadmap Roadmap { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoadmapList { get; set; }
        
    }
}
