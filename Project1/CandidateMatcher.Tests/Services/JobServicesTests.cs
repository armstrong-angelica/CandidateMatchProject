using Moq;
using MyApi.Data;
using MyApi.Services;
using MyApp.Core.Models;
using MyApi.DTOs;

namespace CandidateMatcher.Tests.Services;

public class JobServiceTests
{
    //moq = create fake objects to avoid calling real code

    //fake repo layer object
    private readonly Mock<IJobRepo> _repoMock;

    //create actual service object
    private readonly JobService _sut; //system under test

    public JobServiceTests()
    {
        //creating mock objects for dependencies
        _repoMock = new Mock<IJobRepo>();

        //mock objects to satify RecruiterService constructor 
        _sut = new JobService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateJobAsync_InvalidId_Throws()
    {
        NewJobDTO job = new();
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.CreateJobAsync(job)
        );

        _repoMock.Verify(r => r.FindRecruiter(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task DeleteJobAsync_InvalidId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.DeleteJobAsync(-2)
        );

        _repoMock.Verify(r => r.GetJobByID(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task HireAsync_ValidAndInvalid_Throws()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.HireAsync(3, 0)
        );

        _repoMock.Verify(r => r.Hire(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task HireAsync_InvalidAndInvalid_Throws()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.HireAsync(-2, 0)
        );

        _repoMock.Verify(r => r.Hire(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }




}