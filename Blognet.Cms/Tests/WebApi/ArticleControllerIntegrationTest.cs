using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Domain.Entities;
using Blognet.Cms.Domain.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]
namespace Blognet.Cms.Tests.WebApiTests
{
    // @TODO Seed data for test, use InMemoryDatabase
    public class ArticleControllerIntegrationTest : IClassFixture<WebApplicationFactory<Cms.WebApi.Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Cms.WebApi.Startup> _factory;

        public ArticleControllerIntegrationTest(WebApplicationFactory<Cms.WebApi.Startup> factory)
        {
            //AutoMapper.Mapper.Reset();
            _factory = factory;
        }

        public void Dispose()
        {
            //AutoMapper.Mapper.Reset();
        }

        /// <summary>
        /// Test category articles result count. Expects seeded test data.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_EndpointReturnCategoryArticles()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("projectDomain", "localhost");
            var response = await client.GetAsync("/api/Article/Category/programming/2/0/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var categoryArticleResult = JsonConvert.DeserializeObject<WebCategoryArticlesModel>(content);

            // Test result count
            Assert.True(categoryArticleResult.Articles.Count == 2);         
        }

        /// <summary>
        /// Test get article by slug. Test its content.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_EndpointReturnArticleBySlug()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("projectDomain", "localhost");
            var response = await client.GetAsync("/api/Article/test-article/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var articleResult = JsonConvert.DeserializeObject<WebArticleModel>(content);

            Assert.NotNull(articleResult.Article);
            Assert.Equal("Programming", articleResult.Article.Category.Title);
            Assert.Equal(@"<p>test</p>", articleResult.Article.Content);
            Assert.Equal("test-article", articleResult.Article.Slug);
            Assert.Equal("test-article", articleResult.Article.Slug);
            Assert.Equal("Test article", articleResult.Article.Title);
            Assert.Equal("test", articleResult.Article.Seo.Title);
            Assert.Equal("test", articleResult.Article.Seo.Description);
            Assert.Equal("test,test", articleResult.Article.Seo.Keywords);
            Assert.False(articleResult.Article.ArticleSettings.Nsfw);
            Assert.Equal(PageAdsDensity.High, articleResult.Article.ArticleSettings.PageAdsDensity);
            Assert.True(articleResult.Article.ArticleSettings.AdsActive);
            Assert.True(articleResult.Article.ArticleSettings.ShowComments);
            Assert.True(articleResult.Article.ArticleSettings.ShowSocialPlugins);
            Assert.False(articleResult.Article.ArticleSettings.UpdateSlugOnTitleChange);
            // @TODO test for correct string or write test for Photo...
            Assert.NotEmpty(articleResult.Article.PhotoHeader.ImagePath);
            Assert.NotEmpty(articleResult.Article.PhotoThumbnail.ImagePath);
        }

        /// <summary>
        /// Test endpoint, return article slug by Id.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_EndpointReturnArticleSlugById()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("projectDomain", "localhost");
            var response = await client.GetAsync("/api/Article/Slug/1501/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WebArticleSlugModel>(content);

            Assert.Equal("test-article", result.Slug);
        }
    }
}