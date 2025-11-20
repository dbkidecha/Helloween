using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Belan : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int index;

    private float scaleFactor;
    private Vector2 startingPos;
    private RectTransform rectTransform;
    private Image image;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Image doughImg;
    public Sprite[] doughImgs;
    public GameObject hand;
    private float count = 60;
    private int dough = 0;
    private SoundManager sound;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        scaleFactor = Game6.instance.canvas.scaleFactor;
        startingPos = transform.localPosition;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = Game6.instance.canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

        sound = SoundManager.instance;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
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
            if (results[0].gameObject.name.Equals("Dough"))
            {
                if (count > 0)
                {
                    count--;
                    if (count <= 0)
                    {
                        dough++;
                        if (dough <= 4)
                        {
                            doughImg.sprite = doughImgs[dough];
                            count = 60;
                        }
                        sound.PlaySound(7, false);
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        if (dough >= 4)
        {
            SoundManager.instance.StopSound();
            SoundManager.instance.PlaySound(10);
            Round2.instance.ShowOptions();
            transform.DOLocalMoveX(-1900f, 1f).SetEase(Ease.InFlash);
        }
    }
}