using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cone : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private float scaleFactor;
    private Vector2 startingPos;
    private RectTransform rectTransform;
    private Image image;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public int[] steps;
    public GameObject particle;
    public Decorate decorate;
    public GameObject hand;

    private int tempStep = 0;
    private float count = 70;

    private void Start()
    {
        count = steps[0];
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        scaleFactor = Game6.instance.canvas.scaleFactor;
        startingPos = transform.localPosition;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game6.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        particle.SetActive(true);

        if (hand != null)
            hand.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;

        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.CompareTag("Hint"))
            {
                count--;
                if (count <= 0)
                {
                    tempStep++;
                    decorate.hintIndex++;
                    if (steps.Length > tempStep)
                        count = steps[tempStep];
                    if (tempStep >= steps.Length)
                    {
                        decorate.coneIndex++;
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        particle.SetActive(false);
    }
}