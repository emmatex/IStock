using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ProductBrandDto : BaseEntityDto
    {
        public string Name { get; set; }
    }

    public class CreateUpdateBrandDto 
    {
        [Required]
        [MaxLength(128, ErrorMessage = "The name shouldn't have more than 200 characters.")]
        public string Name { get; set; }
    }


}
