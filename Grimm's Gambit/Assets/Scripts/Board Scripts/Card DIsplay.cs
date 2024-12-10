using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    #region Private Fields

    [Header("Card Reference")]
    [SerializeField]
    private Card m_CardReference;

    public Card CardReference
    {
        get
        {
            return m_CardReference;
        }
        set
        {
            m_CardReference = value;
        }
    }

    [Header("Display Variables")]
    [SerializeField]
    private Vector3 m_CardDisplayDisplacement;

    public Vector3 CardDisplayDisplacement
    {
        get
        {
            return m_CardDisplayDisplacement;
        }
        set
        {
            m_CardDisplayDisplacement = value;
        }
    }

    [SerializeField]
    private Vector3 m_CardDisplayScale;

    public Vector3 CardDisplayScale
    {
        get
        {
            return m_CardDisplayScale;
        }
        set
        {
            m_CardDisplayScale = value;
        }
    }

    private Vector3 m_StartingDisplayScale;

    [SerializeField]
    private int m_OrderLayer;

    public int OrderLayer
    {
        get
        {
            return m_OrderLayer;
        }
        set
        {
            m_OrderLayer = value;
        }
    }


    [Header("Display Component References")]
    [SerializeField]
    private Canvas m_CardCanvas;
    [SerializeField]
    private TMP_Text m_CardCostText;
    [SerializeField]
    private TMP_Text m_CardNameText;
    [SerializeField]
    private TMP_Text m_CardOwnerNameText;
    [SerializeField]
    private TMP_Text m_CardDescriptionText;
    [SerializeField]
    private Image m_CardImage;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        if (m_CardCanvas != null)
        {
            m_StartingDisplayScale = m_CardCanvas.transform.localScale;
            
        }
            
    }


    private void Update()
    {
        if (m_CardNameText != null)
        {
            m_CardNameText.text = m_CardReference.GetName();
            m_CardNameText.font = PlaytestCheats.GetAllFont();
        }
            
        if (m_CardCostText != null)
        {
            m_CardCostText.text = m_CardReference.GetCardCost() + "";
            m_CardCostText.font = PlaytestCheats.GetAllFont();

        }
            
        if (m_CardDescriptionText != null)
        {
            m_CardDescriptionText.text = m_CardReference.GetCardDescription();
            m_CardDescriptionText.font = PlaytestCheats.GetAllFont();

        }
            

        if (m_CardReference.GetCaster().TryGetComponent<BasicCharacter>(out BasicCharacter character))
        {
            m_CardOwnerNameText.color = character.GetCharacterColor();
            m_CardOwnerNameText.text = character.GetCharacterName();
            m_CardOwnerNameText.font = PlaytestCheats.GetAllFont();
        }

        if (m_CardCanvas != null)
        {
            m_CardCanvas.transform.localPosition = m_CardDisplayDisplacement;
            m_CardCanvas.transform.localScale = new Vector3(m_CardDisplayScale.x * m_StartingDisplayScale.x, m_CardDisplayScale.y * m_StartingDisplayScale.y, m_CardDisplayScale.z * m_StartingDisplayScale.z);
            m_CardCanvas.sortingOrder = m_OrderLayer;
        }
    }

    #endregion
}
