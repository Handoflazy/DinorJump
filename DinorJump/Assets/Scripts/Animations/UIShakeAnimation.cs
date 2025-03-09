using DG.Tweening;
using UnityEngine;

public class UIShakeAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform element;
    [Header("Shake Animation Settings")]
    public float shakeTime = 0.5f, shakeStrength=20, randomness = 90;
    public int vibrato = 90;
    public bool fadeOut = true;
    public float delayBetweenShakes = 3;

    [Space(10)]

    
    Sequence sequence;
    public bool PlayOnWake = true;
    
    private void Start()
    {
        if (PlayOnWake)
        {
            PlayAnimation();
        }
    }

    private void CreateSequence()
    {
        sequence = DOTween.Sequence()
            .Append(element.DOShakeRotation(shakeTime, shakeStrength, vibrato, randomness, fadeOut))
            .SetLoops(-1, LoopType.Restart)
            .AppendInterval(delayBetweenShakes);
    }
    public void PlayAnimation() {
        CreateSequence();
        sequence.Play();
    }

    private void OnDestroy()
    {
        if(sequence != null)
        {
            sequence.Kill();
        }
    }
}
