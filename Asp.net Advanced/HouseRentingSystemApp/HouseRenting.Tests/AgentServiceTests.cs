using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Services.Tests
{
    using static Services.Tests.DatabaseSeeder;

    [TestFixture]
	public class AgentServiceTests
    {
        private DbContextOptions<HouseRentingDbContext> dbOptions;
        private HouseRentingDbContext dbContext;

        private IAgentService agentService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<HouseRentingDbContext>()
                .UseInMemoryDatabase("HouseRentingInMemory" + Guid.NewGuid())
                .Options;
            dbContext = new HouseRentingDbContext(dbOptions);
            agentService = new AgentService(dbContext);
            SeedDatabase(dbContext);
        }

        [OneTimeTearDown]
        public void TearDownDb() => dbContext.Dispose();

		[Test]
        public async Task GetAgentIdByUserIdShouldReturnCorrectAgentId()
        {
            string agentUserId = Agent.UserId.ToString();
            string agentId = Agent.Id.ToString();

			string resultAgentId = await agentService.GetAgentIdByUserIdAsync(agentUserId);

            Assert.That(resultAgentId, Is.EqualTo(agentId));
        }

		[Test]
		public async Task GetAgentIdByUserIdShouldReturnNullIfAgentDoesntExist()
		{
            string agentUserId = "returns null";

			string resultAgentId = await agentService.GetAgentIdByUserIdAsync(agentUserId);

			Assert.That(resultAgentId, Is.EqualTo(null));
		}

        [Test]
        public async Task ExistsByIdShouldReturnTrueIfAgentExists()
        {
			string agentUserId = Agent.UserId.ToString();

            bool result = await agentService.ExistsByIdAsync(agentUserId);

            Assert.That(result, Is.True);
		}

		[Test]
		public async Task ExistsByIdShouldReturnFalseIfAgentDoesntExist()
		{
			string agentUserId = "467-325fed-pippa";

			bool result = await agentService.ExistsByIdAsync(agentUserId);

			Assert.That(result, Is.False);
		}

        [Test]
        public async Task AgentWithPhoneNumberExistsShouldReturnTrueIfPhoneExists()
        {
            string agentPhoneNumber = Agent.PhoneNumber;

            bool result = await agentService.AgentWithPhoneNumberExistsAsync(agentPhoneNumber);

            Assert.That(result, Is.True);
        }

        [Test]
		public async Task AgentWithPhoneNumberExistsShouldReturnFalseIfPhoneDoesntExists()
		{
			string agentPhoneNumber = "1234567890";

			bool result = await agentService.AgentWithPhoneNumberExistsAsync(agentPhoneNumber);

			Assert.That(result, Is.False);
		}

        [Test]
        public async Task CreateShouldIncreaseAgentCount()
        {
            int countBefore = dbContext.Agents.Count();
            await agentService.Create(Agent.UserId.ToString(), new Web.ViewModels.Agent.BecomeAgentFormModel()
            {
                PhoneNumber = "1111122222",

            });

            int countAfter = dbContext.Agents.Count();

            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }
	}
}
