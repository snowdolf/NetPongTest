using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public bool IsFireBaseReady { get; private set; } = false;
    public bool IsSignInOnProgress { get; private set; } = false;

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button signInButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser firebaseUser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signInButton.interactable = false;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;

            if (result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFireBaseReady = false;
            }
            else
            {
                IsFireBaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFireBaseReady;
        }
        );
    }

    public void SignIn()
    {
        if (!IsFireBaseReady || IsSignInOnProgress || firebaseUser != null)
        {
            return;
        }

        IsSignInOnProgress = true;
        signInButton.interactable = false;

        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Sign in status : {task.Status}");

            IsSignInOnProgress = false;
            signInButton.interactable = true;

            if (task.IsFaulted)
            {
                Debug.LogError($"Sign in failed: {task.Exception}");
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("Sign in canceled");
            }
            else
            {
                firebaseUser = task.Result.User;
                Debug.Log(firebaseUser.Email);
                SceneManager.LoadScene("Lobby");
            }
        }
        );
    }
}
