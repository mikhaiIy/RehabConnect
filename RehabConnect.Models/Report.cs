using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RehabConnect.Models;

namespace RehabConnect.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        // Navigation properties
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        //public int TherapistID { get; set; }
        //[ForeignKey("TherapistID")]
        //public Therapist Therapist { get; set; }
        //public int CSID { get; set; }
        //[ForeignKey("CSID")]
        //public CustomerSupport CustomerSupport { get; set; }
        // END Navigation properties
        public int NoSession {  get; set; }
        public DateTime DateReport { get; set; }
        public bool IndividualTherapy { get; set; }
        public bool GroupTherapy { get; set; }
        public bool EarlyIntervention { get; set; }
        public bool BehaviorManagement { get; set; }

        // Subjective Assessment
        public bool CanEnterSelf { get; set; }
        public bool WithPrompting { get; set; }
        public bool DifficultiesSeparateWithParents { get; set; }
        public bool WithCryingAndRefuse { get; set; }
        public bool GreetingWithPrompt { get; set; }
        public bool GreetingBySelf { get; set; }
        public bool Mute { get; set; }
        public bool RefuseToEnter { get; set; }
        public string? SubjectiveAssessmentNotes { get; set; }

        // Motor & Praxis Skills
        public bool RangeOfMotion { get; set; }
        public bool MuscleTone { get; set; }
        public bool MuscleStrength { get; set; }
        public bool JointMobility { get; set; }
        public bool TrunkControlBalance { get; set; }
        public bool Standing { get; set; }
        public bool Crawling { get; set; }
        public bool Walking { get; set; }
        public bool Jumping { get; set; }
        public bool BroadJump { get; set; }
        public bool KickBall { get; set; }
        public bool ThrowBall { get; set; }
        public bool GraspRelease { get; set; }
        public bool Reaching { get; set; }
        public bool PutBlockInCup { get; set; }
        public bool Scribbles { get; set; }
        public bool TowerOfCubes { get; set; }
        public bool MaturePencilGrasp { get; set; }
        public bool ImmaturePencilGrasp { get; set; }
        public bool ImitateVerticalLine { get; set; }
        public bool CopySquare { get; set; } //i) Copying Square, Triangle, Rhombus
        public bool CopyTriangle { get; set; } //no need
        public bool CopyRhombus { get; set; } //no need
        public string? MotorPraxisSkillsNotes { get; set; }

        // Sensory Regulation Skills
        public bool Tactile { get; set; }
        public bool Auditory { get; set; }
        public bool Visual { get; set; }
        public bool Otectomy { get; set; }
        public bool Gustatory { get; set; }
        public bool Vestibular { get; set; }
        public bool Proprioception { get; set; }
        public string? SensoryRegulationSkillsNotes { get; set; }

        // Cognitive Regulation Skills
        public bool Alphabet { get; set; }
        public bool Numbers { get; set; }
        public bool Shapes { get; set; }
        public bool Colors { get; set; }
        public bool MemoryFunction { get; set; }
        public bool Attention { get; set; }
        public string? Concentration { get; set; } // Assuming good/fair/poor is represented as a string
        public string? ProblemSolving { get; set; } // Assuming good/fair/poor is represented as a string
        public bool WritingSkill { get; set; }
        public string? CognitiveRegulationSkillsNotes { get; set; }

        // Occupational Performance
        // Activity Daily Living (ADL)
        public bool ADLIndependent { get; set; }
        public string? ADLSupervision { get; set; } // (all the time/ most of the time/ sometimes)
        public bool ADLMaxAssistance { get; set; }
        public bool ADLToiletTrained { get; set; }

        // Instrumental Activity Daily Living (IADL)
        public bool IADLMoneyManagement { get; set; }
        public bool IADLTimeConcept { get; set; }
        public bool IADLFoldingClothes { get; set; }
        public bool IADLHangingClothes { get; set; }
        public bool IADLSweepFloor { get; set; }
        public bool IADLMakingDrinks { get; set; }
        public bool IADLPrepareSimpleFood { get; set; }
        public bool IADLUsePhone { get; set; }
        public string? OccupationalPerformanceNotes { get; set; }

        // Emotional Regulation Skills
        public bool TemperedTantrum { get; set; }
        public bool Manipulative { get; set; }
        public bool EasilyDistracted { get; set; }
        public bool Passive { get; set; }
        public bool Cooperative { get; set; }
        public bool Isolation { get; set; }
        public bool Reluctant { get; set; }
        public string? EmotionalRegulationSkillsNotes { get; set; }

        // Communication & Social Skills
        public bool RepetitivePrompting { get; set; }
        public string? VerbalPrompting { get; set; } // Representing min/mod/max as a string
        public string? PhysicalPrompting { get; set; } // Representing min/mod/max as a string
        public string? EyeContactPerson { get; set; } // Representing good/fair/poor as a string
        public string? EyeContactObject { get; set; } // Representing good/fair/poor as a string
        public bool InitiateAnswerQuestion { get; set; }
        public bool VerbalRespond { get; set; }
        public string? VoiceClarity { get; set; } // Representing clear/slurred/imitate last vowel sound/no speech as a string
        public bool FacialExpressions { get; set; }
        public bool BodyLanguage { get; set; }
        public bool TakingTurn { get; set; }
        public bool Sharing { get; set; }
        public bool StayInGroup { get; set; }
        public string? CommunicationSocialSkillsNotes { get; set; }

        // Academic Performance
        public string? AcademicPerformance { get; set; } // Representing Good/Average/Fair/Poor/Unable to follow as a string
        public string? AcademicPerformanceNotes { get; set; } //no need

        // Final Notes
        public string? AnalysisProblem { get; set; }
        public string? ShortTermGoal { get; set; }
        public string? LongTermGoal { get; set; }
        public string? TxPlan { get; set; }
    }
}