using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Address : Entity, ITimestampedModel, IUserModel, ISoftDelete
    {
        public string Description { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public int UfId { get; set; }
        public int CityId { get; set; }
        public string Cep { get; set; }
        public Uf Uf { get; set; }
        public City City { get; set; }
        public ICollection<Installation> Installations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int UserId { get; set ; }
    }
}