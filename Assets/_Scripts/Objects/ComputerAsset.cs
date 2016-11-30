using UnityEngine;
using System.Collections;
using System;
using Glitch.Player;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ComputerAsset : MonoBehaviour, IInteractable
    {

        public void InteractWith()
        {
            print("Computer!");
            SaveLocation();
            Glitch.UI.GameInterface.instance.ShowChat(new DTO.ChatDTO("Server.Response"));
        }

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }

        private void SaveLocation()
        {
            GameManagerBase.instance.gameObject.GetComponent<GlitchManager>().SetNewRoom(transform.parent.GetComponent<RoomManager>());
        }

        public void TriggerEntered(GlitcherBehaviorBase glitcher)
        {

        }

        public void TriggerExit(GlitcherBehaviorBase glitcher)
        {

        }
    }
}