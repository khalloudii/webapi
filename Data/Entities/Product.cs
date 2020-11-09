using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(80)]
        [Required]
        public string NameAR { get; set; }
        [Required]
        public string DescriptionAR { get; set; }
        public string ShortDescriptionAR { get; set; }

        [MaxLength(100)]
        [Required]
        public string NameEN { get; set; }
        [Required]
        public string DescriptionEN { get; set; }
        public string ShortDescriptionEN { get; set; }


        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductBrandId")]
        public virtual ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        private ICollection<DocPageImage> _docPageImages;
        public virtual ICollection<DocPageImage> Images
        {
            get { return _docPageImages ?? (_docPageImages = new Collection<DocPageImage>()); }
            set { _docPageImages = value; }
        }
    }
}
