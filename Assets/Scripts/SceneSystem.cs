using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    private static SceneSystem instance;
    private int scene_index;

    private bool scene_0_locked = true;
    private bool scene_2_locked = true;
    private bool initialLoadComplete = false;
    public UnityEngine.UI.Image right_arrow;
    public UnityEngine.UI.Image left_arrow;

    // Unlocks access to a screen if upgrade has been purchased
    private void UpdateUnlockStatus()
    {
        if (initialLoadComplete)
        {
            scene_0_locked = PlayerData.upgrade_dict["achievements"] != 1;
        }

        scene_2_locked = PlayerData.upgrade_dict["rankings"] != 1;
    }

    // Checks whether a screen is locked if trying to navigate to it
    private bool IsSceneLocked(int scene_index)
    {
        return (scene_index == 0 && scene_0_locked) || (scene_index == 2 && scene_2_locked);
    }

    // Enables/Disables the UI arrows on screen depending on navigation options
    private void UpdateArrowVisibility()
    {
        if(scene_index == 0 || scene_index == 1 && scene_0_locked)
        {
            left_arrow.enabled = false;
        }
        else
        {
            left_arrow.enabled = true;
        }

        if(scene_index == 5)
        {
            right_arrow.enabled = false;
        }
        else
        {
            right_arrow.enabled = true;
        }
    }

    // Switches screen one to the left if it is unlocked
    public void SwitchSceneRight()
    {
        UpdateUnlockStatus();

        do
        {
            scene_index++;
        } while (IsSceneLocked(scene_index) && scene_index <= 5);

        SceneManager.LoadScene(scene_index);
    }

    // Switches screen one to the left if it is unlocked
    public void SwitchSceneLeft()
    {
        UpdateUnlockStatus();

        if (scene_index == 1 && scene_0_locked)
        {
            return;
        }

        do
        {
            scene_index--;
        } while (IsSceneLocked(scene_index) && scene_index >= 0);

        SceneManager.LoadScene(scene_index);
    }

    // Allows quitting the game from the Unity Editor
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    private void Awake()
    {
        // Loads all the scenes and moves player to the typing screen upon launch
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scene_index = SceneManager.GetActiveScene().buildIndex;

            if (scene_index != 4)
            {
                SceneManager.LoadScene(4);
                scene_index = 4;
            }

            initialLoadComplete = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateArrowVisibility();

        // Switches screen one to the right on right arrow key press
        if (Input.GetKeyUp(KeyCode.RightArrow) && scene_index != 5)
        {
            SwitchSceneRight();
        }

        // Switches screen one to the left on left arrow key press
        if (Input.GetKeyUp(KeyCode.LeftArrow) && scene_index != 0)
        {
            SwitchSceneLeft();
        }

        // Exits application upon pressing escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            QuitGame();
        }
        
    }
}
