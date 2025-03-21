using System.Threading.Tasks;
using GameSave;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace FireBase
{
    public class FirebaseLoadData
    {
        private const string DATABASE_URL =
            "https://unity-rts-28cae-default-rtdb.asia-southeast1.firebasedatabase.app/";

        public static async Task<GameSaveData> LoadFile(string fileName)
        {
            string userId = "", authToken = "";
            FirebaseCheckToken.CheckToken((uId, token) =>
            {
                userId = uId;
                authToken = token;
            });

            var url = $"{DATABASE_URL}users/{userId}/{fileName}.json?auth={authToken}";

            using var request = UnityWebRequest.Get(url);
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var jsonData = request.downloadHandler.text;
                var convertedData = ProcessFileData(jsonData);
                return convertedData;
            }

            Debug.LogError("Failed to load data: " + request.error);

            return null;
        }

        private static GameSaveData ProcessFileData(string jsonData)
        {
            var cloudData = JsonConvert.DeserializeObject<CloudData>(jsonData);
            var data = cloudData.gameData;
            return data;
        }
    }
}