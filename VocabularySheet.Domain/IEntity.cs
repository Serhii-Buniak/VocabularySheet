namespace VocabularySheet.Domain;

public interface IEntity<out TId> where TId : notnull
{
    TId Id { get; }
}

public interface IEntity : IEntity<long>
{

}