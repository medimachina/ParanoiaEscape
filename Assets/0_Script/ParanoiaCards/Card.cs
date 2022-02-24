using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public abstract class Card
{
    private string _identifier;
    private string _name;

    public string Identifier => _identifier;
    public string Name => _name;
}
