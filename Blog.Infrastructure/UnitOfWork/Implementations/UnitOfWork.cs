using Blog.Infrastructure.Repositories.Interfaces;
using Blog.Infrastructure.UnitOfWork.Interfaces;

namespace Blog.Infrastructure.UnitOfWork.Implementations
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
