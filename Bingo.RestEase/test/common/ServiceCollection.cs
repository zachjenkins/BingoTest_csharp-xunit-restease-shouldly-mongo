using Xunit;

namespace Bingo.RestEase.Test.Common
{
    [CollectionDefinition("HttpTest")]
    public class ServiceCollection : IClassFixture<ServiceFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces for Bingo.
    }
}
