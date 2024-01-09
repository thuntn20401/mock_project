using CloudinaryDotNet.Actions;
using InsternShip.Data.ViewModels.Question;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Authorize(Roles = "Admin, Interviewer, Recruiter")]

    public class QuestionController : BaseAPIController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions(string? query, Guid? questionId)
        {
            if (questionId != null)
            {
                var questionList = await _questionService.GetAllQuestions(null, (Guid)questionId);
                if (questionList == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(questionList);
            }

            var listQuestion = await _questionService.GetAllQuestions(query, null);
            if (listQuestion == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(listQuestion);
        }

        [HttpGet("[action]/Language")]
        public async Task<IActionResult> GetAllLanguageQuestions()
        {
            var listQuestion = await _questionService.GetAllLanguageQuestions();
            return Ok(listQuestion);
        }

        [HttpGet("[action]/SoftSkill")]
        public async Task<IActionResult> GetAllSoftSkillQuestions()
        {
            var listQuestion = await _questionService.GetAllSoftSkillQuestions();
            return Ok(listQuestion);
        }

        [HttpGet("[action]/Technology")]
        public async Task<IActionResult> GetAllTechnologyQuestions()
        {
            var listQuestion = await _questionService.GetAllTechnologyQuestions();
            return Ok(listQuestion);
        }

        [HttpGet("[action]/{questionId:guid}")]
        public async Task<IActionResult> GetQuestion(Guid questionId)
        {
            var response = await _questionService.GetQuestion(questionId);
            return response is not null ? Ok(response) : NotFound(questionId);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionAddModel question)
        {
            var response = await _questionService.AddQuestion(question);
            return response is not null ? CreatedAtAction(nameof(AddQuestion), response)
                                    : BadRequest(question);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpPut("{questionId:guid}")]
        public async Task<IActionResult> UpdateQuestion
        (QuestionUpdateModel question, Guid questionId)
        {
            var response = await _questionService.UpdateQuestion(question, questionId);
            return response is true ? NoContent() : NotFound(questionId);
        }

        [HttpDelete("{questionId:guid}")]
        public async Task<IActionResult> RemoveQuestion(Guid questionId)
        {
            var response = await _questionService.RemoveQuestion(questionId);
            return response is true ? NoContent() : NotFound(questionId);
        }
    }
}