using UnityEngine;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public Card CarriedCard { get; private set; }

    [SerializeField] private PlayerInput _inputs;
    private InputAction _mousePosition;

    [SerializeField] private Transform _draggables;
    // This is obselete maybe now lol
    [Tooltip("The offset from the center when picking up an dragging a card. This is purely cosmetic")]
    [SerializeField] private Vector2 _offsetPosition = new Vector3(0.2f, 0.8f);
    [Tooltip("The rotation offset to make the card look angled when being picked up.")]
    [SerializeField] private Vector3 _offsetRotation = Vector3.zero;

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

        CarriedCard.transform.position = _mousePosition.ReadValue<Vector2>();
    }
    
    public void SetCarriedCard(Card card)
    {
        // The CardManager should handle swapping cards
        if (card != null)
            card.ActiveSlot.SetHeldCard(CarriedCard);

        // Carried card settings
        CarriedCard = card;
        if (card != null)
        {
            card.transform.SetParent(_draggables);
            card.transform.Rotate(_offsetRotation, Space.Self);
            (card.transform as RectTransform).pivot = _offsetPosition;
        }
    }
}
