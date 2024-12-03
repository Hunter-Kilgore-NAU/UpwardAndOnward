using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public Text tutorialText; // Drag your Text UI object here
    public string message = "Press SPACE to double jump!"; // Custom message for this trigger
    public float displayTime = 4f; // How long the text shows

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Player entered the trigger
        if (other.CompareTag("Player"))
        {
            // Set the tutorial text
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);

            // Hide the text after a delay
            Invoke(nameof(HideText), displayTime);
        }
    }

    private void HideText()
    {
        tutorialText.gameObject.SetActive(false);
    }
}

