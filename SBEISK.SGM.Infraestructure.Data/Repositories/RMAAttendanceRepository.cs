using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class RMAAttendanceRepository : Repository<RMAAttendance>, IRMAAttendanceRepository
    {
        private readonly SgmDataContext dataContext;

        public RMAAttendanceRepository(SgmDataContext dataContext) : base(dataContext)
        {            
            this.dataContext = dataContext;
        }  

        public IEnumerable<RMAAttendanceMaterial> NewAttendanceMaterials(int id, List<decimal> quantity)
        {
            var RMAmaterials = this.dataContext.RMAMaterials.Where(x => x.RMAId == id).ToList();


            for (int i = 0; i < RMAmaterials.Count; i++)
            {
                RMAAttendanceMaterial AttendanceMaterials = new RMAAttendanceMaterial();
                AttendanceMaterials.MAterialId = RMAmaterials[i].MaterialId;
                AttendanceMaterials.RMAMaterialId = RMAmaterials[i].Id;
                AttendanceMaterials.Quantity = quantity[i];
                RMAmaterials[i].AmountReceived += quantity[i];
                yield return AttendanceMaterials;
            }
            
            yield break;
        }

        public IEnumerable<RMAAttendanceEmails> NewAttendanceEmails(string emails)
        {
            RMAAttendanceEmails attendanceEmails = new RMAAttendanceEmails();

            if (!string.IsNullOrEmpty(emails))
            {
                string[] emailsSplit = emails.Split(',');
                foreach (string email in emailsSplit)
                {
                    attendanceEmails.Email = email; 
                    yield return attendanceEmails;                       
                }
            }
            yield break;
        }

    }
}