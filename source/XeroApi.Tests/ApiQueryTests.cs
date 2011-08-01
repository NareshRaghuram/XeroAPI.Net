﻿using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using XeroApi.Model;
using XeroApi.Tests.Stubs;

namespace XeroApi.Tests
{
    [TestFixture]
    public class ApiQueryTests
    {

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithNoArguments()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            List<Organisation> organisations = repository.Organisations.ToList();

            Assert.AreEqual(0, organisations.Count);
            Assert.AreEqual("Organisation", integrationProxy.LastQueryDescription.ElementName);
        }


        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithOneWhereArgument()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            List<Organisation> organisations = repository.Organisations.Where(o => o.Name == "Demo Company (NZ)").ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("(Name == \"Demo Company (NZ)\")", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
            Assert.AreEqual(0, organisations.Count);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithTwoWhereArguments()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            repository.Organisations
                .Where(o => o.Name == "Demo Company")
                .Where(o => o.APIKey == "ABCDEFG")
                .ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("(Name == \"Demo Company\") AND (APIKey == \"ABCDEFG\")", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithFirstMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            Assert.Throws<InvalidOperationException>(() => repository.Organisations.First());

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithFirstMethodWithPredicate()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            Assert.Throws<InvalidOperationException>(() => repository.Organisations.First(o => o.Name == "Demo Company"));

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("(Name == \"Demo Company\")", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithFirstOrDefaultMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            Organisation organisation = repository.Organisations.FirstOrDefault();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
            Assert.IsNull(organisation);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithFirstOrDefaultMethodWithPredicate()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            Organisation organisation = repository.Organisations.FirstOrDefault(o => o.Name == "Demo Company");

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("(Name == \"Demo Company\")", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
            Assert.IsNull(organisation);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithCountMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            int organisationCount = repository.Organisations.Count();
            
            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("", queryDesctipion.Order);
            Assert.AreEqual(0, organisationCount);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithOneOrderByMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            repository.Organisations.OrderBy(organisation => organisation.CreatedDateUTC).ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("CreatedDateUTC", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithTwoOrderByMethods()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            repository.Organisations
                .OrderBy(organisation => organisation.CreatedDateUTC)
                .OrderBy(organisation => organisation.APIKey)
                .ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("CreatedDateUTC, APIKey", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithOrderByDescMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            repository.Organisations.OrderByDescending(organisation => organisation.CreatedDateUTC).ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("", queryDesctipion.Where);
            Assert.AreEqual("CreatedDateUTC DESC", queryDesctipion.Order);
        }

        [Test]
        public void TestApiQueryCanCallOrganisationsEndpointWithOrderByAndWhereMethod()
        {
            StubIntegrationProxy integrationProxy = new StubIntegrationProxy();
            Repository repository = new Repository(integrationProxy);

            repository.Organisations
                .Where(organisation => organisation.Name == "Demo Company")
                .OrderBy(organisation => organisation.CreatedDateUTC)
                .ToList();

            var queryDesctipion = integrationProxy.LastQueryDescription;
            Assert.AreEqual("Organisation", queryDesctipion.ElementType.Name);
            Assert.AreEqual("(Name == \"Demo Company\")", queryDesctipion.Where);
            Assert.AreEqual("CreatedDateUTC", queryDesctipion.Order);
        }
    }
}
