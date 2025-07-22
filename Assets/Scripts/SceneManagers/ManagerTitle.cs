using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using PersistentData.Bosses;
using PersistentData.Warriors;

public class ManagerTitle : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup levelSelectPanel;
    public CanvasGroup creditsPanel;

    // TODO: Remove later, once character select is working
    public Party currentParty;
    public Warrior warrior1;
    public Warrior warrior2;
    public Warrior warrior3;

    //public Boss[] bosses;
    public List<Boss> bosses;
    public CurrentBoss currentBoss;

    private void Awake()
    {
        if ((bosses?.Count ?? 0) == 0)
        {
            Debug.LogError("Bosses not populated!");
        }

        if (!currentBoss)
        {
            Debug.LogError("CurrentBoss not populated!");
        }
        else
        {
            currentBoss.Reset();
        }
    }

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
        if ((bosses?.Count ?? 0) <= bossIndex)
        {
            Debug.LogError("Selected a boss that doesn't exist.");
        }
        else
        {
            // TODO: Remove once party select is working
            currentParty.warriors = new List<Warrior>()
            {
                warrior2,
                warrior3,
                warrior1
            };
            Debug.Log("CurrentParty: " + currentParty);

            currentBoss.SetValues(bosses[bossIndex]);
            Debug.Log("CurrentBoss: " + currentBoss);

            SceneManager.LoadScene(SceneData.ArenaSceneIndex);
        }
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
