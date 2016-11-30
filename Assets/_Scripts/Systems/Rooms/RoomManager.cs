using UnityEngine;
using System.Collections.Generic;
using Glitch.Enemy;
// By @JavierBullrich
namespace Glitch.Manager
{
    public class RoomManager : MonoBehaviour
    {
        public bool hasStaticCamera;
        public Transform CameraFocus;
        public Enemy.Enemy[] enemies;

        public virtual void Awake()
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

            gameObject.SetActive(false);
        }

        public void TransportCharacter(Transform target)
        {
            gameObject.SetActive(true);
            Glitch.Manager.GlitchManager.instance.returnPlayer().target.position = target.position;
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