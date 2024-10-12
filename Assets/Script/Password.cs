using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    // Reference to the TMP InputField where the user will type the password
    public TMP_InputField passwordInputField;

    // The correct password (editable in Inspector)
    [SerializeField] private string correctPassword = "yourpassword";

    // The index of the scene to load if the password is correct
    [SerializeField] private int sceneIndexToLoad = 1; // Use scene build index

    // Optional: Reference to the UI Text for feedback
    public TextMeshProUGUI feedbackText;

    // Method to check the password and load the scene
    public void CheckPassword()
    {
        string userInput = passwordInputField.text;

        if (userInput == correctPassword)
        {
            // If the password is correct, change the scene by its index
            SceneManager.LoadScene(sceneIndexToLoad);
        }
        else
        {
            // If the password is incorrect, display feedback
            if (feedbackText != null)
            {
                feedbackText.text = "Incorrect Password. Try again.";
            }
        }
    }
}