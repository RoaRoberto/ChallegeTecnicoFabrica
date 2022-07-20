using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Model
{
    [Table ("Order")]
    public class OrderEntity
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Unitvalue")]
        public Decimal Unitvalue { get; set; }

        [Column("Amount")]
        public Double Amount { get; set; }

        [Column("Subtotal")]
        public Decimal Subtotal { get; set; }

        [Column("Iva")]
        public Double Iva { get; set; }

        [Column("Total")]
        public Decimal Total { get; set; }




    }
}
