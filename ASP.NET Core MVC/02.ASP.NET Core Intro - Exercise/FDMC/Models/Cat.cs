using System.ComponentModel.DataAnnotations;

namespace FDMC.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
