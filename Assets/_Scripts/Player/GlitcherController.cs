using UnityEngine;
using System.Collections;
using Glitch.DTO;
using Glitch.UI;

namespace Glitch.Player
{
    [RequireComponent(typeof(CustomGlitcherController2D))]
    [RequireComponent(typeof(AudioSource))]
    public class GlitcherController : MonoBehaviour
    {
        GlitcherBehaviorBase glitcher;
        CustomGlitcherController2D glitcherController2D;
        [SerializeField]
        public GlitcherSettingDTO settings;
        GlitcherAnimations glAnim;
        Animator anim;

        public enum LevelBehaviour
        {
            MainMenu,
            Shooter
        }
        public LevelBehaviour lvlBeavior;

        void Start()
        {
            anim = GetComponent<Animator>();
            glitcherController2D = GetComponent<CustomGlitcherController2D>();
            Init();
        }

        public GlitcherBehaviorBase getBehavior()
        {
            return glitcher;
        }

        void Init()
        {
            //glitcher = new GlitcherShooter(settings);
            if (lvlBeavior == LevelBehaviour.MainMenu)
                glitcher = new GlitcherMainMenu();
            else
                glitcher = new GlitcherShooter();
            glitcher.Init(settings);
            glitcherController2D.Subscribe(glitcher.Interact);
        }


        void Update()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Glitcher.TeleportIn"))
                glitcher.Update();
            InputHandler();
        }

        bool inputDown;
        void InputHandler()
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            glitcher.SetDirectionalInput(directionalInput);

            if (Input.GetButtonDown("Jump"))
                glitcher.OnJumpInputDown();
            if (Input.GetButtonUp("Jump"))
            {
                glitcher.OnJumpInputUp();
                GameInterface.instance.ContinueChat();
            }
            if (Input.GetButton("Fire"))
                glitcher.ActionManager(GlitcherBehaviorBase.KeyInput.Fire);
            else if (Input.GetButtonUp("Fire"))
                glitcher.ActionManager(GlitcherBehaviorBase.KeyInput.FireUp);
            else if (Input.GetButtonDown("Action"))
                glitcher.ActionManager(GlitcherBehaviorBase.KeyInput.Action);
            else if (Input.GetAxisRaw("Vertical") < -0.9f && !inputDown)
            {
                glitcher.ActionManager(GlitcherBehaviorBase.KeyInput.Down);
                inputDown = true;
            }
            else if (Input.GetAxisRaw("Vertical") > -0.2f && inputDown)
                inputDown = false;
        }

    }
}