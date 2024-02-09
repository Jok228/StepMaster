using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;

using Domain.Entity.Main.Titles;
using static Domain.Entity.Main.Titles.Condition;
using static StepMaster.Models.Entity.User;

namespace Application.Services.Entity.Realization_Services
{
    public class Titles_Service : ITitles_Services
    {
        private readonly IAws_Repository _aws_repository;
        private readonly ICondition_Repository _conditionRepository;
        private readonly IDay_Repository _days_repository;
        private readonly IUser_Repository _userRepository;
        
        private readonly int _maxStepGrades = 30000;
        public Titles_Service(IAws_Repository aws_repository, IDay_Repository days_repository, IUser_Repository user_repository, ICondition_Repository condition_repository)
        {
            _aws_repository = aws_repository;
            _days_repository = days_repository;
            _userRepository = user_repository;
            _conditionRepository = condition_repository;
        }

        public async Task<List<Condition>> GetLinksTitles(List<Condition> listPath)
        {
            
           foreach (var title in listPath)
            {
                string url = await _aws_repository.GetLink(title.AwsPath);
                title.awsLink = url;
            }
            return listPath;
        }


        public async Task UpdateTitlesList(string email)
        {
            var conditions = await _conditionRepository.GetConditionsAsync();
            foreach (var ach in conditions.Where(condition => condition.Type == "achievement").ToList())
            {
                if (ach.GroupId == 2)
                {
                    var days = await _days_repository.GetRangDay(email, (int)ach.TimeDay);
                    int i = 0;
                    foreach (var day in days)
                    {
                        if(day.steps >= day.plansteps)
                        {
                            i++;
                        }
                    }
                    if(i >= ach.TimeDay)
                    {
                        await UpdateTitleUser(email, ach);
                    }
                    continue;
                }
                var steps = await _days_repository.GetAllStepsUser(email, ach.TimeDay);
                if (steps >= ach.Distance)
                {
                    await UpdateTitleUser(email, ach);
                }

            }
            foreach (var gra in conditions.Where(condition => condition.Type == "grade").ToList())
            {
                var steps = await _days_repository.GetStepRangeForGrades(email);
                steps += await _days_repository.GetCountDayMoreMax(email) * _maxStepGrades;                
                if (steps >= gra.Distance)
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
            user.UpdateTitles(ach);
            await _userRepository.UpdateObject(user);
        }
        public async Task<List<string>> UpdateSelectUserTitles(string email, string conditionMongoId)
        {
            var user = await _userRepository.GetObjectBy(email);
            user.UpdateSelectedTitles(conditionMongoId);
            var response = user.SelectedTitles;
            await _userRepository.UpdateObject(user);
            return response;
        }
        public async Task<List<Condition>> GetActualAllProgress(string email, List<Condition> list)
        {
            foreach (var item in list)
            {
                if(item.Type == "grade"& item.GroupId == 5 | item.GroupId == 6 | item.GroupId ==7 | item.GroupId == 8)
                {
                    item.Dealt_Progress = null;
                    item.Needed_Progress = null;
                    
                }
                else if (item.Type == "grade")
                {
                    item.Needed_Progress = item.Distance.ToString();
                    item.Dealt_Progress = (_days_repository.GetStepRangeForGrades(email).Result
                        + _days_repository.GetCountDayMoreMax(email).Result * _maxStepGrades).ToString();
                    
                }
                else if(item.Type == "achievement" & item.GroupId == 1 | item.GroupId == 3 | item.GroupId == 4)
                {
                    item.Needed_Progress = item.Distance.ToString();
                    item.Dealt_Progress = _days_repository.GetAllStepsUser(email, item.TimeDay).Result.ToString();
                    
                }
                else if (item.Type == "achievement" & item.GroupId == 2)
                {
                    item.Needed_Progress = item.TimeDay.ToString();
                    var days = await _days_repository.GetRangDay(email, (int)item.TimeDay);
                    int i = 0;
                    foreach (var day in days)
                    {
                        if (day.steps >= day.plansteps)
                        {
                            i++;
                        }
                                             
                    }
                    item.Dealt_Progress = i.ToString();
                   
                }

            }
            foreach(var item in list)
            {
                if(item.Dealt_Progress != null && item.Needed_Progress != null)
                {
                    if(int.Parse(item.Dealt_Progress) > int.Parse(item.Needed_Progress))
                    {
                        item.Dealt_Progress = item.Needed_Progress;
                    }
                }
            }
            return list;
        }
    }
}
