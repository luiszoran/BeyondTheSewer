using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GeneratorEventController : MonoBehaviour
{
    private Vector3 startingPosition;
    private float currentGazingTime;
    private bool gazedAtObject;
    public float gazeTime;
    private bool triggered;

    void Start()
    {
        currentGazingTime = 0f;
        gazeTime = gazeTime != 0f ? gazeTime : 10f;
        gazedAtObject = false;
        startingPosition = transform.localPosition;
        SetGazedAt(false);
    }

    void Update()
    {
        if (currentGazingTime >= gazeTime & !triggered)
        {
            triggered = true;
            StartCoroutine(GameController.gameController.LoadLevel("Menu"));
        }

        if (gazedAtObject)
        {
            currentGazingTime = currentGazingTime + (1 * Time.deltaTime);
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        gazedAtObject = gazedAt ? true : false;
    }

}