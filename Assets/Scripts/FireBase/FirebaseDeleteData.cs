using System;
using UnityEngine;
using UnityEngine.Networking;

namespace FireBase
{
    public class FirebaseDeleteData
    {
        private const string DATABASE_URL =
            "https://unity-rts-28cae-default-rtdb.asia-southeast1.firebasedatabase.app/";

        public static async void DeleteFile(string fileName)
        {
            try
            {
                string userId = "", authToken = "";
                FirebaseCheckToken.CheckToken((uId, token) =>
                {
                    userId = uId;
                    authToken = token;
                });

                var url = $"{DATABASE_URL}users/{userId}/{fileName}.json?auth={authToken}";

                using var request = UnityWebRequest.Delete(url);
                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                    Debug.LogError("Failed to delete data: " + request.error);
                else
                    Debug.Log("Successful delete data");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to delete data: " + e.Message);
            }
        }
    }
}