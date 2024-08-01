using Serilog;
using System.Reflection;

namespace SolarPanelProsumer.App.Reflection
{
    public static class ReflectionHelper
    {
        public static object GetInstanceOfClass(string nameOfClass)
        {
            try
            {
                //Assembly? assembly = Assembly.LoadFrom(nameOfAssembly);
                Assembly assembly = Assembly.GetExecutingAssembly();
                Log.Information($"The assembly is: {assembly}");
                Type? type = assembly?.GetType(nameOfClass);
                Log.Information($"The type is: {type}");
                object? myObject = new object();
                if (type != null)
                    myObject = Activator.CreateInstance(type);

                return myObject != null? myObject : new object();
            }
            catch (Exception ex)
            {
                Log.Fatal($"The assembly or class is not found {ex.Message}");
                return new object();
            }
        }
    }
}
