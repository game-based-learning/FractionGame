using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Cards start out in the deck and can be taken from there. I'm still not entirely sure how the deck
/// works yet but for now I have this class implementation so that cards have a place to return to
/// </summary>
public class CardDeck : MonoBehaviour, IPointerClickHandler, ICardHolder
{
    [SerializeField] private List<Card> cards;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SetHeldCard(CardManager.Instance.CarriedCard);
        }
    }

    public void SetHeldCard(Card card)
    {
        // Check to see that a card is actually being carried
        if (CardManager.Instance.CarriedCard == null)
            return;

        // The card deck doesn't actually need to store card instances for now
        card.ActiveSlot = this;
        card.SetCardTransform(transform);

        CardManager.Instance.SetCarriedCard(null);
    }
}
