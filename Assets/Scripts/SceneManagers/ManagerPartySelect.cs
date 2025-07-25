using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using PersistentData.Warriors;
using UnityEngine.UI;


public class ManagerPartySelect : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup characterSelector;
    public Button toArenaButton;
    public Party currentParty;
    public AllWarriors allWarriors;
    public int partySlot;
    public int currentSelectionIndex;
    private Warrior[] _partyTemp;
    public int partySize = 4;
    public Image[] warriorDisplays;


    // Initialize variables
    public void Awake()
    {
        partySlot = 0;
        currentSelectionIndex = 0;
    }

    public void Start()
    {
        Debug.Assert(currentParty != null, "current party wasn't populated");
        _partyTemp = new Warrior[partySize];
        CheckToEnableContinue();
        currentParty.warriors = new List<Warrior>();
        Debug.Log("ManagerPartySelect.Start CurrentParty: " + currentParty);

    }

    // Toggling selection screen
    public void ToggleCharacterSelect(int buttonNumber)
    {
        CanvasHelper.ToggleCanvasGroup(characterSelector);
        partySlot = buttonNumber;
    }

    public void WarriorSelection(int classNumberIndex)
    {
        currentSelectionIndex = classNumberIndex;
        Debug.Log("selecting class" + currentSelectionIndex);
    }

    public void ConfirmWarriorSelection()
    {
        if (currentSelectionIndex >= allWarriors.warriors.Count)
        {
            Debug.LogError("currentSelectionIndex out of bounds");
        }
        else if (partySlot >= _partyTemp.Length)
        {
            Debug.LogError("partySlot out of bounds");
        }
        else
        {
            _partyTemp[partySlot] = allWarriors.warriors[currentSelectionIndex];
            // testing warrior log
            Debug.Log("Selected Warrior is:" + _partyTemp[partySlot]);
            Debug.Log("Warrior is supposed to be:" + allWarriors.warriors[currentSelectionIndex]);
            Debug.Log("Current party is:");
            for (int n = 0; n < _partyTemp.Length; n++)
            {
                Debug.Log("\n" + _partyTemp[n]);
            }

            // Display warrior sprite
            warriorDisplays[partySlot].sprite = _partyTemp[partySlot].sprite;

            CheckToEnableContinue();

            // Toggle selection screen
            CanvasHelper.ToggleCanvasGroup(characterSelector);
        }
    }

    private void CheckToEnableContinue()
    {
        // Enable Continue button if party is full
        bool _isPartyFull = true;
        foreach (var Warrior in _partyTemp)
        {
            if (Warrior == null) _isPartyFull = false;
        }
        toArenaButton.interactable = _isPartyFull;
    }

    public void EnterArena()
    {
        currentParty.warriors.AddRange(_partyTemp);
        SceneManager.LoadScene(SceneData.ArenaSceneIndex);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
    }

}
