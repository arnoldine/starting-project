using starting_project.Models;
using starting_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using starting_project.Models;
using starting_project.Services;
using System.Threading.Tasks;

namespace starting_project.tests.Services
{
    public class CosmosDbServiceTests
    {
        private readonly Mock<ICosmosDbService> _mockCosmosDbService;

        public CosmosDbServiceTests()
        {
            _mockCosmosDbService = new Mock<ICosmosDbService>();
        }

        [Fact]
        public async Task AddItemAsync_ShouldAddItem()
        {
            // Arrange
            var question = new ParagraphQuestionDto
            {
                Id = "1",
                QuestionText = "Sample Question"
            };

            _mockCosmosDbService.Setup(service => service.AddItemAsync(question)).Returns(Task.CompletedTask);

            // Act
            await _mockCosmosDbService.Object.AddItemAsync(question);

            // Assert
            _mockCosmosDbService.Verify(service => service.AddItemAsync(question), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldUpdateItem()
        {
            // Arrange
            var question = new ParagraphQuestionDto
            {
                Id = "1",
                QuestionText = "Updated Question"
            };

            _mockCosmosDbService.Setup(service => service.UpdateItemAsync(question.Id, question)).Returns(Task.CompletedTask);

            // Act
            await _mockCosmosDbService.Object.UpdateItemAsync(question.Id, question);

            // Assert
            _mockCosmosDbService.Verify(service => service.UpdateItemAsync(question.Id, question), Times.Once);
        }

        [Fact]
        public async Task GetItemsAsync_ShouldReturnItems()
        {
            // Arrange
            var questions = new List<ParagraphQuestionDto>
        {
            new ParagraphQuestionDto { Id = "1", QuestionText = "Sample Question 1" },
            new ParagraphQuestionDto { Id = "2", QuestionText = "Sample Question 2" }
        };

            _mockCosmosDbService.Setup(service => service.GetItemsAsync<ParagraphQuestionDto>("SELECT * FROM c"))
                                .ReturnsAsync(questions);

            // Act
            var result = await _mockCosmosDbService.Object.GetItemsAsync<ParagraphQuestionDto>("SELECT * FROM c");

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
