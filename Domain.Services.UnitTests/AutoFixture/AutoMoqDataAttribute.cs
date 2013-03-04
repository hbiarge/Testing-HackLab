namespace Domain.Services.UnitTests.AutoFixture
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Xunit;

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
