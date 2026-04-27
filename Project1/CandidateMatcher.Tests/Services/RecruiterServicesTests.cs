using Moq;
using MyApi.Data;
using MyApi.Services;
using MyApp.Core.Models;

namespace CandidateMatcher.Tests.Services;

public class RecruiterServiceTests
{
    //moq = create fake objects to avoid calling real code

    //fake repo layer object
    private readonly Mock<IRecruiterRepo> _repoMock;

    //create actual service object
    private readonly RecruiterService _sut; //system under test

    public RecruiterServiceTests()
    {
        //creating mock objects for dependencies
        _repoMock = new Mock<IRecruiterRepo>();
                
        //mock objects to satify RecruiterService constructor 
        _sut= new RecruiterService(_repoMock.Object);
    }


    [Fact]
    public async Task GetRecruiterByIdAsync_InvalidId_Throws()
    {
        //arrange
        //act
        //assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetRecruiterByIdAsync(0) //
        );

       //verify, fired x amount of times
    _repoMock.Verify(r => r.GetRecruiter(It.IsAny<int>()),Times.Never);
    }
    
    
}