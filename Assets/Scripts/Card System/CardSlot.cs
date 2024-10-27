using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// idk what to write yet lol
/// </summary>
public class CardSlot : MonoBehaviour, IPointerClickHandler, ICardHolder
{
    public Card HeldCard { get; set; }

    public delegate void OnCardSlotted(Card card);
    public event OnCardSlotted cardSlotted;

    /*
     * Currently this event only happens when HeldCard is empty, because otherwise cards have their own pointer
     * click events. However, I might change this so that the CardSlot controls when a card is able to be
     * slotted or not. Cards would only have pointer events when they're in the deck or something I think.
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot clicked");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SetHeldCard(CardManager.Instance.CarriedCard);
        }
    }

    public void SetHeldCard(Card card)
    {
        HeldCard = card;
        cardSlotted?.Invoke(HeldCard);

        // It's possible for the passed card to be null
        if (card != null)
        {
            HeldCard.ActiveSlot = this;
            HeldCard.SetCardTransform(transform);

            // I don't like having this line here but I'm not sure how else to tell the CardManager that it's not holding a card anymore
            CardManager.Instance.SetCarriedCard(null);
        }
    }
}
