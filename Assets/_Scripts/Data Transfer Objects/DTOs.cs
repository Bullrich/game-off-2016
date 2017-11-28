using UnityEngine;
using System.Collections;
using Glitch.Player;
// By @JavierBullrich
namespace Glitch.DTO
{
    [System.Serializable]
    public class GlitcherSettingDTO
    {
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;
        public float moveSpeed = 6;
        [Header("Wall Sliding")]
        [Tooltip("Defines if the player can slide through a wall")]
        public bool canWallSlide;
        [Tooltip("Defines if the player can Jump from a wall")]
        public bool canWallJump;
        public Vector2 wallJumpClimb;
        public Vector2 wallJumpOff;
        public Vector2 wallLeap;

        public float wallSlideSpeedMax = 3;
        public float wallStickTime = .25f;

        #region Non-Editable values
        public CustomGlitcherController2D controller;
        public Animator animator;
        public Transform tran;
        public AudioSource audio;
        #endregion
    }

    public class ChatDTO
    {
        public delegate void ChatEndEvent();
        public string chatMessage;
        public ChatEndEvent chatEvent = null;
        public string characterImage = null;

        public ChatDTO(string Message, ChatEndEvent Event = null, string SpriteImage = null)
        {
            chatMessage = Message;
            chatEvent = Event;
            characterImage = SpriteImage;
        }
    }

    public class ManagerDTO
    {
        public Transform target;
        public Object script;
        public GameObject gObject;

        public ManagerDTO(Transform t = null, Object ob = null, GameObject go = null)
        {
            target = t;
            script = ob;
            gObject = go;
        }
    }

    public class StoryDTO
    {
        public bool hasBeenCompleted;

        public StoryDTO(StoryEvents storyEvent = StoryEvents.Empty)
        {
            sEvent = storyEvent;
        }

        public enum StoryEvents
        {
            PickedFirstGun,
            JavascriptBugs,
            CleanedJavascriptBugs,
            WeirdScript,
            HtmlForm,
            HtmlCold,
            HtmlCool,
            Life,
            Empty
        }

        public enum DoorEvents
        {
            NONE,
            Form
        }

        public StoryEvents sEvent;
        public DoorEvents doorEvent;
    }


}