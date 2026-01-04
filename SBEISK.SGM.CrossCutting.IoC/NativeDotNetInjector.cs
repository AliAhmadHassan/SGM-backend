using Microsoft.Extensions.DependencyInjection;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Infraestructure.Data.Repositories;
using SBEISK.SGM.Infraestructure.Data.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Services;
using SBEISK.SGM.Infraestructure.Data.Storage;
using SBEISK.SGM.Infraestructure.Data.Storage.Base;

namespace SBEISK.SGM.CrossCutting.IoC
{
    public static class NativeDotNetInjector
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            IncludeRepositories(serviceCollection);
            IncludeServices(serviceCollection);
        }

        private static void IncludeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IStorage, FileStorage>();
            serviceCollection.AddScoped<ISynchronizationService, SynchronizationService>();
            serviceCollection.AddScoped<JwtTokenGeneratorService>();
        }

        private static void IncludeRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();
            serviceCollection.AddScoped<IInstallationRepository, InstallationRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IMaterialRepository, MaterialRepository>();
            serviceCollection.AddScoped<IProviderRepository, ProviderRepository>();
            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<IUserPermissionReadOnlyRepository, UserPermissionReadOnlyRepository>();
            serviceCollection.AddScoped<IUserProfileRepository, UserProfileRepository>();
            serviceCollection.AddScoped<IReceiverRepository, ReceiverRepository>();
            serviceCollection.AddScoped<IUserInstallationsProfilesReadOnlyRepository, UserInstallationsProfilesReadOnlyRepository>();
            serviceCollection.AddScoped<IInstallationsReadOnlyRepository, InstallationsReadOnlyRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            serviceCollection.AddScoped<IInstallationTypeRepository, InstallationTypeRepository>();
            serviceCollection.AddScoped<IParentPermissionReadOnlyRepository, ParentPermissionReadOnlyRepository>();
            serviceCollection.AddScoped<IInstallationTypeRepository, InstallationTypeRepository>();
            serviceCollection.AddScoped<IUsersReadOnlyRepository, UsersReadOnlyRepository>();
            serviceCollection.AddScoped<IProfilePermissionsReadOnlyRepository, ProfilePermissionsReadOnlyRepository>();
            serviceCollection.AddScoped<IUserProfileInstallationRepository, UserProfileInstallationRepository>();
            serviceCollection.AddScoped<IReceivementInvoiceOrderRepository, ReceivementInvoiceOrderRepository>();
            serviceCollection.AddScoped<IReceivementInvoiceOrderReadOnlyRepository, ReceivementInvoiceOrderReadOnlyRepository>();
            serviceCollection.AddScoped<IReceivementEmailRepository, ReceivementEmailRepository>();
            serviceCollection.AddScoped<IReceivementFileRepository, ReceivementFileRepository>();
            serviceCollection.AddScoped<IReceivementAttachmentRepository, ReceivementAttachmentRepository>();
            serviceCollection.AddScoped<IDivergenceRepository, DivergenceRepository>();
            serviceCollection.AddScoped<IDivergenceFileRepository, DivergenceFileRepository>();
            serviceCollection.AddScoped<IReceivementMaterialRepository, ReceivementMaterialRepository>();
            serviceCollection.AddScoped<IReceivementService, ReceivementService>();
            serviceCollection.AddScoped<IRMARepository, RMARepository>();
            serviceCollection.AddScoped<IRMAReadOnlyRepository, RMAReadOnlyRepository>();
            serviceCollection.AddScoped<IReceivementInvoiceWithoutOrderRepository, ReceivementInvoiceWithoutOrderRepository>();
            serviceCollection.AddScoped<IReceivementProviderReasonRepository, ReceivementProviderReasonRepository>();
            serviceCollection.AddScoped<IReasonWithoutOrderRepository, ReasonWithoutOrderRepository>();
            serviceCollection.AddScoped<ISTMRepository, STMRepository>();
            serviceCollection.AddScoped<IVwTransferAttendanceMaterialReadOnlyRepository, VwTransferAttendanceMaterialReadOnlyRepository>();
            serviceCollection.AddScoped<ISTMMaterialRepository, STMMaterialRepository>();
            serviceCollection.AddScoped<ITransferRepository, TransferRepository>();
            serviceCollection.AddScoped<IRMADetailsReadOnlyRepository, RMARDetailseadOnlyRepository>();
            serviceCollection.AddScoped<ITransferMaterialRepository, TransferMaterialRepository>();
            serviceCollection.AddScoped<IDirectExitReceiverRepository, DirectExitReceiverRepository>();
            serviceCollection.AddScoped<IDirectExitReceiverMaterialRepository, DirectExitReceiverMaterialRepository>();
            serviceCollection.AddScoped<IRMAAttendanceReadOnlyRepository, RMAAttendanceReadOnlyRepository>();
            serviceCollection.AddScoped<IReceivementDevolutionMaterialRepository, ReceivementDevolutionMaterialRepository>();
            serviceCollection.AddScoped<IRMAAttendanceRepository, RMAAttendanceRepository>();
            serviceCollection.AddScoped<IReceivementDevolutionRepository, ReceivementDevolutionRepository>();
            serviceCollection.AddScoped<IDirectExitRepository, DirectExitRepository>();
            serviceCollection.AddScoped<IExitMaterialRepository, ExitMaterialRepository>();
            serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
