using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Manager
{
    public class TableRoom : RoomManager
    {

        private void OnDisable()
        {
            int enemiesLife = 0;
            foreach (Enemy.Enemy e in enemies)
            {
                if (e.lifePoints > 0)
                    enemiesLife += e.lifePoints;
            }
            print(enemiesLife + " e Life");
            if (enemiesLife <= 0)
                GameManagerBase.instance.ExecuteStory(new DTO.StoryDTO(DTO.StoryDTO.StoryEvents.CleanedJavascriptBugs));
        }

    }
}