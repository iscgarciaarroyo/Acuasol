namespace Acuasol.Backend.Models
{
    using Dominio;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class VistaUsuario : Usuario
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud del campo {0} debe estar entre {1} y {2} caracteres", MinimumLength = 6)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden")]
        [Display(Name = "Contraseña confirmada")]
        public string PasswordConfirm { get; set; }
    }
}