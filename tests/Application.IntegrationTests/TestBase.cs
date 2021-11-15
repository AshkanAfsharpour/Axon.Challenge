using Axon.Application.Common.Interfaces;
using Axon.Presistence;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Axon.Application.IntegrationTests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetState();
        }
    }
}
