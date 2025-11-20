using DG.Tweening;
using UnityEngine;

public class ScaleAnim : MonoBehaviour
{
    public float duration = 0.5f;
    public float wait = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;        
        Invoke(nameof(ScaleObj), wait);
    }

    private void ScaleObj()
    {
        transform.DOScale(1f, duration);
    }
}