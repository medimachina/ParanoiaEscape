using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorTag
{
    RED,
    BLUE,
    YELLOW
}

public class ColoredFloor : MonoBehaviour
{
    [SerializeField]
    private ColorTag _floorColor;
    [SerializeField]
    private MeshRenderer _renderer;

    private string _cantWalkMessage;

    static string ColorTagToString(ColorTag tag)
    {
        switch (tag)
        { 
            case ColorTag.BLUE:
                return "BLUE";
            case ColorTag.RED:
                return "RED";
            case ColorTag.YELLOW:
                return "YELLOW";
        }

        return "GREY";
    }

    public static Color ColorTagToColor(ColorTag tag)
    {
        switch(tag)
        {
            case ColorTag.BLUE:
                return ColorPalette.BlueDark;
            case ColorTag.RED:
                return ColorPalette.Red;
            case ColorTag.YELLOW:
                return ColorPalette.Yellow;
            default:
                return ColorPalette.Grey;
        }
    }

    void Awake()
    {
        _cantWalkMessage = "CANT_WALK_" + ColorTagToString(_floorColor);
    }

    private void OnEnable()
    {
        Debug.Log($"Set listener {_cantWalkMessage}");
        MessageDispatcher.AddListener(_cantWalkMessage, EnableCollider);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(_cantWalkMessage, EnableCollider);
    }
    private void EnableCollider(IMessage rMessage)
    {
        Debug.Log($"Changed collider");
        Collider collider = GetComponentInChildren<Collider>();
        collider.isTrigger = false;
    }

}
