namespace FakeItEasy.AutoFake;

file interface IFoo { }

file class Foo : IFoo { }

public class AutoFakerTests
{
    [Fact]
    public void Use_checks_args()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(IFoo);
        var service = new Foo();

        AutoFaker sut = new(container, factory);

        sut.Invoking(s => s.Use(type: null!, service)).Should()
            .Throw<ArgumentNullException>().WithParameterName(nameof(type));

        sut.Invoking(s => s.Use(type, service: null!)).Should()
            .Throw<ArgumentNullException>().WithParameterName(nameof(service));

        sut.Invoking(s => s.Use<IFoo>(service: null!)).Should()
            .Throw<ArgumentNullException>().WithParameterName(nameof(service));
    }

    [Fact]
    public void Use_calls_container()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(IFoo);
        var service = new Foo();

        AutoFaker sut = new(container, factory);
        sut.Use(type, service);
        sut.Use<IFoo>(service);

        A.CallTo(() => container.Use(type, service))
            .MustHaveHappened(2, Times.Exactly);
    }

    [Fact]
    public void Get_checks_args()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(IFoo);

        AutoFaker sut = new(container, factory);

        sut.Invoking(s => s.Get(type: null!)).Should()
            .Throw<ArgumentNullException>().WithParameterName(nameof(type));
    }

    [Fact]
    public void Get_returns_from_container()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(IFoo);
        var service = new Foo();

        object? svc;
        A.CallTo(() => container.TryGet(type, out svc))
            .Returns(true).AssignsOutAndRefParameters(service);

        AutoFaker sut = new(container, factory);

        sut.Get(type).Should().Be(service);
        sut.Get<IFoo>().Should().Be(service);
    }

    [Fact]
    public void Get_handles_unsuccess()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(IFoo);

        object? svc;
        A.CallTo(() => container.TryGet(type, out svc)).Returns(false);

        AutoFaker sut = new(container, factory);

        sut.Invoking(s => s.Get(type)).Should()
           .Throw<InvalidOperationException>();

        sut.Invoking(s => s.Get<IFoo>()).Should()
           .Throw<InvalidOperationException>();
    }

    [Fact]
    public void CreateInstance_checks_args()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        AutoFaker sut = new(container, factory);

        Type type = null!;
        sut.Invoking(s => s.CreateInstance(type)).Should()
           .Throw<ArgumentNullException>().WithParameterName(nameof(type));
    }

    [Fact]
    public void CreateInstance_returns_from_factory()
    {
        var container = A.Fake<IServiceContainer>();
        var factory = A.Fake<IObjectFactory>();

        var type = typeof(Foo);
        var instance = new Foo();
        A.CallTo(() => factory.CreateInstance(type)).Returns(instance);

        AutoFaker sut = new(container, factory);

        sut.CreateInstance(type).Should().Be(instance);
        sut.CreateInstance<Foo>().Should().Be(instance);
    }
}
