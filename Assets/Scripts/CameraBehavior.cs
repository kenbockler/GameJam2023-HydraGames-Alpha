using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Transform playerObj;
    private Vector3 velocity = Vector3.zero;
    public Vector3 positionOffset;

    [Range(0, 1)]
    public float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player").transform;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(playerObj.position.x + positionOffset.x, positionOffset.y, positionOffset.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }
}
