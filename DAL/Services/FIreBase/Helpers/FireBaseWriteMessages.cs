using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FIreBase.Helpers
    {
    public static class FireBaseWriteMessages
        {
        public enum PushType
            {
            FriendRequest = 0,
            Message = 1,
            }
        public static Dictionary<string, string> GenerateMessage(string email, PushType type) {
            if (PushType.Message.ToString() == type.ToString())
                {
                return new Dictionary<string, string>() {
                    {"email",email },
                    {"pushType","0" }
                };
                }
            if (PushType.Message.ToString() == type.ToString())
                {
                return new Dictionary<string, string>() {
                    {"email",email },
                    {"pushType","1" }
                };
                }
            return new Dictionary<string, string>() {
                    {"email",email },
                    {"pushType","1" }
                };
            }
        }
    }
