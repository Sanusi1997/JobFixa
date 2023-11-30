using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
namespace JobFixa.Entities;

public class Employer
{

    [Key]
    public Guid EmployerId { get; set; }
    public string? Name { get; set; }
    public string? Industry { get; set; }
    public string? Address { get; set; }
    public string? Logo { set; get; }
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }    [DataType(DataType.Date)]
    public DateTime DateModified { get; set; }
    [ForeignKey("JobFIxaUserId")]
    public JobFixaUser? WiFindUser { get; set; }
    public Guid JobFIxaUserId { get; set; }


}