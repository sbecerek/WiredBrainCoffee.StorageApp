using System.Text.Json;

namespace WiredBrainCoffee.StorageApp.Entities
{
    public static class EntityExtensions
    {
        public static T? Copy<T>( this T itemToCopy)
        where T : IEntity
        //since we used generic T here we don't want other classes
        //to be extend with this copy method
        {
            var json = JsonSerializer.Serialize<T>(itemToCopy); 
            return JsonSerializer.Deserialize<T>(json);
        }
        
    }
}