using System.Collections;
using TMPro;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.IO;
using System;

namespace GrabCoin.Services.Chat
{
    public class ChatWindow : MonoBehaviour
    {
        [SerializeField, Tooltip("Is chat open when the scene starts")]
        private bool chatIsOpen = false;

        [SerializeField, Tooltip("Write messages to a file")]
        private bool logging = false;

        [Header("UI")]
        [SerializeField]
        private TMP_InputField inputFieldComp = null;
        [SerializeField]
        private Transform content = null;
        [SerializeField]
        private Scrollbar scrollbar = null;
        [SerializeField]
        private Animation _animation = null;

        [SerializeField]
        private TypesOfMessages messageTypes = new TypesOfMessages();
        public void Awake()
        {
            PlayerNetworkHandler.OnMessage += OnPlayerMessage;
        }

        private void Start()
        {
            if (chatIsOpen)
                OpenChat();
            else
            {
                _animation.Play("CloseChat");
                chatIsOpen = false;

                DisableInputField();
            }
        }

        public void OpenChat()
        {
            _animation.Play("OpenChat");
            chatIsOpen = true;

            EnableInputField();
        }

        public void CloseChat()
        {
            if (chatIsOpen)
            {
                _animation.Play("CloseChat");
                chatIsOpen = false;

                DisableInputField();
            }
        }

        public void AddSystemMessage(string text, int effect, float duration = 0f)
        {
            if (effect < 0)
            {
                AppendMessage(text, messageTypes.Warning);
            }
            else if (effect > 0)
            {
                AppendMessage(text, messageTypes.Notification);
            }
            else
            {
                AppendMessage(text, messageTypes.Process, duration);
            }
        }

        public void OnSendMessage()
        {
            if (inputFieldComp.text.Trim() == "")
                return;

            // Get our player.
            PlayerNetworkHandler player = NetworkClient.connection.identity.GetComponent<PlayerNetworkHandler>();

            // Send a message.
            player.CmdSend(inputFieldComp.text.Trim());

            inputFieldComp.text = "";

            EnableInputField();
        }

        private void OnPlayerMessage(PlayerNetworkHandler player, string message)
        {
            string prettyMessage = $"{player.playerName}: {message}";

            player.ShowMessageBilboard(message);

            if (player.isLocalPlayer)
                AppendMessage(prettyMessage, messageTypes.Message);
            else
                AppendMessage(prettyMessage, messageTypes.OpponentMessage);

            Debug.Log(prettyMessage);
        }

        private void EnableInputField()
        {
            inputFieldComp.readOnly = false;
            inputFieldComp.Select();
            inputFieldComp.ActivateInputField();
        }

        private void DisableInputField()
        {
            inputFieldComp.readOnly = true;
            inputFieldComp.DeactivateInputField();
        }

        internal void AppendMessage(string message, MessageType type, float duration = 0f)
        {
            StartCoroutine(AppendAndScroll(message, type, duration));
        }

        private IEnumerator AppendAndScroll(string message, MessageType type, float duration = 0f)
        {
            if (type == messageTypes.Process)
                CreateProcess(message, duration);
            else
                CreateMessage(message, type);

            // It takes 2 frames for the UI to update?!?!
            yield return null;
            yield return null;

            scrollbar.value = 0;
        }

        private void CreateMessage(string text, MessageType type)
        {
            Message message = new Message
            {
                Text = text,
                Time = DateTime.UtcNow.ToString("HH:mm"),
                Type = type
            };

            var obj = Instantiate(message.Type.Prefab, content);
            var messageComp = obj.GetComponent<MessageComponent>();
            messageComp.Init(message);

            if (logging)
                SaveMessage(message);
        }

        private void CreateProcess(string text, float duration)
        {
            Process process = new Process
            {
                Text = text,
                Duration = duration,
                Time = DateTime.UtcNow.ToString("HH:mm"),
                CompletionTime = FormattedTime(0 + duration),
                Type = messageTypes.Process
            };

            var obj = Instantiate(process.Type.Prefab, content);
            var processComp = obj.GetComponent<ProcessComponent>();
            processComp.Init(process);

            if (logging)
                SaveProcess(process);
        }

        private string FormattedTime(float timer)
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = Mathf.Floor(timer % 60).ToString("00");
            string time = string.Format("{0}:{1}", minutes, seconds);

            return time;
        }

        #region Save to file

        private static string fileName = string.Format("{0}_{1}_{2}_{3}_{4}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute) + ".txt";

        private void SaveMessage(Message message)
        {
            string line = message.Time + " - " + message.Text;

            StreamWriter sw = new StreamWriter(Application.dataPath + "/Logs/" + fileName);
            sw.WriteLine(line);
            sw.Close();
        }

        private void SaveProcess(Process process)
        {
            string line = process.CompletionTime + " - " + process.Text;

            StreamWriter sw = new StreamWriter(Application.dataPath + "/Logs/" + fileName);
            sw.WriteLine(line);
            sw.Close();
        }

        #endregion
    }
}

