//dotnet new webapi -n MyApi

using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApp.Data;
using MyApi.Services;
using MyApp.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//add context
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

builder.Services.AddScoped<ICandidateRepo, CandidateRepo>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

builder.Services.AddScoped<IRecruiterRepo, RecruiterRepo>();
builder.Services.AddScoped<IRecruiterService, RecruiterService>();

builder.Services.AddScoped<IJobRepo, JobRepo>();
builder.Services.AddScoped<MyApi.Services.IJobService, MyApi.Services.JobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //json
    app.UseSwaggerUI(); //ui
}

//app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var recruiterService = services.GetRequiredService<IRecruiterService>();
    var jobService = services.GetRequiredService<IJobService>();
    var candidateService = services.GetRequiredService<ICandidateService>();

    try
    {
        Console.WriteLine("Please enter your Recruiter Id#:");
        if (int.TryParse(Console.ReadLine(), out int recruiterid))
        {
            var recruiter = await recruiterService.GetRecruiterByIdAsync(recruiterid);
            System.Console.WriteLine($"Welcome {recruiter.Name}!");
            System.Console.WriteLine(" ");

            var jobs = await jobService.GetAllJobsByRecruiterAsync(recruiterid);
            foreach (Job j in jobs)
            {
                System.Console.WriteLine("___________________________________________________________________________________________________________");
                if (j.HiredCandidate is null) //&& Status == "Opened"
                {
                    System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} |");
                }
                else
                {
                    System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} | Hired:{j.Recruiter?.Name}");
                }
            }
            System.Console.WriteLine("___________________________________________________________________");
            System.Console.WriteLine(" ");

            Console.WriteLine("Do you want to view Opened job postings only?");
            if (string.Equals(Console.ReadLine(), "Yes", StringComparison.OrdinalIgnoreCase))
            {
                var jobsFiltered = await jobService.GetJobsByStatusAsync("Opened");
                var recruiterJobsFiltered = jobsFiltered.Where(j => j.RecruiterId == recruiterid);
                foreach (Job j in recruiterJobsFiltered)
                {
                    System.Console.WriteLine("___________________________________________________________________");
                    if (j.HiredCandidate is null) //&& Status == "Opened"
                    {
                        System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} |");
                    }
                    else
                    {
                        System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} | Hired:{j.Recruiter?.Name}");
                    }

                }
                System.Console.WriteLine("___________________________________________________________________");
            }
            System.Console.WriteLine(" ");
            Console.WriteLine("To view qualified candidates for a specific job, type in the job id#: ");
            if (int.TryParse(Console.ReadLine(), out int jobId))
            {
                System.Console.WriteLine("___________________________________________________________________");
                var singleJob = await jobService.GetJobByIdAsync(jobId);
                if (singleJob.Status == "Closed" && singleJob.HiredCandidate is not null)
                {
                    System.Console.WriteLine($"{singleJob.HiredCandidate.Name} was already hired for this role");


                }
                else
                {
                    var candidates = await candidateService.GetAllCandidatesAsync();
                    foreach (Candidate c in (candidates.Where(c => c.HighestEducation >= singleJob.RequiredEducation && c.Status == "Active").OrderByDescending(c => c.HighestEducation)))
                    {
                        System.Console.WriteLine("___________________________________________________________________");
                        System.Console.WriteLine($"Candidate[Id:{c.Id} | Name:{c.Name} | Highest Education:{c.HighestEducation}]");
                    }
                    System.Console.WriteLine("___________________________________________________________________");

                    System.Console.WriteLine(" ");
                    Console.WriteLine("To hire one of these candiates, enter their id#: ");
                    if (int.TryParse(Console.ReadLine(), out int candidateId))
                    {
                        //if (candidates.Where(c => c.HighestEducation >= singleJob.RequiredEducation && c.Status == "Active").Any(c => c.Id == candidateId))
                        {
                            var hireCandidate = await candidateService.GetCandidateByIdAsync(candidateId);
                            await jobService.HireAsync(jobId, hireCandidate.Id);
                            // System.Console.WriteLine($"Job | Id:{singleJob.Id} | Title:{singleJob.Title} | Required Education:{singleJob.RequiredEducation} | Status:{singleJob.Status} | Hired:{singleJob.Recruiter.Name}");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("You've entered an invalid id");
                    }

                    foreach (Job j in jobs)
                    {
                        System.Console.WriteLine("___________________________________________________________________");
                        if (j.HiredCandidate is null) //&& Status == "Opened"
                        {
                            System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} |");
                        }
                        else
                        {
                            System.Console.WriteLine($"Job | Id:{j.Id} | Title:{j.Title} | Required Education:{j.RequiredEducation} | Status:{j.Status} | Hired:{j.Recruiter?.Name}");
                        }
                    }
                }


            }
            else
            {
                System.Console.WriteLine("You've entered an invalid id");
            }
        
        }
        else
        {
            System.Console.WriteLine("Invalid Id format entered");
        }

    }
    catch (Exception e)
    {
        System.Console.WriteLine($"Error: {e.Message}");
    }

} 



app.Run();


