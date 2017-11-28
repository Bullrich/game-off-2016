using UnityEngine;
using System.Collections;
using Glitch.Player;
using Glitch.DTO;
using Glitch.Story;
// By @JavierBullrich
namespace Glitch.Manager {
    [RequireComponent(typeof(AudioSource))]
    public class GlitchManager : GameManagerBase
    {
        public GlitcherController glController;
        public RoomManager startingRoom;
        int playerLifePoints;
        UIPresentation story;
        public LostScreenMenu lostScreen;

        public override void Awake()
        {
            base.Awake();
            instance = this;
            InitManager();
            story = new UIPresentation();
            StartCoroutine(StartLevel());
            audioS = Camera.main.GetComponent<AudioSource>();
        }

        public void RestartGame()
        {
            StartCoroutine(StartLevel());
            glController.gameObject.SetActive(true);
            glController.getBehavior().Restart(playerLifePoints);
        }

        public override void ExecuteStory(StoryDTO storyDto)
        {
            story.ExecuteEvent(storyDto);
        }

        public override bool canExecuteStory(StoryDTO storyDto)
        {
            return story.CanExecute(storyDto);
        }

        public void Died()
        {
            lostScreen.gameObject.SetActive(true);
            TilesManager.instance.ResetDifficulty();
            PlaySfx(SoundManager.Sfx.heroDeath);
        }

        public override ManagerDTO returnPlayer()
        {
            ManagerDTO dto = new ManagerDTO();
            dto.gObject = glController.transform.gameObject;
            dto.script = glController;
            dto.target = glController.transform;
            return dto;
        }

        public void SetNewRoom(RoomManager room)
        {
            startingRoom = room;
            playerLifePoints = glController.getBehavior().lifePoints;
            print(playerLifePoints);
        }

        IEnumerator StartLevel()
        {
            yield return new WaitForEndOfFrame();
            startingRoom.gameObject.SetActive(true);
            startingRoom.TransportCharacter(startingRoom.transform.GetChild(0));
            playerLifePoints = glController.getBehavior().lifePoints;
        }
    }
}
