using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using PersistentData.Bosses;

public class ManagerTitle : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup levelSelectPanel;
    public CanvasGroup creditsPanel;

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

    public void LoadPartySelectScene(int bossIndex)
    {
        if ((bosses?.Count ?? 0) <= bossIndex)
        {
            Debug.LogError("Selected a boss that doesn't exist.");
        }
        else
        {
            currentBoss.SetValues(bosses[bossIndex]);
            Debug.Log("CurrentBoss: " + currentBoss);

            SceneManager.LoadScene(SceneData.PartySelectSceneIndex);
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
