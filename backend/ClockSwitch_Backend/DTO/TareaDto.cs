using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    public class TareaDto
    {
        [Key]
        [MaxLength(9)]
        public int IdTarea { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(600)]
        public string? Descripcion{ get; set; }

        [MaxLength(50)]
        public string Estado { get; set; }
        // Pendiente - En progreso - Bloqueada - Completada - Cancelada

    }
}
