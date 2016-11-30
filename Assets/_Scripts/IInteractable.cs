using UnityEngine;
using Glitch.Player;
// By @JavierBullrich
namespace Glitch.Interactable
{
    public interface IInteractable
    {
        void InteractWith();

        void TriggerEntered(GlitcherBehaviorBase glitcher);
        void TriggerExit(GlitcherBehaviorBase glitcher);
        GameObject ReturnGameobject();
    }
}
