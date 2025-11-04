using System.Collections.Generic;

public static class ResourceManager
{
    private static readonly Dictionary<Resource, Base> _resourceOwners = new();

    public static bool TryClaimResource(Resource resource, Base claimingBase)
    {
        if (_resourceOwners.ContainsKey(resource))
            return false;

        _resourceOwners[resource] = claimingBase;
        return true;
    }

    public static void ReleaseResource(Resource resource) =>
        _resourceOwners.Remove(resource);

    public static bool IsResourceOwnedBy(Resource resource, IBase baseToCheck) =>
         _resourceOwners.TryGetValue(resource, out var owner) && owner == (object)baseToCheck;
}