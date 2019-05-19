using Blognet.Cms.Core.MenuItems.Models;
using Blognet.Cms.Core.Model;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blognet.Cms.Tests.WebApiTests
{
    // @TODO Seed data for test, use InMemoryDatabase
    public class MenuControllerIntegrationTest : IClassFixture<WebApplicationFactory<Cms.WebApi.Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Cms.WebApi.Startup> _factory;

        public MenuControllerIntegrationTest(WebApplicationFactory<Cms.WebApi.Startup> factory)
        {
            AutoMapper.Mapper.Reset();
            _factory = factory;
        }

        public void Dispose()
        {
            AutoMapper.Mapper.Reset();
        }

        /// <summary>
        /// Test endpoint for getting menu items.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_EndpointReturnMenu()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("projectDomain", "localhost");
            var response = await client.GetAsync("/api/Menu/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WebMenuModel>(content);

            // Test object type
            Assert.IsAssignableFrom<List<MenuItemDTO>>(result.MenuItems);

            // Test selected parent
            var items = (List<MenuItemDTO>)result.MenuItems;
            Assert.NotEmpty(items);
            Assert.Equal("#", items[0].Url);
            Assert.Equal("Test parent", items[0].Title);

            // Test 
            var children = (List<MenuItemDTO>)items[0].MenuItems;
            Assert.NotEmpty(children);
            Assert.Equal("xUnit", children[0].Title);
            Assert.Equal(@"/x-unit/", children[0].Url);
        }
    }
}