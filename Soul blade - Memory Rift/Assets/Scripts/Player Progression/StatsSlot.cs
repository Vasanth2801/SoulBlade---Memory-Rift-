using TMPro;
using UnityEngine;

public class StatsSlot : MonoBehaviour
{
    public string displayName;
    public TMP_Text nameText;
    public TMP_Text valueText;
    public TMP_Text previewText;

    private void Awake()
    {
        nameText.text = displayName;
    }

    public void Refresh(float baseValue, float previewValue)
    {
        valueText.text = previewValue.ToString();
        float diff = previewValue - baseValue;

        if(diff > 0)
        {
            previewText.text = "+" + diff.ToString("F1");   
        }
        else if(diff < 0)
        {
            previewText.text = "-" + diff.ToString("F1");
        }
        else
        {
            previewText.text = "";
        }
    }
}