using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Order;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Perfis)]
    public class OrderController : BaseController
    {
        private readonly IOrderRepository orderRepository;
        private readonly IReceivementInvoiceOrderRepository receivementRepository;

        public OrderController(IOrderRepository orderRepository, IReceivementInvoiceOrderRepository receivementRepository)
        {
            this.orderRepository = orderRepository;
            this.receivementRepository = receivementRepository;
        }

        [HttpGet]
        public IActionResult Index(int page, int items)
        {
            PaginatedQuery paginatedQuery = new PaginatedQuery(page, items);
            PaginatedQueryResult<Order> orders = orderRepository.All(paginatedQuery);
            return Ok((orders.Transform(x => Mapper.Map<OrderViewModel>(x))).AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult OrderAndItems(int id, List<int?> amounts)
        {
            var a = orderRepository.OrderItems(id, amounts);
            return Ok(Mapper.Map<IList<OrderItemViewModel>>(a).AsSuccessGenericResponse());
        }
       
       [HttpGet]
       [Route("Combo")]
        public IActionResult Combo()
        {
            IList<OrderStatus> offices = orderRepository.Status();
            return Ok(SelectItemBuilder.Generate(offices, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}
