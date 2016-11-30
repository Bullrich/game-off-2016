using UnityEngine;
using System.Collections;
using Glitch.Manager;
using Glitch.DTO;
using System;
// By @JavierBullrich

public class MainMenuManager : GameManagerBase
{
    public Glitch.Player.GlitcherController glitcher;
    public GameObject credits;

    public override ManagerDTO returnPlayer()
    {
        return new ManagerDTO(glitcher.transform);
    }

    public override void Awake()
    {
        instance = this;
        base.Awake();
    }

    public override void ExecuteStory(StoryDTO storyDto)
    {
        throw new NotImplementedException();
    }

    public void ShowCredits()
    {
        print("called");
        credits.SetActive(true);
    }

    public override bool canExecuteStory(StoryDTO storyDto)
    {
        throw new NotImplementedException();
    }
}
