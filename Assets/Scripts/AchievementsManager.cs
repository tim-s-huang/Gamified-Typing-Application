using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AchievementsManager : MonoBehaviour
{
    public UnityEngine.UI.Image[] trophy_sprites;

    private void UpdateAchievements()
    {
        int index = 0;

        // Loops through list of UI images and colors black if they are not unlocked
        foreach (UnityEngine.UI.Image trophy in trophy_sprites)
        {
            var entry = PlayerData.achieve_dict.ElementAt(index);
            bool unlocked = entry.Value;

            if(!unlocked)
            {
                trophy.color = Color.black;
            }
            
            index++;
        }
    }
    
    void Start()
    {
        UpdateAchievements();
    }

    void Update()
    {
        PlayerData.UnlockAchievements();
        UpdateAchievements();
    }
}
