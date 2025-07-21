using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;

public class ManagerTitle : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup levelSelectPanel;
    public CanvasGroup creditsPanel;
    
    public IntVariable currentlySelectedBossIndex;
    
    // TODO: Remove later, once character select is working
    //[SerializeField]
    public Party currentParty;
    public Warrior warrior1;
    public Warrior warrior2;
    public Warrior warrior3;
    
    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        Debug.LogWarning("Load Data method is not implemented yet.");
    }

    public void LoadUpgradeScene()
    {
        SceneManager.LoadScene(SceneData.UpgradeSceneIndex);
    }

    public void LoadArenaScene(int bossIndex)
    {
        currentlySelectedBossIndex.value = bossIndex;
        // TODO: Remove once party select is working
        currentParty.warriors = new[]
        {
            warrior2,
            warrior3,
            warrior1
        };
        Debug.Log("CurrentParty: " + currentParty);
        SceneManager.LoadScene(SceneData.ArenaSceneIndex);
    }

    public void ToggleLevelSelect()
    {
        CanvasHelper.ToggleCanvasGroup(levelSelectPanel);
    }

    public void ToggleCredits()
    {
        CanvasHelper.ToggleCanvasGroup(creditsPanel);
    }
}
