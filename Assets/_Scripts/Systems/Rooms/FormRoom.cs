using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Manager
{
    public class FormRoom : RoomManager
    {
        public BreakableBlock[] blocks;

        private void Start()
        {
            foreach(BreakableBlock bl in blocks)
            {
                bl.transform.gameObject.SetActive(false);
            }
        }

        public void Fight(bool active)
        {
            foreach (BreakableBlock bl in blocks)
            {
                bl.transform.gameObject.SetActive(active);
            }
        }

    }
}