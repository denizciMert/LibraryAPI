using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BLL.Core
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static ServiceResult<T> SuccessResult(T data) => new ServiceResult<T> { Data = data, Success = true };
        public static ServiceResult<T> FailureResult(string errorMessage) => new ServiceResult<T> { Success = false, ErrorMessage = errorMessage };
    }
}
