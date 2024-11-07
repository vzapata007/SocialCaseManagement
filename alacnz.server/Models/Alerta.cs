namespace alacnz.server.Models
{
    public class Alerta
    {
        public int Id { get; set; }                     // Identificador único de la alerta
        public int CasoId { get; set; }                  // Clave foránea hacia la entidad Caso
        public Caso Caso { get; set; }                   // Navegación hacia el Caso
        public DateTime FechaAlerta { get; set; }        // Fecha en que se debe enviar la alerta
        public string TipoAlerta { get; set; }           // Tipo de alerta (ej. Seguimiento, Cierre Próximo)
        public string Descripcion { get; set; }          // Descripción adicional de la alerta
    }
}
