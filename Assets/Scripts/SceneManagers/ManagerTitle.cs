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

    public void LoadPartySelectScene()
    {
        SceneManager.LoadScene("Party Select");
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
