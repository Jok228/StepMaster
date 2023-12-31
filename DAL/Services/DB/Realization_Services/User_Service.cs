using MongoDB.Driver;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.API;
using Application.Repositories.S3.Interfaces;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.Entity.Realization_Services;

namespace StepMaster.Services.ForDb.Repositories
{
    public class User_Service : IUser_Service
    {
       
        private readonly IUser_Repository _usersRepository;

        public User_Service(IUser_Repository users, IRating_Repository rating, IAws_Repository aws, IDay_Repository day, IRating_Service ratingervice)
        {
            _usersRepository = users;
        }
        public async Task<BaseResponse<User>> GetByLoginAsync(string email)
        {
            return await _usersRepository.GetObjectBy(email);
        }
        public async Task<BaseResponse<User>> RegUserAsync(User newUser)
        {            
                await _usersRepository.SetObject(newUser); // inser newUsers
                return BaseResponse<User>.Create(newUser, MyStatus.Success);            
        }
        public async Task<BaseResponse<User>> UpdateUser(User userUpdate)
        {
            return await _usersRepository.UpdateObject(userUpdate);
        }
        public async Task<BaseResponse<User>> GetUserbyCookie(string cookies)
        {
            return await _usersRepository.GetByCookie(cookies);
        }
        public async Task<BaseResponse<bool>> DeleteCookie(string userEmail)
        {
            var user = await _usersRepository.GetObjectBy(userEmail);
            if (user.Data != null)
            {
                user.Data.lastCookie = null;
                return BaseResponse<bool>.Create(true, MyStatus.Success);
            }
            else
            {
                return BaseResponse<bool>.Create(false, user.Status);
            }


        }
        public async Task<BaseResponse<User>> EditPassword(string email, string newPassword, string oldPassword)
        {
            var user = await _usersRepository.GetObjectBy(email);
            if (user.Data != null)
            {
                if (HashCoder.Verify(user.Data.password, oldPassword))
                {
                    user.Data.password = HashCoder.GetHash(newPassword);
                    return await _usersRepository.UpdateObject(user.Data);
                }
                else
                {
                    return BaseResponse<User>.Create(null, MyStatus.Iteapot);
                }
            }
            else
            {
                return BaseResponse<User>.Create(null, MyStatus.NotFound);
            }

        }

        public async Task<BaseResponse<string>> DeleteUser(string email)
        {
            return  await _usersRepository.DeleteUser(email)? BaseResponse<string>.Create(email, MyStatus.Success): BaseResponse<string>.Create(email, MyStatus.NotFound);            
            
        }
    }
}
