using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application._Common
{
    public class CommonGridQuery<T> : IRequest<GridsToReturnDto<T>>
    {
		private const int minPageNum = 1;
        private int pageNum;

		public int PageNum
		{
			get { return pageNum; }
			set { pageNum = value <= 0 ? minPageNum : value; }
		}

		private const int minPageSize = 5;
		private const int maxPageSize = 15;
        private int pageSize;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value < minPageSize ? minPageSize : (value > maxPageSize ? maxPageSize : value); }
		}


	}
}
