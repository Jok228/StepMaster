using Domain.Entity.API;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.S3.Interfaces
{
    public interface IAws_Repository
    {
        public Task<string> GetLink(string userName);

        public Task<bool> InsertFile(string userName, IFormFile file);
    }
}
