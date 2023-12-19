using Blog.App.Repositories.Interfaces;

namespace Blog.App.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; }
    }
}
