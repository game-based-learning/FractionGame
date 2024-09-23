using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Card HeldCard { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if (carriedCard == null) return;
            //SetCard(carriedCard);
        }
    }

    public void SetCard(Card card)
    {
        //carriedCard = null;

        card.ActiveSlot.HeldCard = null;
        HeldCard = card;

        HeldCard.ActiveSlot = this;
        HeldCard.transform.SetParent(transform);
        HeldCard.CanvasGroup.blocksRaycasts = true;
    }
}
