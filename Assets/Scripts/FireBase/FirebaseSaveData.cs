using System.Text;
using System.Threading.Tasks;
using FireBase;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseSaveData
{
    public static async Task SaveData(string fileName, string jsonData, string saveTime)
    {
        string userId = "", authToken = "";
        FirebaseCheckToken.CheckToken((uId, token) =>
        {
            userId = uId;
            authToken = token;
        });

        if (string.IsNullOrEmpty(userId)) Debug.LogError("User ID is null or empty!");
        if (string.IsNullOrEmpty(authToken)) Debug.LogError("ID Token is null or empty!");

        var url =
            "https://unity-rts-28cae-default-rtdb.asia-southeast1.firebasedatabase.app/" +
            $"users/{userId}/{fileName}.json?auth={authToken}";

        var wrappedJsonData = $"{{\"gameData\": {jsonData}, \"saveTime\": \"{saveTime}\"}}";

        using (var request = CreateWebRequest(url, "PUT", wrappedJsonData))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log($"Error while saving data: {request.error}");
            else
                Debug.Log("Save successfully!");
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