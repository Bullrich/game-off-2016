using UnityEngine;
using System.Collections.Generic;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponController[] weapons;
        List<WeaponController> ListOfWeapons;
        Glitch.Player.GlitcherShooter shooterController;
        List<int> currentWeapons = new List<int>();

        void Awake()
        {
            ListOfWeapons = weaponsList();
        }

        public void SetUp(Glitch.Player.GlitcherShooter shooter)
        {
            shooterController = shooter;
            //AssignWeapons(2);
        }

        /// <summary>Add a new weapon to the player's current inventory</summary>
        public void AssignWeapons(int newWeapon)
        {
            if (!currentWeapons.Contains(newWeapon))
                currentWeapons.Add(newWeapon);
            print(currentWeapons.Count + " weapon");
            shooterController.AssignWeapons(UpdateWeaponList(currentWeapons.ToArray()));
        }

        List<WeaponController> weaponsList()
        {
            List<WeaponController> wp = new List<WeaponController>();
            foreach (WeaponController wep in weapons)
            {
                WeaponController newWeapon = Instantiate(wep);
                Vector3 position = wep.transform.localPosition;
                newWeapon.transform.parent = transform.parent;
                wp.Add(newWeapon);
                newWeapon.transform.localPosition = position;
                newWeapon.gameObject.SetActive(false);
            }
            return wp;
        }

        List<WeaponController> UpdateWeaponList(int[] weaponIds)
        {
            List<WeaponController> wp = new List<WeaponController>();
            foreach (WeaponController wep in ListOfWeapons)
            {
                foreach (int i in weaponIds)
                {
                    if (wep.WeaponId == i)
                        wp.Add(wep);
                }
            }
            return wp;
        }

        void DeleteSelf()
        {
            Destroy(gameObject);
        }

    }
}