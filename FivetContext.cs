using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;

namespace Fivet.Dao
{
    public class FivetContext: DbContext
    {
        public DbSet<Persona> Personas {get;set;}
        public DbSet<Ficha> Fichas {get;set;}
        public DbSet<Control> Controles {get;set;}
        public DbSet<Foto> Fotos {get;set;}
        public DbSet<Examen> Examenes {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = fivet.db",options=>{
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   // update model and example for Insert method to Persona Table
            modelBuilder.Entity<Persona>(p =>
            {
                p.HasKey(p=> p.uid);
                p.Property(p=> p.email).IsRequired();
                //p.Property(p=> p.email).IsUnique();
            });

            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    uid = 1,
                    rut ="189725965",
                    nombre ="Alvaro",
                    apellido="Castillo",
                    direccion="Antilhue 1350",
                    email = "alvarocc96@gmail.com"
                }
            );

            // update the model and example for Insert the data for Contrl
            modelBuilder.Entity<Control>(c =>
            {
                c.HasKey(c=> c.uid);
                c.Property(c=> c.fechaControl).IsRequired();
                c.Property(c=> c.fechaProximoControl).IsRequired();
                c.Property(c=> c.temperatura).IsRequired();
                c.Property(c=> c.peso).IsRequired();
                c.Property(c=> c.altura).IsRequired();
                c.Property(c=> c.diagnostico).IsRequired();
                c.Property(c=> c.nombreVeterinario).IsRequired();
                //c.Property(c=> c.urlFoto).IsRequired();
            });

            modelBuilder.Entity<Control>().HasData(
                new Control()
                {
                    uid = 1,
                    fechaControl ="fecha1",
                    fechaProximoControl="fecha2",
                    temperatura = 30f,
                    peso= 20f,
                    altura=30,
                    diagnostico="muy malito",
                    nombreVeterinario="Dr Simi"
                }
            );

            // update the model and example for Insert the data for Contrl
            modelBuilder.Entity<Control>(c =>
            {
                c.HasKey(c=> c.uid);
                c.Property(c=> c.fechaControl).IsRequired();
                c.Property(c=> c.fechaProximoControl).IsRequired();
                c.Property(c=> c.temperatura).IsRequired();
                c.Property(c=> c.peso).IsRequired();
                c.Property(c=> c.altura).IsRequired();
                c.Property(c=> c.diagnostico).IsRequired();
                c.Property(c=> c.nombreVeterinario).IsRequired();
                //c.Property(c=> c.urlFoto).IsRequired();
            });

            modelBuilder.Entity<Control>().HasData(
                new Control()
                {
                    uid = 1,
                    fechaControl ="fecha1",
                    fechaProximoControl="fecha2",
                    temperatura = 30f,
                    peso= 20f,
                    altura=30,
                    diagnostico="muy malito",
                    nombreVeterinario="Dr Simi"
                }
            );

            // update the model and example for Insert the data for Ficha
            modelBuilder.Entity<Ficha>(f =>
            {
                f.HasKey(f=> f.uid);
                f.Property(f=> f.numeroFicha).IsRequired();
                f.Property(f=> f.nombrePaciente).IsRequired();
                f.Property(f=> f.especie).IsRequired();
                //f.Property(f=> f.fechaNacimiento).IsRequired();
                //f.Property(f=> f.raza).IsRequired();
                //f.Property(f=> f.sexo).IsRequired();
                //f.Property(f=> f.color).IsRequired();
                f.Property(f=> f.tipoPaciente).IsRequired();
                
            });

            modelBuilder.Entity<Ficha>().HasData(
                new Ficha()
                {
                    uid = 1,
                    numeroFicha =1,
                    nombrePaciente = "hachiko",
                    especie ="Perro",
                    fechaNacimiento ="fecha1",
                    raza ="akita",
                    sexo=Sexo.MACHO,
                    color = "Arcoiris",
                    tipoPaciente=TipoPaciente.INTERNO
                }
            );

            // update the model and example for Insert the data for Examen
            modelBuilder.Entity<Examen>(e =>
            {
                e.HasKey(e=> e.uid);
                e.Property(e=> e.nombreExamen).IsRequired();
                e.Property(e=> e.fechaExamen).IsRequired();
            });

            modelBuilder.Entity<Examen>().HasData(
                new Examen()
                {
                    uid = 1,
                    nombreExamen ="Nombre Del Examen",
                    fechaExamen = "Fecha Examen",
                }
            );
        }
    }   
}