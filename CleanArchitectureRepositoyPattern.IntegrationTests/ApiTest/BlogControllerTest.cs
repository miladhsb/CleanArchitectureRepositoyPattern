using CleanArchitectureRepositoyPattern.IntegrationTests.Configurations;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureRepositoyPattern.IntegrationTests.ApiTest
{
    public class BlogControllerTest:IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public BlogControllerTest(CustomWebApplicationFactory factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async void CreatePostStatusCodeTest()
        {
          var Client=  _factory.CreateDefaultClient();
          var Content=  JsonContent.Create(new RequestBlugPostDTO() {Body="12346",MetaDescription="123456",Title="123456",Tags="123", MetaKeywords="ff,hh"});
          var PostResult=await  Client.PostAsync("Blog/CreatePost", Content);
          PostResult.EnsureSuccessStatusCode();
        }
        [Fact]
        public async void CreatePostBadrequestTest()
        {
            var Client = _factory.CreateDefaultClient();
            var Content = JsonContent.Create(new RequestBlugPostDTO() { Body = "", MetaDescription = "123456", Title = "123456", Tags = "123", MetaKeywords = "ff,hh" });
            var PostResult = await Client.PostAsync("Blog/CreatePost", Content);
            Assert.Equal(PostResult.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
    }
}
