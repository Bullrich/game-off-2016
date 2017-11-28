using UnityEngine;
using System.Collections.Generic;
using Glitch.Enemy;
using Glitch.NPC;

// By @JavierBullrich
namespace Glitch.Manager
{
    public class RoomManager : MonoBehaviour
    {
        public bool hasStaticCamera;
        public Transform CameraFocus;
        public Enemy.Enemy[] enemies;

        public void Awake()
        {
            List<Enemy.Enemy> lEnemies = new List<Enemy.Enemy>();
            foreach(Transform t in transform)
            {
                if(t.gameObject.layer == 10)
                {
                    lEnemies.Add(t.GetComponent<Enemy.Enemy>());
                }
            }
            enemies = lEnemies.ToArray();
        }

        public void TransportCharacter(Transform target)
        {
            ResetRoom();
            Glitch.Manager.GlitchManager.instance.returnPlayer().target.position = target.position;
        }

        public void ResetRoom()
        {
            gameObject.SetActive(true);
            if(hasStaticCamera)
                GameManagerBase.instance.ChangeCameraFocus(CameraFocus);
            else
                GameManagerBase.instance.ChangeCameraFocus(null);
            
            foreach(Enemy.Enemy e in enemies)
            {
                e.ResetEnemy();
            }
        }

    }
}