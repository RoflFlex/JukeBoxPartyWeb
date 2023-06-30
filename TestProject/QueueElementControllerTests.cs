using JukeBoxPartyAPI.Controllers;
using JukeBoxPartyAPI.Data;
using JukeBoxPartyAPI.Models;
using JukeBoxPartyWeb.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class QueueElementControllerTests
    {
        public class QueueELementControllerTests
        {
            private readonly Mock<IQueueElementsRepository> _queueElementRepositoryMock;
            private readonly QueueElementsController _controller;


            public QueueELementControllerTests()
            {
                _queueElementRepositoryMock = new Mock<IQueueElementsRepository>();
                _controller = new QueueElementsController(_queueElementRepositoryMock.Object);
            }

            [Fact]
            public async Task GetQueueElement_ShouldReturnNotFound_WhenElementDoesntExistAsync()
            {
                //Arrange
                _queueElementRepositoryMock.Setup(x => x.GetElementById(It.IsAny<int>())).ReturnsAsync(() => null);
                //Act
                var result = await _controller.GetQueueElement(It.IsAny<int>());

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
            [Fact]
            public async Task GetQueueElement_ShouldReturnQueueElement_WhenElementExistsAsync()
            {
                //Arrange
                int id = 1;
                QueueElement queueElement = new QueueElement()
                {
                    Id = id,
                    AddedAt = new DateTime(),
                    UserId = Guid.NewGuid(),
                    PlayedAt = new DateTime(),
                    LobbyId = Guid.NewGuid(),
                    SongId = 1
                };
                _queueElementRepositoryMock.Setup(x => x.GetElementById(id)).ReturnsAsync(queueElement);
                //Act
                var result = await _controller.GetQueueElement(id);

                // Assert
                Assert.Equal<int>(result.Value!.Id, id);
            }
            [Fact]
            public async Task GetQueueElement_ShouldReturnNotFoundResult_WhenCollectionIsEmpty()
            {
                //Arrange
                _queueElementRepositoryMock.Setup(x => x.GetElementById(It.IsAny<int>())).Throws<InvalidOperationException>();
                //Act
                var result = await _controller.GetQueueElement(It.IsAny<int>());

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
            [Fact]
            public async Task Play_ShouldReturnOk_WhenPlayingIsSucceded()
            {
                //Arrange
                int id = 1213;
                _queueElementRepositoryMock.Setup(x => x.PlayQueueElement(id)).ReturnsAsync(true);
                //Act
                var result = await _controller.Play(id);

                // Assert
                Assert.IsType<OkResult>(result);
            }
            [Fact]
            public async Task Play_ShouldReturnBadRequest_WhenPlayingIsNotSucceded()
            {
                //Arrange
                int id = 1213;
                _queueElementRepositoryMock.Setup(x => x.PlayQueueElement(id)).ReturnsAsync(false);
                //Act
                var result = await _controller.Play(id);

                // Assert
                Assert.IsType<BadRequestResult>(result);
            }
            [Fact]
            public async Task PostQueueElement_ShouldReturnCreatedAtAction_WhenCreationIsSucceded()
            {
                //Arrange
                int id = 1;
                QueueElement queueElement = new QueueElement()
                {
                    Id = id,
                    AddedAt = new DateTime(),
                    UserId = Guid.NewGuid(),
                    PlayedAt = new DateTime(),
                    LobbyId = Guid.NewGuid(),
                    SongId = 1
                };
                PostQueueElement postQueueElement = new PostQueueElement()
                {
                    LobbyId = queueElement.LobbyId,
                    SongId = queueElement.SongId,
                    UserId = queueElement.UserId,
                };
                _queueElementRepositoryMock.Setup(x => x.CreateQueueElement(postQueueElement)).ReturnsAsync(queueElement);
                //Act
                var result = await _controller.PostQueueElement(postQueueElement);
                // Assert
                
                Assert.IsType<ActionResult<QueueElement>>(result);
                CreatedAtActionResult res = (CreatedAtActionResult)result.Result;
                QueueElement actualQueueElement = (QueueElement)res.Value;
                Assert.Equal(actualQueueElement.Id, queueElement.Id);
            }
            [Fact]
            public async Task PostQueueElement_ShouldReturnBadRequestWithDetails_WhenCreationisTooEarly()
            {
                //Arrange
                int id = 1;
                QueueElement queueElement = new QueueElement()
                {
                    Id = id,
                    AddedAt = new DateTime(),
                    UserId = Guid.NewGuid(),
                    PlayedAt = new DateTime(),
                    LobbyId = Guid.NewGuid(),
                    SongId = 1
                };
                var problemDetails = new ProblemDetails
                {
                    Status = 400,
                    Title = "Invalid request",
                    Detail = "20.0",
                };
                PostQueueElement postQueueElement = new PostQueueElement()
                {
                    LobbyId = queueElement.LobbyId,
                    SongId = queueElement.SongId,
                    UserId = queueElement.UserId,
                };
                _queueElementRepositoryMock.Setup(x => x.CreateQueueElement(postQueueElement)).ThrowsAsync(
                    new TimeoutException("The data is invalid.") { Data = { ["ProblemDetails"] = problemDetails } });
                //Act
                var result = await _controller.PostQueueElement(postQueueElement);
                BadRequestObjectResult res = (BadRequestObjectResult)result.Result;
                ProblemDetails problemDetailsActual = (ProblemDetails)res.Value;
                // Assert
                Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Equal(problemDetails.Detail, problemDetailsActual.Detail);
            }
            [Fact]
            public async Task PostQueueElement_ShouldReturnBadRequest_WhenGivenSongOrLobbyDoesntExist()
            {
                //Arrange
                int id = 1;
                QueueElement queueElement = new QueueElement()
                {
                    Id = id,
                    AddedAt = new DateTime(),
                    UserId = Guid.NewGuid(),
                    PlayedAt = new DateTime(),
                    LobbyId = Guid.NewGuid(),
                    SongId = 1
                };
                PostQueueElement postQueueElement = new PostQueueElement()
                {
                    LobbyId = queueElement.LobbyId,
                    SongId = queueElement.SongId,
                    UserId = queueElement.UserId,
                };
                _queueElementRepositoryMock.Setup(x => x.CreateQueueElement(postQueueElement)).ThrowsAsync(
                    new NullReferenceException());
                //Act
                var result = await _controller.PostQueueElement(postQueueElement);
                // Assert
                Assert.IsType<BadRequestResult>(result.Result);
            }
        }



    }
}
