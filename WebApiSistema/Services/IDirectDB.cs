using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.Services
{
    public interface IDirectDB
    {
        public Task<List<Dictionary<string, object>>> GetListData(string query);
        public Task<Dictionary<string, object>> GetData(string query);
    }
}
