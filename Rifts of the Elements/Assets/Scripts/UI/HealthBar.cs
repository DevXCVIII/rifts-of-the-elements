using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private Transform target;

    public void Initialize(Transform followTarget)
    {
        target = followTarget;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = target.position + offset;

        // Optional: make it always face the camera
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Flip it
    }

    public void SetHealth(float current, float max)
    {
        if (slider != null)
            slider.value = current / max;
    }
}
