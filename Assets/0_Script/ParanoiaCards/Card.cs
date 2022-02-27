using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Card
{
    [SerializeField]
    protected string _identifier;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected string _shortDescription;
    [SerializeField]
    protected Color _color;

    public string Identifier => _identifier;
    public string Name => _name;
    public string Description => _description;
    public string Short => _shortDescription;
    public Color Color => _color;

    //public void SetFromOther(Card other)
    //{
    //    _identifier = other.Identifier;
    //    _name = other.Name;
    //    _description = other.Description;
    //    _color = other.Color;
    //}

    public override string ToString()
    {
        return _identifier;
    }

    public abstract Card NewInstance();

    public abstract void Start();
}

[System.Serializable]
public abstract class CantWalkColorCard : Card
{
    protected virtual string colorString => "GREY";

    public override void Start()
    {
        Debug.Log($"Send message {"CANT_WALK_" + colorString}");
        MessageDispatcher.SendMessage("CANT_WALK_"+colorString);
    }
}

[System.Serializable]
public class CantWalkRedCard : CantWalkColorCard
{
    public CantWalkRedCard()
    {
        _identifier = "cant_walk_red";
        _name = "Can't Walk on Red!";
        _description = "Red floor has a forcefield. You can't walk on it.";
        _shortDescription = "Avoids red";
        _color = ColorPalette.Red;
    }

    protected override string colorString => "RED";

    public override Card NewInstance()
    {
        return new CantWalkRedCard();
    }
}

[System.Serializable]
public class CantWalkYellowCard : CantWalkColorCard
{
    public CantWalkYellowCard()
    {
        _identifier = "cant_walk_yellow";
        _name = "Can't Walk on Yellow!";
        _description = "Yellow floor has a forcefield. You can't walk on it.";
        _shortDescription = "Avoids yellow";
        _color = ColorPalette.Orange;
    }

    protected override string colorString => "YELLOW";

    public override Card NewInstance()
    {
        return new CantWalkYellowCard();
    }
}

[System.Serializable]
public class CantWalkBlueCard : CantWalkColorCard
{
    protected override string colorString => "BLUE";
    public CantWalkBlueCard()
    {
        _identifier = "cant_walk_blue";
        _name = "Can't Walk on Blue!";
        _description = "Blue floor has a forcefield. You can't walk on it.";
        _shortDescription = "Avoids blue";
        _color = ColorPalette.BlueDark;
    }

    public override Card NewInstance()
    {
        return new CantWalkBlueCard();
    }
}