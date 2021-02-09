using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ProductDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string MeasurementName { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }

    public class ProductManipulationDto
    {
        [Required]
        [MaxLength(128, ErrorMessage = "The name shouldn't have more than 128 characters.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The name shouldn't have more than 50 characters.")]
        public string Code { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [MaxLength(250, ErrorMessage = "The description shouldn't have more than 250 characters.")]
        public virtual string Description { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The measurement name shouldn't have more than 50 characters.")]
        public string MeasurementName { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        public string ProductBrandId { get; set; }
        [Required]
        public string ProductTypeId { get; set; }
    }

    public class ProductToCreateDto : ProductManipulationDto
    {

    }

    public class ProductToUpdateDto : ProductManipulationDto
    {
        [Required]
        public override string Description { get => base.Description; set => base.Description = value; }
    }

}
