namespace alacnz.server.Models
{
    public class Caso
    {
        public int Id { get; set; }                        // Identificador único del caso
        public DateTime FechaCaso { get; set; }             // Fecha de apertura del caso
        public int ClienteId { get; set; }                  // Clave foránea hacia la entidad Cliente
        public Cliente Cliente { get; set; }                // Navegación hacia el Cliente
        public int NumeroBeneficiariosAdultos { get; set; } // Número de adultos beneficiarios en el caso
        public int NumeroBeneficiariosNinos { get; set; }   // Número de niños beneficiarios en el caso
        public int NumeroBeneficiariosJovenes { get; set; } // Número de jóvenes beneficiarios en el caso
        public string EquipoTrabajo { get; set; }           // Equipo de trabajo asignado al caso
        public string CiudadServicio { get; set; }          // Ciudad donde se brinda el servicio
        public string ServiciosEspecificos { get; set; }    // Servicios específicos asociados al caso
        public DateTime? FechaCierre { get; set; }          // Fecha en que el caso es cerrado (opcional)
        public string ReferenciaCaso { get; set; }          // Referencia del caso (número de expediente)
        public string MotivoDerivacion { get; set; }        // Motivo de derivación del caso
        public int NumeroSesiones { get; set; }             // Número de sesiones realizadas en el caso
        public DateTime? FechaCompletitud { get; set; }     // Fecha en que el caso se completó (opcional)
    }
}
