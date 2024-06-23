using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //public DbSet<Announcement> Announcements { get; set; }
        //public DbSet<RegistrationStatus> RegistrationStatuses { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<ParentDetail> ParentDetails { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<CustomerSupport> CustomerSupports { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Roadmap> Roadmap { get; set; }
        
        public DbSet<StudentProgram> StudentPrograms { get; set; }
        
        
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Program Module
            // Data Seeding -

            modelBuilder.Entity<Roadmap>().HasData(
                new Roadmap{RoadmapId = 1,Name = "Idzmir Hub Roadmap"},
                new Roadmap{RoadmapId = 2,Name = "Test 2"});
            
            modelBuilder.Entity<Step>().HasData(
                new Step{StepId = 1, RoadmapId = 1, StepNo = 1, Title = "Screening and Consultation", CombinedPricing = true, PriceWeekday = 150, PriceWeekend = 200},
                new Step{StepId = 2, RoadmapId = 1, StepNo = 2, Title = "Full Assessment", CombinedPricing = true, PriceWeekday = 700, PriceWeekend = 800},
                new Step{StepId = 3, RoadmapId = 1, StepNo = 3, Title = "Intervention with consistency", CombinedPricing = false},
                new Step{StepId = 4, RoadmapId = 1, StepNo = 4, Title = "Re-assessment", CombinedPricing = true, PriceWeekday = 700, PriceWeekend = 800},
                new Step{StepId = 5, RoadmapId = 1, StepNo = 5, Title = "Road to School", CombinedPricing = false},
                new Step{StepId = 6, RoadmapId = 2, StepNo = 1, Title = "Test1"},
                new Step{StepId = 7, RoadmapId = 2, StepNo = 2, Title = "Test2"}
                );
            
            modelBuilder.Entity<Program>().HasData(
                new Program{ProgramID = 1, ProgramName = "Consultation", NumOfSession = 1,StepId = 1},
                new Program{ProgramID = 2, ProgramName = "Assessment",  NumOfSession = 3,StepId = 2},
                new Program{ProgramID = 3, ProgramName = "Full Development Report",NumOfSession = 1,StepId = 2},
                new Program{ProgramID = 4, ProgramName = "Program A", NumOfSession = 4,StepId =3, PriceWeekday = 500, PriceWeekend = 600},
                new Program{ProgramID = 5, ProgramName = "Program B", NumOfSession = 8,StepId = 3, PriceWeekday = 800, PriceWeekend = 900},
                new Program{ProgramID = 6, ProgramName = "Program C",  NumOfSession = 12,StepId = 3, PriceWeekday = 1100, PriceWeekend = 1200},
                new Program{ProgramID = 7, ProgramName = "Program D",  NumOfSession = 20,StepId = 3, PriceWeekday = 2000, PriceWeekend = 2100},
                new Program{ProgramID = 8, ProgramName = "Assessment",  NumOfSession = 3,StepId = 4},
                new Program{ProgramID = 9, ProgramName = "Full Development Report",  NumOfSession = 1,StepId = 4},
                new Program{ProgramID = 10, ProgramName = "Ready to School A",  NumOfSession = 4,StepId = 5, PriceWeekday = 1000, PriceWeekend = 1200},
                new Program{ProgramID = 11, ProgramName = "Ready to School B",  NumOfSession = 8,StepId = 5, PriceWeekday = 2000, PriceWeekend = 2200}
                );

            modelBuilder.Entity<ParentDetail>().HasData(
                new ParentDetail
                {
                    ParentID = 1,
                    FatherName = "John Doe",
                    FatherPhoneNum = "123456789",
                    FatherIC = "A1234567",
                    FatherRace = "Race",
                    FatherOccupation = "Occupation",
                    FatherEmail = "john.doe@example.com",
                    FatherAddress = "123 Main St",
                    FatherPostcode = "12345",
                    FatherCity = "City",
                    FatherCountry = "Country",
                    MotherName = "Jane Doe",
                    MotherPhoneNum = "987654321",
                    MotherIC = "B7654321",
                    MotherRace = "Race",
                    MotherOccupation = "Occupation",
                    MotherEmail = "jane.doe@example.com",
                    MotherAddress = "123 Main St",
                    MotherPostcode = "12345",
                    MotherCity = "City",
                    MotherCountry = "Country",
                    HouseholdIncome = "50000"
                },
                new ParentDetail
                {
                    ParentID = 2,
                    FatherName = "Homelander",
                    FatherPhoneNum = "123456789",
                    FatherIC = "A1234567",
                    FatherRace = "Race",
                    FatherOccupation = "Occupation",
                    FatherEmail = "Homelander.doe@example.com",
                    FatherAddress = "123 Main St",
                    FatherPostcode = "12345",
                    FatherCity = "City",
                    FatherCountry = "Country",
                    MotherName = "Deep",
                    MotherPhoneNum = "987654321",
                    MotherIC = "B7654321",
                    MotherRace = "Race",
                    MotherOccupation = "Occupation",
                    MotherEmail = "Deep.doe@example.com",
                    MotherAddress = "123 Main St",
                    MotherPostcode = "12345",
                    MotherCity = "City",
                    MotherCountry = "Country",
                    HouseholdIncome = "50000"
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentID = 1,
                    ChildName = "Alice Johnson",
                    ChildIC = "192837465",
                    ChildAge = 8,
                    ChildDOB = new DateTime(2016, 3, 10),
                    ChildPassportNo = "C19283746",
                    ChildNationality = "British",
                    ChildRace = "African",
                    ChildBirthPlace = "London, UK",
                    ChildSex = "Female",
                    ChildAddress = "789 Oak St",
                    ChildPostcode = "E1 6AN",
                    ChildCity = "London",
                    ChildCountry = "UK",
                    Pediatricians = "Dr. White",
                    HospReccommendation = "Royal Hospital",
                    DiagnosisOrCondition = "Autism",
                    OccupationalTheraphyPlace = "Therapy Center G",
                    SpeechTheoryPlace = "Speech Clinic H",
                    OthersUnitPlace = "Unit I",
                    UserId = "user789",
                }
            );

           modelBuilder.Entity<Report>().HasData(
           new Report
           {
               ReportID = 1,
               StudentID = 1,
               DateReport = DateTime.Now,
               IndividualTherapy = true,
               GroupTherapy = false,
               EarlyIntervention = true,
               BehaviorManagement = false,
               CanEnterSelf = true,
               WithPrompting = false,
               SubjectiveAssessmentNotes = "The child entered without any issues.",
               MotorPraxisSkillsNotes = "Good motor skills.",
               SensoryRegulationSkillsNotes = "No issues observed.",
               CognitiveRegulationSkillsNotes = "Good attention span.",
               OccupationalPerformanceNotes = "Independent in ADL.",
               EmotionalRegulationSkillsNotes = "Cooperative behavior.",
               CommunicationSocialSkillsNotes = "Good eye contact.",
               AcademicPerformance = "Good",
               AcademicPerformanceNotes = "Performs well academically.",
               AnalysisProblem = "No significant problems.",
               ShortTermGoal = "Maintain current progress.",
               LongTermGoal = "Achieve higher academic performance.",
               TxPlan = "Regular therapy sessions."
           }
       );

            // Seed Invoice
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    InvoiceID = 1,
                    TotalAmount = 1000,
                    ParentID = 1,  // Reference existing ParentID
                    DateIssued = DateTime.Now
                }
            );

            base.OnModelCreating(modelBuilder);

        }

    }
}
