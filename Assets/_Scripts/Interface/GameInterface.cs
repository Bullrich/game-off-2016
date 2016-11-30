using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using BlueFlow.Util;
using Glitch.DTO;
using Glitch.Manager;
// By @JavierBullrich

namespace Glitch.UI
{
    public class GameInterface : MonoBehaviour
    {
        public static GameInterface instance;

        public Image lifeBar;
        public TextAsset csvFile;
        public Transform chatPanel;
        private Dictionary<string, string> chatDictionary;
        private ChatSystem chatSys;
        [HideInInspector]
        public bool canContinue = true, canSwith = false;
        BlackScreenInterface blackScreen;

        public CharacterImage[] charImage;

        void Start()
        {
            instance = this;
            canContinue = true;
            SetChatSystem(chatPanel);
            chatPanel.gameObject.SetActive(false);
            CSVParser csvPar = new CSVParser();
            chatDictionary = csvPar.SetDictionary(csvFile);
            GameManagerBase.instance.SuscribeUI(UpdateLifePoints);
            SetBlackScreen();
        }

        void SetBlackScreen()
        {
            foreach (Transform t in transform)
            {
                if (t.GetComponent<BlackScreenInterface>() != null)
                    blackScreen = t.GetComponent<BlackScreenInterface>();
            }

            ManagerDTO blackDTO = new ManagerDTO();
            blackDTO.script = this;
            blackScreen.SetUp(blackDTO);
        }

        public void OpenBlackScreen()
        {
            blackScreen.ShowBlackScreen();
        }

        public void ChangeBlackScreen()
        {
            blackScreen.KeepScreenChange();
        }

        #region Public methods

        public void ContinueChat()
        {
            canSwith = true;
        }
        public delegate void ChatEndEvent();

        public void ShowChat(ChatDTO chatDto)
        {
            string messageToSend;
            if (!chatDictionary.ContainsKey(chatDto.chatMessage))
                messageToSend = "Error, message doesn't exist: " + chatDto.chatMessage;
            else
                messageToSend = chatDictionary[chatDto.chatMessage];
            Text chatText;
            if (chatDto.characterImage == null)
            {
                chatSys.SetNormalChat(true);
                chatText = chatSys.normalChat;
            }
            else
            {
                chatSys.SetNormalChat(false);
                foreach (CharacterImage chImg in charImage)
                {
                    if (chImg.CharacterName == chatDto.characterImage)
                        chatSys.characterImage.sprite = chImg.ProfilePicture;
                }
                chatText = chatSys.charaterChat;
            }
            StartCoroutine(TypeText(chatText, messageToSend, 0.01f, chatDto));
        }

        void SetChatSystem(Transform chatP)
        {
            chatSys.normalChat = chatP.GetChild(0).GetComponent<Text>();
            chatSys.charaterChat = chatP.GetChild(1).GetChild(0).GetComponent<Text>();
            chatSys.characterImage = chatP.GetChild(1).GetChild(1).GetComponent<Image>();
        }
        #endregion

        IEnumerator TypeText(Text textComp, string message, float letterPause, ChatDTO chatDto)
        {
            canSwith = false;
            canContinue = false;
            GameManagerBase.instance.PauseGame(true);
            textComp.text = "";
            message = message.Replace("<br>", "\n");
            foreach (char letter in message.ToCharArray())
            {
                textComp.text += letter;
                yield return 0;
                yield return new WaitForSeconds(letterPause);
            }
            while (!canSwith)
            {
                yield return new WaitForSeconds(0.5f);
                textComp.text = message + " —";
                yield return new WaitForSeconds(0.5f);
                textComp.text = message;
            }
            chatSys.CloseAll();
            canContinue = true;
            GameManagerBase.instance.PauseGame(false);
            if (chatDto.chatEvent != null)
                chatDto.chatEvent();

        }

        public void UpdateLifePoints(int lifePoints, int maxLifePoints)
        {
            float fill = ((float)lifePoints / (float)maxLifePoints);
            print(lifePoints + "/" + maxLifePoints + "=" + fill);
            lifeBar.fillAmount = fill;
        }

    struct ChatSystem
        {
            public Text normalChat, charaterChat;
            public Image characterImage;

            public void SetNormalChat(bool normal)
            {
                normalChat.transform.parent.gameObject.SetActive(true);
                normalChat.gameObject.SetActive(normal);
                characterImage.gameObject.SetActive(!normal);
                charaterChat.gameObject.SetActive(!normal);
            }

            public void CloseAll()
            {
                normalChat.transform.parent.gameObject.SetActive(false);
            }
        }

        [System.Serializable]
        public struct CharacterImage
        {
            public string CharacterName;
            public Sprite ProfilePicture;
        }
    }
}