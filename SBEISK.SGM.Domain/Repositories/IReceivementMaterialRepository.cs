using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementMaterialRepository : IRepository<ReceivementMaterial>
    {  
        IEnumerable<ReceivementMaterial> NewReceivementMaterial(IList<OrderItem> order, List<int?> array);      
        IEnumerable<ReceivementMaterial> NewReceivementMaterialWithoutOrder(string jsonMaterial);
        IEnumerable<ReceivementMaterial> NewReceivementMaterialWithoutOrder(MaterialWithoutOrderProjection[] jsonMaterial);
        void MergeReceivementsOrder(ICollection<ReceivementMaterial> original, ICollection<ReceivementMaterial> other, Action<ReceivementMaterial, ReceivementMaterial> updateStrategy);
    }
}