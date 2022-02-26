using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CardDeck
{
    [SerializeField]
    private List<Card> _cardList;

    public int Count => _cardList.Count;

    public Card Get(int index)
    {
        return _cardList[index];
    }

    public List<Card> CardList
    {
        get => _cardList;
        set
        {
            _cardList = new List<Card>(value);
        }
    }

    public CardDeck()
    {
        _cardList = new List<Card>();
    }

    public CardDeck(List<Card> cards)
    {
        _cardList = new List<Card>(cards);
    }

    private static List<Card>  ShallowCopyList(List<Card> inputList)
    {
        List<Card> copiedList = new List<Card>();

        foreach (Card card in inputList)
        {
            copiedList.Add(card.NewInstance());
        }

        return copiedList;
    }

    public static CardDeck operator +(CardDeck a, CardDeck b)
    {
        List<Card> mergedCards = ShallowCopyList(a.CardList);
        mergedCards.AddRange(ShallowCopyList(b.CardList));
        return new CardDeck(mergedCards);
    }

    public static CardDeck operator +(CardDeck a, Card b)
    {
        List<Card> mergedCards = ShallowCopyList(a.CardList);
        mergedCards.Add(b.NewInstance());
        return new CardDeck(mergedCards);
    }

    private Dictionary<string, List<int>> GetIdToCardIndexedDict()
    {
        Dictionary<string, List<int>> idToIndex = new Dictionary<string, List<int>>();

        for (int i = 0; i < _cardList.Count; i++)
        {
            string identifier = _cardList[i].Identifier;

            if (!idToIndex.ContainsKey(identifier))
            {
                idToIndex.Add(identifier, new List<int>());
            }

            idToIndex[identifier].Add(i);
        }

        return idToIndex;
    }

    public static CardDeck operator -(CardDeck a, CardDeck b)
    {
        List<Card> cardsLeft = ShallowCopyList(a.CardList);
        Dictionary<string, List<int>> idToIndex = a.GetIdToCardIndexedDict();
        List<int> indicesToRemove = new List<int>();

        foreach (Card card in b.CardList)
        {
            if (idToIndex.ContainsKey(card.Identifier))
            {
                List<int> indexList = idToIndex[card.Identifier];
                if (indexList.Count > 0)
                {
                    indicesToRemove.Add(indexList[indexList.Count - 1]);
                    indexList.RemoveAt(indexList.Count - 1);
                }
            }
        }

        indicesToRemove.Sort();
        indicesToRemove.Reverse();

        foreach (int index in indicesToRemove)
        {
            cardsLeft.RemoveAt(index);
        }

        return new CardDeck(cardsLeft);
    }

    public static CardDeck operator -(CardDeck a, Card b)
    {
        List<Card> cardsLeft = ShallowCopyList(a.CardList);

        for (int i = a.CardList.Count - 1; i >= 0; i--)
        {
            if (b.Identifier == cardsLeft[i].Identifier)
            {
                cardsLeft.RemoveAt(i);
                break;
            }
        }

        return new CardDeck(cardsLeft);
    }

    public static CardDeck operator *(CardDeck a, int b)
    {
        List<Card> resultingList = new List<Card>();
        
        for (int i = 0; i < b; i++)
        {
            resultingList.AddRange(ShallowCopyList(a.CardList));
        }

        return new CardDeck(resultingList);
    }

    public override string ToString()
    {
        string temp = "(";

        foreach (Card card in _cardList)
        {
            temp += card.ToString() + ", ";
        }

        temp += ")";
        return temp;
    }

    public CardDeck Shuffle()
    {
        return new CardDeck(_cardList.OrderBy(a => UnityEngine.Random.Range(0.0f,1.0f)).ToList());
    }

    public Card DrawCard()
    {
        if (_cardList.Count == 0)
        {
            return null;
        }
        
        Card drawnCard = _cardList[0];
        _cardList.RemoveAt(0);

        return drawnCard;
    }

    public CardDeck DrawCards(int amount)
    {
        if (_cardList.Count < amount)
        {
            return null;
        }

        List<Card> drawnCards = new List<Card>();

        for (int i = amount - 1; i >= 0; i--)
        {
            drawnCards.Add(_cardList[i]);
            _cardList.RemoveAt(i);
        }

        drawnCards.Reverse();

        return new CardDeck(drawnCards);
    }

    public CardDeck RemoveAllCardsOfType<T>() where T : Card
    {
        Debug.Log(typeof(T));

        List<Card> result = ShallowCopyList(_cardList);

        for (int i = result.Count - 1; i >= 0; i--)
        {
            if (result[i] is T)
            {
                result.RemoveAt(i);
            }
        }

        return new CardDeck(result);
    }

}
