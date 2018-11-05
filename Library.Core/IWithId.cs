namespace Library.Core
{
    public interface IWithId<TId>
    {
        TId Id { get; set; }
    }
}