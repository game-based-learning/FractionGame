using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Cards start out in the deck and can be taken from there. I'm still not entirely sure how the deck
/// works yet but for now I have this class implementation so that cards have a place to return to
/// </summary>
public class CardDeck : MonoBehaviour, IPointerClickHandler, ICardHolder
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SetHeldCard(CardManager.Instance.CarriedCard);
        }
    }

    public void SetHeldCard(Card card)
    {
        throw new NotImplementedException();
    }
}
