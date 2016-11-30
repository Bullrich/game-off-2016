using System;
using UnityEngine;
using Glitch.Player;
// By @JavierBullrich
namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Popup : MonoBehaviour, IInteractable
    {
        public string PopUpMessage;

        public void InteractWith()
        {
            print("Called");
            UI.GameInterface.instance.ShowChat(new DTO.ChatDTO(PopUpMessage));
        }

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }

        public void TriggerEntered(GlitcherBehaviorBase glitcher)
        {

        }

        public void TriggerExit(GlitcherBehaviorBase glitcher)
        {

        }
    }
}