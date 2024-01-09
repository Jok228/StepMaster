using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.API;
using Infrastructure.MongoDb.Cache.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Auth.ResponseLogic;
using StepMaster.Models.API.TitlesModel;

namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly ITitles_Services _titles_services;
        private readonly IMy_Cache _cache;
        private readonly ICondition_Repository _condition_repository;

        private readonly string _achievementPath = "StepMaster/Titles/Achievements";
        private readonly string _gradesPath = "StepMaster/Titles/Grades";
        private readonly string _cacheKey = "Titles";
        public TitleController(ITitles_Services services, IMy_Cache cache, ICondition_Repository condition_repository)
        {
            _titles_services = services;
            _cache = cache;
            _condition_repository = condition_repository;
        }

        [HttpGet]
        [Route("GetTitles")]
        public async Task<ResponseTitle> GetTitles()
        {
            var response = _cache.GetObject(_cacheKey);
            
            if (response == null)
            {
                var conditions = _condition_repository.GetConditionsAsync();
                var achievements = await _titles_services.GetTitles(_achievementPath);
                var grades = await _titles_services.GetTitles(_gradesPath);
                if (grades.Status == MyStatus.Success && achievements.Status == MyStatus.Success)
                {
                    response = new ResponseTitle()
                    {
                        achievements = achievements.Data,
                        grades = grades.Data,
                    };
                    _cache.SetObject(_cacheKey, response, 240);
                    return ResponseLogic<ResponseTitle>.Response(Response, MyStatus.Success, (ResponseTitle)response);
                }
                return ResponseLogic<ResponseTitle>.Response(Response, grades.Status, null);
            }
            else
            {
                return ResponseLogic<ResponseTitle>.Response(Response, MyStatus.Success, (ResponseTitle)response);
            }
        }
    }
}
