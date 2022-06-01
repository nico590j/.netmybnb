using Mybnb.dtolibrary.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace Mybnb.blazor.Data
{
	public class ApiService_Authentication
	{
		private HttpClient _httpClient;
		public ApiService_Authentication(HttpClient client)
		{
			_httpClient = client;
			_httpClient.BaseAddress = new Uri("https://localhost:7258");
		}
		public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authRequest)
		{
			AuthenticateResponse? AuthResponse = null;

			using (_httpClient)
			{
				var request = new StringContent(JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync("api/users/authenticate", request);
				var content = await response.Content.ReadAsStringAsync();
				
				if (!response.IsSuccessStatusCode)
					throw new ApiException("Response was unsuccessful", (int)response.StatusCode, content, null, null);

				if(!string.IsNullOrEmpty(content))
					AuthResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(content);
			}

			return AuthResponse;
		}
	}
}
