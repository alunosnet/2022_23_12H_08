using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto17B.Models
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }

        [Display(Name = "Data de compra")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Tem de indicar a data da compra")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime data_compra { get; set; }

        [DataType(DataType.Currency)]

        public decimal valor_pagar { get; set; }


        [ForeignKey("utilizador")]
        [Display(Name = "Utilizador")]
        [Required(ErrorMessage = "Tem de indicar o utilizador")]
        public int UtilizadorId { get; set; }

        public Utilizador utilizador { get; set; }

        [ForeignKey("roupa")]
        [Display(Name = "Roupa")]
        [Required(ErrorMessage = "Tem de indicar uma roupa")]
        public int RoupaId { get; set; }

        public Roupas roupa { get; set; }

        public Compras()
        {
            data_compra = DateTime.Now;
        }

    }
}