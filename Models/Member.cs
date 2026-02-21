using System.ComponentModel.DataAnnotations;

namespace 商品展示系統.Models
{
    public class Member
    {
        [Key]
        public int Member_Id { get; set; }
        public string Account { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Name { get; set; }
        public bool Permissions { get; set; } = false;
    }
}
