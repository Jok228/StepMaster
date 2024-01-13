using API.Auth.AuthCookie;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using Infrastructure.MongoDb.Cache.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Auth.ResponseLogic;
using StepMaster.Models.API.Title;
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
        private readonly string _cacheKey = "Titles";
        public TitleController(ITitles_Services services, IMy_Cache cache, ICondition_Repository condition_repository)
        {
            _titles_services = services;
            _cache = cache;
            _condition_repository = condition_repository;
        }

        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetTitles")]
        public async Task<ResponseList<ConditionResponse>>GetTitles()
        {
            var email = User.Identity.Name;
            var conditions = (List<Condition>)_cache.GetObject(_cacheKey);
            if (conditions == null)
            {
                conditions = await _condition_repository.GetConditionsAsync();
                conditions = await _titles_services.GetLinksTitles(conditions);
                _cache.SetObject(_cacheKey, conditions, 280);
            }
                conditions = await _titles_services.GetActualAllProgress(email, conditions);
                conditions = conditions.OrderByDescending(con => con.Type)
                .ThenBy(con => con.GroupId)
                .ThenBy(con => con.IdLocal).ToList();
              var response = new ResponseList<ConditionResponse>(ConditionResponse.Convert(conditions));
            return response;

        }
    }
}
