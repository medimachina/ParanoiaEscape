using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public abstract class Card
{
    [SerializeField]
    protected string _identifier;
    protected string _name;
    protected string _description;

    public string Identifier => _identifier;
    public string Name => _name;
    public string Description => _description;

    public override string ToString()
    {
        return _identifier;
    }
}

[System.Serializable]
public class CantWalkColorCard : Card
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
    }
}