using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseSignIn : MonoBehaviour
{
    public static IEnumerator StartSignin(string API_KEY, Action<string, string, string> onSuccess,
        Action<string> onError)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={API_KEY}";
        var jsonData = "{\"returnSecureToken\": true}";

        using (var request = CreateWebRequest(url, "POST", jsonData))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(request.error);
            }
            else
            {
                var responseJson = request.downloadHandler.text;
                var authResponse = JsonUtility.FromJson<AuthResponse>(responseJson);
                onSuccess?.Invoke(authResponse.idToken, authResponse.localId, authResponse.refreshToken);
                Debug.Log($"{authResponse.idToken}, {authResponse.localId}, {authResponse.refreshToken}");
            }
        }
    }

    private static UnityWebRequest CreateWebRequest(string url, string method, string jsonData)
    {
        var request = new UnityWebRequest(url, method);
        var bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }
}