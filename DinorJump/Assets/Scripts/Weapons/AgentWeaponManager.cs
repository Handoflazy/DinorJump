using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using SVS.Level;
using UnityEngine.InputSystem;

namespace WeaponSystem
{
    public class AgentWeaponManager : AgentSystem
    {
        SpriteRenderer spriteRenderer;
        private WeaponStorage weaponStorage;
        public UnityEvent<Sprite> OnWeaponSwap;

        public UnityEvent OnWeaponPickUp;
        
        bool isSwappedThisFrame = false;
        protected override void Awake()
        {
            base.Awake();
            weaponStorage = new WeaponStorage();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ToggleWeaponVisibility(false);
        }

        private void LateUpdate() {
            isSwappedThisFrame = false;
        }

        private void OnEnable() {
            Agent.ID.PlayerEvents.OnWeaponChange += SwapWeapon;
        }

        private void OnDisable() {
            Agent.ID.PlayerEvents.OnWeaponChange -= SwapWeapon;
        }

        public void ToggleWeaponVisibility(bool v)
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
        public void SwapWeapon() {
            if(isSwappedThisFrame)
                return;
            Debug.Log(1);
            isSwappedThisFrame = true;
            if (weaponStorage.WeaponCount < 2)
            {
                return;
            }
            SwapWeaponSprite(weaponStorage.SwapWeapon().weaponSprite);
        }
        public void AddWeaponData(WeaponData weaponData) {
            if(weaponStorage.IsContainWeapon(weaponData))
                return;
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
        
        public void SaveWeaponData()
        {
            SaveSystem.SaveWeapons(GetPlayerWeaponNames());
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