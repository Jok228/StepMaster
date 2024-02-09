using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Cache.Interfaces
{
    public interface IMy_Cache
    {
        object GetObject(string key);

        void SetObject(string key, object value,int timeMinute = 0);

        void DeleteObject(string key);
        
    }
}
