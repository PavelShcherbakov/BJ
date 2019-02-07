using BJ.DAL.Repositories;

namespace BJ.BLL.Services
{
    public class HistoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public HistoryService(UnitOfWork uof)
        {
            _unitOfWork = uof;
        }
    }
}
