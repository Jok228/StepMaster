using API.Auth.AuthCookie;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using AutoMapper;
using Domain.Entity.Main.Message;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.API.File;
using StepMaster.Models.API.Mapper;

namespace StepMaster.Controllers.api
    {
    [Route ("api/[controller]")]
    [ApiController]
    public class FileController : Controller
        {
        private readonly IMessageFile_Service _messageFileService;
        private readonly IAws_Repository _awsRepository;

        public FileController(IMessageFile_Service messageFileService,IAws_Repository awsRepository = null)
            {
            _messageFileService = messageFileService;
            _awsRepository = awsRepository;
            }

        [HttpPost]
        [CustomAuthorizeUser ("all")]
        [RequestSizeLimit (50*1024*1024)]
        [Route ("AddFile")]
        public async Task<FileView> AddFile([FromForm] IFormFile file)
            {
            var messageFile = new MessageFile (file);
            var response = await _messageFileService.SetNewFile (messageFile);
            await _awsRepository.SaveFile (file,response.Path);
            FileView result = MyMapper.MapFileView (messageFile);
            result.Link = await _awsRepository.GetLink (response.Path);
            return result;
            }        

        [HttpGet]
        [CustomAuthorizeUser ("all")]
        [Route ("GetFile")]
        public async Task<FileView> GetFile([FromQuery]string id)
            {

            var fileDb = await _messageFileService.GetFile (id);

            

            var link = await _awsRepository.GetLink (fileDb.Path);

            FileView result = MyMapper.MapFileView (fileDb);
            result.Link = link;

            return result;

            }
        }
    }
