using UnityEngine;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public Card CarriedCard { get; private set; }

    [SerializeField] private PlayerInput _inputs;
    private InputAction _mousePosition;

    [SerializeField] private Transform _draggables;
    [Tooltip("The offset from the center when picking up an dragging a card. This is purely cosmetic")]
    [SerializeField] private Vector2 offsetPosition;
    [Tooltip("The rotation offset to make the card look angled when being picked up.")]
    [SerializeField] private Vector3 offsetRotation;

    #region Unity Callbacks

    private void Awake()
    {
        // Singleton logic
        if (Instance == null && Instance != this)
            Instance = this;
        else
            Destroy(Instance.gameObject);

        _mousePosition = _inputs.actions.FindAction("Point");
    }

    #endregion

    private void Update()
    {
        if (CarriedCard == null)
            return;

        CarriedCard.transform.position = _mousePosition.ReadValue<Vector2>() + offsetPosition;
    }

    
    public void SetCarriedCard(Card card)
    {
        // The CardManager should handle swapping cards
        if (CarriedCard != null && card != null)
            card.ActiveSlot.SetCard(CarriedCard);

        // Carried card settings
        CarriedCard = card;
        if (card != null)
        {
            card.transform.SetParent(_draggables);
            card.transform.Rotate(offsetRotation, Space.Self);
        }
    }
}
