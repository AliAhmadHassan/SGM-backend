using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Address;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;


namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly SgmDataContext context;
        public AddressRepository(SgmDataContext context) : base(context)
        {
            this.context = context;
        }

        public override Address Add(Address entity)
        {
            Uf uf =  context.Ufs.Find(entity.UfId);
            if(uf == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(Uf).Name} with given key was not found.");
            }
            
            City city = context.Cities.Find(entity.CityId);
            if(city == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(City).Name} with given key was not found.");
            }
            
            return base.Add(entity);
        }

        public IList<Address> WithItems()
        {
            return DataContext.Addresses.ToList();
        }

        public PaginatedQueryResult<Address> All(GenericPaginatedQuery<AddressQuery> query)
        {
            IQueryable<Address> queryable = ApplyFilter(query.Filter);

            return ApplyPagination(queryable, query);
        }

        public IQueryable<Address> All(AddressQuery query)
        {
            return ApplyFilter(query);
        }
        private IQueryable<Address> ApplyFilter(AddressQuery query)
        {
            IQueryable<Address> queryable = base.Query()
            .Include(x => x.City)
            .Include(x => x.Uf);

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.PublicPlace))
            {
                queryable = queryable.Where(x => x.PublicPlace.Contains(query.PublicPlace));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if (!string.IsNullOrEmpty(query.Number))
            {
                queryable = queryable.Where(x => x.Number.Contains(query.Number));
            }

            if (!string.IsNullOrEmpty(query.Neighborhood))
            {
                queryable = queryable.Where(x => x.Neighborhood.Contains(query.Neighborhood));
            }

            if (!string.IsNullOrEmpty(query.Complement))
            {
                queryable = queryable.Where(x => x.Complement.Contains(query.Complement));
            }

            if (!string.IsNullOrEmpty(query.Reference))
            {
                queryable = queryable.Where(x => x.Reference.Contains(query.Reference));
            }

            if (!string.IsNullOrEmpty(query.City))
            {
                queryable = queryable.Where(x => x.City.Name.Contains(query.City));
            }

            if (!string.IsNullOrEmpty(query.Uf))
            {
                queryable = queryable.Where(x => x.Uf.Initials.Contains(query.Uf));
            }

            if (!string.IsNullOrEmpty(query.Cep))
            {
                queryable = queryable.Where(x => x.Cep.Equals(query.Cep));
            }

            return queryable;
        }

        public override void Delete(int id)
        {
            Address queryable = DataContext.Addresses
                .Include(x => x.Installations)
                .FirstOrDefault(x => x.Id == id);

            if (queryable == null)
                throw new EntityNotFoundException($"Entity {typeof(Address).Name} with given key was not found.");

            if (queryable.Installations.Any(x => x.DeletedAt == null))
                throw new EntityCannotBeDeletedException($"Este endereço não pode ser deletado porque possui instalação!");      

            this.DbSet.Remove(queryable);
        }
    }
}