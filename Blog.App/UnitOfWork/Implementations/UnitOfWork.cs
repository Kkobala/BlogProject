using Blog.App.Repositories.Interfaces;
using Blog.App.UnitOfWork.Interfaces;

namespace Blog.App.UnitOfWork.Implementations
{
    public class UnitOfWork: IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; set; }

        public UnitOfWork(IBaseRepository baseRepository)
        {
            BaseRepository = baseRepository;
        }
    }
}
