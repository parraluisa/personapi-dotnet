using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.Entities;

public partial class Telefono
{
    [Required]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El número de teléfono debe contener solo dígitos.")]
    public string Num { get; set; } = null!;

    public string Oper { get; set; } = null!;

    public int Duenio { get; set; }

    public virtual Persona DuenioNavigation { get; set; } = null!;
}
