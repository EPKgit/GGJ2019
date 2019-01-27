using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public Planet currentPlanet;

    public float orbitDistance;
    public float rotationPeriod;

    private float currentRotationRads;
    
    IEnumerator Start()
    {
        enabled = false;
        while(currentPlanet == null)
        {
           yield return null;
        }
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        currentRotationRads = 0f;
        transform.position = currentPlanet.transform.position + Vector3.up * orbitDistance;
    }

    void Update()
    {
        //Debug.Log(currentRotationRads);
        currentRotationRads += Time.deltaTime / rotationPeriod * 2 * Mathf.PI;
        transform.position = currentPlanet.transform.position;
        transform.position += Vector3.up * orbitDistance * Mathf.Sin(currentRotationRads);
        transform.position += Vector3.right * orbitDistance * Mathf.Cos(currentRotationRads);

        transform.rotation = Quaternion.Euler(0f, 0f, currentRotationRads * Mathf.Rad2Deg);
    }
}
