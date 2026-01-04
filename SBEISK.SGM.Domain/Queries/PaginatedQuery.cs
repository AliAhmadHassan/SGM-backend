using System;

namespace SBEISK.SGM.Domain.Queries
{
    public class PaginatedQuery
    {
        public int Page { get; set; }
        public int Items { get; set; }
        public PaginatedQuery(int page, int items)
        {
            if (page <= 0 || items <= 0)
            {
                throw new InvalidOperationException("Dados de paginação inválidos.");
            }

            Page = page;
            Items = items;
        }
    }
}
