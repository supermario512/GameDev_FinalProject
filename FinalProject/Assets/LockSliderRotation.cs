using UnityEngine;

public class LockSliderRotation : MonoBehaviour
{
    private void Update()
    {
        // Lock the rotation of the slider to (0, 0, 0)
        transform.rotation = Quaternion.identity;
    }
}
