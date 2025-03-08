using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
namespace WeaponSystem
{
    public class AgentWeaponManager : AgentSystem
    {
        SpriteRenderer spriteRenderer;
        private WeaponStorage weaponStorage;
        public UnityEvent<Sprite> OnWeaponSwap;

        public UnityEvent OnWeaponPickUp;
        protected override void Awake()
        {
            base.Awake();
            weaponStorage = new WeaponStorage();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ToggleWeaponVisiblity(false);
        }
        private void OnEnable()
        {
            Agent.ID.PlayerEvents.OnWeaponChange += SwapWeapon;
        }
        private void OnDisable()
        {
            Agent.ID.PlayerEvents.OnWeaponChange -= SwapWeapon;
        }

        public void ToggleWeaponVisiblity(bool v)
        {
            if (!spriteRenderer)
                return;
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
            if (spriteRenderer)
            {
                spriteRenderer.sprite = weaponSprite;
                Agent.ID.PlayerEvents.OnWeaponSwap?.Invoke(weaponSprite);

            }
        }
        public void SwapWeapon()
        {
            if (weaponStorage.WeaponCount < 2)
            {
                return;
            }
            SwapWeaponSprite(weaponStorage.SwapWeapon().weaponSprite);
        }
        public void AddWeaponData(WeaponData weaponData)
        {
            weaponStorage.AddWeaponData(weaponData);
            if (weaponStorage.WeaponCount == 2)
                Agent.ID.PlayerEvents.OnMulipleWeapons?.Invoke(true);
            SwapWeaponSprite(weaponData.weaponSprite);
        }
        public void PickUpWeapon(WeaponData weaponData)
        {
            AddWeaponData(weaponData);
            OnWeaponPickUp?.Invoke();
        }
        public bool CanIUseWeapon(bool isGrounded)
        {
            if (weaponStorage.WeaponCount <= 0)
            {
                return false;
            }
            return weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded);
        }
        public List<string> GetPlayerWeaponNames()
        {
            return weaponStorage.GetPlayerWeaponNames();
        }
        public void ClearAllWeapon()
        {
            SwapWeaponSprite(null);
            weaponStorage.ClearStorage();
            Agent.ID.PlayerEvents.OnMulipleWeapons?.Invoke(false);
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0));
            }
            

        }
        public void DestroyAllChildren()
        {
            GetComponentsInChildren<Transform>().Where(t=> t!= transform).ToList().ForEach(t=>Destroy(t.gameObject)); 
        }
    }
}