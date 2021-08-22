using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnicalTest.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category Id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Category name")]
        [StringLength(60, ErrorMessage = "The property \"{0}\" cannot have more than {1} characters.")]
        public string Name { get; set; }

        [Display(Name = "Category description")]
        [StringLength(210, ErrorMessage = "The property \"{0}\" cannot have more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}