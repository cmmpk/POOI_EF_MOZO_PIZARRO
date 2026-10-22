using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POOI_EF_MOZO_PIZARRO.Models
{
    public class Consumidor
    {
        [Required(ErrorMessage = "El dni es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Ingrese un Dni válido (8 dígitos).")]
        [Display(Name = "DNI")]
        public string DniCont { get; set; }

        [Required(ErrorMessage = "El campo nombre no debe estar vacío.")]
        [Display(Name = "Nombre")]
        public string NomCont { get; set; }

        [Required(ErrorMessage = "El campo apellido no debe estar vacío.")]
        [Display(Name = "Apellido")]
        public string ApeCont { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [Display(Name = "Correo")]
        public string EmailCont { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Seleccione un pais.")]
        public string IdPais { get; set; }
        public Consumidor()
        {
        }

        public Consumidor(string dniCont, string nomCont, string apeCont, string emailCont, string idPais)
        {
            DniCont = dniCont;
            NomCont = nomCont;
            ApeCont = apeCont;
            EmailCont = emailCont;
            IdPais = idPais;
        }
    }
}