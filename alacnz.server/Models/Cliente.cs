namespace alacnz.server.Models
{
    public class Cliente
    {
        public int Id { get; set; }                     // Identificador único del cliente
        public string Nombre { get; set; }               // Nombre completo del cliente
        public string Nacionalidad { get; set; }         // Nacionalidad del cliente
        public string Genero { get; set; }               // Género del cliente
        public int Edad { get; set; }                    // Edad del cliente (como un valor numérico)
        public string EstadoMigratorio { get; set; }     // Estado migratorio (Ej. Residente, Ciudadano)
        public bool BeneficiosSeguridadSocial { get; set; }  // Si recibe beneficios de seguridad social
        public string TipoVisa { get; set; }             // Tipo de visa del cliente, si aplica
    }
}
