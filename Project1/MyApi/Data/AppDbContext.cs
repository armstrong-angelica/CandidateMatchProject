//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Design

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.Core.Models;
//using SQLitePCL;

namespace MyApp.Data;

public class AppDbContext : DbContext
{

    //avoid error with AppDbContext constructors
    public AppDbContext() : base() { }
    public AppDbContext(DbContextOptions options) : base(options) { }



    //use models to tell ef core what tables to create in this db context class
    //set up models + relationshsips correctly then this is all we need

    public DbSet<Recruiter> Recruiters { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Job> Jobs { get; set; }
    //public DbSet<Skills> Skills { get; set; }
    public DbSet<JobCandidateMatch> JobCandidateMatches { get; set; } //junction for jobs and candidates

    //connect to database (asp.net will be in different place)
    //override method from DbContext to tell it where to find/create db
    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBUilder) //
    {
        optionsBUilder.UseSqlite("Data Source = JobCandidateMatcher.db");
        //chain  .LogTo(Console.WriteLine, LogLevel.Information); // to see sql log, using Microsoft.Extensions.Logging;
    } */



    //dotnet tool install --global dotnet-ef
    //tool install

    //dotnet ef migrations add IntialDbCreate
    //dotnet ef database update




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobCandidateMatch>(entity =>
        entity.HasKey(m => new { m.JobId, m.CandidateId }));

        //seeding data
        //inside onmodelcreating we can see data with method HasData
        //insert records when schema is created (at migration time, not program startup)
        //c data must be provided with primary key, ef uses provided PK to make sure it doesnt apply seed data twice over different migrations

        //seed Recruiters
        modelBuilder.Entity<Recruiter>().HasData(
            new Recruiter { Id = 1, Name = "Susan Berkly" },
            new Recruiter { Id = 2, Name = "Tom Holms" }
        );

        //seed Candidates
        modelBuilder.Entity<Candidate>().HasData(
            new Candidate { Id = 1, Name = "Angel Hernandez", HighestEducation = EducationLevel.Bachelors }, //java js sql
            new Candidate { Id = 2, Name = "Ryan Smith", HighestEducation = EducationLevel.HighSchool }, //java python matlab
            new Candidate { Id = 3, Name = "Courtney Bush", HighestEducation = EducationLevel.Masters }, //java Python js sql c#
            new Candidate { Id = 4, Name = "Lorenzo White", HighestEducation = EducationLevel.Bachelors } //java Python js c++
        );

        //seed Jobs
        modelBuilder.Entity<Job>().HasData(
            new Job { Id = 1, Title = "Software Developer II", RequiredEducation = EducationLevel.Bachelors, RecruiterId = 2 }, //RequiredSkills = "Java,Python",
            new Job { Id = 2, Title = "Senior Software Developer", RequiredEducation = EducationLevel.Masters, RecruiterId = 2 }, //RequiredSkills = "Java,Python,C++", 
            new Job { Id = 3, Title = "Software Developer - Entry", RequiredEducation = EducationLevel.HighSchool, RecruiterId = 1 }, //RequiredSkills = "Java,Python", 
            new Job { Id = 4, Title = "Software Developer I", RequiredEducation = EducationLevel.Bachelors, RecruiterId = 1 }, //RequiredSkills = "Java,Python",
            new Job { Id = 5, Title = "Software Developer - Entry", RequiredEducation = EducationLevel.HighSchool, RecruiterId = 2 },
            new Job { Id = 6, Title = "Software Developer I", RequiredEducation = EducationLevel.Bachelors, RecruiterId = 2 }
        );

        /* //seed Skills
        modelBuilder.Entity<Skills>().HasData(
            new Skills { Id = 1, Name = "C#" },
            new Skills { Id = 2, Name = "Java" },
            new Skills { Id = 3, Name = "C++" },
            new Skills { Id = 4, Name = "Python" },
            new Skills { Id = 5, Name = "Matlab" },
            new Skills { Id = 6, Name = "SQL" },
            new Skills { Id = 7, Name = "Javascript" }
        );

        //seed Junction tables
        modelBuilder.Entity("JobSkills").HasData(
            new { JobsId = 1, SkillsId = 2 }, //java
            new { JobsId = 1, SkillsId = 4 }, //python
            new { JobsId = 1, SkillsId = 3 }, //C++
            new { JobsId = 1, SkillsId = 7 }, //JS

            new { JobsId = 2, SkillsId = 2 }, //java
            new { JobsId = 2, SkillsId = 4 }, //python
            new { JobsId = 2, SkillsId = 3 }, //C++
            new { JobsId = 2, SkillsId = 1 }, //C#
            new { JobsId = 2, SkillsId = 6 }, //SQl
            new { JobsId = 2, SkillsId = 7 }, //JS

            new { JobsId = 3, SkillsId = 2 }, //java
            new { JobsId = 3, SkillsId = 4 }, //python

            new { JobsId = 5, SkillsId = 2 }, //java
            new { JobsId = 5, SkillsId = 4 }, //python

            new { JobsId = 4, SkillsId = 2 }, //java
            new { JobsId = 4, SkillsId = 4 }, //python
            new { JobsId = 4, SkillsId = 7 }, //JS

            new { JobsId = 6, SkillsId = 2 }, //java
            new { JobsId = 6, SkillsId = 4 }, //python
            new { JobsId = 6, SkillsId = 7 }  //JS


        );

        modelBuilder.Entity("CandidateSkills").HasData(

            //java js sql
            new { CandidatesId = 1, SkillsId = 2 },
            new { CandidatesId = 1, SkillsId = 6 },
            new { CandidatesId = 1, SkillsId = 7 },

            //java python matlab 
            new { CandidatesId = 2, SkillsId = 2 },
            new { CandidatesId = 2, SkillsId = 4 },
            new { CandidatesId = 2, SkillsId = 5 },

            //java Python js sql c#
            new { CandidatesId = 3, SkillsId = 1 },
            new { CandidatesId = 3, SkillsId = 2 },
            new { CandidatesId = 3, SkillsId = 4 },
            new { CandidatesId = 3, SkillsId = 6 },
            new { CandidatesId = 3, SkillsId = 7 },

            //java Python js c++
            new { CandidatesId = 4, SkillsId = 2 },
            new { CandidatesId = 4, SkillsId = 3 },
            new { CandidatesId = 4, SkillsId = 4 },
            new { CandidatesId = 4, SkillsId = 7 }
        ); */

        //delete migrations and .db so no unique error



    }
}