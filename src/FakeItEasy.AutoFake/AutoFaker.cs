namespace FakeItEasy.AutoFake;

/// <inheritdoc/>
public sealed class AutoFaker : IAutoFaker
{
    /// <inheritdoc/>
    public void Use(Type type, object service)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public object GetFake(Type type)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public object CreateInstance(Type type)
    {
        throw new NotImplementedException();
    }
}
