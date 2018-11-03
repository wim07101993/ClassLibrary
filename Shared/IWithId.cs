namespace Shared
{
    public interface IWithId<TId>
    {
        TId Id { get; set; }
    }
}