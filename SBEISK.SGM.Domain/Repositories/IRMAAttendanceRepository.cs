using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public interface IRMAAttendanceRepository : IRepository<RMAAttendance>
    {
        IEnumerable<RMAAttendanceMaterial> NewAttendanceMaterials(int id, List<decimal> quantity);
        IEnumerable<RMAAttendanceEmails> NewAttendanceEmails(string emails);
    }
}