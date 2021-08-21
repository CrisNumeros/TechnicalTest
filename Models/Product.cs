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
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Product description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The property \"{0}\" is required.")]
        [Display(Name = "Product category")]
        public int IdCategory { get; set; }

        public Category Category { get; set; }
    }
}