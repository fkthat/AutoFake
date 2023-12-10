using System.Diagnostics.CodeAnalysis;

namespace FakeItEasy.AutoFake;

internal interface IServiceContainer
{
    void Use(Type type, object service);

    bool TryGet(Type type, [NotNullWhen(true)] out object? service);
}
