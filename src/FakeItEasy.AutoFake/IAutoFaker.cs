namespace FakeItEasy.AutoFake;

/// <summary>
/// An auto-faking IoC container that generates fake objects using FakeItEasy.
/// </summary>
public interface IAutoFaker
{
    /// <summary>
    /// Adds an instance to the container.
    /// </summary>
    /// <param name="type">The type of service to use.</param>
    /// <param name="service">The service to use</param>
    void Use(Type type, object service);

    /// <summary>
    /// Searches and retrieves an object from the container that matches the <paramref
    /// name="type"/>. This can be a service setup explicitly via `.Use()` or implicitly with
    /// `.CreateInstance()`.
    /// </summary>
    /// <param name="type">The type of service to retrieve.</param>
    object GetFake(Type type);

    /// <summary>
    /// Constructs an instance from known services. Any dependencies (constructor arguments) are
    /// fulfilled by searching the container or, if not found, automatically generating fakes.
    /// </summary>
    /// <param name="type">A concrete type.</param>
    /// <returns>
    /// An instance of type with all constructor arguments derived from services setup in the
    /// container.
    /// </returns>
    object CreateInstance(Type type);
}
