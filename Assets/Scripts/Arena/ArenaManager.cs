using UnityEngine;
using System;
using System.Diagnostics;
using PersistentData;
using PersistentData.Bosses;
using PersistentData.Warriors;
using Debug = UnityEngine.Debug;

namespace Arena
{
    public class ArenaManager : MonoBehaviour
    {
        public IntReference currentlySelectedBossIndex;

        public CurrentBoss currentBoss;

        public IntVariable  currentBossHealth;

        public Party currentParty;

        public Warrior currentWarrior;

        public Canvas introMessage;

        public int introMessageDisplayTimeInSeconds = 2;

        public Canvas nextMessage;

        public int nextMessageDisplayTimeInSeconds = 2;

        public Canvas gameOverMessage;

        /// <summary>
        /// TODO: Do we display this for a duration or until a button press?
        /// </summary>
        public int gameOverMessageDisplayTimeInSeconds = 2;

        public Canvas results;

        /// <summary>
        /// TODO: probably replace with button
        /// </summary>
        public int resultsDisplayTimeInSeconds = 2;

        private ArenaState _currentState;
        private Stopwatch _messageTimer;
        private TimeSpan _introMessageDisplayTime;
        private TimeSpan _nextMessageDisplayTime;
        private TimeSpan _gameOverMessageDisplayTime;

        private void Awake()
        {
            Debug.Log("ArenaManager Awake start");
            _messageTimer = new Stopwatch();
            _introMessageDisplayTime = TimeSpan.FromSeconds(introMessageDisplayTimeInSeconds);
            _nextMessageDisplayTime = TimeSpan.FromSeconds(nextMessageDisplayTimeInSeconds);
            _gameOverMessageDisplayTime = TimeSpan.FromSeconds(gameOverMessageDisplayTimeInSeconds);
            _currentState = ArenaState.Awake;

            Debug.Log("ArenaManager Awake end");
        }

        private void Start()
        {
            Debug.Log("ArenaManager Start start");

            if ((currentParty?.warriors?.Count ?? 0) == 0)
            {
                Debug.LogError(nameof(currentParty) + " is not populated.");
            }
            else
            {
                Debug.Log("currentParty: " + currentParty);
                currentWarrior.SetValues(currentParty.warriors[0]);
                currentWarrior.currentHealth.Value = currentWarrior.maxHealth.Value;
                Debug.Log("currentWarrior: " + currentWarrior);
                currentParty.warriors.RemoveAt(0);
            }

            if (!currentBoss)
            {
                Debug.LogError(nameof(currentBoss) + " is not populated.");
            }
            else
            {
                Debug.Log("currentBoss: " + currentBoss);
            }

            if (currentBoss?.maxHealth == null)
            {
                Debug.LogError(nameof(currentBoss.maxHealth) + " is null.");
            }
            else
            {
                Debug.Log("Setting current health to: " + currentBoss.maxHealth.Value / 2);
                currentBossHealth.value = currentBoss.maxHealth.Value / 2;
            }

            Debug.Log("ArenaManager Start end");
        }

        private void Update()
        {
            switch (_currentState)
            {
                case ArenaState.Awake:
                    _messageTimer.Start();
                    introMessage.enabled = true;
                    _currentState = ArenaState.DisplayingIntroMessage;
                    // TODO: spawn player paused
                    // TODO: spawn boss paused
                    break;
                case ArenaState.DisplayingIntroMessage:
                    if (_messageTimer.Elapsed > _introMessageDisplayTime)
                    {
                        _messageTimer.Stop();
                        introMessage.enabled = false;
                        _currentState = ArenaState.CombatStart;
                    }

                    break;
                case ArenaState.CombatStart:
                case ArenaState.Unknown:
                default:
                    // UnityEngine.Debug.LogWarning("Unexpected state: " + _currentState);
                    // SceneManager.LoadScene(SceneData.MainMenuSceneIndex);
                    break;
            }
        }

        private enum ArenaState
        {
            Unknown = 0,
            Awake,
            DisplayingIntroMessage,
            CombatStart,
            CombatPaused,
            CharacterDeath,
            DisplayingNextMessage,
            DisplayingGameOverMessage,
            DisplayingResults,
            Finished
        }
    }
}
