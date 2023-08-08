using System.Net;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Helper;

namespace SearchAndMatch.Application.Tests.Services
{
    public class CreateSearchEngineServiceTest
    {
        [Theory, AutoMoq]
        public async Task EngineMatching1Async_ShouldReturnOk(
           [Frozen] Mock<HttpMessageHandler> httpMessageHandlerMock,
           [Frozen] Mock<ILogger<CreateSearchEngineService>> loggerMock,
           IFixture fixture,
           EngineRequest1 engineRequest,
           Engine engine,
           EndpointResponse expectedResponseContent
       )
        {
            // Arrange
            engine.Endpoint = "http://localhost/";
            var expectedUrl = new Uri(engine.Endpoint);

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponseContent))
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = expectedUrl
            };

            fixture.Inject(httpClient);
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient("Client")).Returns(httpClient);

            var createSearchEngineService = new CreateSearchEngineService(mockHttpClientFactory.Object, loggerMock.Object);

            // Act
            var result = await createSearchEngineService.EngineMatching1Async(engineRequest, engine);

            // Assert
            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post && req.RequestUri == expectedUrl),
                ItExpr.IsAny<CancellationToken>());
            result.Should().BeEquivalentTo(expectedResponseContent);
        }

        [Theory, AutoMoq]
        public async Task EngineMatching2Async_ShouldReturnOk(
           [Frozen] Mock<HttpMessageHandler> httpMessageHandlerMock,
           [Frozen] Mock<ILogger<CreateSearchEngineService>> loggerMock,
           IFixture fixture,
           EngineRequest2 engineRequest,
           Engine engine,
           EndpointResponse expectedResponseContent
       )
        {
            // Arrange
            engine.Endpoint = "http://localhost/";
            var expectedUrl = new Uri(engine.Endpoint);

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponseContent))
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = expectedUrl
            };

            fixture.Inject(httpClient);
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient("Client")).Returns(httpClient);

            var createSearchEngineService = new CreateSearchEngineService(mockHttpClientFactory.Object, loggerMock.Object);

            // Act
            var result = await createSearchEngineService.EngineMatching2Async(engineRequest, engine);

            // Assert
            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post && req.RequestUri == expectedUrl),
                ItExpr.IsAny<CancellationToken>());
            result.Should().BeEquivalentTo(expectedResponseContent);
        }
    }
}
