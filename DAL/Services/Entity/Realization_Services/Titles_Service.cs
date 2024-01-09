using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using StepMaster.Models.API.Title;
using StepMaster.Services.ForDb.Interfaces;

namespace Application.Services.Entity.Realization_Services
{
    public class Titles_Service : ITitles_Services
    {
        private readonly IAws_Repository _aws_repository;
        private readonly ICondition_Repository _conditionRepository;
        private readonly IDay_Repository _days_repository;
        private readonly IUser_Repository _userRepository;
        
        private readonly int maxStepGrades = 30000;
        public Titles_Service(IAws_Repository aws_repository, IDay_Repository days_repository, IUser_Repository user_repository, ICondition_Repository condition_repository)
        {
            _aws_repository = aws_repository;
            _days_repository = days_repository;
            _userRepository = user_repository;
            _conditionRepository = condition_repository;
        }

        public async Task<List<GroupTitle>> GetAllTitles(List<string> list)
        {
           

        }
        
        private string GetFileName(string path)
        {
            return path.TrimEnd('/').Split('/').Last().Split('@').Last();
        }
        private int GetFileId(string path, bool folder)
        {
            if (folder)
            {
                return int.Parse(path.Split('@').First().Split('/').Last());
            }
            else
            {
                return int.Parse(path.Split('/').Last().Split('@').First().Split('/').Last());
            }


        }

        public async Task UpdateTitlesList(string email)
        {
            var conditions = await _conditionRepository.GetConditionsAsync();
            foreach (var ach in conditions.Where(condition => condition.type == "achievement").ToList())
            {
                if (ach.groupId == 2)
                {
                    var days = await _days_repository.GetRangDay(email, (int)ach.timeDay);
                    int i = 0;
                    foreach (var day in days)
                    {
                        if(day.steps >= day.plansteps)
                        {
                            i++;
                        }
                    }
                    if(i >= ach.timeDay)
                    {
                        await UpdateTitleUser(email, ach);
                    }
                    continue;
                }
                var steps = await _days_repository.GetStepRangeForAchievements(email, ach.timeDay);
                if (steps >= ach.distance)
                {
                    await UpdateTitleUser(email, ach);
                }

            }
            foreach (var gra in conditions.Where(condition => condition.type == "grade").ToList())
            {
                var steps = await _days_repository.GetStepRangeForGrades(email, gra.timeDay);
                steps += await _days_repository.GetCountDayMoreMax(email, gra.timeDay) * maxStepGrades;                
                if (steps >= gra.distance)
                {
                    await UpdateTitleUser(email, gra);
                }
                else
                {
                    break;
                }
            }


        }
        private async Task UpdateTitleUser(string email, Condition ach)
        {
            var user = await _userRepository.GetObjectBy(email);
            user.Data.UpdateTitles(new TitleDb
            {
                type = ach.type,
                id = ach.localId,
                groupId = ach.groupId,
                name = ach.name,
            });
            await _userRepository.UpdateObject(user.Data);
        }
        public async Task<BaseResponse<List<TitleDb>>> UpdateSelectUserTitles(string email, TitleDb newTitle)
        {
            var user = await _userRepository.GetObjectBy(email);
            user.Data.UpdateSelectedTitles(newTitle);
            var selectedList = user.Data.selectedTitles;
            await _userRepository.UpdateObject(user.Data);
            return BaseResponse<List<TitleDb>>.Create(selectedList, MyStatus.Success);
        }

        public async Task<TitleProgress> GetActualProgress(string email)
        {
            var user = await _userRepository.GetObjectBy(email);
            var conditions = await _conditionRepository.GetConditionsAsync();            
            var stepsUser = await _days_repository.GetStepRangeForAchievements(email, null);
            var lastAchievement = conditions.FirstOrDefault(condition => condition.distance >= stepsUser & condition.groupId == 3 & condition.type == "achievement");
            if(lastAchievement == null)
            {
                lastAchievement = conditions.Last(condition => condition.groupId == 3 & condition.type == "achievement");
                return new TitleProgress()
                {
                    title = new TitleDb { groupId = lastAchievement.groupId, name = lastAchievement.name, id = lastAchievement.localId, type = lastAchievement.type},
                    km_dealt = conditions.Find(condition => condition.localId == lastAchievement.localId & condition.groupId == lastAchievement.groupId).distance,
                    km_needed = conditions.Find(condition => condition.localId == lastAchievement.localId & condition.groupId == lastAchievement.groupId).distance
                };
            }
         
            return new TitleProgress()
            {
                title = new TitleDb { groupId = lastAchievement.groupId, name = lastAchievement.name, id = lastAchievement.localId, type = lastAchievement.type },
                km_dealt = stepsUser,
                km_needed = conditions.Find(condition => condition.localId == lastAchievement.localId & condition.groupId == lastAchievement.groupId).distance
            };
        }
    }
}
