using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Address;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class DivergenceFileRepository : Repository<DivergenceFiles>, IDivergenceFileRepository
    {
        private readonly SgmDataContext dataContext;
        public DivergenceFileRepository(SgmDataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SaveFiles(IFormFileCollection files, int id)
        {
            if(files == null)
            {
                return;
            }

            foreach(var item in files)
            {
                using (Stream stream = item.OpenReadStream())
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        stream.CopyTo(mStream);
                        byte[] array = mStream.ToArray();
                        Add( new DivergenceFiles{ Document = array, DivergenceId = id, Name = item.FileName});
                    }
                }   
            }
        }
    }
}