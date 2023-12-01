using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobFixa.Entities;

public class JobSeeker
{

    [Key]
    public Guid JobSeekerId { get; set; }
    public string? EducationalLevel{ get; set; }
    public string? Name { get; set; }
    public string? Skills { get; set; }
    public DateTime DateCreated { get; set; }   
     [DataType(DataType.Date)]
    public DateTime DateModified { get; set; }
    [ForeignKey("JobFIxaUserId")]
    public JobFixaUser? JobFixaUser { get; set; }
    public Guid JobFixaUserId { get; set; }
}