using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Blognet.Cms.Tests.WebApiTests
{
    // @TODO Seed data for test, use InMemoryDatabase
    public class ForwardControllerIntegrationTest : IClassFixture<WebApplicationFactory<Cms.WebApi.Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Cms.WebApi.Startup> _factory;

        public ForwardControllerIntegrationTest(WebApplicationFactory<Cms.WebApi.Startup> factory)
        {
            AutoMapper.Mapper.Reset();
            _factory = factory;
        }

        public void Dispose()
        {
            AutoMapper.Mapper.Reset();
        }

        /// <summary>
        /// Test forward endpoint, return correct article slug.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_EndpointReturnForwardedArticleSlug()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("projectDomain", "localhost");
            // Need to be double escaped like format %25
            var param = Uri.EscapeDataString(Uri.EscapeDataString("/1/test/"));
            var response = await client.GetAsync($"/api/Forward/{param}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WebArticleSlugModel>(content);

            Assert.Equal("test-article", result.Slug);
        }

    }
}