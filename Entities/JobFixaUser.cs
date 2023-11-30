using System.ComponentModel.DataAnnotations;

namespace JobFixa.Entities
{
    public class JobFixaUser
    {
        [Key]
        public Guid JobFixaUserId { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string UserType { get; set; } = "";
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
    }
}
