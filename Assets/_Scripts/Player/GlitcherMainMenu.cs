using UnityEngine;
using System.Collections;
using Glitch.Player;
using Glitch.Interactable;
using System;
using Glitch.DTO;
// By @JavierBullrich

public class GlitcherMainMenu : GlitcherBehaviorBase
{
    public override void ActionManager(KeyInput inp)
    {
        if (inp == KeyInput.Down)
        {
            if (switchObject != null)
                switchObject.InteractWith();
        }
    }

    public override void Init(GlitcherSettingDTO glitcherDTO)
    {
        SetValues(glitcherDTO);
    }

    public override void Interact(IInteractable interactable)
    {
        switchObject = interactable;
    }
}
