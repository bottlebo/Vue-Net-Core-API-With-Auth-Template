using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        //[ForeignKey("Product")]
        //public int productId { get; set; }
        //public Product Product { get; set; }

    }
}
