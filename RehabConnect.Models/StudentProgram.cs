using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RehabConnect.Models;

public class StudentProgram
{
    public int StudentID { get; set; }
    [ForeignKey("StudentID")]
    public Student Student { get; set; }
    
    public int ProgramID { get; set; }
    [ForeignKey("ProgramID")]
    public Program Program { get; set; }
    

}