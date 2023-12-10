namespace FakeItEasy.AutoFake;

/// <summary>
/// Extensions to the <see cref="IAutoFaker"/> interface.
/// </summary>
public static class AutoFakerExtensions
{
    /// <summary>
    /// Adds an instance to the container.
    /// </summary>
    /// <typeparam name="T">The type that the instance will be registered as.</typeparam>
    /// <param name="faker">An instance of the <see cref="IAutoFaker"/>.</param>
    /// <param name="service">The instance to register.</param>
    public static void Use<T>(this IAutoFaker faker, T service) where T : class =>
        (faker ?? throw new ArgumentNullException(nameof(faker))).Use(typeof(T), service);

    /// <summary>
    /// Searches and retrieves an object from the container that matches the <typeparamref
    /// name="T"/>. This can be a service setup explicitly via <see cref="Use"/> or implicitly with
    /// <see cref="CreateInstance"/>.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <param name="faker">An instance of the <see cref="IAutoFaker"/>.</param>
    public static T GetFake<T>(this IAutoFaker faker) where T : class =>
        (T)(faker ?? throw new ArgumentNullException(nameof(faker))).GetFake(typeof(T));

    /// <summary>
    /// Constructs an instance from known services. Any dependencies (constructor arguments)
    /// are fulfilled by searching the container or, if not found, automatically generating
    /// fakes.
    /// </summary>
    /// <typeparam name="T">A concrete type.</typeparam>
    /// <param name="faker">An instance of the <see cref="IAutoFaker"/>.</param>
    /// <returns>An instance of T with all constructor arguments derived from services 
    /// setup in the container.</returns>
    public static T CreateInstance<T>(this IAutoFaker faker) where T : class =>
        (T)(faker ?? throw new ArgumentNullException(nameof(faker))).CreateInstance(typeof(T));
}
