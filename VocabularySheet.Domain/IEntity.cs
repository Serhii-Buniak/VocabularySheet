namespace VocabularySheet.Domain;

public interface IEntity<TId>
{
    TId Id { get; set; }
}

public interface IEntity : IEntity<long>
{

}