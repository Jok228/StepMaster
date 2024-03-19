using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FIreBase.Helpers
    {

    public  class RequestMessage : FireMessage_Factory
        {
       public RequestMessage(MessageParam param)
            {
            message = new Dictionary<string,string> () {
                    {"email",param.Email},
                    {"pushType",param.TypePush }
                };
            }       
        }
    public class TextMessage : FireMessage_Factory
        {
        public TextMessage(MessageParam param)
            {
            message = new Dictionary<string,string> () {
                    {"email",param.Email},
                    {"pushType",param.TypePush },
                    {"text",param.Text }
                };
            }
        }

    public struct MessageParam
        {

        public string? Text;

        public string? Email;

        public string? TypePush;

        }
    abstract public class FireMessage_Factory
        {
        public Dictionary<string,string> message;
        public FireMessage_Factory()
            {
            message = new Dictionary<string,string> ();
            }
        }
        
    }
