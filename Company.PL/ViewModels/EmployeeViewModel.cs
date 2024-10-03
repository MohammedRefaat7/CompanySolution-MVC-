using Company.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; }

        [MaxLength(55)]
        [MinLength(4, ErrorMessage = "Min Length is 4 Chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Is Required")]
        public string Address { get; set; }
        [Range(18, 45, ErrorMessage = "Age Must be in Range from 18 To 45")]
        public int Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime HiringDate { get; set; }

        [Display(Name = "Image")]
        
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Please upload an image file (jpg, jpeg, png).")]
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }


        public Department? Department { get; set; }     // Navigational Property ... 

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
    }
}
