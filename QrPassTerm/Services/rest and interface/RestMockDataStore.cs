using Newtonsoft.Json;
using QrPassTerm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QrPassTerm.Helpers;
using QrPassTerm.Helpers.REST;
using Newtonsoft.Json.Linq;

namespace QrPassTerm.Services.rest_and_interface
{
    public class RestMockDataStore : RestI
    {
        #region Login
        public async Task<string> LoginAsync(UserDto user)
        {
            var result = string.Empty;
            try
            {

                var request = JsonConvert.SerializeObject(user);

                var response = await new RequestServiceREST().Post(Constans.Login, request, "application/json");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    
                   result = JObject.Parse(responseData)["access_token"].ToString();

                     result.Replace("\"", "");
                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    throw new Exception(responseData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        #endregion
        #region qr
        public async Task<GenerateQr> GetQr()
            {
            var result = new GenerateQr();

            try
            {
                var response = await new RequestServiceREST().Get(Constans.GetQr);
                if (response.IsSuccessStatusCode)
                {
                    string rs = JObject.Parse(await response.Content.ReadAsStringAsync())["detail"].ToString();
                    var responseData = JsonConvert.DeserializeObject<GenerateQr>(rs);
                    result = responseData;

                }
                else
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    throw new Exception(responseData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
          


            return result;
        }
        #endregion
    }
}
