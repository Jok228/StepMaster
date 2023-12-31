using Application.Repositories.S3.Interfaces;
using Application.Services.S3.Interfaces_Services;
using Domain.Entity.API;
using Domain.Entity.Main.Titles;

namespace Application.Services.S3.Realization_Services
{
    public class Titles_Service:ITitles_Services
    {
        private readonly IAws_Repository _aws_repository;
        
        public Titles_Service(IAws_Repository aws_repository)
        {
            _aws_repository = aws_repository;
        }

        public async Task<BaseResponse<List<GroupTitle>>> GetTitles(string path)
        {
            try
            {
                var listGroup = new List<GroupTitle>();
                var folders = await _aws_repository.GetFolders(path);
                foreach (var folder in folders)
                {
                    var listFiles = await _aws_repository.GetListFiles(folder);
                    var files = listFiles.S3Objects.Select(x => x.Key);
                    var folderName = GetFileName(folder);
                    var folderId = GetFileId(folder,true);
                    listGroup.Add(new GroupTitle(folderName, folderId));
                    foreach (var file in files)
                    {
                        listGroup.Find(list => list.name == folderName).data.Add(new Title
                        {

                            name = Path.GetFileNameWithoutExtension(GetFileName(file)),
                            link = await _aws_repository.GetLink(file),
                            id = GetFileId(file,false)

                        });
                    }
                    listGroup.Find(list => list.name == folderName).SortById();

                }
               
                return BaseResponse<List<GroupTitle>>.Create(listGroup, MyStatus.Success);
            }
            catch (Exception ex)
            {
                return BaseResponse<List<GroupTitle>>.Create(null, MyStatus.Except);
            }
            
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

    }
}
