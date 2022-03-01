using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float minXClamp = -0.77f;
    public float maxXClamp = 105.27f;

    void LateUpdate()
    {
        if (GameManager.instance.playerInstance)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = GameManager.instance.playerInstance.transform.position.x + 5;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);

            transform.position = cameraTransform;

        }
    }
}
