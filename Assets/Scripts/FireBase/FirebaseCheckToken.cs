using System;
using UnityEngine;

namespace FireBase
{
    public class FirebaseCheckToken
    {
        public static async void CheckToken(Action<string, string> action)
        {
            try
            {
                if (FirebaseSystem.Instance != null)
                {
                    var tokenExpiryTime = FirebaseSystem.Instance.tokenExpiryTime;

                    if (Time.time >= tokenExpiryTime)
                        await FirebaseSystem.Instance.RefreshToken();

                    var userId = FirebaseSystem.Instance.localId;
                    var authToken = FirebaseSystem.Instance.idToken;

                    action?.Invoke(userId, authToken);
                }
                else
                {
                    Debug.LogWarning("FirebaseSystem.Instance is null, cannot perform operations");
                    action?.Invoke("", "");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error checking token: {e.Message}");
            }
        }
    }
}