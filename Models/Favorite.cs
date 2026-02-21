using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace 商品展示系統.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Prod_Id { get; set; }

    }
}
