using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject follow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -10);
    }
}
