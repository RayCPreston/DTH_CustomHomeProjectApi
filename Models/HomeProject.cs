using Microsoft.EntityFrameworkCore;

namespace DTH.API.Models
{
    [Index(nameof(ProjectId), IsUnique = true)]
    public class HomeProject
    {
        public Guid Id { get; set; }
        public string? ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public required string ClientName { get; set; }
        public  ClientStanding? ClientStanding { get; set; }
        public string? StreetAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimatedCompletionDate{ get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public decimal Budget { get; set; }
    }
    
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public enum ClientStanding
    {
        Good,
        Poor
    }

    public record HomeProjectDTO(string ProjectId, string ProjectName, string ClientName, string? StreetAddress, DateTime? StartDate, DateTime? EstimatedCompletionDate, ProjectStatus? ProjectStatus, decimal Budget);
}
