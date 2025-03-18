using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireBase;
using GameSave;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseGetData
{
    private const string DATABASE_URL = "https://unity-rts-28cae-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public static async Task GetFileNames(Action<Dictionary<string, CloudData>> action)
    {
        string userId = "", authToken = "";
        FirebaseCheckToken.CheckToken((uId, token) =>
        {
            userId = uId;
            authToken = token;
        });

        var url = $"{DATABASE_URL}users/{userId}.json?auth={authToken}";

        using (var request = UnityWebRequest.Get(url))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var jsonData = request.downloadHandler.text;
                ProcessFileNames(jsonData, action);
            }
            else
            {
                Debug.LogError("Failed to load data: " + request.error);
            }
        }
    }

    private static void ProcessFileNames(string jsonData, Action<Dictionary<string, CloudData>> action)
    {
        var userData = JsonConvert.DeserializeObject<Dictionary<string, CloudData>>(jsonData);

        action?.Invoke(userData);
    }
}