using System.Collections;
using UnityEngine;

public class FirebaseSystem : MonoBehaviour
{
    public static FirebaseSystem Instance;

    private const string API_KEY = "AIzaSyC-FfGTwi-hr9bxJQGIPP6vgY-yjQ7f_Qo";

    public string IdToken { get; private set; }
    public string LocalId { get; private set; }

    private string _refreshToken;
    private float _tokenExpiryTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSavedCredentials();
    }

    private void LoadSavedCredentials()
    {
        if (PlayerPrefs.HasKey("userIdToken") && PlayerPrefs.HasKey("localId") && PlayerPrefs.HasKey("tokenExpiry"))
        {
            IdToken = PlayerPrefs.GetString("userIdToken");
            LocalId = PlayerPrefs.GetString("localId");
            _tokenExpiryTime = PlayerPrefs.GetFloat("tokenExpiry");

            if (Time.time < _tokenExpiryTime)
                Debug.Log("Loaded saved credentials.");
            else
                StartCoroutine(RefreshToken());
        }
        else
        {
            StartCoroutine(FirebaseSignIn.StartSignin(API_KEY, OnSignInSuccess, OnSignInError));
        }
    }

    public IEnumerator RefreshToken()
    {
        yield return FirebaseRefreshToken.RefreshToken(API_KEY, _refreshToken, OnRefreshSuccess, OnRefreshError);
    }

    private void OnRefreshSuccess(string newIdToken, string newRefreshToken)
    {
        IdToken = newIdToken;
        _refreshToken = newRefreshToken;
        _tokenExpiryTime = Time.time + 3600; // Token expires in 1 hour

        SaveCredentials();
        Debug.Log("Token refreshed successfully.");
    }

    private void OnRefreshError(string error)
    {
        Debug.LogError("Failed to refresh token: " + error);
        StartCoroutine(FirebaseSignIn.StartSignin(API_KEY, OnSignInSuccess, OnSignInError));
    }

    private void OnSignInSuccess(string idToken, string localId, string refreshToken)
    {
        IdToken = idToken;
        LocalId = localId;
        _refreshToken = refreshToken;
        _tokenExpiryTime = Time.time + 3600; // Token expires in 1 hour

        SaveCredentials();
        Debug.Log("Authenticated successfully! User ID: " + localId);
    }

    private void OnSignInError(string error)
    {
        Debug.LogError("Sign-in failed: " + error);
    }

    private void SaveCredentials()
    {
        PlayerPrefs.SetString("userIdToken", IdToken);
        PlayerPrefs.SetString("localId", LocalId);
        PlayerPrefs.SetFloat("tokenExpiry", _tokenExpiryTime);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveCredentials();
        Debug.Log("Credentials saved on application quit.");
    }
}