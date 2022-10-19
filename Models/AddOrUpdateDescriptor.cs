namespace SymphonyExpressApi.Models;
public class AddOrUpdateDescriptor : IAddOrUpdateDescriptor
{
    public AddOrUpdate ActionType { get; }
    public int Id { get; }
    public AddOrUpdateDescriptor(AddOrUpdate actionType, int id)
    {
        ActionType = actionType;
        Id = id;
    }
}