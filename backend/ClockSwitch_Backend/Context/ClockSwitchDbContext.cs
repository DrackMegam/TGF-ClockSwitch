using ClockSwitch_Backend.DTO;
using Microsoft.EntityFrameworkCore;

namespace ClockSwitch_Backend.Context
{
    public class ClockSwitchDbContext : DbContext
    {
        // https://www.youtube.com/watch?v=7rY9vKB_BdA&list=WL&index=1&t=1711s&ab_channel=OpenEducation

        public ClockSwitchDbContext(DbContextOptions<ClockSwitchDbContext> options) : base(options)
        {

        } 

        // Esta propiedad será la "tabla" que recogerá la información de la BBDD.
        public DbSet<PersonaDto> Persona { get; set; }
        public DbSet<UsuarioDto> Usuario { get; set; }
        public DbSet<SuscripcionDto> Suscripcion { get; set; }
        public DbSet<TareaDto> Tarea { get; set; }
        public DbSet<HistorialDto> Historial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Transformo el tipo de columna a una variable para poder trabajar con ella.
            cargarPersona(modelBuilder);
            cargarUsuario(modelBuilder);
            cargarSuscripcion(modelBuilder);
            cargarTarea(modelBuilder);
            cargarHistorial(modelBuilder);
        }

        private void cargarPersona(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Dni).HasColumnType("varchar(9)").HasConversion<string>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(50)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Apellidos).HasColumnType("varchar(100)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Descripcion).HasColumnType("varchar(500)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Telefono).HasColumnType("int(9)").HasConversion<int?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(100)").HasConversion<string?>());
        }

        private void cargarUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioDto>(e => e.Property(o => o.IdUsuario).HasColumnType("int(5)").HasConversion<int>());
            modelBuilder.Entity<UsuarioDto>(e => e.Property(o => o.DniPersona).HasColumnType("varchar(9)").HasConversion<string?>());
            modelBuilder.Entity<UsuarioDto>(e => e.Property(o => o.Username).HasColumnType("varchar(50)").HasConversion<string>());
            modelBuilder.Entity<UsuarioDto>(e => e.Property(o => o.Password).HasColumnType("varchar(100)").HasConversion<string>());
            modelBuilder.Entity<UsuarioDto>(e => e.Property(o => o.IsAdmin).HasColumnType("int(1)").HasConversion<int>());
        }

        private void cargarSuscripcion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuscripcionDto>(e => e.Property(o => o.Id).HasColumnType("int(5)").HasConversion<int>());
            modelBuilder.Entity<SuscripcionDto>(e => e.Property(o => o.IdSuscriptor).HasColumnType("int(5)").HasConversion<int?>());
            modelBuilder.Entity<SuscripcionDto>(e => e.Property(o => o.IdObjetivo).HasColumnType("int(5)").HasConversion<int>());
        }

        private void cargarTarea(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TareaDto>(e => e.Property(o => o.IdTarea).HasColumnType("int(9)").HasConversion<int>());
            modelBuilder.Entity<TareaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(200)").HasConversion<string>());
            modelBuilder.Entity<TareaDto>(e => e.Property(o => o.Descripcion).HasColumnType("varchar(600)").HasConversion<string?>());
            modelBuilder.Entity<TareaDto>(e => e.Property(o => o.Estado).HasColumnType("varchar(50)").HasConversion<string>());
        }

        private void cargarHistorial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.Id).HasColumnType("int(9)").HasConversion<int>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.IdTarea).HasColumnType("int(9)").HasConversion<int>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.IdUsuario).HasColumnType("int(5)").HasConversion<int>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasLunes).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasMartes).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasMiercoles).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasJueves).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasViernes).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasSabado).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.HorasDomingo).HasColumnType("double").HasConversion<double?>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.Ano).HasColumnType("int(4)").HasConversion<int>());
            modelBuilder.Entity<HistorialDto>(e => e.Property(o => o.Semana).HasColumnType("int(2)").HasConversion<int>());

        }
    }
}
