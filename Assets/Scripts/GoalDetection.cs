using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDetection : MonoBehaviour
{
    // This method will be called when the player enters the Goal trigger area
    private void OnTriggerEnter(Collider enteredObject)
    {
        // Check if the player has entered the Goal area
        if (enteredObject.CompareTag("Goal"))
        {
            // Load the MainMenu scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
