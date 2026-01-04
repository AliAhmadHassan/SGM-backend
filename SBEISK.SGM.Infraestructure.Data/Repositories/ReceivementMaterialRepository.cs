using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementMaterialRepository : Repository<ReceivementMaterial>, IReceivementMaterialRepository
    {
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly IMaterialRepository materialRepository;
        public ReceivementMaterialRepository(SgmDataContext dataContext, IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository,
                                            IMaterialRepository materialRepository) : base(dataContext)
        {
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.materialRepository = materialRepository;
        }

        public IEnumerable<ReceivementMaterial> NewReceivementMaterial(IList<OrderItem> orders, List<int?> array)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                ReceivementMaterial receivementMaterial = new ReceivementMaterial();

                receivementMaterial.MaterialId = orders[i].MaterialId;
                receivementMaterial.OrderItemId = orders[i].Id;

                if (i < array.Count)
                {
                    receivementMaterial.Amount = array[i];
                }


                yield return receivementMaterial;
            }
            yield break;
        }

        public void MergeReceivementsOrder(ICollection<ReceivementMaterial> original, ICollection<ReceivementMaterial> other, Action<ReceivementMaterial, ReceivementMaterial> updateStrategy)
        {
            Merger<ReceivementMaterial> merger = new Merger<ReceivementMaterial>((x, y) => x.Id == y.Id, (x, y) => x.ReceivementInvoiceOrderId == y.ReceivementInvoiceOrderId);
            MergeResult<ReceivementMaterial> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.ReceivementMaterials.RemoveRange(result.ItemsToDelete);
            this.DataContext.ReceivementMaterials.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }

        public IEnumerable<ReceivementMaterial> NewReceivementMaterialWithoutOrder(string jsonMaterial)
        {
            if (!string.IsNullOrEmpty(jsonMaterial))
            {
                IList<MaterialWithoutOrderProjection> materialWithoutOrderProjections = JsonConvert.DeserializeObject<IList<MaterialWithoutOrderProjection>>(jsonMaterial);

                foreach (MaterialWithoutOrderProjection item in materialWithoutOrderProjections)
                {
                    if(String.IsNullOrEmpty(item.MaterialCode))
                    {
                        continue;
                    }
                    Material material = this.materialRepository.MaterialByCode(item.MaterialCode);

                    ReceivementMaterial receivementMaterial = new ReceivementMaterial();
                    receivementMaterial.Amount = item.ReceivementAmount;
                    if (material != null)
                    {
                        receivementMaterial.MaterialId = material.Id;
                    }
                    receivementMaterial.ReceivementInvoiceOrderId = this.receivementInvoiceOrderRepository.LastIdReceiver();

                    yield return receivementMaterial;
                }
            }
            yield break;
        }

        public IEnumerable<ReceivementMaterial> NewReceivementMaterialWithoutOrder(MaterialWithoutOrderProjection[] materials)
        {
            if (materials != null)
            {
                foreach (MaterialWithoutOrderProjection item in materials)
                {
                    Material material = this.materialRepository.MaterialByCode(item.MaterialCode);

                    ReceivementMaterial receivementMaterial = new ReceivementMaterial();
                    receivementMaterial.Amount = item.ReceivementAmount;
                    receivementMaterial.MaterialId = material.Id;
                    receivementMaterial.ReceivementInvoiceOrderId = this.receivementInvoiceOrderRepository.LastIdReceiver();

                    yield return receivementMaterial;
                }
            }
            yield break;
        }
    }
}