using HouseRenting.Services.Data;
using HouseRenting.Services.Data.Interfaces;
using HouseRenting.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Services.Tests
{
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
        }
    }
}
