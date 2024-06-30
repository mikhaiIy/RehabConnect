using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RehabConnect.Models;

public class StudentProgram
{
    [Key]
    public int StudentProgramId { get; set; }
    
    public int StudentID { get; set; }
    [ForeignKey("StudentID")]
    public Student Student { get; set; }
    
    public int ProgramID { get; set; }
    [ForeignKey("ProgramID")]
    public Program Program { get; set; }
    
    public StudentStatus Status { get; set; }
    
}

public enum StudentStatus
{
    Ongoing,
    Completed,
    OnLeave,
    PaymentPending
}

