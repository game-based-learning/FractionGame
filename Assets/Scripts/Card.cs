using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private Image cardIcon;
    public CanvasGroup CanvasGroup { get; private set; }

    [SerializeField] private CardData cardData;
    public CardData CardData => cardData;
    public CardSlot ActiveSlot { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        cardIcon = GetComponent<Image>();

        //cardIcon.sprite = cardData.sprite;
    }

    public void Initialize(CardSlot parent)
    {
        ActiveSlot = parent;
        ActiveSlot.HeldCard = this;        
    }

    // This function is only called when the card is put down
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CanvasGroup.blocksRaycasts = false;
            CardManager.instance.SetCarriedCard(this);
        }
    }
}
