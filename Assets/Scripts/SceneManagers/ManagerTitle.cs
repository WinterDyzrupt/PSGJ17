using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;

public class ManagerTitle : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup LevelSelectPanel;
    public CanvasGroup CreditsPanel;


    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        Debug.LogWarning("Load Data method is not implemented yet.");
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void LoadArenaScene()
    {
        // Open Modal for Boss Select, let those buttons do the arena later
        SceneManager.LoadScene("Arena");
    }


    public void ToggleLevelSelect()
    {
        CanvasHelper.ToggleCanvasGroup(LevelSelectPanel);
    }


    public void ToggleCredits()
    {
        CanvasHelper.ToggleCanvasGroup(CreditsPanel);
    }
}
