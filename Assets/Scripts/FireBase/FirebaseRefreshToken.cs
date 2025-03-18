using System;
using System.Text;
using System.Threading.Tasks;
using FireBase;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseRefreshToken
{
    private const string REFRESH_TOKEN_URL = "https://securetoken.googleapis.com/v1/token?key=";

    public static async Task RefreshToken(string apiKey, string refreshToken, Action<string, string> onSuccess,
        Action<string> onError)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            onError?.Invoke("No refresh token available.");
            return;
        }

        var requestBody = $"{{\"grant_type\": \"refresh_token\", \"refresh_token\": \"{refreshToken}\"}}";
        var url = REFRESH_TOKEN_URL + apiKey;

        using (var request = new UnityWebRequest(url, "POST"))
        {
            var bodyRaw = Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<FirebaseRefreshTokenResponse>(request.downloadHandler.text);
                onSuccess?.Invoke(response.id_token, response.refresh_token);
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }
}