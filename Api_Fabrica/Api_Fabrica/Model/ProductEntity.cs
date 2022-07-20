using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Model
{
    [Table ("Product")]
    public class ProductEntity
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Value")]
        public Decimal Value { get; set; }
        
    }
}
