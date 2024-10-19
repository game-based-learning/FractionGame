using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/*
 * Cards should be responsible for handling raycast blocking 
 */
public class Card : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private CardData _cardData;
    //public CardData CardData => _cardData;

    public int value = 0;

    private ICardHolder _activeSlot;
    public ICardHolder ActiveSlot 
    { 
        get { return _activeSlot; }
        set 
        {
            _activeSlot = value;
            _canvasGroup.blocksRaycasts = true;
        }
    }
    
    private ICardHolder _lastSlot;
    
    private Image _cardIcon;
    private CanvasGroup _canvasGroup;    

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardIcon = GetComponent<Image>();

        //cardIcon.sprite = cardData.sprite;

        // For now cards will check if they are in a slot
        ICardHolder parentSlot = GetComponentInParent<ICardHolder>();
        if (parentSlot != null)
            ActiveSlot = parentSlot;

        GetComponentInChildren<TMP_Text>().text = value.ToString();
    }

    // This function is only called when the card is picked up
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _canvasGroup.blocksRaycasts = false;
            CardManager.Instance.SetCarriedCard(this);
        }
    }

    /// <summary>
    /// Handles card transform re-parenting and re-positioning
    /// </summary>
    /// <param name="parent"></param>
    public void SetCardTransform(Transform parent)
    {
        transform.SetParent(parent);
        transform.localRotation = Quaternion.identity;
        (transform as RectTransform).pivot = new Vector2(0.5f, 0.5f);
    }
}
