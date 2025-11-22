using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowCam : MonoBehaviour
{
    public GameObject follow;
    public float zDistance = 10;
    public float allowableOffset;
    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {

        //stolen from assignment 4
        if (Vector3.Distance(transform.position, follow.transform.position + Vector3.back * zDistance) > allowableOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, (new Vector3(follow.transform.position.x, follow.transform.position.y + 1.3f)) + Vector3.back * zDistance, speed * Time.deltaTime);
        }
    }
}
