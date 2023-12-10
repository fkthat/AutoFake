namespace FakeItEasy.AutoFake;

internal interface IObjectFactory
{
    object CreateInstance(Type type);
}
