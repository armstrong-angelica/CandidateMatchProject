using System.Text.Json.Serialization;

namespace MyApp.Core.Models;
public class JobCandidateMatch
{
    public int JobId { get; set; }

    [JsonIgnore] 
    public Job? Job { get; set; }

    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }


    public string Status { get; set; } = "Matched"; //Hired, Rejected  //Applied, Reviewed


}
