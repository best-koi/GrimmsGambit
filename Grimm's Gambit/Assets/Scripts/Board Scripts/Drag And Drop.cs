using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    #region Private Fields

    [Header("Hover/Pick-Up/Drop Fields")]
    [SerializeField]
    private Transform m_HoveredObject;
    [SerializeField]
    private Transform m_SelectedObject;
    [SerializeField]
    private float m_DistanceFromCamera;

    [Header("Pick-Up/Drop Layers")]
    [SerializeField]
    private LayerMask m_PickUpLayers;
    [SerializeField]
    private LayerMask m_SlotLayers;

    private Camera m_MainCamera;

    private Transform m_SelectedObjectParent;
    private int m_SelectedChildIndex;

    [SerializeField]
    private EncounterController controller;

    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    private void Start()
    {
        m_MainCamera = Camera.main;
        if (controller == null)
            controller = FindObjectOfType<EncounterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
            ManageClickInteraction(ray);
        else
            ManageHoverInteraction(ray);

        ManageSelectedObject(ray);
    }

    #endregion

    #region Private Methods

    private void ManageSelectedObject(Ray ray)
    {
        // Drag card around when selected by system
        if (m_SelectedObject != null)
        {
            Ray center = new Ray(m_MainCamera.transform.position, m_MainCamera.transform.forward);
            float angle = Vector3.Angle(center.direction, ray.direction);

            m_SelectedObject.position = ray.GetPoint(m_DistanceFromCamera / Mathf.Cos(angle * Mathf.Deg2Rad));
            m_SelectedObject.rotation = m_MainCamera.transform.rotation;
        }
    }

    private void ManageClickInteraction(Ray ray)
    {
        RaycastHit hit;

        if (m_SelectedObject == null && Physics.Raycast(ray, out hit, 1000, m_PickUpLayers))
        {
            m_SelectedObject = hit.transform;
            m_SelectedChildIndex = m_SelectedObject.GetSiblingIndex();
            m_SelectedObjectParent = m_SelectedObject.parent;

            m_SelectedObject.parent = null;
            m_SelectedObject.localScale = Vector3.one;

            if (m_HoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                cd.OrderLayer = 1;
                cd.CardDisplayScale = Vector3.one;
                cd.CardDisplayDisplacement = Vector3.zero;
            }
        }
        else if (m_SelectedObject != null)
        {
            // Insert card gameObject into slot gameObject through parenting and local transformations
            if (controller != null && controller.SpendResources(m_SelectedObject.GetComponent<Card>().GetCardCost()) && Physics.Raycast(ray, out hit, 1000, m_SlotLayers))
            {
                //m_SelectedObject.parent = hit.transform;
                Card c = m_SelectedObject.GetComponent<Card>();
                Minion hitMinion = hit.transform.parent.GetComponent<Minion>();

                if (hitMinion != null)
                    hitMinion.ConsumeCard(c);
                else
                    controller.SpendResources(-m_SelectedObject.GetComponent<Card>().GetCardCost());

                Destroy(m_SelectedObject.gameObject);

            }
            else
            {
                m_SelectedObject.parent = m_SelectedObjectParent;
                m_SelectedObject.SetSiblingIndex(m_SelectedChildIndex);
            }

            m_SelectedObject.localPosition = Vector3.zero;
            m_SelectedObject.localRotation = Quaternion.identity;
            m_SelectedObject.localScale = Vector3.one * .9f;
            m_SelectedObject = null;
        }
    }

    private void ManageHoverInteraction(Ray ray)
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, 1000, m_PickUpLayers);

        if (m_HoveredObject != null && (!hasHit || m_HoveredObject != hit.transform))
        {
            if (m_HoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                if (m_HoveredObject != m_SelectedObject)
                    cd.OrderLayer = 0;
                cd.CardDisplayScale = Vector3.one;
                cd.CardDisplayDisplacement = Vector3.zero;
            }

            m_HoveredObject = null;
        }
        else if (hasHit && m_SelectedObject == null)
        {
            m_HoveredObject = hit.transform;

            if (m_HoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                cd.OrderLayer = 1;
                cd.CardDisplayScale = Vector3.one * 1.1f;
                cd.CardDisplayDisplacement = Vector3.up * .07f;
            }
        }
    }

    #endregion
}