using UnityEngine;
using System.Collections;
using Glitch.Interactable;
using System;
using Glitch.UI;
using Glitch.DTO;
using Glitch.Player;
// By @JavierBullrich
namespace Glitch.NPC
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class NonPlayableCharacter : MonoBehaviour, IInteractable
    {
        public DTO.StoryDTO.StoryEvents characterEvent;

        public abstract void InteractWith();

        protected void Chat(ChatDTO chatMessage)
        {
            GameInterface.instance.ShowChat(chatMessage);
        }

        protected ChatDTO ChatSetup(string message, ChatDTO.ChatEndEvent chatEvent = null, string charactersprite = null)
        {
            return new ChatDTO(message, chatEvent, charactersprite);
        }

        public abstract void TriggerEntered(GlitcherBehaviorBase glitcher);

        public abstract void TriggerExit(GlitcherBehaviorBase glitcher);

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }
    }
}