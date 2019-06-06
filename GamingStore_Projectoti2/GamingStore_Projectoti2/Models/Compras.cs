using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    public class Compras
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")] //só regista 'datas', não 'horas'.
        public DateTime Data { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public int ClientesFK { get; set; }
        [ForeignKey("ClientesFK")]
        public virtual Clientes Clientes { get; set; }



    }
}