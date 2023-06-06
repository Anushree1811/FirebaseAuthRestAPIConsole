using FirebaseAuthRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;

namespace FirebaseAuthRestAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    


    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(string email, string password)
    {
        var httpClient = new HttpClient();

        var requestBody = new
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyDsRqHl36anY_xBrPOf1lzKXt4IsSDAVk8");
        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<AuthCredentials>(responseData);

            // Extract the idToken and other properties from the response
            var idToken = responseObject.IdToken;
            var localId = responseObject.LocalId;
            var refreshToken = responseObject.RefreshToken;
            var expiresIn = responseObject.ExpiresIn;

            // Return the idToken and other properties as a JSON response
            var responseDataJson = JsonConvert.SerializeObject(new { idToken, localId, refreshToken, expiresIn });
            return Content(responseDataJson, "application/json");
        }

        // Return an error response if the request was not successful
        return StatusCode((int)response.StatusCode);
    }


    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(string email, string password) {

        var httpClient = new HttpClient();

        var requestBody = new
        {
            email = email,
            password = password,
            returnSecureToken = true
        };


        var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyDsRqHl36anY_xBrPOf1lzKXt4IsSDAVk8");
        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<AuthCredentials>(responseData);

            // Extract the idToken and other properties from the response
            var idToken = responseObject.IdToken;
            var localId = responseObject.LocalId;
            var refreshToken = responseObject.RefreshToken;
            var expiresIn = responseObject.ExpiresIn;

            // Return the idToken and other properties as a JSON response
            var responseDataJson = JsonConvert.SerializeObject(new { idToken, localId, refreshToken, expiresIn });
            return Content(responseDataJson, "application/json");
        }

        // Return an error response if the request was not successful
        return StatusCode((int)response.StatusCode);
    }

    [HttpPost("SendOOBCode")]
    public async Task SendOOBCode(string idToken)
    {

        var httpClient = new HttpClient();

        var requestBody = new
        {

            requestType = "VERIFY_EMAIL",
            idToken = idToken,
            emailLinkExpireInSec = 60
        };


        var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=AIzaSyDsRqHl36anY_xBrPOf1lzKXt4IsSDAVk8");

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody));
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        var response = await httpClient.SendAsync(request);

    }

    //public async Task VerifyPasswordResetCode(string oobCode) { 

    //   var httpClient = new HttpClient();


    //    var requestBody = new
    //    {
    //        oobCode = oobCode,
    //        requestType = "PASSWORD_RESET"
    //    };


    //    var request = new HttpRequestMessage(new HttpMethod("POST"), "https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key=AIzaSyDsRqHl36anY_xBrPOf1lzKXt4IsSDAVk8");

    //        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody));
    //        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

    //        var response = await httpClient.SendAsync(request);



    //}


}
