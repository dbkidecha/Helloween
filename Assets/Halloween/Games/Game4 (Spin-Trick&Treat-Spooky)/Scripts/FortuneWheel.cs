using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
    public Transform wheel;

    public List<float> prize;
    public AnimationCurve animationCurves;

    private bool spinning;
    private float anglePerItem;
    public int itemNumber;
    public int spinTime;

    public bool allDone = false;
    public GameObject hand;
    public Button spinBtn;
    public Transform disablePart;
    public GameObject[] animText;

    private List<int> _wheelNo = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
    private static List<int> wheelNo = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
    private int currentNo = 0;

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;

        if (wheelNo.Count <= 0)
            wheelNo = new List<int>(_wheelNo);
        SetDisablePart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }
    }

    public void Spin()
    {
        if (spinning)
            return;

        CancelInvoke(nameof(_ShowHand));

        GetWheelNo();
        float maxAngle = 360 * spinTime + (prize[itemNumber] * anglePerItem);

        StartCoroutine(SpinTheWheel(0.3f * spinTime, maxAngle));

        spinBtn.interactable = false;

        SoundManager.instance.PlaySound(8);
        SoundManager.instance.PlaySound(1);
    }

    public void ShowHand()
    {
        Invoke(nameof(_ShowHand), 5f);
    }

    private void _ShowHand()
    {
        hand.SetActive(true);
    }

    private void GetWheelNo()
    {
        int no = Random.Range(0, wheelNo.Count);
        currentNo = wheelNo[no];

        itemNumber = currentNo;
        wheelNo.RemoveAt(no);

        if (wheelNo.Count <= 0)
        {
            allDone = true;
        }
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves.Evaluate(timer / time);
            wheel.eulerAngles = new Vector3(0.0f, 0.0f, -(angle + startAngle));
            timer += Time.deltaTime;
            yield return 0;
        }

        wheel.eulerAngles = new Vector3(0.0f, 0.0f, -(maxAngle + startAngle));
        spinning = false;

        GetResult(itemNumber);
        disablePart.GetChild(currentNo).gameObject.SetActive(true);
    }

    private void SetDisablePart()
    {
        for (int i = 0; i < disablePart.childCount; i++)
        {
            if (!wheelNo.Contains(i))
            {
                disablePart.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void GetResult(int index)
    {
        //if (index.Equals(0))
        //{
        //    Debug.Log("Treat-Pink");
        //}
        //else if (index.Equals(1))
        //{
        //    Debug.Log("Trick-Yellow");
        //}
        //else if (index.Equals(2))
        //{
        //    Debug.Log("Treat-Blue");
        //}
        //else if (index.Equals(3))
        //{
        //    Debug.Log("Spooky-Red");
        //}
        //else if (index.Equals(4))
        //{
        //    Debug.Log("Trick-LBlue");
        //}
        //else if (index.Equals(5))
        //{
        //    Debug.Log("Treat-Pink1");
        //}
        //else if (index.Equals(6))
        //{
        //    Debug.Log("Spooky-Orange");
        //}
        //else if (index.Equals(7))
        //{
        //    Debug.Log("Trick-Green");
        //}

        animText[0].SetActive(false);
        animText[1].SetActive(false);
        animText[2].SetActive(false);

        if (index.Equals(0) || index.Equals(2) || index.Equals(5))
        {
            //Debug.Log("Treat");
            StartCoroutine(Game4.instance.ShowTreatGame());
            SoundManager.instance.PlaySound(3);

            animText[0].SetActive(true);
        }
        else if (index.Equals(1) || index.Equals(4) || index.Equals(7))
        {
            //Debug.Log("Trick");
            StartCoroutine(Game4.instance.ShowTrickGame());
            SoundManager.instance.PlaySound(4);

            animText[1].SetActive(true);
        }
        else
        {
            //Debug.Log("Spooky");
            StartCoroutine(Game4.instance.ShowSpookyGame());
            SoundManager.instance.PlaySound(2);

            animText[2].SetActive(true);
        }
    }
}