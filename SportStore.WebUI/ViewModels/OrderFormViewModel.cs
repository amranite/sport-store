using System.ComponentModel.DataAnnotations;

namespace SportStore.WebUI.ViewModels;

public class OrderFormViewModel
{
    [Required(ErrorMessage = "First name is required.")]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Street is required.")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "Number is required.")]
    public string Number { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zip is required.")]
    [Display(Name = "Zip")]
    public string Zip { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; } = string.Empty;
}
