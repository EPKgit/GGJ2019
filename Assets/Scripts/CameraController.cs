using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    //public GameObject toFollow;
    public Vector3 point;

    public Vector3 offset = new Vector3(0, 0, -10f);
    
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }    
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, point + offset, Time.deltaTime);
    }
}
