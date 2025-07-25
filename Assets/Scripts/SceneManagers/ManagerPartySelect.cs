using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Helper;
using PersistentData;
using PersistentData.Warriors;
using System;
using System.Linq;
using JetBrains.Annotations;

public class ManagerPartySelect : MonoBehaviour
{
    // Canvas Groups
    public CanvasGroup characterSelector;
    public Party currentParty;
    public AllWarriors allWarriors;
    public int partySlot;
    public int currentSelectionIndex;
    private Warrior[] PartyTemp;
    public int PartySize = 4;

    // Initialize variables
    public void Awake()
    {
        partySlot = 0;
        currentSelectionIndex = 0;
    }

    public void Start()
    {
        Debug.Assert(currentParty != null, "current party wasn't populated");
        PartyTemp = new Warrior[PartySize];
        currentParty.warriors = new List<Warrior>();
        Debug.Log("ManagerPartySelect.Start CurrentParty: " + currentParty);

    }

    // Toggling selection screen
    public void ToggleCharacterSelect(int buttonNumber)
    {
        CanvasHelper.ToggleCanvasGroup(characterSelector);
        partySlot = buttonNumber;
    }

    public void Selection(int classNumberIndex)
    {
        currentSelectionIndex = classNumberIndex;
        Debug.Log("selecting class" + currentSelectionIndex);
    }

    public void Confirm()
    {
        if (currentSelectionIndex >= allWarriors.warriors.Count)
        {
            Debug.LogError("currentSelectionIndex out of bounds");
        }
        else if (partySlot >= PartyTemp.Length)
        {
            Debug.LogError("partySlot out of bounds");
        }
        else
        {
            PartyTemp[partySlot] = allWarriors.warriors[currentSelectionIndex];
            // testing warrior log
            Debug.Log("Selected Warrior is:" + PartyTemp[partySlot]);
            Debug.Log("Warrior is supposed to be:" + allWarriors.warriors[currentSelectionIndex]);
            Debug.Log("Current party is:");
            for(int n=0; n < PartyTemp.Length; n++)
            {
                Debug.Log("\n" + PartyTemp[n]);
            }

            // Toggle selection screen
            CanvasHelper.ToggleCanvasGroup(characterSelector);
        }
    }

    public void EnterArena()
    {       
        currentParty.warriors.AddRange(PartyTemp);
    }

}
