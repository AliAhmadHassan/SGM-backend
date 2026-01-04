using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.TransferAttendanceMaterial;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IVwTransferAttendanceMaterialReadOnlyRepository: IReadOnlyRepository<VwTransferAttendanceMaterial>
    {
        PaginatedQueryResult<VwTransferAttendanceMaterial> All(GenericPaginatedQuery<VwTransferAttendanceMaterialQuery> transferAttendanceMaterialQuery);

        IQueryable<VwTransferAttendanceMaterial> Filter(VwTransferAttendanceMaterialQuery query);

        VwTransferAttendanceMaterial GetSTM(int id);

        List<STMMaterial> GetMaterials(int id);
    }
}
