using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public static class HttpClientExtensions {

		public static async Task<(HttpStatusCode, T)> GetValueAsync<T>( this HttpClient client, string url ) {
			using HttpResponseMessage response = await client.GetAsync( url );
			return await ProcessResponse<T>( response );
		}

		public static async Task<(HttpStatusCode, T)> PostValueAsync<T>( this HttpClient client, string url, object value ) {
			string json = JsonSerializer.Serialize( value );
			using var stringContent = new StringContent( json, Encoding.UTF8, "application/json" );
			using var response = await client.PostAsync( url, stringContent );

			return await ProcessResponse<T>(response);
		}

		public static async Task<(HttpStatusCode, T)> PutValueAsync<T>( this HttpClient client, string url, object value ) {
			string json = JsonSerializer.Serialize( value );
			return await PutValueAsync<T>( client, url, json );
		}

		public static async Task<(HttpStatusCode, T)> PutValueAsync<T>( this HttpClient client, string url, string json ) {
			using var stringContent = new StringContent( json, Encoding.UTF8, "application/json" );
			using var response = await client.PutAsync( url, stringContent );

			return await ProcessResponse<T>( response );
		}

		private static async Task<(HttpStatusCode, T)> ProcessResponse<T>(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
				return (response.StatusCode, default);

			string content = await response.Content.ReadAsStringAsync();
			T responseValue = JsonSerializer.Deserialize<T>(content, Consts.JsonSerializerOptions);

			return (response.StatusCode, responseValue);
		}
	}
}
