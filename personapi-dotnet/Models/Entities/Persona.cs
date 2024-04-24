using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.Entities;

public partial class Persona
{
    public int Cc { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    [Required]
    [RegularExpression("^[MF]$", ErrorMessage = "El género debe ser 'F' para femenino o 'M' para masculino.")]
    public string Genero { get; set; } = null!;

    public int? Edad { get; set; }

    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
