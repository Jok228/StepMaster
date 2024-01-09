using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using StepMaster.Models.API.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface ITitles_Services
    {
        Task<List<GroupTitle>> GetTitles(string path);

        Task UpdateTitlesList(string email);

        Task<BaseResponse<List<TitleDb>>> UpdateSelectUserTitles(string email, TitleDb newTitle);

        Task<TitleProgress> GetActualProgress(string email);

    }
}
