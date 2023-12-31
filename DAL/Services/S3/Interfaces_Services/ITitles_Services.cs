using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.S3.Interfaces_Services
{
    public interface ITitles_Services
    {
        Task<BaseResponse<List<GroupTitle>>> GetTitles(string path);


    }
}
