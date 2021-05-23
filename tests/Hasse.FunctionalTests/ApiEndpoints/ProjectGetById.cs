﻿using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using Hasse.Web;
using Hasse.Web.Endpoints.ProjectEndpoints;
using Xunit;

namespace Hasse.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class ProjectGetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProjectGetById(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsSeedProjectGivenId1()
        {
            var result = await _client.GetAndDeserialize<GetProjectByIdResponse>(GetProjectByIdRequest.BuildRoute(1));

            Assert.Equal(1, result.Id);
            Assert.Equal(SeedData.TestProject1.Name, result.Name);
            Assert.Equal(3, result.Items.Count);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenId0()
        {
            var route = GetProjectByIdRequest.BuildRoute(0);
            _ = await _client.GetAndEnsureNotFound(route);
        }
    }
}