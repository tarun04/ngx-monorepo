namespace Ngx.Monorepo.Framework.Core.Interfaces
{
    /// <summary>
    /// Interface to mark an entity has deleted.
    /// </summary>
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
    }
}
