using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class PointUI : MonoBehaviour
    {
        private TextMeshProUGUI pointText;

        public UnityEvent OnTextChange;

        private void Awake()
        {
            pointText = GetComponentInChildren<TextMeshProUGUI>();
        }
        public void SetPoints(int points)
        {
            pointText.SetText(points.ToString());
            OnTextChange.Invoke();
        }
    }
}