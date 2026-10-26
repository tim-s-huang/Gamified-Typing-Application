using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class TypingHelpButton : MonoBehaviour
{
    public GameObject typing_tutorial;
    private bool tutorial_shown = false;

    // Shows instructions image on screen
    public void PressTutorialButton()
    {
        tutorial_shown = true;
        typing_tutorial.SetActive(true);
    }


    void Start()
    {
        typing_tutorial.SetActive(false);
    }


    void Update()
    {
        // Hides instructions image upon any key press
        if(tutorial_shown)
        {
            if(Input.anyKeyDown)
            {
                tutorial_shown = false;
                typing_tutorial.SetActive(false);
            }
        }
    }
}
