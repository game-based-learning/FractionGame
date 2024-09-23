using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private Image cardIcon;
    public CanvasGroup CanvasGroup { get; private set; }

    public CardData CardData { get; set; }
    public CardSlot ActiveSlot { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        cardIcon = GetComponent<Image>();
    }

    public void Initialize(CardData cardData, CardSlot parent)
    {
        CardData = cardData;
        cardIcon.sprite = cardData.sprite;
        ActiveSlot = parent;
        ActiveSlot.HeldCard = this;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // SetCarriedItem, which I think is the pickup method
        }
    }
}
