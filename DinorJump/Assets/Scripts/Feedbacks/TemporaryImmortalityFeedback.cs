using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class TemporaryImmotalFeedback : AgentSystem, IHittable
{
    public SpriteRenderer spriteRenderer;
    public Collider2D[] colliders = new Collider2D[0];
    public float flashDelay = 0.1f;
    [Range(0f, 1f)]
    public float flashAlpha = 0.5f;

    public float immortalityTime;

    protected override void Awake()
    {
        if (colliders.Length == 0)
            colliders = GetComponents<Collider2D>();

    }
    private void OnDisable()
    {
        ToggleColliders(false);
        StopAllCoroutines();
    }
    public void GetHit(GameObject opponent, int weaponDamage)
    {
        ToggleColliders(false);

        if (GetComponent<Damageable>().CurrentHealth > 0)
        {
            StartCoroutine(ResetColliders());
            StartCoroutine(Flash(flashAlpha));
        }


    }

    private IEnumerator Flash(float flash)
    {
        flash = Mathf.Clamp01(flash);
        ChangeSpriteRendererColorAlpha(flash);
        yield return new WaitForSeconds(flashDelay);
        StartCoroutine(Flash(flash < 1 ? 1 : flashAlpha));
    }

    IEnumerator ResetColliders()
    {
        yield return new WaitForSeconds(immortalityTime);
        StopAllCoroutines();
        ToggleColliders(true);
        ChangeSpriteRendererColorAlpha(1);
    }

    private void ChangeSpriteRendererColorAlpha(float alpha)
    {

        spriteRenderer.material.SetFloat("_Alpha", alpha); ;
    }

    private void ToggleColliders(bool v)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = v;
        }
    }
}
