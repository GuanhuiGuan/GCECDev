using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GCECDev.Controllers
{
    public interface IRestAPI
    {
        Task<T> Get<T>(string value) where T: class;

        IEnumerable<Task<T>> GetAll<T>() where T : class;

        Task<T> Post<T>(T value) where T : class;

        Task<T> Delete<T>(string value) where T : class;
    }
}
