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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Transformo el tipo de columna a una variable para poder trabajar con ella.
            
            modeloPersona(modelBuilder);

        }

        private void modeloPersona(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Dni).HasColumnType("varchar(9)").HasConversion<string>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(50)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Apellidos).HasColumnType("varchar(100)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Descripcion).HasColumnType("varchar(500)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Telefono).HasColumnType("int(9)").HasConversion<int?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(100)").HasConversion<string?>());
        }

        /*
        private void modeloUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Dni).HasColumnType("varchar(9)").HasConversion<string>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(50)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Apellidos).HasColumnType("varchar(100)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Descripcion).HasColumnType("varchar(500)").HasConversion<string?>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Telefono).HasColumnType("int(9)").HasConversion<int>());
            modelBuilder.Entity<PersonaDto>(e => e.Property(o => o.Nombre).HasColumnType("varchar(100)").HasConversion<string?>());
        }
        */
    }
}
