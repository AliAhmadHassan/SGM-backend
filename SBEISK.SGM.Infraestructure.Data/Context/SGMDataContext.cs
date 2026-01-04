using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SBEISK.SGM.Infraestructure.Data.Context
{
    public class SgmDataContext : DbContext
    {
        private int _instalationId;
        private int _userId;
        public DbSet<BranchOffice> BranchOffices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Receiver> Receiver { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Entities.Action> Actions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ProfileAction> ProfileActions { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialStatus> MaterialsStatus { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Installation> Installation { get; set; }
        public DbSet<UserProfileInstallation> UserProfileInstallation { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Uf> Ufs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<InstallationType> InstallationTypes { get; set; }
        public DbSet<ReceiverType> ReceiverTypes { get; set; }
        public DbSet<Synchronization> Synchronizations { get; set; }
        public DbSet<ReceivementInvoiceOrder> ReceivementInvoiceOrders { get; set; }
        public DbSet<ReceivementMaterial> ReceivementMaterials { get; set; }
        public DbSet<ReceivementEmail> ReceivementEmail { get; set; }
        public DbSet<ReceivementPhoto> ReceivementPhotos { get; set; }
        public DbSet<ReceivementAttachment> receivementAttachments { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<RequisitionOfMaterialForApplication> RMA { get; set; }
        public DbSet<RMAattachments> RMAattachments { get; set; }
        public DbSet<RMAMaterial> RMAMaterials { get; set; }
        public DbSet<RMAStatus> RMAStatus { get; set; }
        public DbSet<RMAAttendance> RMAAttendance { get; set; }
        public DbSet<RMAAttendanceMaterial> RMAAttendanceMaterial { get; set; }
        public DbSet<RMAAttendanceEmails> RMAAttendanceEmails { get; set; }
        public DbSet<RMAAttendanceAttachments> RMAAttendanceAttachments { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<STM> STMs { get; set; }
        public DbSet<STMEmail> STMEmails { get; set; }
        public DbSet<STMAttachment> STMAttachments { get; set; }
        public DbSet<STMMaterial> STMMaterials { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<SolicitationStatus> SolicitationStatus { get; set; }
        public DbSet<TransferEmail> TransferEmails { get; set; }
        public DbSet<TransferAttachment> TransferAttachments { get; set; }
        public DbSet<TransferPhoto> TransferPhotos { get; set; }
        public DbSet<TransferMaterial> TransferMaterials { get; set; }
        public DbSet<TransferStatus> TransferStatus { get; set; }
        public DbSet<ReasonWithoutOrder> ReasonWithoutOrders { get; set; }
        public DbSet<DirectExit> DirectExit { get; set; }
        public DbSet<ExitMaterial> ExitMaterials { get; set; }
        public DbSet<ExitEmail> ExitEmails { get; set; }
        public DbSet<ExitAttachment> ExitAttachments { get; set; }
        public DbSet<ExitStatus> ExitStatus { get; set; }
        public DbSet<DirectExitReceiver> DirectExitReceivers { get; set; }
        public DbSet<DirectExitReceiverAttachment> DirectExitReceiverAttachments { get; set; }
        public DbSet<DirectExitReceiverEmail> DirectExitReceiverEmails { get; set; }
        public DbSet<DirectExitReceiverMaterial> DirectExitReceiverMaterials { get; set; }
        public DbSet<ReceivementDevolutionReceiver> ReceivementDevolutionReceivers { get; set; }
        public DbSet<ReceivementDevolutionMaterial> ReceivementDevolutionMaterials { get; set; }
        public DbSet<ReceivementDevolutionEmail> ReceivementDevolutionEmails { get; set; }
        public DbSet<ReceivementDevolutionAttachment> ReceivementDevolutionAttachments { get; set; }

        public void SetInstalationId(int instalationId)
        {
            this._instalationId = instalationId;
        }

        public void SetUserId(int userId)
        {
            this._userId = userId;
        }

        public SgmDataContext(DbContextOptions<SgmDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Order>().HasQueryFilter(x => x.InstalationId == _instalationId);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            UpdateUser();
            MakeSoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            UpdateUser();
            MakeSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void MakeSoftDelete()
        {
            IEnumerable<EntityEntry> deleting = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is ISoftDelete);
            foreach (EntityEntry entry in deleting)
            {
                (entry.Entity as ISoftDelete).DeletedAt = DateTime.Now;
                entry.State = EntityState.Modified;
            }
        }

        private void UpdateTimestamps()
        {
            IEnumerable<ITimestampedModel> including = this.ChangeTracker.Entries()
                            .Where(x => x.State == EntityState.Added && x.Entity is ITimestampedModel)
                            .Select(x => x.Entity as ITimestampedModel);

            IEnumerable<ITimestampedModel> updating = this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && x.Entity is ITimestampedModel)
                .Select(x => x.Entity as ITimestampedModel);

            foreach (ITimestampedModel item in including)
            {
                item.CreatedAt = DateTime.Now;
                item.UpdatedAt = DateTime.Now;
            }

            foreach (ITimestampedModel item in updating)
            {
                item.UpdatedAt = DateTime.Now;
            }
        }

        private void UpdateUser()
        {
            IEnumerable<IUserModel> updating = this.ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) && x.Entity is IUserModel).Select(x => x.Entity as IUserModel);

            foreach (var item in updating)
            {
                item.UserId = this._userId;
            }
        }
    }
}
