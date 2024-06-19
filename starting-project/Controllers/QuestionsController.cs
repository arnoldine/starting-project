using Microsoft.AspNetCore.Mvc;
using starting_project.Models;
using starting_project.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starting_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public QuestionsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest("Question data is null");
            }

            if (string.IsNullOrEmpty(questionDto.Id))
            {
                questionDto.Id = Guid.NewGuid().ToString();
            }

            await _cosmosDbService.AddItemAsync(questionDto);
            return Ok(questionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDto questionDto)
        {
            if (questionDto == null || id != questionDto.Id)
            {
                return BadRequest("Question data is null or ID mismatch");
            }

            await _cosmosDbService.UpdateItemAsync(id, questionDto);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var query = "SELECT * FROM c";
            var questions = await _cosmosDbService.GetItemsAsync<QuestionDto>(query);
            return Ok(questions);
        }

        //[HttpPost("submit")]
        //public async Task<IActionResult> SubmitApplication([FromBody] List<AnswerDto> answers)
        //{
        //    if (answers == null)
        //    {
        //        return BadRequest("Application data is null");
        //    }

        //    foreach (var answer in answers)
        //    {
        //        if (string.IsNullOrEmpty(answer.QuestionId))
        //        {
        //            return BadRequest("Each answer must have a valid QuestionId.");
        //        }
        //    }

        //    await _cosmosDbService.AddItemAsync(answers);
        //    return Ok(answers);
        //}
    }

    public class AnswerDto
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
    }
}
