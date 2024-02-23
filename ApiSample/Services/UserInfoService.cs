using System.Text.Json;
using static LineBotTemplate.Services.UserInfoService;

namespace LineBotTemplate.Services
{
    public interface IUserInfoService
    {
        public UserModel GetUserInfo(string userId);
    }
    public class UserInfoService : IUserInfoService
    {
        private readonly string _accessToken = "Your AccessToken";

        public record UserModel(string userId, string displayName, string pictureUrl);

        public UserModel GetUserInfo(string userId)
        {
            string url = $"https://api.line.me/v2/bot/profile/{userId}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = client.GetAsync(url).Result;
            string result = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UserModel>(result);
        }
    }
}
