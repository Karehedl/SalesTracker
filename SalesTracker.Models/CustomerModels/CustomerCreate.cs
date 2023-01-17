using System.ComponentModel.DataAnnotations;

public class CustomerCreate
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    // [Required]
    // public string FullName { get; set; }
}