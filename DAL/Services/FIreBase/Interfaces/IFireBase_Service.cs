using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FIreBase.Interfaces
{
    public interface IFireBase_Service
    {
        public Task SendMessage(string token, Dictionary<string, string> data);
    }
}
