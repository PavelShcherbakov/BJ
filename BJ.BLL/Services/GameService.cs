using BJ.DAL.Repositories;

namespace BJ.BLL.Services
{
    public class GameService
    {
        private readonly UnitOfWork _unitOfWork;

        public GameService(UnitOfWork uof)
        {
            _unitOfWork = uof;
        }
    }
}
