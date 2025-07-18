using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerTitle : MonoBehaviour
{
    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        Debug.LogWarning("Load Data method is not implemented yet.");
    }

    public void GotoUpgradeScene()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void GoToArenaScene()
    {
        // Open Modal for Boss Select, let those buttons do the arena later
        SceneManager.LoadScene("Arena");
    }

    public CanvasGroup LevelSelectPanel;
    public void ToggleLevelSelect()
    {
        bool state = !LevelSelectPanel.interactable;
        LevelSelectPanel.interactable = state;
        LevelSelectPanel.blocksRaycasts = state;
        LevelSelectPanel.alpha = state ? 1 : 0;
    }

    public CanvasGroup CreditsPanel;
    public void ToggleCredits()
    {
        bool state = !CreditsPanel.interactable;
        CreditsPanel.interactable = state;
        CreditsPanel.blocksRaycasts = state;
        CreditsPanel.alpha = state ? 1 : 0;
    }
}
