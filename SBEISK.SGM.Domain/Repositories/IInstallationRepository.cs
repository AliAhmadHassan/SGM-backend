using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Installation;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IInstallationRepository : IRepository<Installation>
    {
        void MergeResponsibles(ICollection<UserProfileInstallation> original, ICollection<UserProfileInstallation> other, Action<UserProfileInstallation, UserProfileInstallation> updateStrategy);
        //Installation GetWithResponsibles(int id);
        Installation Get(int id);
        PaginatedQueryResult<Installation> All(GenericPaginatedQuery<InstallationQuery> installationQuery);
    }
}