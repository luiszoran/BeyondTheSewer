using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitGameEventController : MonoBehaviour
{
    private Vector3 startingPosition;
    private float gazing;
    private bool gazedAtObject;
    public float gazeTime;

    void Start()
    {
        gazing = 0f;
        gazeTime = gazeTime != 0f ? gazeTime : 3f;
        gazedAtObject = false;
        startingPosition = transform.localPosition;
    }

    void Update()
    {
        if (gazing >= gazeTime)
        {
            Application.Quit();
        }

        if (gazedAtObject)
        {
            gazing = gazing + (1 * Time.deltaTime);
        }

    }

    public void SetGazedAt(bool gazedAt)
    {
        gazedAtObject = gazedAt ? true : false;
    }

}