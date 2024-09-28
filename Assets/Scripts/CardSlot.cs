using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Card HeldCard { get; set; }

    private void Awake()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Check to see that a card is actually being carried
            if (CardManager.Instance.CarriedCard == null) 
                return;

            SetCard(CardManager.Instance.CarriedCard);
            CardManager.Instance.SetCarriedCard(null);
        }
    }

    public void SetCard(Card card)
    {        
        HeldCard = card;
        // ActiveSlot property will handle things like parenting and raycast blocking
        HeldCard.ActiveSlot = this;
    }
}
