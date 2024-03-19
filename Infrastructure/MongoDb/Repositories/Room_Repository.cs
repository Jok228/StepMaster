using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main.Message;
using Domain.Entity.Main.Room;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace Infrastructure.MongoDb.Repositories
    {
    public class Room_Repository : IRoom_Repository
        {
        private readonly IMongoCollection<Room> _roomDb;
        private IMy_Cache _cache;
        private ILogger<Room_Repository> _logger;
        private string _globalChatRoom = "65db00b4fdf0dd4420784265";

        public Room_Repository(IMy_Cache cache,IAPIDatabaseSettings settings,IMongoClient mongoClient,ILogger<Room_Repository> logger)
            {
            var database = mongoClient.GetDatabase (settings.DatabaseName);
            _roomDb = database.GetCollection<Room> (TableName.Rooms);
            _cache = cache;
            _logger = logger;
            }

        public async Task DeleteRoom(string id)
            {
            try
                {
                _cache.DeleteObject (id);
                await _roomDb.DeleteOneAsync(room => room._id == id);
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Room> GetGlobalRoom()
            {
            try
                {
                var room = await _roomDb.FindAsync (room => room._id == _globalChatRoom)
                .Result
                .FirstAsync();
                return room;
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    throw new HttpRequestException ("Global chat not found",null,HttpStatusCode.NotFound);
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Room> GetRoom(string id)
            {
            try
                {
                var room = (Room)_cache.GetObject (id);
                if (room == null)
                    {
                    room = await _roomDb.FindAsync (room => room._id == id)
                        .Result
                        .FirstAsync ();
                    return room;
                    }
                else
                    {
                    return room;
                    }
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    throw new HttpRequestException ("404 Not Found",null,HttpStatusCode.NotFound);
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<List<Room>> GetRoomsByEmail(string email, bool isPublic = false)
            {
            try
                {
                var room = await _roomDb.FindAsync (room => room.Users.Contains (email) &
                room.IsPublic == isPublic)
                .Result
                .ToListAsync();
                return room;
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    return new List<Room> ();
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Room> GetRoomTo(string emailOne,string emailTwo,bool isPublic = false)
            {
            try
                {
            var room = await _roomDb.FindAsync (room => room.Users.Contains (emailOne) &
            room.Users.Contains (emailTwo) &
            room.IsPublic == isPublic &
            room.Name == "NaDvoix")
            .Result
            .FirstAsync ();
                return room;
                }
            catch(Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    var newRoom = new Room ()
                        {
                        IsPublic = isPublic,
                        Users = new List<string> { emailOne,emailTwo },
                        Name = "NaDvoix"
                        };
                    return await SetRoom (newRoom);
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Room> SetRoom(Room newRoom)
            {
            try
                {
                await _roomDb.InsertOneAsync (newRoom);
                _cache.SetObject (newRoom._id,newRoom,10);
                return newRoom;
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Room> UpdateRoom(Room room)
        {
            try
                {
                var filter = Builders<Room>.Filter.Eq ("_id",room._id);
                await _roomDb.ReplaceOneAsync (filter,room);
                _cache.DeleteObject (room._id);
                _cache.SetObject (room._id,room,10);
                return room;
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }
        }
    }
