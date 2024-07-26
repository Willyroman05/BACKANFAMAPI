using System;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BACKANFAMAPI.Models;

public partial class AnfamDataBaseContext : DbContext
{
    internal object paciente;

    public AnfamDataBaseContext()
    {
    }

    public AnfamDataBaseContext(DbContextOptions<AnfamDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AntecedentePatFam> AntecedentePatFams { get; set; }

    public virtual DbSet<AntecedentePatPer> AntecedentePatPers { get; set; }

    public virtual DbSet<AntecedentesObstetrico> AntecedentesObstetricos { get; set; }

    public virtual DbSet<AntecedentesPersonale> AntecedentesPersonales { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<EmbarazoActual> EmbarazoActuals { get; set; }

    public virtual DbSet<Epicrisis> Epicrises { get; set; }

    public virtual DbSet<HistoriaClinicaGeneral> HistoriaClinicaGenerals { get; set; }

    public virtual DbSet<Informacion> Informacions { get; set; }

    public virtual DbSet<ListaProblema> ListaProblemas { get; set; }

    public virtual DbSet<NotaEvolucion> NotaEvolucions { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Referencia> Referencias { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<PacienteDepartamento> PacienteDepartamento { get; set; }
    public virtual DbSet<PacienteUnidos> PacienteUnidos { get; set; }

    public virtual DbSet<ListaProbleasNombrePaciente> ListaProbleasNombrePaciente { get; set; }

    public virtual DbSet<EpicrisisNomPaciNomDoc> EpicrisisNomPaciNomDoc { get; set; }

    public virtual DbSet<NotaEvolucionNomPacNomDoc> NotaEvolucionNomPacNomDoc { get; set; }

    public virtual DbSet<RefereciaNomPacNomDocNomDep> RefereciaNomPacNomDocNomDep { get; set; }

    public virtual DbSet<HistoriaCliNomPacNomDoc> HistoriaCliNomPacNomDoc { get; set; }

    public virtual DbSet<ClasificaciondeRiesgos> ClasificaciondeRiesgos { get; set; }
    public virtual DbSet<cita> citas { get; set; }


    public async Task<List<ListaProbleasNombrePaciente>> PBuscarPacienteNombre_ListaproblemaAsync(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.ListaProbleasNombrePaciente.FromSqlRaw("EXEC PBuscarPacienteNombre_Listaproblema @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<ListaProbleasNombrePaciente>> PBuscarPacientePorNombres_Listaproblema(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.ListaProbleasNombrePaciente.FromSqlRaw("EXEC PBuscarPacientePorNombres_Listaproblema @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();
        
        return result;
    }

    public async Task<List<ClasificaciondeRiesgos>> PBuscarHistoriaClin_Embrazo_Obstetricos_codRiesgo(int COD_HOJARIESGO)
    {
        var param = new SqlParameter("@COD_HOJARIESGO", COD_HOJARIESGO);
        var result = await this.ClasificaciondeRiesgos.FromSqlRaw("EXEC PBuscarHistoriaClin_Embrazo_Obstetricos_codRiesgo @COD_HOJARIESGO", param).ToListAsync();
        return result;
    }

    public async Task<List<EpicrisisNomPaciNomDoc>> PBuscarPacienteNombre_EpiNombrePac_EpiNombreDoc(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.EpicrisisNomPaciNomDoc.FromSqlRaw("EXEC PBuscarPacienteNombre_EpiNombrePac_EpiNombreDoc @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<EpicrisisNomPaciNomDoc>> PBuscarEpicrisis_NombrePaciente(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.EpicrisisNomPaciNomDoc.FromSqlRaw("EXEC PBuscarEpicrisis_NombrePaciente @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }
    public async Task<List<NotaEvolucionNomPacNomDoc>> PBuscarPacienteNombre_NotaNombrePac_NotaNombreDoc(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.NotaEvolucionNomPacNomDoc.FromSqlRaw("EXEC PBuscarPacienteNombre_NotaNombrePac_NotaNombreDoc @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }

    public async Task<List<NotaEvolucionNomPacNomDoc>> PBuscarNotaEvo_NombrePaciente(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.NotaEvolucionNomPacNomDoc.FromSqlRaw("EXEC PBuscarNotaEvo_NombrePaciente @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }

    public async Task<List<EpicrisisNomPaciNomDoc>> PBuscarPacientecodigo_EpiNombrePac_EpiNombreDoc(int COD_EPICRISIS)
    {
        var param = new SqlParameter("@COD_EPICRISIS", COD_EPICRISIS);
        var result = await this.EpicrisisNomPaciNomDoc.FromSqlRaw("EXEC PBuscarPacientecodigo_EpiNombrePac_EpiNombreDoc @COD_EPICRISIS", param).ToListAsync();
        return result;
    }

    public async Task<List<RefereciaNomPacNomDocNomDep>> PBuscarReferencia_NomPac_NomDoc_NomDep(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.RefereciaNomPacNomDocNomDep.FromSqlRaw("EXEC PBuscarReferencia_NomPac_NomDoc_NomDep @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<RefereciaNomPacNomDocNomDep>> PBuscarReferencia_PacienteNombre(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.RefereciaNomPacNomDocNomDep.FromSqlRaw("EXEC PBuscarReferencia_PacienteNombre @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }

    public async Task<List<HistoriaCliNomPacNomDoc>> PBuscarHistoriaClin_NomPac_NomDoc(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.HistoriaCliNomPacNomDoc.FromSqlRaw("EXEC PBuscarHistoriaClin_NomPac_NomDoc @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<HistoriaCliNomPacNomDoc>> PBuscarHistoriaClin_PacienteNombre(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.HistoriaCliNomPacNomDoc.FromSqlRaw("EXEC PBuscarHistoriaClin_PacienteNombre @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }
    public async Task<List<PacienteUnidos>> PPaciente_Unidos(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.PacienteUnidos.FromSqlRaw("EXEC PPaciente_Unidos @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<PacienteUnidos>> PPaciente_Unidos_PacienteNombre(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.PacienteUnidos.FromSqlRaw("EXEC PPaciente_Unidos_PacienteNombre @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }

    public async Task<List<ClasificaciondeRiesgos>> PBuscarHistoriaClin_Embarazo_Obstetricos(string NUM_EXPEDIENTE)
    {
        var param = new SqlParameter("@NUM_EXPEDIENTE", NUM_EXPEDIENTE ?? (object)DBNull.Value);
        var result = await this.ClasificaciondeRiesgos.FromSqlRaw("EXEC PBuscarHistoriaClin_Embarazo_Obstetricos @NUM_EXPEDIENTE", param).ToListAsync();
        return result;
    }
    public async Task<List<ClasificaciondeRiesgos>> PBuscarHistoriaClin_Embrazo_Obstetricos_NombrePac(string PRIMER_NOMBRE, string PRIMER_APELLIDO)
    {
        var param = new SqlParameter("@PRIMER_NOMBRE", PRIMER_NOMBRE ?? (object)DBNull.Value);
        var param2 = new SqlParameter("@PRIMER_APELLIDO", PRIMER_APELLIDO ?? (object)DBNull.Value);
        var result = await this.ClasificaciondeRiesgos.FromSqlRaw("EXEC PBuscarHistoriaClin_Embrazo_Obstetricos_NombrePac @PRIMER_NOMBRE, @PRIMER_APELLIDO", param, param2).ToListAsync();

        return result;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=T15PGENWIROMAN\\SQLEXPRESS; Database=ANFAM_DataBase; Trusted_Connection=True; TrustServerCertificate=True ");
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AntecedentePatFam>(entity =>
        {
            entity.HasKey(e => e.CodAntpatfam).HasName("PKANTPATFAM");

            entity.ToTable("AntecedentePatFam");

            entity.Property(e => e.CodAntpatfam).HasColumnName("COD_ANTPATFAM");
            entity.Property(e => e.CaColon).HasColumnName("CA_COLON");
            entity.Property(e => e.CaCu).HasColumnName("CA_CU");
            entity.Property(e => e.CaMama).HasColumnName("CA_MAMA");
            entity.Property(e => e.CaOvario).HasColumnName("CA_OVARIO");
            entity.Property(e => e.CacoParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CACO_PARENTESCO");
            entity.Property(e => e.CacuParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CACU_PARENTESCO");
            entity.Property(e => e.CamParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAM_PARENTESCO");
            entity.Property(e => e.CaovaParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAOVA_PARENTESCO");
            entity.Property(e => e.DIABETESF).HasColumnName("DIABETESF");
            entity.Property(e => e.DiabetesParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIABETES_PARENTESCO");
            entity.Property(e => e.EnfCardiacas).HasColumnName("ENF_CARDIACAS");
            entity.Property(e => e.EnfRenales).HasColumnName("ENF_RENALES");
            entity.Property(e => e.EnfcarParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ENFCAR_PARENTESCO");
            entity.Property(e => e.EnfrenParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ENFREN_PARENTESCO");
            entity.Property(e => e.Hepatitis).HasColumnName("HEPATITIS");
            entity.Property(e => e.HepatitisParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HEPATITIS_PARENTESCO");
            entity.Property(e => e.HIPERTENSIONF).HasColumnName("HIPERTENSIONF");
            entity.Property(e => e.HipertensionParentesco)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HIPERTENSION_PARENTESCO");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");

            /*entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.AntecedentePatFams)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_FAM");*/


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PacienteDepartamento>().HasNoKey();
            modelBuilder.Entity<PacienteUnidos>().HasNoKey();
            modelBuilder.Entity<ListaProbleasNombrePaciente>().HasNoKey();
            modelBuilder.Entity<EpicrisisNomPaciNomDoc>().HasNoKey();
            modelBuilder.Entity<NotaEvolucionNomPacNomDoc   >().HasNoKey();
            modelBuilder.Entity<RefereciaNomPacNomDocNomDep>().HasNoKey();
            modelBuilder.Entity<HistoriaCliNomPacNomDoc>().HasNoKey();
            modelBuilder.Entity<ClasificaciondeRiesgos>().HasNoKey();



        });
        

        modelBuilder.Entity<AntecedentePatPer>(entity =>
        {
            entity.HasKey(e => e.CodAntparper).HasName("PKANTPARPER");

            entity.ToTable("AntecedentePatPer");

            entity.Property(e => e.CodAntparper).HasColumnName("COD_ANTPARPER");
            entity.Property(e => e.AlergiaAli).HasColumnName("ALERGIA_ALI");
            entity.Property(e => e.AlergiaMed).HasColumnName("ALERGIA_MED");
            entity.Property(e => e.Anemia).HasColumnName("ANEMIA");
            entity.Property(e => e.Cacerut).HasColumnName("CACERUT");
            entity.Property(e => e.CamDer).HasColumnName("CAM_DER");
            entity.Property(e => e.CamIzq).HasColumnName("CAM_IZQ");
            entity.Property(e => e.Cardiopatia).HasColumnName("CARDIOPATIA");
            entity.Property(e => e.Cirugias).HasColumnName("CIRUGIAS");
            entity.Property(e => e.Diabetes).HasColumnName("DIABETES");
            entity.Property(e => e.Extirpacion).HasColumnName("EXTIRPACION");
            entity.Property(e => e.Fibrodenoma).HasColumnName("FIBRODENOMA");
            entity.Property(e => e.Hepatopatias).HasColumnName("HEPATOPATIAS");
            entity.Property(e => e.Hipertension).HasColumnName("HIPERTENSION");
            entity.Property(e => e.Its)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ITS");
            entity.Property(e => e.Matriz).HasColumnName("MATRIZ");
            entity.Property(e => e.Nefropatia).HasColumnName("NEFROPATIA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.Vif).HasColumnName("VIF");
            entity.Property(e => e.Vih).HasColumnName("VIH");

          /*  entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.AntecedentePatPers)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEX_PER");
          */
        });

        modelBuilder.Entity<AntecedentesObstetrico>(entity =>
        {
            entity.HasKey(e => e.CodHojariesgo).HasName("PKHOJARIESGO");

            entity.ToTable("Antecedentes_Obstetricos");

            entity.Property(e => e.CodHojariesgo).HasColumnName("COD_HOJARIESGO");
            entity.Property(e => e.AntAbortos).HasColumnName("ANT_ABORTOS");
            entity.Property(e => e.CirugiasPrevias).HasColumnName("CIRUGIAS_PREVIAS");
            entity.Property(e => e.Internada).HasColumnName("INTERNADA");
            entity.Property(e => e.MuerteFetal).HasColumnName("MUERTE_FETAL");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.Peso250).HasColumnName("PESO_250");
            entity.Property(e => e.Peso450).HasColumnName("PESO_450");
            entity.Property(e => e.Telefono).HasColumnName("Telefono");
            entity.Property(e => e.ID_CITA).HasColumnName("ID_CITA");
            entity.Property(e => e.NUM_CITA).HasColumnName("NUM_CITA");

            /*  entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.AntecedentesObstetricos)
                  .HasForeignKey(d => d.NumExpediente)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_NUMEXP_RIESGO");
            */
        });

        modelBuilder.Entity<AntecedentesPersonale>(entity =>
        {
            entity.HasKey(e => e.CodAntper).HasName("PKCODPER");

            entity.Property(e => e.CodAntper).HasColumnName("COD_ANTPER");
            entity.Property(e => e.Abortos).HasColumnName("ABORTOS");
            entity.Property(e => e.Biopasis).HasColumnName("BIOPASIS");
            entity.Property(e => e.CigarrosDia).HasColumnName("CIGARROS_DIA");
            entity.Property(e => e.CompSexuales).HasColumnName("COMP_SEXUALES");
            entity.Property(e => e.Crioterapia).HasColumnName("CRIOTERAPIA");
            entity.Property(e => e.Embarazo).HasColumnName("EMBARAZO");
            entity.Property(e => e.EstadoPareja).HasColumnName("ESTADO_PAREJA");
            entity.Property(e => e.FecNacHijo).HasColumnName("FEC_NAC_HIJO");
            entity.Property(e => e.Fum).HasColumnName("FUM");
            entity.Property(e => e.Fuma).HasColumnName("FUMA");
            entity.Property(e => e.Gestas).HasColumnName("GESTAS");
            entity.Property(e => e.HistEmbarazo).HasColumnName("HIST_EMBARAZO");
            entity.Property(e => e.HistPap).HasColumnName("HIST_PAP");
            entity.Property(e => e.Lactancia).HasColumnName("LACTANCIA");
            entity.Property(e => e.Mac)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAC");
            entity.Property(e => e.Mamografia).HasColumnName("MAMOGRAFIA");
            entity.Property(e => e.Menopausia).HasColumnName("MENOPAUSIA");
            entity.Property(e => e.Menstruacion).HasColumnName("MENSTRUACION");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.Pap).HasColumnName("PAP");
            entity.Property(e => e.PapAlterado).HasColumnName("PAP_ALTERADO");
            entity.Property(e => e.Partos).HasColumnName("PARTOS");
            entity.Property(e => e.ReempHormonal).HasColumnName("REEMP_HORMONAL");
            entity.Property(e => e.Sa).HasColumnName("SA");
            entity.Property(e => e.VidaSexual).HasColumnName("VIDA_SEXUAL");
            entity.Property(e => e.THERMOCUAGULACION).HasColumnName("THERMOCUAGULACION");

            /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.AntecedentesPersonales)
                 .HasForeignKey(d => d.NumExpediente)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_NUMEXP");*/
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.CodDepartamento).HasName("PKDEP");

            entity.ToTable("Departamento");

            entity.Property(e => e.CodDepartamento).HasColumnName("COD_DEPARTAMENTO");
            entity.Property(e => e.Nombre)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.CodDoctor).HasName("PK_CODDOC");

            entity.ToTable("Doctor");

            entity.Property(e => e.CodDoctor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COD_DOCTOR");


            entity.Property(e => e.PrimerNombred)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PRIMER_NOMBRED");

            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_NOMBRE");


            entity.Property(e => e.PrimerApellidod)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PRIMER_APELLIDOD");


            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_APELLIDO");

            entity.Property(e => e.CEDULA)
               .HasMaxLength(20)
               .IsUnicode(false)
               .HasColumnName("CEDULA");
            entity.Property(e => e.CLINICA)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("CLINICA");

        });

        modelBuilder.Entity<EmbarazoActual>(entity =>
        {
            entity.HasKey(e => e.CodEmbarazo).HasName("PKEMBARAZO");

            entity.ToTable("Embarazo_Actual");

            entity.Property(e => e.CodEmbarazo).HasColumnName("COD_EMBARAZO");
            entity.Property(e => e.Diagnostico).HasColumnName("DIAGNOSTICO");
            entity.Property(e => e.Isoinmunizacion).HasColumnName("ISOINMUNIZACION");
            entity.Property(e => e.MasaPelvica).HasColumnName("MASA_PELVICA");
            entity.Property(e => e.Mayorde35).HasColumnName("MAYORDE35");
            entity.Property(e => e.Menor20).HasColumnName("MENOR20");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.PresionArterial).HasColumnName("PRESION_ARTERIAL");
            entity.Property(e => e.Sangradov).HasColumnName("SANGRADOV");
            entity.Property(e => e.ID_CITA).HasColumnName("ID_CITA");
            entity.Property(e => e.NUM_CITA).HasColumnName("NUM_CITA");


            /*entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.EmbarazoActuals)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_EMB");*/
        });

        modelBuilder.Entity<Epicrisis>(entity =>
        {
            entity.HasKey(e => e.CodEpicrisis).HasName("PKEPICRISIS");

            entity.ToTable("Epicrisis");

            entity.Property(e => e.CodEpicrisis).HasColumnName("COD_EPICRISIS");
            entity.Property(e => e.CodDoctor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COD_DOCTOR");
            entity.Property(e => e.Complicaciones)
                .IsUnicode(false)
                .HasColumnName("COMPLICACIONES");
            entity.Property(e => e.DatosRelevantes)
                .IsUnicode(false)
                .HasColumnName("DATOS_RELEVANTES");
            entity.Property(e => e.Descartes)
                .IsUnicode(false)
                .HasColumnName("DESCARTES");
            entity.Property(e => e.DiagEgreso)
                .IsUnicode(false)
                .HasColumnName("DIAG_EGRESO");
            entity.Property(e => e.DiagIngreso)
                .IsUnicode(false)
                .HasColumnName("DIAG_INGRESO");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.FechaEgreso).HasColumnName("FECHA_EGRESO");
            entity.Property(e => e.FechaIngreso).HasColumnName("FECHA_INGRESO");
            entity.Property(e => e.Hora)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("HORA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.Recomendaciones)
                .IsUnicode(false)
                .HasColumnName("RECOMENDACIONES");
            entity.Property(e => e.Resultado)
                .IsUnicode(false)
                .HasColumnName("RESULTADO");
            entity.Property(e => e.Tratamiento)
                .IsUnicode(false)
                .HasColumnName("TRATAMIENTO");

           /* entity.HasOne(d => d.CodDoctorNavigation).WithMany(p => p.Epicrises)
                .HasForeignKey(d => d.CodDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCEPI");*/

           /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.Epicrises)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_EPI");
           */
        });

        modelBuilder.Entity<HistoriaClinicaGeneral>(entity =>
        {
            entity.HasKey(e => e.CodHistoriaClinica).HasName("PKHISTORIA");

            entity.ToTable("Historia_Clinica_General");

            entity.Property(e => e.CodHistoriaClinica).HasColumnName("COD_HISTORIA_CLINICA");
            entity.Property(e => e.AltoRiesgo).HasColumnName("ALTO_RIESGO");
            entity.Property(e => e.Cardiopatia).HasColumnName("CARDIOPATIA");
            entity.Property(e => e.CodDoctor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COD_DOCTOR");
            entity.Property(e => e.ConsumoDrogas).HasColumnName("CONSUMO_DROGAS");
            entity.Property(e => e.CualquierOtro).HasColumnName("CUALQUIER_OTRO");
            entity.Property(e => e.DiabetesMellitus).HasColumnName("DIABETES_MELLITUS");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
         
            entity.Property(e => e.Nefropatia).HasColumnName("NEFROPATIA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.ID_CITA).HasColumnName("ID_CITA");
            entity.Property(e => e.NUM_CITA).HasColumnName("NUM_CITA");

            /*entity.HasOne(d => d.CodDoctorNavigation).WithMany(p => p.HistoriaClinicaGenerals)
                .HasForeignKey(d => d.CodDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCCLI");*/

            /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.HistoriaClinicaGenerals)
                 .HasForeignKey(d => d.NumExpediente)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_NUMEXP_CLI");*/
        });

        modelBuilder.Entity<Informacion>(entity =>
        {
            entity.HasKey(e => e.CodInfo).HasName("PKINFO");

            entity.ToTable("Informacion");

            entity.Property(e => e.CodInfo).HasColumnName("COD_INFO");
            entity.Property(e => e.MotVisita)
                .IsUnicode(false)
                .HasColumnName("MOT_VISITA");
            entity.Property(e => e.NotaMedica)
                .IsUnicode(false)
                .HasColumnName("NOTA_MEDICA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");




            /*entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.Informacions)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_INFO");*/
        });

        modelBuilder.Entity<ListaProblema>(entity =>
        {
            entity.HasKey(e => e.CodProblemas).HasName("PKPROBLEMAS");

            entity.ToTable("Lista_Problemas");

            entity.Property(e => e.CodProblemas).HasColumnName("COD_PROBLEMAS");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.NombreProblema)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PROBLEMA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.NumeroNota).HasColumnName("NUMERO_NOTA");
            entity.Property(e => e.Resuelto).HasColumnName("RESUELTO");

           /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.ListaProblemas)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_PRO");*/
        });

        modelBuilder.Entity<NotaEvolucion>(entity =>
        {
            entity.HasKey(e => e.CodNota).HasName("PKNOTA");

            entity.ToTable("Nota_Evolucion");

            entity.Property(e => e.CodNota).HasColumnName("COD_NOTA");
            entity.Property(e => e.CodDoctor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COD_DOCTOR");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.FrecCardiaca).HasColumnName("FREC_CARDIACA");
            entity.Property(e => e.FrecResp).HasColumnName("FREC_RESP");
            entity.Property(e => e.Hora)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("HORA");
            entity.Property(e => e.Imc).HasColumnName("IMC");
            entity.Property(e => e.NotaEvolucion1)
                .IsUnicode(false)
                .HasColumnName("NOTA_EVOLUCION");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.NumeroNota).HasColumnName("NUMERO_NOTA");
            entity.Property(e => e.Planes)
                .IsUnicode(false)
                .HasColumnName("PLANES");
            entity.Property(e => e.Presion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PRESION");
            entity.Property(e => e.Talla).HasColumnName("TALLA");
            entity.Property(e => e.Temperatura).HasColumnName("TEMPERATURA");

            entity.Property(e => e.PESO)
                .IsUnicode(false)
                .HasColumnName("PESO");

            /*  entity.HasOne(d => d.CodDoctorNavigation).WithMany(p => p.NotaEvolucions)
                  .HasForeignKey(d => d.CodDoctor)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CODDOC");*/

            /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.NotaEvolucions)
                 .HasForeignKey(d => d.NumExpediente)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_NUMEXP_NOTA");*/
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.NumExpediente).HasName("PK_NUMEXP");

            entity.ToTable("Paciente");

            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CEDULA");
            entity.Property(e => e.Centro)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CENTRO");
            entity.Property(e => e.CodDepartamento).HasColumnName("COD_DEPARTAMENTO");
            entity.Property(e => e.Direccion)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Escolaridad)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ESCOLARIDAD");
            entity.Property(e => e.FechaIngreso).HasColumnName("FECHA_INGRESO");
            entity.Property(e => e.FechaNac).HasColumnName("FECHA_NAC");
            entity.Property(e => e.Imc).HasColumnName("IMC");
            entity.Property(e => e.Peso).HasColumnName("PESO");
            entity.Property(e => e.Presion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PRESION");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PRIMER_APELLIDO");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PRIMER_NOMBRE");
            entity.Property(e => e.Profesion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PROFESION");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_APELLIDO");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SEGUNDO_NOMBRE");
            entity.Property(e => e.Sexo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SEXO");
            entity.Property(e => e.Talla).HasColumnName("TALLA");
            entity.Property(e => e.Temperatura).HasColumnName("TEMPERATURA");
            entity.Property(e => e.Usuaria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USUARIA");
            /*
           entity.HasOne(d => d.CodDepartamentoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.CodDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEP");*/
        });

        modelBuilder.Entity<Referencia>(entity =>
        {
            entity.HasKey(e => e.CodReferencias).HasName("PKREFERENCIAS");

            entity.Property(e => e.CodReferencias).HasColumnName("COD_REFERENCIAS");
            entity.Property(e => e.CodDepartamento).HasColumnName("COD_DEPARTAMENTO");
            entity.Property(e => e.CodDoctor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COD_DOCTOR");
            entity.Property(e => e.Contrareferencia)
                .IsUnicode(false)
                .HasColumnName("CONTRAREFERENCIA");
            entity.Property(e => e.Diagnostico)
                .IsUnicode(false)
                .HasColumnName("DIAGNOSTICO");
            entity.Property(e => e.ExamenesPrevios)
                .IsUnicode(false)
                .HasColumnName("EXAMENES_PREVIOS");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.FechaEgreso).HasColumnName("FECHA_EGRESO");
            entity.Property(e => e.InfoAtencion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("INFO_ATENCION");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");

            /*entity.HasOne(d => d.CodDepartamentoNavigation).WithMany(p => p.Referencia)
                .HasForeignKey(d => d.CodDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEPREF");*/

          /*  entity.HasOne(d => d.CodDoctorNavigation).WithMany(p => p.Referencia)
                .HasForeignKey(d => d.CodDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCREF");*/

           /* entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.Referencia)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_REF");*/
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.CodRol).HasName("PKROLES");

            entity.ToTable("Rol");

            entity.Property(e => e.CodRol).HasColumnName("COD_ROL");
            entity.Property(e => e.NombreRol).HasColumnName("NOMBRE_ROL");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CodAdmin).HasName("PKADMIN");

            entity.ToTable("Usuario");

            entity.Property(e => e.CodAdmin).HasColumnName("COD_ADMIN");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CodRol).HasColumnName("COD_ROL");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CONTRASEÑA");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            //entity.HasOne(d => d.CodRolNavigation).WithMany()
             //   .HasForeignKey(d => d.CodRol)
             //   .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_ROL");
        });


        modelBuilder.Entity<Informacion>(entity =>
        {
            entity.HasKey(e => e.CodInfo).HasName("PKINFO");

            entity.ToTable("Informacion");

            entity.Property(e => e.CodInfo).HasColumnName("COD_INFO");
            entity.Property(e => e.MotVisita)
                .IsUnicode(false)
                .HasColumnName("MOT_VISITA");
            entity.Property(e => e.NotaMedica)
                .IsUnicode(false)
                .HasColumnName("NOTA_MEDICA");
            entity.Property(e => e.NumExpediente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_EXPEDIENTE");




            /*entity.HasOne(d => d.NumExpedienteNavigation).WithMany(p => p.Informacions)
                .HasForeignKey(d => d.NumExpediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUMEXP_INFO");*/
        });
        modelBuilder.Entity<cita>(entity =>
        {
            entity.HasKey(e => e.id_cita).HasName("PKINFO");

            entity.ToTable("cita");

            entity.Property(e => e.id_cita)
                .HasColumnName("ID_CITA");

            entity.Property(e => e.num_cita)
                .HasColumnName("NUM_CITA");

     

          
        });

        OnModelCreatingPartial(modelBuilder);
    }

    internal async Task GetPacientesAsync()
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
