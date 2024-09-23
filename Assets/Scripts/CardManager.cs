using UnityEngine;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public static Card carriedCard;

    [SerializeField] private PlayerInput inputs;
    private InputAction mousePosition;

    [SerializeField] private CardSlot[] cardSlots;

    [SerializeField] private Transform draggables;
    [SerializeField] private Card cardPrefab;

    private void Awake()
    {
        instance = this;

        mousePosition = inputs.actions.FindAction("Point");
    }

    private void Update()
    {
        if (carriedCard == null) 
            return;

        carriedCard.transform.position = mousePosition.ReadValue<Vector2>();
    }

    public void SetCarriedCard(Card card)
    {
        if (carriedCard != null)
            card.ActiveSlot.SetCard(carriedCard);

        carriedCard = card;
        carriedCard.CanvasGroup.blocksRaycasts = false;
        card.transform.SetParent(draggables);
    }
}
