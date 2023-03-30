using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto17B.Models
{
    public class Roupas
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Tem de indicar o nome da roupa")]
        [UIHint("Indique o nome da roupa")]
        [Display(Name = "Nome da roupa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tem de indicar a marca da roupa")]
        [UIHint("Indique a marca da roupa")]
        [Display(Name = "Marca da roupa")]

        public string Marca { get; set; }
        [Required(ErrorMessage = "Tem de indicar a quantidade da roupa")]
        [UIHint("Indique a quantidade da roupa")]
        [Display(Name = "Quantidade da roupa")]

        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Tem de indicar o custo da roupa")]
        //TODO custo maior q 0
        [UIHint("Indique o custo da roupa")]
        [Display(Name = "Custo da roupa")]
        [DataType(DataType.Currency)]
        public int Preco { get; set; }



    }
}