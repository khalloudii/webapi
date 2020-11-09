using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(32)]
        [Required]
        public string NamePlural { get; set; }

        [MaxLength(32)]
        [Required]
        public string NameSingular { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }


        private ICollection<Product> _products;
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new Collection<Product>()); }
            set { _products = value; }
        }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }


        private ICollection<Category> _subCategories;
        public virtual ICollection<Category> SubCategories
        {
            get { return _subCategories ?? (_subCategories = new Collection<Category>()); }
            set { _subCategories = value; }
        }
    }
}
