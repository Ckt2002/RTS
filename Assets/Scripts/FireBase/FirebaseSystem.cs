using System;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseSystem : MonoBehaviour
{
    public static FirebaseSystem Instance;

    private const string API_KEY = "AIzaSyC-FfGTwi-hr9bxJQGIPP6vgY-yjQ7f_Qo";

    public string idToken { get; private set; }
    public string localId { get; private set; }
    public float tokenExpiryTime { get; private set; }

    private string refreshToken;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadSavedCredentials();
    }

    private async void LoadSavedCredentials()
    {
        try
        {
            if (PlayerPrefs.HasKey("userIdToken") && PlayerPrefs.HasKey("localId") && PlayerPrefs.HasKey("tokenExpiry"))
            {
                idToken = PlayerPrefs.GetString("userIdToken");
                localId = PlayerPrefs.GetString("localId");
                refreshToken = PlayerPrefs.GetString("refreshToken");
                tokenExpiryTime = PlayerPrefs.GetFloat("tokenExpiry");

                await RefreshToken();
            }
            else
            {
                await FirebaseSignIn.StartSignin(API_KEY, OnSignInSuccess, OnSignInError);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async Task RefreshToken()
    {
        await FirebaseRefreshToken.RefreshToken(API_KEY, refreshToken, OnRefreshSuccess, OnRefreshError);
    }

    private void OnRefreshSuccess(string newIdToken, string newRefreshToken)
    {
        idToken = newIdToken;
        refreshToken = newRefreshToken;
        tokenExpiryTime = Time.time + 3600;

        SaveCredentials();
    }

    private async void OnRefreshError(string error)
    {
        try
        {
            Debug.LogError("Failed to refresh token: " + error);
            await FirebaseSignIn.StartSignin(API_KEY, OnSignInSuccess, OnSignInError);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error while trying to signin: {e.Message}");
        }
    }

    private void OnSignInSuccess(string idToken, string localId, string refreshToken)
    {
        this.idToken = idToken;
        this.localId = localId;
        this.refreshToken = refreshToken;
        tokenExpiryTime = Time.time + 3600;
    }

    private void OnSignInError(string error)
    {
        Debug.LogError("Sign-in failed: " + error);
    }

    private void SaveCredentials()
    {
        PlayerPrefs.SetString("userIdToken", idToken);
        PlayerPrefs.SetString("localId", localId);
        PlayerPrefs.SetString("refreshToken", refreshToken);
        PlayerPrefs.SetFloat("tokenExpiry", tokenExpiryTime);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveCredentials();
        // Debug.Log("Credentials saved on application quit.");
    }
}