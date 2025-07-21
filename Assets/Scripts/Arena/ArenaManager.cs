using UnityEngine;
using System;
using System.Diagnostics;
//using UnityEngine.SceneManagement;
using PersistentData;
using Debug = UnityEngine.Debug;

namespace Arena
{
    public class ArenaManager : MonoBehaviour
    {
        public IntReference currentlySelectedBossIndex;

        public Party currentParty;

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
            Debug.Log("ArenaManager awake start");
            _messageTimer = new Stopwatch();
            _introMessageDisplayTime = TimeSpan.FromSeconds(introMessageDisplayTimeInSeconds);
            _nextMessageDisplayTime = TimeSpan.FromSeconds(nextMessageDisplayTimeInSeconds);
            _gameOverMessageDisplayTime = TimeSpan.FromSeconds(gameOverMessageDisplayTimeInSeconds);
            _currentState = ArenaState.Awake;

            // TODO: Remove this once character select is working
            //var characterData = ScriptableObject.CreateInstance<CharacterData>();
            //characterData.
            //CharacterData[0] = Instantiate();

            Debug.Log("ArenaManager awake end");
        }

        private void Start()
        {
            Debug.Log("ArenaManager start start");
            Debug.Log("currentlySelectedBossIndex: " + currentlySelectedBossIndex.Value);

            if ((currentParty?.warriors?.Length ?? 0) == 0)
            {
                Debug.LogError("currentParty is not populated.");
            }
            else
            {
                Debug.Log("currentParty: " + currentParty);
            }
            Debug.Log("ArenaManager start end");
        }

        private void Update()
        {
            switch (_currentState)
            {
                case ArenaState.Awake:
                    _messageTimer.Start();
                    introMessage.enabled = true;
                    _currentState = ArenaState.DisplayingIntroMessage;
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

