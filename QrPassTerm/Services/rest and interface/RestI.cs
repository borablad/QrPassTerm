using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QrPassTerm.Models;
namespace QrPassTerm.Services.rest_and_interface
{
    public interface RestI
    {
        Task<string> LoginAsync(UserDto user);
        Task<GenerateQr> GetQr();
    }
}
