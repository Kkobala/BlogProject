using Blog.Infrastructure.Repositories.Interfaces;

namespace Blog.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; }
    }
}
