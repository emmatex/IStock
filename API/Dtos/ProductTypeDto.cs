using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ProductTypeDto : BaseEntityDto
    {
        public string Name { get; set; }
    }

    public class CreateUpdateTypeDto
    {
        [Required]
        [MaxLength(128, ErrorMessage = "The name shouldn't have more than 200 characters.")]
        public string Name { get; set; }
    }

}
