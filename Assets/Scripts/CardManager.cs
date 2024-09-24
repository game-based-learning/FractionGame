using UnityEngine;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public Card CarriedCard { get; private set; }

    [SerializeField] private PlayerInput inputs;
    private InputAction mousePosition;

    [SerializeField] private Transform draggables;
    [SerializeField] private Card cardPrefab;

    #region Unity Callbacks

    private void Awake()
    {
        instance = this;

        mousePosition = inputs.actions.FindAction("Point");
    }

    private void OnEnable()
    {
        mousePosition.performed += UpdateCarriedCardPosition;
    }

    private void OnDisable()
    {
        mousePosition.performed -= UpdateCarriedCardPosition;
    }

    #endregion

    private void UpdateCarriedCardPosition(InputAction.CallbackContext context)
    {
        if (CarriedCard == null)
            return;

        CarriedCard.transform.position = context.ReadValue<Vector2>();
    }

    public void SetCarriedCard(Card card)
    {
        if (CarriedCard != null)
            card.ActiveSlot.SetCard(CarriedCard);

        CarriedCard = card;
        card.transform.SetParent(draggables);
    }
}
