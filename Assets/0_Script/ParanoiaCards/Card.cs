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
    protected Color _color;

    public string Identifier => _identifier;
    public string Name => _name;
    public string Description => _description;
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
}

[System.Serializable]
public abstract class CantWalkColorCard : Card
{
}

[System.Serializable]
public class CantWalkRedCard : CantWalkColorCard
{
    public CantWalkRedCard()
    {
        _identifier = "cant_walk_red";
        _name = "Can't Walk on Red!";
        _description = "Red floor has a forcefield. You can't walk on it.";
        _color = ColorPalette.Red;
    }

    public override Card NewInstance()
    {
        return new CantWalkRedCard();
    }
}

[System.Serializable]
public class CantWalkGreenCard : CantWalkColorCard
{
    public CantWalkGreenCard()
    {
        _identifier = "cant_walk_green";
        _name = "Can't Walk on Green!";
        _description = "Green floor has a forcefield. You can't walk on it.";
        _color = ColorPalette.Green;
    }

    public override Card NewInstance()
    {
        return new CantWalkGreenCard();
    }
}

[System.Serializable]
public class CantWalkBlueCard : CantWalkColorCard
{
    public CantWalkBlueCard()
    {
        _identifier = "cant_walk_blue";
        _name = "Can't Walk on Blue!";
        _description = "Blue floor has a forcefield. You can't walk on it.";
        _color = ColorPalette.BlueDark;
    }

    public override Card NewInstance()
    {
        return new CantWalkBlueCard();
    }
}