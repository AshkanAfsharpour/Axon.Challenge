
namespace Axon.Application.Common.Models
{
    public abstract class PaginatedRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
