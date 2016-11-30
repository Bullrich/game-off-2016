using UnityEngine;
using System.Collections;
using Glitch.Interactable;
using Glitch.Player;
using System;
using UnityEngine.Events;
// By @JavierBullrich
namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FacebookGateway : MonoBehaviour, IInteractable
    {
        [Header("Change Location")]
        public UnityEvent targetChange;
        Animator anim;
        bool playerInTrigger;
        public DTO.StoryDTO.DoorEvents doorEvent = DTO.StoryDTO.DoorEvents.NONE;
        DTO.StoryDTO dto = new DTO.StoryDTO();

        private void Start()
        {
            anim = GetComponent<Animator>();
            dto.doorEvent = doorEvent;
        }

        public void InteractWith()
        {
            if(doorEvent==DTO.StoryDTO.DoorEvents.NONE || canExecute())
            UseDoor();
        }

        bool canExecute()
        {
            return Glitch.Manager.GameManagerBase.instance.canExecuteStory(dto);
        }

        void UseDoor()
        {
            if (Glitch.UI.GameInterface.instance.canContinue)
            {
                Glitch.UI.GameInterface.instance.OpenBlackScreen();
                StartCoroutine(WaitToSwitch(MovePlayer));
            }
        }

        IEnumerator WaitToSwitch(Action ActionToDo)
        {
            yield return new WaitUntil(() => Glitch.UI.GameInterface.instance.canSwith);
            ActionToDo();
            transform.parent.gameObject.SetActive(false);
        }

        void AnimationManager()
        {
            string animationToPlay, animationToCheck;
            if (playerInTrigger)
            {
                animationToCheck = "PopUpCloseIdle";
                animationToPlay = "PopUpShow";
            }else
            {
                animationToCheck = "PopupOpen";
                animationToPlay = "PopupGatewayClose";
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationToCheck))
                anim.Play(animationToPlay);
        }

        void MovePlayer()
        {
            targetChange.Invoke();
        }

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }

        private void Update()
        {
            AnimationManager();
        }

        public void TriggerEntered(GlitcherBehaviorBase glitcher)
        {
            if (doorEvent == DTO.StoryDTO.DoorEvents.NONE || canExecute())
                playerInTrigger = true;
        }

        public void TriggerExit(GlitcherBehaviorBase glitcher)
        {
            playerInTrigger = false;
        }
    }
}