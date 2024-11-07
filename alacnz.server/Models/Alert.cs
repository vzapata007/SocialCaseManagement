namespace alacnz.server.Models
{
    public class Alert
    {
        public int Id { get; set; }                     // Identificador único de la alerta
        public int CasoId { get; set; }                  // Clave foránea hacia la entidad Caso
        public Case Caso { get; set; }                   // Navegación hacia el Caso
        public DateTime FechaAlerta { get; set; }        // Fecha en que se debe enviar la alerta
        public string TipoAlerta { get; set; }           // Tipo de alerta (ej. Seguimiento, Cierre Próximo)
        public string Descripcion { get; set; }          // Descripción adicional de la alerta
    }
}
