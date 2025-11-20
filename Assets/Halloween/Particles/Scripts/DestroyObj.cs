using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float wait = 2f;
    public bool hideObj = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (hideObj)
            Invoke(nameof(Hide), wait);
        else
            Destroy(gameObject, wait);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}