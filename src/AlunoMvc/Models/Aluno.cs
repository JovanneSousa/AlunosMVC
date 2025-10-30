using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo não {0} pode ser vazio!")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo não {0} pode ser vazio!")]
        [StringLength(60, MinimumLength = 2,ErrorMessage ="O valor do campo {0} deve estar entre {2} e {1}!")]
        [EmailAddress(ErrorMessage = "O campo {0} não é válido!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo {0} é obrigatório!")]
        [Display(Name = "Confirme o email")]
        [NotMapped]
        [Compare("Email", ErrorMessage = "Os e-mais informados não são identicos!")]
        public string EmailConfirmacao { get; set; }  

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo não {0} pode ser vazio!")]
        [DataType(DataType.Date, ErrorMessage = "O campo {0} está com um valor inválido!")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo {0} é obrigatório!")]
        [Range(1,5, ErrorMessage = "O campo {0} deve estar entre {1} e {2}")]
        [Display(Name = "Avaliação")]
        public int Avaliacao    { get; set; }

        public bool Ativo {  get; set; }
    }
}
