using UnityEngine;
using TMPro;

public class ProgressionManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text pointsText;
    public AttributesSlot[] attributesSlots;

    [Header("Settings")]
    public int availablePoints = 5;
    private int startingPoints; // Tracking the start value 

    public Attributes baseAttributes;
    public Attributes previewAttributes;

    private void Start()
    {
        //Initialize
        startingPoints = availablePoints;
        previewAttributes = baseAttributes.Clone();

        foreach(AttributesSlot slot in attributesSlots)
        {
            slot.Setup(this);
        }

        RefreshUI();
    }

    public void ModifyAttribute(AttributeType type, int amount)
    {
        int currentPreview = previewAttributes.Get(type);

        if(amount > 0 && availablePoints <= 0)   //Stop over spending 
        {
            return;
        }

        //// Can't Decrease below zero
        //if(amount < 0 && currentPreview <= 0)
        //{
        //    return;
        //}

        // Can't decrease below the base values 
        int permanentBase = baseAttributes.Get(type);
        if (amount <= 0 && currentPreview <= permanentBase)
        {
            return;
        }

        // Apply change to preview and adjust our points 
        previewAttributes.Set(type, currentPreview + amount);
        availablePoints -= amount;

        RefreshUI();
    }

    public void ConfirmChanges()
    {
        //overwrite our base attributes 
        baseAttributes = previewAttributes.Clone();
        startingPoints = availablePoints;
        RefreshUI();
    }

    public void CancelChanges()
    {
        previewAttributes = baseAttributes.Clone();
        availablePoints = startingPoints;
        RefreshUI();
    }

    public int GetAttribute(AttributeType type, bool isPreview)
    {
        return isPreview ? previewAttributes.Get(type) : baseAttributes.Get(type);
    }

    void RefreshUI()
    {
        pointsText.text = availablePoints.ToString();

        foreach(AttributesSlot slot in attributesSlots)
        {
            slot.Refresh();
        }
    }
}