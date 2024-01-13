using Amazon.S3.Model;
using Domain.Entity.API;
using Domain.Entity.Main.Titles;
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
        public Task<string> GetUserAvatarLink(string userName);

        public Task<string> GetLink(string path);

        public Task<IEnumerable<string>> GetFolders(string path);

        public Task<bool> InsertFile(string userName, IFormFile file);

        public Task<ListObjectsResponse> GetListFiles(string path);
    }
}
