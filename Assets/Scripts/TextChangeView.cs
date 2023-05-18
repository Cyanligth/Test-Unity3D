using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChangeView : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        GameManager.Data.OnShootChanged += ChangeText;
    }
    private void OnDisable()
    {
        GameManager.Data.OnShootChanged -= ChangeText;
    }

    private void ChangeText(int count)
    {
        text.text = count.ToString();
    }
}
