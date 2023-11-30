using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobFixa.Entities;

public class JobListing
{

    [Key]
    public Guid JobId { get; set; }
    public string? JobTitle { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public string? SalaryRange {get; set;}
    public string? JobStatus { get; set; }
    [DataType(DataType.Date)]
    public DateTime DatePosted{ get; set; }  
    [DataType(DataType.Date)]
    public DateTime DateClosed{ get; set; }  
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }   
     [DataType(DataType.Date)]
    public DateTime DateModified { get; set; }
    [ForeignKey("EmployerId")]
    public Employer? Employer {get; set;}
    public Guid EmployerId {get;set;}

}