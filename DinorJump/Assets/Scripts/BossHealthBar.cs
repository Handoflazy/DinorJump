using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject HealPanel;

    public void ToggleBossHealthBar(bool val)
    {
        HealPanel.SetActive(val);
    }
    public void Initialize(int maxValue)
    {
        ToggleBossHealthBar(true);
        slider.maxValue = maxValue;
    }

    public void SetValue(int val)
    {
        slider.value = val;
    }

}
