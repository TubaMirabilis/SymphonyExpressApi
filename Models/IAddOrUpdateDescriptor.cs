namespace SymphonyExpressApi.Models;
public interface IAddOrUpdateDescriptor
{
    AddOrUpdate ActionType { get; }
    int Id { get; }
}