using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.PackageManager.Requests;

/*
 * Cards should be responsible for setting their own slot property, handling raycast blocking, and clearing previous slot data
 * (Maybe setting their own transform? But honestly the CardManager already sets the card transform so maybe other classes
 * should worry about that).
 */
public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardData _cardData;
    public CardData CardData => _cardData;

    private CardSlot _activeSlot;
    public CardSlot ActiveSlot 
    { 
        get { return _activeSlot; }
        set 
        {
            _activeSlot = value;
            _canvasGroup.blocksRaycasts = true;
            transform.SetParent(value.transform);
        }
    }
    
    // Does the last position of the card necessarily need to be a card slot? My thought is no
    private Transform _lastPosition;
    
    private Image _cardIcon;
    private CanvasGroup _canvasGroup;    

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardIcon = GetComponent<Image>();

        //cardIcon.sprite = cardData.sprite;
    }

    // This function is only called when the card is picked up
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ActiveSlot != null)
                ActiveSlot.HeldCard = null;

            _canvasGroup.blocksRaycasts = false;
            CardManager.instance.SetCarriedCard(this);
        }
    }
}
