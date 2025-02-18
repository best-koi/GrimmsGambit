using UnityEngine;

public class DragAndDropV2 : MonoBehaviour
{
    #region Serialized Fields

    [Header("Hover/Pick-Up/Drop Fields")]
    [SerializeField] private float _distanceFromCamera = .25f;
    [SerializeField] private float _hoverScale = 1.1f;
    [SerializeField] private float _hoverPopDistance = 1f;

    [Header("Pick-Up/Drop Layers")]
    [SerializeField] private LayerMask _pickUpLayers;
    [SerializeField] private LayerMask _slotLayers;

    [Header("References")]
    [SerializeField] private EncounterController _controller;

    #endregion

    #region Private Fields

    private Camera _mainCamera;

    private Transform _hoveredObject;
    private Transform _selectedObject;

    private Transform _selectedObjectParent;
    private int _selectedChildIndex;

    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
        if (_controller == null)
            _controller = FindObjectOfType<EncounterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

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
        if (_selectedObject != null)
        {
            Ray center = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
            float angle = Vector3.Angle(center.direction, ray.direction);

            _selectedObject.position = ray.GetPoint(_distanceFromCamera / Mathf.Cos(angle * Mathf.Deg2Rad));
            _selectedObject.rotation = _mainCamera.transform.rotation;
        }
    }

    private void ManageClickInteraction(Ray ray)
    {
        RaycastHit hit;

        if (_selectedObject == null && Physics.Raycast(ray, out hit, 1000, _pickUpLayers))
        {
            _selectedObject = hit.transform;
            _selectedChildIndex = _selectedObject.GetSiblingIndex();
            _selectedObjectParent = _selectedObject.parent;

            _selectedObject.parent = null;
            _selectedObject.localScale = Vector3.one;

            if (_hoveredObject != null && _hoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                cd.OrderLayer = 1;
                cd.CardDisplayScale = Vector3.one;
                cd.CardDisplayDisplacement = Vector3.zero;
            }
        }
        else if (_selectedObject != null)
        {
            // Insert card gameObject into slot gameObject through parenting and local transformations
            if (_controller != null && Physics.Raycast(ray, out hit, 1000, _slotLayers))
            {
                int cardCost = _selectedObject.GetComponent<Card>().CardCost;
                Minion hitMinion = hit.transform.parent.GetComponent<Minion>();

                if (hitMinion != null && _controller.EnoughResources(cardCost) && _selectedObject.GetComponent<Card>().TargetsEnemies != hitMinion.ownerPlayer) //The third statement in this conditional statement makes sure that cards who target enemies can be used on on ownerPlayers that are false and vice versa
                {
                    //m_SelectedObject.parent = hit.transform;
                    hitMinion.ConsumeCard(_selectedObject.GetComponent<Card>());
                    _controller.SpendResources(cardCost);
                    //Plays the audio associated with the card
                    AudioSource _audioSource;
                    _audioSource = GetComponent<AudioSource>(); //Uses audio source within the drag and drop prefab
                    _selectedObject.GetComponent<Card>().PlaySound(_audioSource);

                    Destroy(_selectedObject.gameObject);
                }
                else
                {
                    _selectedObject.parent = _selectedObjectParent;
                    _selectedObject.SetSiblingIndex(_selectedChildIndex);
                }
            }
            else
            {
                _selectedObject.parent = _selectedObjectParent;
                _selectedObject.SetSiblingIndex(_selectedChildIndex);
            }

            _selectedObject.localPosition = Vector3.zero;
            _selectedObject.localRotation = Quaternion.identity;
            _selectedObject.localScale = Vector3.one;
            _selectedObject = null;
        }
    }

    private void ManageHoverInteraction(Ray ray)
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, 1000, _pickUpLayers);

        if (_hoveredObject != null && (!hasHit || _hoveredObject != hit.transform))
        {
            if (_hoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                if (_hoveredObject != _selectedObject)
                    cd.OrderLayer = 0;
                cd.CardDisplayScale = Vector3.one;
                cd.CardDisplayDisplacement = Vector3.zero;
            }

            _hoveredObject = null;
        }
        else if (hasHit && _selectedObject == null)
        {
            _hoveredObject = hit.transform;

            if (_hoveredObject.TryGetComponent<CardDisplay>(out CardDisplay cd))
            {
                cd.OrderLayer = 1;
                cd.CardDisplayScale = Vector3.one * _hoverScale;
                cd.CardDisplayDisplacement = Vector3.up * _hoverPopDistance;
            }
        }
    }

    #endregion
}