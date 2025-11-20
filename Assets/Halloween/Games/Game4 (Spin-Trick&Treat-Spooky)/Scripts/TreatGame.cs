using UnityEngine;

public class TreatGame : MonoBehaviour
{
    public static TreatGame instance;

    public Transform candyPositions;
    public Transform parent;
    public GameObject candyPrefab;

    private int totalGenerated = 0;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        totalGenerated = 0;
        InvokeRepeating(nameof(GenerateCandy), 1f, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void GenerateCandy()
    {
        Instantiate(candyPrefab, candyPositions.GetChild(Random.Range(0, 5)).position, Quaternion.identity, parent);

        totalGenerated++;
        if (totalGenerated >= 10)
        {
            CancelInvoke(nameof(GenerateCandy));
            Invoke(nameof(EndGame), 2f);
        }
    }

    private void EndGame()
    {
        StartCoroutine(Game4.instance.DisableTreatGame());
        SoundManager.instance.PlaySound(11);
    }
}