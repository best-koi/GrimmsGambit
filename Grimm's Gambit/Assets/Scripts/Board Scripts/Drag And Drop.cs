using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [Header("Pick-Up/Drop Fields")]
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

    // Start is called before the first frame update
    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (m_SelectedObject == null && Physics.Raycast(ray, out hit, 1000, m_PickUpLayers))
            {
                m_SelectedObject = hit.transform;
                m_SelectedObject.parent = null;
                m_SelectedObject.localScale = Vector3.one;
            }
            else
            {
                if (Physics.Raycast(ray, out hit, 1000, m_SlotLayers))
                {
                    m_SelectedObject.parent = hit.transform;
                    m_SelectedObject.localPosition = Vector3.zero;
                    m_SelectedObject.localRotation = Quaternion.identity;
                    m_SelectedObject.localScale = Vector3.one * .9f;
                }

                m_SelectedObject = null;
            }
        }

        if (m_SelectedObject != null)
        {
            m_SelectedObject.position = ray.GetPoint(m_DistanceFromCamera);
            m_SelectedObject.rotation = m_MainCamera.transform.rotation * Quaternion.Euler(180, 0, 0);
        }
    }
}