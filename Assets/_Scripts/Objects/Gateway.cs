using UnityEngine;
using System.Collections;
using System;
using Glitch.Manager;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Glitch.Player;
// By @JavierBullrich

namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Gateway : MonoBehaviour, IInteractable
    {
        [Header("Change Location")]
        public UnityEvent targetChange;
        [Header("Change Scene")]
        public bool ChangeScene;
        public string SceneToChange;

        void UseDoor()
        {
            if (Glitch.UI.GameInterface.instance.canContinue)
            {
                if (ChangeScene)
                {
                    Glitch.UI.GameInterface.instance.ChangeBlackScreen();
                    StartCoroutine(WaitToSwitch(ChangeTheScene));
                }
                else
                {
                    Glitch.UI.GameInterface.instance.OpenBlackScreen();
                    StartCoroutine(WaitToSwitch(MovePlayer));
                }
            }
        }

        void MovePlayer()
        {
            targetChange.Invoke();
        }

        IEnumerator WaitToSwitch(Action ActionToDo)
        {
            yield return new WaitUntil(() => Glitch.UI.GameInterface.instance.canSwith);
            ActionToDo();
            transform.parent.gameObject.SetActive(false);
        }
        
        void ChangeTheScene()
        {
            SceneManager.LoadScene(SceneToChange);
        }
        
        public void InteractWith()
        {
            UseDoor();
        }

        public void TriggerEntered(GlitcherBehaviorBase glitcher)
        {

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