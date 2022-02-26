using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CardManager : SerializedMonoBehaviour
{
    public class CardManagerStates
    {
        public const string SelectingCard = "SELECTING_CARD";

    }

    [SerializeField]
    private CardSelectionDisplay _selectionDisplay;
    [SerializeField]
    private CardTopBar _topBar;
    [SerializeField]
    private CardDeck _deck;

    private CardManagerStates _currentState;

    private int _chosenSoFar;

    void Awake()
    {
        _deck.Shuffle();
        _chosenSoFar = 0;
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void DisplayCardsForSelection(int amount)
    {
        _selectionDisplay.Display(_deck.DrawCards(amount));
    }

}
