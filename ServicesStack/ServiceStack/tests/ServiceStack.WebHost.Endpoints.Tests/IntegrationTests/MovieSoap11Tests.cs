﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace ServiceStack.WebHost.Endpoints.Tests.IntegrationTests
{
    [TestFixture]
    public class MovieSoap11Tests : IntegrationTestBase
    {
        private Soap11ServiceClient soapClient;

        [SetUp]
        public void OnBeforeEachTest(){
            soapClient = new Soap11ServiceClient(BaseUrl);
            soapClient.Send<ResetMoviesResponse>(new ResetMovies());
        }

        [Test]
        public void Can_list_all_movies(){
            var response = soapClient.Send<RestMoviesResponse>(new RestMovies());
            Assert.That(response.Movies, Has.Count.EqualTo(ConfigureDatabase.Top5Movies.Count));
        }

        [Test]
        public void Can_ResetMovieDatabase()
        {
            var response = soapClient.Send<ResetMoviesResponse>(new ResetMovies());
            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
        }
    }
}
