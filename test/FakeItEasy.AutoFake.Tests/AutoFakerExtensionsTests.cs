namespace FakeItEasy.AutoFake;

public class AutoFakerExtensionsTests
{
    [Fact]
    public void Use_checks_null_arg()
    {
        IAutoFaker faker = null!;
        FluentActions.Invoking(() => faker.Use<IBar>(new Bar()))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(faker));
    }

    [Fact]
    public void Use_calls_nongeneric()
    {
        IAutoFaker faker = A.Fake<IAutoFaker>();
        Bar bar = new();

        faker.Use<IBar>(bar);

        A.CallTo(() => faker.Use(typeof(IBar), bar)).MustHaveHappened();
    }

    [Fact]
    public void GetFake_checks_null_arg()
    {
        IAutoFaker faker = null!;
        FluentActions.Invoking(() => faker.GetFake<IBar>())
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(faker));
    }

    [Fact]
    public void GetFake_calls_nongeneric()
    {
        IAutoFaker faker = A.Fake<IAutoFaker>();
        Bar bar = new();
        A.CallTo(() => faker.GetFake(typeof(IBar))).Returns(bar);

        IBar result = faker.GetFake<IBar>();

        result.Should().Be(bar);
    }

    [Fact]
    public void CreateInstance_checks_null_arg()
    {
        IAutoFaker faker = null!;
        FluentActions.Invoking(() => faker.CreateInstance<Foo>())
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(faker));
    }

    [Fact]
    public void CreateInstance_calls_nongeneric()
    {
        IAutoFaker faker = A.Fake<IAutoFaker>();
        Foo foo = new();
        A.CallTo(() => faker.CreateInstance(typeof(Foo))).Returns(foo);

        Foo result = faker.CreateInstance<Foo>();

        result.Should().Be(foo);
    }
}

file class Foo
{
}

file interface IBar
{
}

file class Bar : IBar
{
}
