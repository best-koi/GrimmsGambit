using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardHand : ObjectContainer
{
    [SerializeField] private CardDatabase _dataBase;
    [SerializeField] private Vector2 _angleBounds;
    [SerializeField] private Vector3 _displacementFromContainerCenter;
    [SerializeField] private float _depthBetweenCards;

    protected override void Awake()
    {
        Deck.onDraw += AddCardFromIndex;
        Deck.onDiscard += RemoveCardFromIndex;
        base.Awake();
    }

    private void OnDestroy()
    {
        Deck.onDraw -= AddCardFromIndex;
        Deck.onDiscard -= RemoveCardFromIndex;
    }

    private void Start()
    {
        
    }

    public void AddCardFromIndex(int owner, int cardIndex)
    {
        if(cardIndex < 0)
            return;
        GameObject card = _dataBase.SpawnCardObject(owner, cardIndex);
        Add(card.transform);
    }

    public void RemoveCardFromIndex(int cardIndex)
    {
        Transform t = Remove(cardIndex);

        if (t != null)
            Destroy(t.gameObject);
    }

    #region Object Container Callbacks

    public override void UpdatePositions()
    {
        // Calculate angle between cards
        int cardNum = m_ChildTransforms.Count;
        float firstAngle = _angleBounds.x;
        float secondAngle = _angleBounds.y;
        float totalAngle = Mathf.Abs(firstAngle - secondAngle);

        int angleSpaceCount = (cardNum % 2 == 0 ? cardNum : cardNum - 1);

        float angleBetweenCards = totalAngle;
        if (angleSpaceCount > 0)
            angleBetweenCards /= angleSpaceCount;

        if (firstAngle > secondAngle)
            angleBetweenCards *= -1;

        float currentAngle = firstAngle;
        if (cardNum == 1 || cardNum % 2 == 0)
            currentAngle += angleBetweenCards / 2;

        int orderLayer = cardNum * -1 + 1;
        float cardDepth = _depthBetweenCards * orderLayer;

        // Apply transformations to all cards
        foreach (Transform t in m_ChildTransforms)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 displacement = rotation * _displacementFromContainerCenter + cardDepth * Vector3.back;

            SetChildLocalRotation(t, rotation);
            SetChildLocalPosition(t, displacement);

            // Increment angle for each card
            currentAngle += angleBetweenCards;
            cardDepth += _depthBetweenCards;

            if (t.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                cd.OrderLayer = orderLayer;
                orderLayer++;
            }
        }
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(CardHand))]
[CanEditMultipleObjects]
public class CardHandV2Editor : ObjectContainerEditor
{

}
#endif