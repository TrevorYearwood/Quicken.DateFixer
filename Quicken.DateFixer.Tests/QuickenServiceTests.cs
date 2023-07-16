using Quicken.DateFixer.Api.Services;

namespace Quicken.DateFixer.Tests
{
    public class QuickenServiceTests
    {
        private readonly IQuickenService _quickenService;

        public QuickenServiceTests()
        {
            _quickenService = new QuickenService();
        }

        [Fact]
        public async Task Test1()
        {
            //Arrange             
            //Act
            var result = await _quickenService.UpdateFile("Trevor", "C:\\Filename_Trevor.qif");

            //Assert
            Assert.Equal("success", result);
        }
    }
}