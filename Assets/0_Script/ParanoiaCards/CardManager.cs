using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using com.ootii.Messages;

public static class CardManagerState
{
    public static string ChosingCards => "ChosingCards";
}


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
    private List<Card> _cardList;


    private CardDeck _deck;

    private string state = CardManagerState.ChosingCards;

    private CardManagerStates _currentState;

    private int _chosenSoFar;

    void Awake()
    {
        _deck = new CardDeck();
        _deck.Add(new CantWalkBlueCard());
        _deck.Add(new CantWalkRedCard());
        _deck.Add(new CantWalkYellowCard());
        _deck.Add(new CantWalkBlueCard());
        //_deck.Shuffle();
        _chosenSoFar = 0;
    }

    void Start()
    {
        DisplayCardsForSelection(2);
    }

    IEnumerator WaitWithShowingCards()
    {
        yield return new WaitForSeconds(1);
        DisplayCardsForSelection(2);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void SelectedCard()
    {
        _chosenSoFar++;
        StartCoroutine(WaitAfterSelectedCard());
    }

    private IEnumerator WaitAfterSelectedCard()
    {
        yield return new WaitForSeconds(1.2f);

        if (_chosenSoFar >= GameController.Instance.CurrentLevel)
        {
            GameController.Instance.SetParanoias(_topBar.Deck);
            MessageDispatcher.SendMessage(Msg.AllParanoiasSelected);
        }
        else
        {
            _topBar.HasSelectedThisRound = false;
            DisplayCardsForSelection(2);
        }
    }


    private void DisplayCardsForSelection(int amount)
    {
        _selectionDisplay.Display(_deck.DrawCards(amount));
    }

}
