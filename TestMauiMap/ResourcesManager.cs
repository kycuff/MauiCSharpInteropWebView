using System.Resources;

namespace TestMauiMap;

public static class ResourcesManager
{
    private static readonly ResourceManager resourceManager =
        new ResourceManager(typeof(Resources));

    public static ResourceManager ResourceManager
    {
        get { return resourceManager; }
    }
}
