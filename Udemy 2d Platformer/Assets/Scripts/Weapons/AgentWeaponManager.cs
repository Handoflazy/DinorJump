using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class AgentWeaponManager : PlayerSystem
    {
        SpriteRenderer spriteRenderer;
        private WeaponStorage weaponStorage;
        public UnityEvent<Sprite> OnWeaponSwap;
        public UnityEvent OnMulipleWeapons;
        public UnityEvent OnWeaponPickUp;
        protected override void Awake()
        {
            base.Awake();
            weaponStorage  = new WeaponStorage();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ToggleWeaponVisiblity(false);
        }
        public void ToggleWeaponVisiblity(bool v)
        {
            if (v)
            {
                SwapWeaponSprite(GetCurrentWeapon().weaponSprite);

            }
            spriteRenderer.enabled = v;
        }

        public WeaponData GetCurrentWeapon()
        {
            return weaponStorage.GetCurrentWeapon();
        }

        public void SwapWeaponSprite(Sprite weaponSprite)
        {
            spriteRenderer.sprite = weaponSprite;
            OnWeaponSwap?.Invoke(weaponSprite);
        }
        public void SwapWeapon()
        {
            if(weaponStorage.WeaponCount < 0)
            {
                return;
            }
            SwapWeaponSprite(weaponStorage.SwapWeapon().weaponSprite);
        }
        public void AddWeaponData(WeaponData weaponData)
        {
            weaponStorage.AddWeaponData(weaponData);
            if(weaponStorage.WeaponCount ==2)
                OnMulipleWeapons?.Invoke();
            SwapWeaponSprite(weaponData.weaponSprite);
        }
        public void PickUpWeapon(WeaponData weaponData)
        {
            AddWeaponData(weaponData);
            OnWeaponPickUp?.Invoke();
        }
        public bool CanIUseWeapon(bool isGrounded)
        {
            if(weaponStorage.WeaponCount <= 0) {
                return false;
            }
            return weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded);
        }
        public List<String> GetPlayerWeaponNames()
        {
            return weaponStorage.GetPlayerWeaponNames();
        }
        
    }
}