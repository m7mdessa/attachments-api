using Domain.SharedKernel;

namespace Application
{
    public class BaseService
    {
        private IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        protected internal IUnitOfWork UnitOfWork { get; set; }
    }
}