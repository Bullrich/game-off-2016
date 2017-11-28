using System.Collections;
using System.Collections.Generic;
using Glitch.DTO;
using Glitch.Interactable;
using Glitch.Manager;
using Glitch.NPC;
using UnityEngine;

// by @Bullrich
[RequireComponent(typeof(RoomManager))]
public class TileModule : MonoBehaviour
{
    [Range(0, 10)] public int ModuleDifficulty = 0;
    private Gateway[] doors;
    private RoomManager rmManager;
    public NonPlayableCharacter npc;

    private void Awake()
    {
        doors = GetComponentsInChildren<Gateway>();
        rmManager = GetComponent<RoomManager>();
        if (npc != null)
        {
            npc.characterEvent = StoryDTO.StoryEvents.PickedFirstGun;
            print("INTI!");
            print(rmManager);
        }
    }

    public void SetNPC()
    {
        if (npc.characterEvent == StoryDTO.StoryEvents.Empty)
            npc.characterEvent = StoryDTO.StoryEvents.PickedFirstGun;
        else if (npc.characterEvent == StoryDTO.StoryEvents.PickedFirstGun)
            npc.characterEvent = StoryDTO.StoryEvents.JavascriptBugs;
        else if (npc.characterEvent == StoryDTO.StoryEvents.JavascriptBugs)
            npc.characterEvent = StoryDTO.StoryEvents.Life;
    }

    public Vector3 ChangeRoom()
    {
        rmManager.ResetRoom();
        foreach(Gateway door in doors)
            door.gameObject.SetActive(true);
        int randomDoor = Random.Range(0, doors.Length);
        Vector3 doorPos = doors[randomDoor].transform.position;
        if(doors.Length > 1)
        doors[randomDoor].gameObject.SetActive(false);
        return doorPos;
    }
}
