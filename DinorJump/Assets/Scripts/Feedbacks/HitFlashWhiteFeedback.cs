using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HitFlashWhiteFeedback : MonoBehaviour
{
    private Material defaultMaterial;
    public Material FlashMaterial;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float feedbackTime = 0.1f;
    private void Awake()
    {
        if (spriteRenderer == null)
            return;
        defaultMaterial = spriteRenderer.material;
    }

    public void PlayFeedback()
    {
        if (spriteRenderer == null)
            return;
        if(!spriteRenderer.material.HasProperty("_MakeSolidColor"))
        {
            spriteRenderer.material = FlashMaterial;
        }
        ToggleMaterial(1);
        StopAllCoroutines();
        StartCoroutine(ResetColor());
        
    }
    private void ToggleMaterial(int val)
    {
        val = Mathf.Clamp(val,0,1);
        spriteRenderer.material.SetInt("_MakeSolidColor", val);


    }
    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(feedbackTime);
        ToggleMaterial(0);
        spriteRenderer.material = defaultMaterial;
    }
}
