using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Text;
>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
<<<<<<< HEAD
using PersistentData.Warriors;
using UnityEngine.UI;


=======
using UnityEngine.UI;

>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
public class ManagerPartySelect : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup characterSelector;
    public Button toArenaButton;
<<<<<<< HEAD
    public Party currentParty;
    public AllWarriors allWarriors;
    public int partySlot;
    public int currentSelectionIndex;
    private Warrior[] _partyTemp;
    public int partySize = 4;
    public Image[] warriorDisplays;


=======
    public CombatantGroup currentParty;
    public CombatantGroup allWarriors;
    public int partySlot;
    public int currentSelectionIndex;
    private Combatant[] _partyTemp;
    public int partySize = 4;
    public Image[] warriorDisplays;

>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
    // Initialize variables
    public void Awake()
    {
        partySlot = 0;
        currentSelectionIndex = 0;
    }

    public void Start()
    {
        Debug.Assert(currentParty != null, "current party wasn't populated");
<<<<<<< HEAD
        _partyTemp = new Warrior[partySize];
        CheckToEnableContinue();
        currentParty.warriors = new List<Warrior>();
=======
        _partyTemp = new Combatant[partySize];
        CheckToEnableContinue();
        currentParty.combatants = new List<Combatant>();
>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
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
<<<<<<< HEAD
        if (currentSelectionIndex >= allWarriors.warriors.Count)
=======
        if (currentSelectionIndex >= allWarriors.combatants.Count)
>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
        {
            Debug.LogError("currentSelectionIndex out of bounds");
        }
        else if (partySlot >= _partyTemp.Length)
        {
            Debug.LogError("partySlot out of bounds");
        }
        else
        {
<<<<<<< HEAD
            _partyTemp[partySlot] = allWarriors.warriors[currentSelectionIndex];
            // testing warrior log
            Debug.Log("Selected Warrior is:" + _partyTemp[partySlot]);
            Debug.Log("Warrior is supposed to be:" + allWarriors.warriors[currentSelectionIndex]);
            Debug.Log("Current party is:");
            for (int n = 0; n < _partyTemp.Length; n++)
            {
                Debug.Log("\n" + _partyTemp[n]);
            }

=======
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
>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
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
<<<<<<< HEAD
        currentParty.warriors.AddRange(_partyTemp);
=======
        currentParty.combatants.AddRange(_partyTemp);
>>>>>>> 912d3852974bcea060ff91076b237ab03fb7b944
        SceneManager.LoadScene(SceneData.ArenaSceneIndex);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
    }

}
