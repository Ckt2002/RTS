using System;

[Serializable]
public class AuthResponse
{
    public string idToken;
    public string localId;
    public string refreshToken;
    public string expiresIn;
}


[Serializable]
public class FirebaseRefreshTokenResponse
{
    public string id_token;
    public string refresh_token;
}