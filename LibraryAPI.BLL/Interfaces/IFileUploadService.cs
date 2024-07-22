using LibraryAPI.BLL.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BLL.Interfaces
{
    public interface IFileUploadService
    {
        Task<ServiceResult<string>> UploadImage(IFormFile? imageFile);
    }
}
