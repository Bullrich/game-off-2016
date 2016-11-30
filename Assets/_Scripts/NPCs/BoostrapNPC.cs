using Glitch.Manager;

// By @JavierBullrich
namespace Glitch.NPC
{
    public class BoostrapNPC : NonPlayableCharacter
    {

        public override void InteractWith()
        {
            GameManagerBase.instance.ExecuteStory(new DTO.StoryDTO(characterEvent));
        }

        public override void TriggerEntered(Glitch.Player.GlitcherBehaviorBase glitcher)
        {

        }

        public override void TriggerExit(Glitch.Player.GlitcherBehaviorBase glitcher)
        {

        }
    }
}
