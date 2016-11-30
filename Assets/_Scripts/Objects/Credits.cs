using UnityEngine;
using System.Collections;
using Glitch.Player;
using System;
// By @JavierBullrich

public class Credits : MonoBehaviour, Glitch.Interactable.IInteractable
{
    public GameObject credits;

    public void InteractWith()
    {
        print("called");
    }

    private void ShowCredits(bool show)
    {
        credits.gameObject.SetActive(show);
        if (credits.activeInHierarchy)
            Invoke("Close", 2f);
    }

    void Close()
    {
        if(credits.activeInHierarchy)
        credits.gameObject.SetActive(false);
    }

    public GameObject ReturnGameobject()
    {
        return gameObject;
    }

    public void TriggerEntered(GlitcherBehaviorBase glitcher)
    {
        ShowCredits(true);
    }

    public void TriggerExit(GlitcherBehaviorBase glitcher)
    {
    }
}
