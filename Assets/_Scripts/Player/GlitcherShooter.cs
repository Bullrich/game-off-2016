using UnityEngine;
using System.Collections.Generic;
using System;
using Glitch.Weapons;
using Glitch.DTO;
using Glitch.Interactable;
// By @JavierBullrich
namespace Glitch.Player
{
    public class GlitcherShooter : GlitcherBehaviorBase
    {
        WeaponController weapon;
        List<WeaponController> wpTotal = new List<WeaponController>();

        public override void Init(GlitcherSettingDTO glitcherDTO)
        {
            SetValues(glitcherDTO);
            _tran.GetChild(0).GetComponent<WeaponManager>().SetUp(this);
            //_tran.GetChild(0).GetComponent<WeaponManager>().AssignWeapons(1);
            Glitch.Manager.GameManagerBase.instance.UpdateLifeUI(lifePoints, maxLifePoints);
        }

        public void AssignWeapons(List<WeaponController> assignedWeapons)
        {
            wpTotal = assignedWeapons;
            weapon = wpTotal[wpTotal.Count - 1];
            weapon.gameObject.SetActive(true);
        }

        public override void Interact(IInteractable interactable)
        {
            switchObject = interactable;
        }

        public override void Update()
        {
            if (Glitch.UI.GameInterface.instance.canContinue)
            {
                if (weapon == null || weapon.canMove)
                {
                    base.Update();
                    Vector2 weaponAngle = directionalInput;
                    if (weapon != null)
                        weapon.TurnWeapon(directionalInput, wallSliding);
                }
                else
                    glAnim.Play("Idle");
            }
        }

        void SwitchWeapon()
        {
            int cIndex = wpTotal.FindIndex(a => a.GetComponent<WeaponController>() == weapon);
            Debug.Log(cIndex);
            weapon.gameObject.SetActive(false);
            if (cIndex + 1 >= wpTotal.Count)
                cIndex = 0;
            else
                cIndex++;
            weapon = wpTotal[cIndex];
            weapon.gameObject.SetActive(true);
            weapon.SwitchedWeapon(_tran.localScale.x > 1 ? true : false);
        }

        bool canMove = true;

        public override void ActionManager(KeyInput inp)
        {
            if (inp == KeyInput.Fire)
            {
                if (weapon != null)
                    weapon.Fire();
            }
            else if (inp == KeyInput.FireUp)
            {
                if (weapon != null)
                    weapon.FireUp();
            }
            else if (inp == KeyInput.Action)
                SwitchWeapon();
            else if (inp == KeyInput.Down)
            {
                Debug.Log(" DOWN ");
                if (switchObject != null && Glitch.UI.GameInterface.instance.canContinue)
                    switchObject.InteractWith();
            }
        }
    }
}