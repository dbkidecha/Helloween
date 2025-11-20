using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KnifeTool : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private float scaleFactor;
    private RectTransform rectTransform;
    private Image image;
    private SoundManager sound;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private float count = 300;
    public Pumpkin pumpkin;
    public Transform innerKnife;
    public GameObject hand;
    private Game1 game1;

    private void OnEnable()
    {
        sound = SoundManager.instance;
        game1 = Game1.instance;

        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        scaleFactor = Game1.instance.canvas.scaleFactor;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game1.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        count = 100;
        image.raycastTarget = true;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
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
            if (results[0].gameObject.name.Equals("Face"))
            {
                if (count > 0)
                {
                    count--;
                    sound.PlaySound(2, true);
                    if (count <= 0)
                    {
                        pumpkin.face.color = Color.white;
                        pumpkin.face.transform.GetChild(0).gameObject.SetActive(false);
                        game1.TapEffect(pumpkin.face.transform.position);
                        sound.PlaySound(3);
                        game1.ShowPumpkinDecoration(true, "");
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        innerKnife.DOLocalMove(new Vector3(50, 50, 0), 0.25f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        innerKnife.DOLocalMove(new Vector3(20, 30, 0), 0.25f);
    }
}