using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Card))]
public class CardDisplay : MonoBehaviour
{
    #region Serialized Fields

    [Header("Display Variables")]
    [SerializeField] private Vector3 _cardDisplayDisplacement;
    [SerializeField] private Vector3 _cardDisplayScale = Vector3.one;
    [SerializeField] private int _orderLayer;

    [Header("Display Component References")]
    [SerializeField] private Canvas _cardCanvas;
    [SerializeField] private TMP_Text _cardCostText;
    [SerializeField] private TMP_Text _cardNameText;
    [SerializeField] private TMP_Text _cardOwnerNameText;
    [SerializeField] private TMP_Text _cardDescriptionText;
    [SerializeField] private Image _cardImage;

    #endregion

    #region Private Fields

    private Card _cardReference;
    private Vector3 _startingDisplayScale;

    #endregion

    #region Properties

    public Card CardReference { get => _cardReference; set => _cardReference = value; }
    public Vector3 CardDisplayDisplacement { get => _cardDisplayDisplacement; set => _cardDisplayDisplacement = value; }
    public Vector3 CardDisplayScale { get => _cardDisplayScale; set => _cardDisplayScale = value; }
    public int OrderLayer { get => _orderLayer; set => _orderLayer = value; }

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        _cardReference = GetComponent<Card>();

        if (_cardCanvas != null)
            _startingDisplayScale = _cardCanvas.transform.localScale;
    }

    private void Update()
    {
        if (_cardNameText != null)
            _cardNameText.text = _cardReference.CardName;
        if (_cardCostText != null)
            _cardCostText.text = _cardReference.CardCost + "";
        if (_cardDescriptionText != null)
            _cardDescriptionText.text = _cardReference.CardDescription;
        
        if (_cardReference.Caster != null)
        {
            if (_cardReference.Caster.TryGetComponent<BasicCharacter>(out BasicCharacter character))
            {
                _cardOwnerNameText.color = character.GetCharacterColor();
                _cardOwnerNameText.text = character.GetCharacterName();
            }
        }

        if (_cardCanvas != null)
        {
            _cardCanvas.transform.localPosition = _cardDisplayDisplacement;
            _cardCanvas.transform.localScale = new Vector3(_cardDisplayScale.x * _startingDisplayScale.x, _cardDisplayScale.y * _startingDisplayScale.y, _cardDisplayScale.z * _startingDisplayScale.z);
            _cardCanvas.sortingOrder = _orderLayer;
        }

        if (_cardImage != null)
            _cardImage.sprite = CardReference.CardSprite;
    }

    #endregion
}
