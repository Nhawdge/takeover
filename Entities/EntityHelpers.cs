using System;
using System.Linq;
using System.Reflection;
using Takeover.Components;

namespace Takeover.Entities
{
    public static class EntityHelpers
    {
        public static T GetComponentByType<T>(this Entity entity)
        {

            var component = entity.Components.FirstOrDefault(x => x.GetType() == typeof(T));
            return (T)Convert.ChangeType(component, typeof(T));
        }
        public static string ShortId(this Entity entity)
        {
            return entity.Id.ToString().Substring(0, 4);
        }
    }
}