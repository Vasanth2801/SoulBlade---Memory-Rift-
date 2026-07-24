using UnityEngine;

[System.Serializable]
public class Attributes
{
    public int power;
    public int vitality;
    public int focus;
    public int agility;

    //Create a copy from preview purposes 
    public Attributes Clone()
    {
        return new Attributes
        {
            power = this.power,
            vitality = this.vitality,
            focus = this.focus,
            agility = this.agility
        };
    }

    public int Get(AttributeType type)
    {
        return type switch
        {
            AttributeType.Power => power,
            AttributeType.Vitality => vitality,
            AttributeType.Agility => agility,
            AttributeType.Focus => focus,

            _ => 0
        };
    }

    public void Set(AttributeType type, int value)
    {
        switch(type)
        {
            case AttributeType.Power: power = value; break;
            case AttributeType.Vitality: vitality = value; break;
            case AttributeType.Agility: agility = value; break;
            case AttributeType.Focus: focus = value; break;
        }
    }
}

public enum AttributeType { Power, Vitality, Focus, Agility }