using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolShape : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool isknife = false;
    public float scale = 1;

    private float scaleFactor;
    private Vector2 startingPos;
    private RectTransform rectTransform;
    private Image image;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public GameObject hand;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        scaleFactor = Game1.instance.canvas.scaleFactor;
        startingPos = transform.localPosition;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game1.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        transform.DOScale(scale, 0.2f);

        if (hand != null)
            hand.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
            if (results[0].gameObject.name.Equals("Face"))
            {
                Game1.instance.ShowPumpkinDecoration(isknife, name);
                transform.localPosition = startingPos;
                SoundManager.instance.PlaySound(3);
                gameObject.SetActive(false);
            }
            else
            {
                ResetShape();
            }
        }
        else
        {
            ResetShape();
        }
        image.raycastTarget = true;
    }

    private void ResetShape()
    {
        transform.DOLocalMove(startingPos, 0.3f);
        ResetScale(0.25f);
        //SoundManager.instance.PlaySound(3);
    }

    private void ResetScale(float duration)
    {
        transform.DOScale(1f, duration);
    }
}