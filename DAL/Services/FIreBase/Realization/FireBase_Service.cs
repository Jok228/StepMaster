using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.FIreBase.Interfaces;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FIreBase.Realization
    {
    public class FireBase_Service : IFireBase_Service
        {
        private ILogger<FireBase_Service> _logger;

        public FireBase_Service(ILogger<FireBase_Service> logger) {
            _logger = logger;
            }

        public async Task SendMessage(string token, Dictionary<string, string> data) {

            var registrationToken = token;
            var message = new Message() {
                Data = data,
                Token = registrationToken,
                };
            try
                {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
                }
            catch (Exception ex)
                {
                _logger.LogError(ex.Message); 
                }
            }
        }
    }
