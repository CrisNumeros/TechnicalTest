using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnicalTest.Models
{
    public class Product
    {
        [Key]
        [Display(Name="Product Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Product name")]
        [StringLength(60, ErrorMessage = "The property \"{0}\" cannot have more than {1} characters.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Product description")]
        [StringLength(210, ErrorMessage = "The property \"{0}\" cannot have more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Product category")]
        public int IdCategory { get; set; }

        public Category Category { get; set; }
    }
}