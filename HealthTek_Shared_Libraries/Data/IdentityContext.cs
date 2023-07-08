using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HealthTek_Shared_Libraries.Data
{
    public class IdentityContext : IdentityDbContext<AppUser, UserRoles, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Dashboards>(a =>
            {
                a.HasKey("DashboardId");
            });
            builder.Entity<Widgets>(a =>
            {
                a.HasKey("WidgetId");
            });
            builder.Entity<DashboardWidgets>(a =>
            {
                a.HasKey("DashboardWidgetId");
            });
            builder.Entity<Logins>(a =>
            {
                a.HasKey("LoginId");
            });
            builder.Entity<UserInfo>(a =>
            {
                a.HasKey("Id");
            });
            builder.Entity<Preferences>().HasOne(u => u.FkCaregiverTrainingGoals).WithOne(m => m.FkPreferences).HasForeignKey<CaregiverTrainingGoals>(m => m.FkPreferencesId);
            builder.Entity<Maladaptives>().HasMany(u => u.FunctionsList).WithOne(m => m.FkMaladaptives).HasForeignKey(m => m.FkMaladaptivesId);
            builder.Entity<Dashboards>().HasMany(u => u.FkDashboardWidgets).WithOne(m => m.FkDashboards).HasForeignKey(m => m.FkDashboardId);
            builder.Entity<DashboardWidgets>().HasOne(u => u.FkWidget);
            builder.Entity<Appointments>().HasOne(u => u.FkStartLocation).WithMany(m => m.ApptsStartLocation);
            builder.Entity<Appointments>().HasOne(u => u.FkEndLocation).WithMany(m => m.ApptsEndLocation);
            builder.Entity<Comments>().HasOne(u => u.FkEmployees).WithMany(u => u.Comments).HasForeignKey(m => m.FkUserId);
            builder.Entity<Appointments>().HasOne(u => u.BaAssessments).WithOne(m => m.FkAppointments).HasForeignKey<Appointments>(m => m.FkBaAssessmentsId);
            builder.Entity<Appointments>().HasOne(u => u.BaProgressNotes).WithOne(m => m.FkAppointments).HasForeignKey<Appointments>(m => m.FkBaProgressNotesId);
            builder.Entity<Appointments>().HasOne(u => u.BaMonthlyReports).WithOne(m => m.FkAppointments).HasForeignKey<Appointments>(m => m.FkBaMonthlyReportsId);
            builder.Entity<Appointments>().HasOne(u => u.Cfars).WithOne(m => m.FkAppointments).HasForeignKey<Appointments>(m => m.FkCfarsId);
            builder.Entity<Appointments>().HasOne(u => u.FkBatches).WithMany(m => m.Appointments);
            builder.Entity<BaReassessments>().HasOne(u => u.InitialAssessment).WithOne(m => m.FkReAssessment).HasForeignKey<BaReassessments>(e => e.FkBaInitialAssessmentsId);
            builder.Entity<BaReassessments>().HasMany(u => u.Reassessments).WithOne(m => m.ReAssessment).HasForeignKey(m => m.BaReassessmentsId);
            builder.Entity<BaAssessments>().HasOne(u => u.FkAnalystSignature).WithMany(m => m.BaAssessmentFkAnalystSignature).HasForeignKey(m => m.FkAnalystSignatureId);
            builder.Entity<BaAssessments>().HasOne(u => u.FkAppointments).WithOne(m => m.BaAssessments);
            builder.Entity<BaAssessments>().HasOne(u => u.FkSupervisorSignature).WithMany(m => m.BaAssessmentFkSupervisorSignature).HasForeignKey(m => m.FkSupervisorSignatureId);
            builder.Entity<BaAssessments>().HasMany(u => u.Authorizations).WithOne(m => m.FkBaAssessments).IsRequired(false);
            builder.Entity<BaProgressNotes>().HasOne(u => u.FkEmployeeSignature).WithMany(m => m.BaPnFkEmployeeSignature);
            builder.Entity<BaProgressNotes>().HasOne(u => u.FkSupervisorSignature).WithMany(m => m.BaPnFkSupervisorSignature);
            builder.Entity<BaProgressNotes>().HasOne(u => u.CaregiverCompetencies).WithOne(m => m.BaProgressNotes).HasForeignKey<CaregiverCompetencies>(m => m.FkBaProgressNotesId);
            builder.Entity<Cfars>().HasOne(u => u.FkEmployeeSignature).WithMany(m => m.CfarsFkEmployeeSignature);
            builder.Entity<Documents>().HasOne(u => u.FkUploadedBy).WithMany(u => u.DocumentsUploadedBy);
            builder.Entity<Employees>().HasMany(u => u.TasksFkAssignedBy).WithOne(m => m.FkAssignedBy);
            builder.Entity<Employees>().HasMany(u => u.TasksFkAssignedTo).WithOne(m => m.FkAssignedTo);
            builder.Entity<Employees>().HasOne(u => u.FkESignatures).WithOne(m => m.FkEmployees).HasForeignKey<ESignatures>(m => m.FkEmployeesId);
            builder.Entity<Employees>().HasOne(u => u.Locations).WithOne(m => m.Employees).HasForeignKey<Locations>(m => m.FkEmployeesId);
            builder.Entity<Employees>().HasMany(u => u.DocumentationProcess).WithOne(m => m.FkEmployees).HasForeignKey(m => m.FkEmployeesId);

            builder.Entity<DocumentationProcess>().HasOne(u => u.RoleDocsCatalogs).WithOne(m => m.DocumentationProcess).HasForeignKey<DocumentationProcess>(m => m.FkRoleDocsCatalogId);
            //builder.Entity<DocumentationProcess>().HasOne(u => u.FkEmployees).WithMany(m => m.DocumentationProcess).HasForeignKey(m => m.FkEmployeesId);
            builder.Entity<ESignatures>().HasMany(u => u.SupvUserSignatures).WithOne(m => m.FkUserSignature);
            builder.Entity<ESignatures>().HasMany(u => u.SupvSupervisorSignatures).WithOne(m => m.FkSupervisorSignature);
            builder.Entity<Caregivers>().HasOne(u => u.Locations).WithOne(m => m.Caregivers).HasForeignKey<Locations>(m => m.FkCaregiversId);
            builder.Entity<Facilities>().HasOne(u => u.Locations).WithOne(m => m.Facilities).HasForeignKey<Locations>(m => m.FkFacilitiesId);
            builder.Entity<Maladaptives>().HasOne(u => u.FkReplacements).WithOne(m => m.FkMaladaptives).HasForeignKey<Replacements>(m => m.FkMaladaptivesId);
            builder.Entity<Maladaptives>().HasOne(u => u.FkMaladaptiveDischarges).WithOne(m => m.FkMaladaptives).HasForeignKey<MaladaptiveDischarges>(m => m.FkMaladaptivesId);
            builder.Entity<Maladaptives>().HasOne(u => u.FkCaregiverTrainingGoals).WithOne(m => m.FkMaladaptives).HasForeignKey<CaregiverTrainingGoals>(m => m.FkMaladaptivesId);
            builder.Entity<Replacements>().HasOne(u => u.FkCaregiverTrainingGoals).WithOne(m => m.FkReplacements).HasForeignKey<CaregiverTrainingGoals>(m => m.FkReplacementsId);
            builder.Entity<CaregiverCompetencies>().HasMany(u => u.CaregiverCompChecks).WithOne(m => m.FkCaregiverCompetencies).HasForeignKey(m => m.FkCaregiverCompetenciesId);
            builder.Entity<BaAssessmentsInterventions>().HasOne(u => u.FkCaregiverTrainingGoals).WithOne(m => m.FkBaAssessmentsInterventions).HasForeignKey<CaregiverTrainingGoals>(m => m.FkBaAssessmentsInterventionsId);
            builder.Entity<CaregiverCompChecks>().HasOne(u => u.FkCaregiverComptChecksCatalog);

        }

        public DbSet<HealthTek_Shared_Libraries.SupportTickets> SupportTickets { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Logins> Logins { get; set; }
        public DbSet<HealthTek_Shared_Libraries.DocumentationProcess> DocumentationProcess { get; set; }
        public DbSet<HealthTek_Shared_Libraries.UserInfo> UserInfo { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Messages> Messages { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Widgets> Widgets { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Dashboards> Dashboards { get; set; }
        public DbSet<HealthTek_Shared_Libraries.DashboardWidgets> DashboardWidgets { get; set; }
        public DbSet<HealthTek_Shared_Libraries.IntakeDocsCatalog> IntakeDocsCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientInsurancesCatalog> ClientInsurancesCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ReplacementsCatalog> ReplacementsCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverCompChecksCatalog> CaregiverCompChecksCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.EnvironmentalsCatalog> EnvironmentalsCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.MaladaptivesCatalog> MaladaptivesCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Appointments> Appointments { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Assignments> Assignments { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Authorizations> Authorizations { get; set; }
        public DbSet<HealthTek_Shared_Libraries.AuthorizationNotes> AuthorizationNotes { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaAssessments> BaAssessments { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaAssessmentsInterventions> BaAssessmentsInterventions { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaCrisisPlan> BaCrisisPlan { get; set; }
        public DbSet<HealthTek_Shared_Libraries.AbcReports> AbcReports { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaMonthlyReports> BaMonthlyReports { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaProgressNotes> BaProgressNotes { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaProgressNotesInterventions> BaProgressNotesInterventions { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BaReassessments> BaReassessments { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Batches> Batches { get; set; }
        public DbSet<HealthTek_Shared_Libraries.BcabaSupvMeetings> BcabaSupvMeetings { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverCompChecks> CaregiverCompChecks { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverCompetencies> CaregiverCompetencies { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverFeedback> CaregiverFeedback { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverFeedbackNotesCheck> CaregiverFeedbackNotesCheck { get; set; }
        public DbSet<HealthTek_Shared_Libraries.CaregiverTrainingGoals> CaregiverTrainingGoals { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Caregivers> Caregivers { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Cfars> Cfars { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Clients> Clients { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientContacts> ClientContacts { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientEvents> ClientEvents { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientEventTypesCatalog> ClientEventTypesCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientInsurances> ClientInsurances { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ClientsFacilities> ClientsFacilities { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Comments> Comments { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Diagnosis> Diagnosis { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Documents> Documents { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Employees> Employees { get; set; }
        public DbSet<HealthTek_Shared_Libraries.EmployeesFacilities> EmployeesFacilities { get; set; }
        public DbSet<HealthTek_Shared_Libraries.EmployeesOperatingCounties> EmployeesOperatingCounties { get; set; }
        public DbSet<HealthTek_Shared_Libraries.EmployeesRoleNames> EmployeesRoleNames { get; set; }
        public DbSet<HealthTek_Shared_Libraries.EnvironmentalChanges> EnvironmentalChanges { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Facilities> Facilities { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Functions> Functions { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ESignatures> ESignatures { get; set; }
        public DbSet<HealthTek_Shared_Libraries.FacilitiesOperatingCounties> FacilitiesOperatingCounties { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Intakes> Intakes { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Interventions> Interventions { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Locations> Locations { get; set; }
        public DbSet<HealthTek_Shared_Libraries.LongTermObjectives> LongTermObjectives { get; set; }
        public DbSet<HealthTek_Shared_Libraries.MaladaptiveMeasurements> MaladaptiveMeasurements { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Maladaptives> Maladaptives { get; set; }
        public DbSet<HealthTek_Shared_Libraries.MaladaptiveDischarges> MaladaptiveDischarges { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Medications> Medications { get; set; }
        public DbSet<HealthTek_Shared_Libraries.OperatingCounties> OperatingCounties { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Preferences> Preferences { get; set; }
        public DbSet<HealthTek_Shared_Libraries.RbtCompetencies> RbtCompetencies { get; set; }
        public DbSet<HealthTek_Shared_Libraries.RbtCompTrainings> RbtCompTrainings { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ReplacementMeasurements> ReplacementMeasurements { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Replacements> Replacements { get; set; }
        public DbSet<HealthTek_Shared_Libraries.PreferencesCatalog> PreferencesCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.RbtCompTrainingsCatalog> RbtCompTrainingsCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.RoleNames> RoleNames { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ReinforcerCatalog> ReinforcerCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.RoleDocsCatalog> RoleDocsCatalog { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ServiceCodes> ServiceCodes { get; set; }
        public DbSet<HealthTek_Shared_Libraries.ShortTermObjectives> ShortTermObjectives { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Supervisions> Supervisions { get; set; }
        public DbSet<HealthTek_Shared_Libraries.Tasks> Tasks { get; set; }
        public DbSet<HealthTek_Shared_Libraries.TaskNotes> TaskNotes { get; set; }
    }
}
