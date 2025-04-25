using TMPro;
using UnityEngine;

public class PlayerNameText : MonoBehaviour
{
    private TMP_Text nameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameText = GetComponent<TMP_Text>();

        if (AuthManager.firebaseUser != null)
        {
            nameText.text = $"Hi, {AuthManager.firebaseUser.Email}!";
        }
        else
        {
            nameText.text = "ERROR: User not found!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
