using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class CategoryVM
    {
        public class AddCategoryVM
        {
            public string NamePlural { get; set; }

            public string NameSingular { get; set; }

            public string Description { get; set; }
            public string PictureUrl { get; set; }

            public int? ParentId { get; set; }
        }
    }
}
