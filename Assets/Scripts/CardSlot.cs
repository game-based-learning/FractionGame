using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Card HeldCard { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (CardManager.instance.carriedCard == null) 
                return;

            SetCard(CardManager.instance.carriedCard);
        }
    }

    public void SetCard(Card card)
    {
        CardManager.instance.carriedCard = null;

        card.ActiveSlot.HeldCard = null;
        HeldCard = card;

        HeldCard.ActiveSlot = this;
        HeldCard.transform.SetParent(transform);
        HeldCard.CanvasGroup.blocksRaycasts = true;
    }
}
