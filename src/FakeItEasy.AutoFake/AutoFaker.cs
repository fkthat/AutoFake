using System.Diagnostics.CodeAnalysis;

namespace FakeItEasy.AutoFake;

/// <inheritdoc/>
public sealed class AutoFaker
{
    private readonly IObjectFactory _factory;
    private readonly IServiceContainer _container;

    /// <summary>
    /// Initializes an instance of <see cref="AutoFaker"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public AutoFaker()
    {
        // temporary
        _factory = null!;
        _container = null!;
    }

    internal AutoFaker(IServiceContainer container, IObjectFactory factory)
    {
        _container = container;
        _factory = factory;
    }

    /// <summary>
    /// Adds an instance to the container.
    /// </summary>
    /// <param name="type">The type of service to use.</param>
    /// <param name="service">The service to use</param>
    public void Use(Type type, object service)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        ArgumentNullException.ThrowIfNull(service, nameof(service));

        _container.Use(type, service);
    }

    /// <summary>
    /// Adds an instance to the container.
    /// </summary>
    /// <typeparam name="T">The type that the instance will be registered as.</typeparam>
    /// <param name="service">The instance to register.</param>
    public void Use<T>(T service) where T : class => Use(typeof(T), service);

    /// <summary>
    /// Searches and retrieves an object from the container that matches the <paramref
    /// name="type"/>. This can be a service setup explicitly via <see cref="Use"/> or implicitly
    /// with <see cref="CreateInstance"/>.
    /// </summary>
    /// <param name="type">The type of service to retrieve.</param>
    public object Get(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));

        return _container.TryGet(type, out var service) ? service
            : throw new InvalidOperationException($"Cannot resolve the {type} service.");
    }

    /// <summary>
    /// Searches and retrieves an object from the container that matches the <typeparamref
    /// name="T"/>. This can be a service setup explicitly via <see cref="Use"/> or implicitly with
    /// <see cref="CreateInstance"/>.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    public T Get<T>() => (T)Get(typeof(T));

    /// <summary>
    /// Constructs an instance from known services. Any dependencies (constructor arguments) are
    /// fulfilled by searching the container or, if not found, automatically generating fakes.
    /// </summary>
    /// <param name="type">A concrete type.</param>
    /// <returns>
    /// An instance of type with all constructor arguments derived from services setup in the
    /// container.
    /// </returns>
    public object CreateInstance(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));

        return _factory.CreateInstance(type);
    }

    /// <summary>
    /// Constructs an instance from known services. Any dependencies (constructor arguments) are
    /// fulfilled by searching the container or, if not found, automatically generating fakes.
    /// </summary>
    /// <typeparam name="T">A concrete type.</typeparam>
    /// <returns>
    /// An instance of T with all constructor arguments derived from services setup in the
    /// container.
    /// </returns>
    public T CreateInstance<T>() where T : class => (T)CreateInstance(typeof(T));
}
