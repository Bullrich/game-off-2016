using UnityEngine;
using System.Collections;
using Glitch.DTO;
using System.Collections.Generic;
using Glitch.Weapons;
using Glitch.UI;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Manager
{
    // I'll be honest, this class is a mess. I have to rewrite it into something more "correct".
    public abstract class StoryManager : MonoBehaviour
    {
        Dictionary<string, StoryDTO> StoryEvents;

        protected void Chat(ChatDTO chatMessage)
        {
            GameInterface.instance.ShowChat(chatMessage);
        }
        protected ChatDTO ChatSetup(string message, ChatDTO.ChatEndEvent chatEvent = null, string charactersprite = null)
        {
            return new ChatDTO(message, chatEvent, charactersprite);
        }

        public abstract bool CanExecute(StoryDTO dto);

        public abstract void ExecuteEvent(StoryDTO dto);
    }
}

namespace Glitch.Story
{
    public class UIPresentation : Manager.StoryManager
    {
        public bool pickedFirstGun, cleanedJavascriptBugs, gotDivPermision;
        int weaponToAssign;

        void PickedFirstGun(string npcMessage)
        {
            if (!pickedFirstGun)
            {
                Chat(ChatSetup(npcMessage, GotConsoleWeapon, "Bootstrap"));
                 pickedFirstGun = true;
            }
            else
                Chat(ChatSetup("Bootstrap.TakeWeapon2", null, "Bootstrap"));
        }
        private void GotConsoleWeapon()
        {
            weaponToAssign = 1;
            GameManagerBase.instance.PlaySfx(SoundManager.Sfx.pickup);
            Chat(ChatSetup("Weapon.Console", AssignNewWeaponToPlayer));
        }
        private void GotShotgunWeapon()
        {
            weaponToAssign = 2;
            GameManagerBase.instance.PlaySfx(SoundManager.Sfx.pickup);
            Chat(ChatSetup("Weapon.Shotgun", AssignNewWeaponToPlayer));
        }
        int weirdChat = 1;
        private void WeirdScript()
        {
            string weirdbootstrapChat = "Bootstrap.Weird" + weirdChat;
            Chat(ChatSetup(weirdbootstrapChat, null, "Bootstrap"));
            weirdChat++;
            if (weirdChat >= 7)
                weirdChat = 1;
        }

        private void BugsWithJavascript()
        {
            Chat(ChatSetup("Html.Bugs"));
            if(cleanedJavascriptBugs)
            Chat(ChatSetup("Html.BugsCleaned", GotShotgunWeapon));
        }

        private void FormDoorman()
        {
            string message = "Html.Form";
            if (gotDivPermision && cleanedJavascriptBugs)
                message = "Html.FormApproved";
            Chat(ChatSetup(message, null, "Html"));   
        }

        void AssignNewWeaponToPlayer()
        {
            GameManagerBase.instance.returnPlayer().target.GetChild(0).GetComponent<WeaponManager>().AssignWeapons(weaponToAssign);
        }

        private void ColdConversation(bool isCold)
        {
            string message, portrait;
            if (isCold)
            {
                if (gotDivPermision)
                    message = "Html.ColdBad";
                else
                    message = "Html.Cold";
                portrait = message;
            }
            else
            {
                if (gotDivPermision)
                    message = "Html.CoolBad";
                else
                    message = "Html.Cool";
                portrait = message;
                gotDivPermision = true;
            }
            Chat(ChatSetup(message, null, portrait));
        }

        public override void ExecuteEvent(StoryDTO dto)
        {
            switch (dto.sEvent)
            {
                case StoryDTO.StoryEvents.PickedFirstGun:
                    PickedFirstGun("Bootstrap.TakeWeapon");
                    break;
                case StoryDTO.StoryEvents.JavascriptBugs:
                    BugsWithJavascript();
                    break;
                case StoryDTO.StoryEvents.HtmlForm:
                    FormDoorman();
                    break;
                case StoryDTO.StoryEvents.CleanedJavascriptBugs:
                    cleanedJavascriptBugs = true;
                    break;
                case StoryDTO.StoryEvents.WeirdScript:
                    WeirdScript();
                    break;
                case StoryDTO.StoryEvents.HtmlCold:
                    ColdConversation(true);
                    break;
                case StoryDTO.StoryEvents.HtmlCool:
                    ColdConversation(false);
                    break;
                default:
                    Debug.LogWarning("This event isn't registered!");
                    break;
            }
        }

        public override bool CanExecute(StoryDTO dto)
        {
            bool executeEvent = false;
            switch (dto.doorEvent)
            {
                case StoryDTO.DoorEvents.NONE:
                    executeEvent = true;
                    break;
                case StoryDTO.DoorEvents.Form:
                    if (gotDivPermision && cleanedJavascriptBugs)
                        executeEvent = true;
                    break;
                default:
                    break;
            }
            return executeEvent;
        }
    }
}
