using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportStore.WebUI.ViewModels;

public class ProductFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "Price must be between €0.01 and €1,000,000.00.")]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public IFormFile? Image { get; set; }

    public string ExistingImageUrl { get; set; } = string.Empty;

    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
}
