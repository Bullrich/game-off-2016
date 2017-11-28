using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public TileModule[] modules;
    private List<TileModule> tileModules;

    public static TilesManager instance;
    private int currentDifficulty = 0;
    private Transform player;
    public TileModule restModule;

    // Use this for initialization
    void Awake()
    {
        instance = this;
        tileModules = new List<TileModule>();
        foreach (TileModule module in modules)
        {
            GameObject moduleGO = Instantiate(module.gameObject);
            tileModules.Add(moduleGO.GetComponent<TileModule>());
        }
        tileModules.Sort((s1, s2) => s1.ModuleDifficulty.CompareTo(s2.ModuleDifficulty));
        restModule = (Instantiate(restModule.gameObject)).GetComponent<TileModule>();
        Invoke("TurnOffModules", .2f);
    }

    private void TurnOffModules()
    {
        player = Glitch.Manager.GlitchManager.instance.returnPlayer().target;
        foreach (TileModule module in tileModules)
            module.gameObject.SetActive(false);
        restModule.gameObject.SetActive(false);
    }

    public void TransportCharacter()
    {
        player.position = NewRoom();
    }

    public Vector3 NewRoom()
    {
        return nextModule().ChangeRoom();
    }

    public void ResetDifficulty()
    {
        currentDifficulty = 0;
    }

    private TileModule lastModule;

    private TileModule nextModule()
    {
        int moduleIndex = Random.Range(currentDifficulty - 1, currentDifficulty + 1);
        moduleIndex = Mathf.Clamp(moduleIndex, 0, tileModules.Count - 1);
        TileModule nextModule = tileModules[moduleIndex];
        moduleIndex++;
        if (currentDifficulty % 4 == 1)
        {
            nextModule = restModule;
            restModule.SetNPC();
        }
        currentDifficulty++;
        if (lastModule == nextModule)
            nextModule = this.nextModule();
        lastModule = nextModule;
        print(nextModule.name);
        return nextModule;
    }
}