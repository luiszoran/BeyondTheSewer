using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LizardEventController : MonoBehaviour
{
    private Vector3 startingPosition;
    private float gazing;
    private bool gazedAtObject;
    public float gazeTime;
    public LizardController lizardController;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    void Start()
    {
        gazing = 0f;
        gazeTime = gazeTime != 0f ? gazeTime : 0.5f;
        gazedAtObject = false;
        startingPosition = transform.localPosition;
        SetGazedAt(false);
    }

    void Update()
    {
        if (gazing >= gazeTime)
        {
            gazing = 0;
            lizardController.Hit();
        }

        if (gazedAtObject)
        {
            gazing = gazing + (1 * Time.deltaTime);
            Debug.Log(gazing);
        }

    }

    public void SetGazedAt(bool gazedAt)
    {
        gazedAtObject = gazedAt ? true : false;
    }

}
