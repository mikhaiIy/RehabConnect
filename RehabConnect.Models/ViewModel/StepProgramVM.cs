namespace RehabConnect.Models.ViewModel;

public partial class StepProgramVM
{
    public IEnumerable<Step> Step { get; set; }
    public IEnumerable<Program> Program { get; set; }
}