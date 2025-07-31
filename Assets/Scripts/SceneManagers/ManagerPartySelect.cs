using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using UnityEngine.UI;
using TMPro;

public class ManagerPartySelect : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup characterSelector;
    public CanvasGroup settingsPanel;
    public Button toArenaButton;
    public CombatantGroup currentParty;
    public CombatantGroup allWarriors;
    public int partySlot;
    public int currentSelectionIndex;
    private Combatant[] _partyTemp;
    public int partySize = 4;
    public Image[] warriorDisplays;
    public TMP_Text[] buttonTexts;

    // Initialize variables
    public void Awake()
    {
        partySlot = 0;
        currentSelectionIndex = 0;
    }

    public void Start()
    {
        Debug.Assert(currentParty != null, "current party wasn't populated");
        _partyTemp = new Combatant[partySize];
        CheckToEnableContinue();
        currentParty.combatants = new List<Combatant>();
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

        ConfirmWarriorSelection();
    }

    private void ConfirmWarriorSelection()
    {
        if (currentSelectionIndex >= allWarriors.combatants.Count)
        {
            Debug.LogError("currentSelectionIndex out of bounds");
        }
        else if (partySlot >= _partyTemp.Length)
        {
            Debug.LogError("partySlot out of bounds");
        }
        else
        {
            _partyTemp[partySlot] = allWarriors.combatants[currentSelectionIndex];
            // testing warrior log
            Debug.Log("Selected Warrior is:" + _partyTemp[partySlot]);
            Debug.Log("Warrior is supposed to be:" + allWarriors.combatants[currentSelectionIndex]);
            var currentPartyMessage = new StringBuilder();
            currentPartyMessage.AppendLine("Current party is:");
            for (int n = 0; n < _partyTemp.Length; n++)
            {
                currentPartyMessage.AppendLine($"\t{_partyTemp[n]}");
            }

            Debug.Log(currentPartyMessage.ToString());
            // Display warrior sprite
            warriorDisplays[partySlot].sprite = _partyTemp[partySlot].sprite;
            buttonTexts[partySlot].text = _partyTemp[partySlot].displayName;

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
        currentParty.combatants.AddRange(_partyTemp);
        SceneManager.LoadScene(SceneData.ArenaSceneIndex);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
    }

    public void ToggleSettings()
    {
        CanvasHelper.ToggleCanvasGroup(settingsPanel);
    }
}