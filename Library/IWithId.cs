namespace Library
{
    public interface IWithId<TId>
    {
        TId Id { get; set; }
    }
}