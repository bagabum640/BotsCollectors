public interface IResourcePicker
{
    public bool HasResource { get; }
    public void PickResource(Resource resource);
    public void DropResource();
}