using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace 商品展示系統.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Prod_Id { get; set; }

        [Required(ErrorMessage = "請輸入螢幕名稱")]
        [Display(Name = "產品名稱")]
        public string? Prod_Name { get; set; }

        [Display(Name = "品牌")]
        public string? Prod_Brand { get; set; }

        [Range(0, 1000000, ErrorMessage = "價格必須大於 0")]
        [Display(Name = "價格")]
        public int Prod_Price { get; set; }

        [Display(Name = "規格說明")]
        public string? Prod_Specification { get; set; }

        [Display(Name = "圖片路徑")]
        public string? Prod_Image_Url { get; set; }

        [Display(Name = "商品網站連結")]
        [Url(ErrorMessage = "請輸入正確的網址格式")]
        public string? Prod_Url { get; set; }

        [NotMapped] // 加上這個標籤，代表這只是顯示用，不用存進資料庫的欄位
        public bool IsFavorite { get; set; } = false;
    }
}