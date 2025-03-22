using TMPro;
using UnityEngine;

public class CounterField : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.text = "0";
    }

    public void Display(int value)
    {
        _text.text = value.ToString();
    }
}
