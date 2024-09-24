using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Card HeldCard { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (CardManager.instance.CarriedCard == null) 
                return;

            SetCard(CardManager.instance.CarriedCard);
        }
    }

    public void SetCard(Card card)
    {
        //CardManager.instance.CarriedCard = null;
        CardManager.instance.SetCarriedCard(null);

        HeldCard = card;

        HeldCard.ActiveSlot = this;
        HeldCard.transform.SetParent(transform);
    }
}
