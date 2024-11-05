using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CardDisplay : MonoBehaviour
{
    #region Private Fields

    [Header("Card Reference")]
    [SerializeField]
    private Card m_CardReference;

    [Header("Display Component References")]
    [SerializeField]
    private TMP_Text m_CardCostText;
    [SerializeField]
    private TMP_Text m_CardNameText;
    [SerializeField]
    private TMP_Text m_CardOwnerNameText;
    [SerializeField]
    private SpriteRenderer m_CardSprite;

    #endregion

    #region MonoBehaviour Callbacks

    private void Update()
    {
        m_CardCostText.text = m_CardReference.GetCardCost() + "";
        m_CardNameText.text = m_CardReference.GetName();

        if (m_CardReference.GetCaster().TryGetComponent<BasicCharacter>(out BasicCharacter character))
        {
            m_CardOwnerNameText.color = character.GetCharacterColor();
            m_CardOwnerNameText.text = character.GetCharacterName();
        }
    }

    #endregion
}
