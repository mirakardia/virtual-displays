using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectBody : MonoBehaviour
{
    private Vector3 originalPosition;

    private float horizontalScaling = 1.5f;

    public void set(float _horizontalScaling)
    {
        originalPosition = this.transform.position;

        horizontalScaling = _horizontalScaling;
    }

    public void updateHorizontalOffset(float offset)
    {
        this.transform.position = new Vector3(originalPosition.x + offset*horizontalScaling, this.transform.position.y, this.transform.position.z);
    }
}
