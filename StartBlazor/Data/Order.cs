using System.ComponentModel.DataAnnotations;

namespace StartBlazor.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
