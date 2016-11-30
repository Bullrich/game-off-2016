using UnityEngine;
using System.Collections;
using System;
using Glitch.Player;
// By @JavierBullrich

namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExitGateway : MonoBehaviour, IInteractable
    {
        public void InteractWith()
        {
            Glitch.UI.GameInterface.instance.OpenBlackScreen();
            StartCoroutine(WaitToSwitch(QuitGame));
        }

        IEnumerator WaitToSwitch(Action ActionToDo)
        {
            yield return new WaitUntil(() => Glitch.UI.GameInterface.instance.canSwith);
            ActionToDo();
        }

        void QuitGame()
        {
            Application.Quit();
        }

        public void TriggerEntered(GlitcherBehaviorBase glitcher)
        {
            InteractWith();
        }

        public void TriggerExit(GlitcherBehaviorBase glitcher)
        {

        }

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }
    }
}
