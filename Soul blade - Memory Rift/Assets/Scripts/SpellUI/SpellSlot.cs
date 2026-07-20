using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image iconImage;
    public GameObject highlight;

    public SpellSO AssignedSpell {  get; private set; }

    public void SetSpell(SpellSO spellSO)
    {
        AssignedSpell = spellSO;

        if(spellSO != null)
        {
            iconImage.sprite = spellSO.icon;
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            AssignedSpell = null;
            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
        }

        SetHighlight(false);
    }

    public void SetHighlight(bool active)
    {
        highlight.SetActive(active);
    }
}