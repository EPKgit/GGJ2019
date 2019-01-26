using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerationScript : MonoBehaviour
{
    public GameObject[] planetPrefabs;
    public GameObject lineRenderer;
    public LayerMask planetLayer;
    public float numberOfPlanetsToGen;
    public float generationMin;
    public float generationMax;
    public float planetsTooClose;
    public float connectionRadius;
    public float legMax;

    private List<GameObject> generatedObjects;

    IEnumerator Start()
    {
        yield return new WaitUntil( () => CameraController.instance != null);
        generatedObjects = new List<GameObject>();
        foreach (Transform child in gameObject.transform) 
        {
            generatedObjects.Add(child.gameObject);
        }
        GenerateConnections();
        CenterCamera();
        //RegenerateMap();
        
    }

    void GeneratePlanets()
    {
        generatedObjects = new List<GameObject>();
        Vector2 position;
        for(int x = 0; x < numberOfPlanetsToGen; ++x)
        {
            GameObject planet = Instantiate(planetPrefabs[0], Vector3.zero, Quaternion.identity, gameObject.transform);
            planet.name = "Planet " + x.ToString();
            generatedObjects.Add(planet);
            //Debug.Log("genning " + planet.name);
            do
            {
                /*
                position = (x == 0) ? Vector3.zero : generatedObjects[x - 1].transform.position;
                float randX = Random.Range(-1, 1);
                float randY = (1 - (randX * randX)) * ((Random.Range(0, 1) == 0) ? -1 : 1);
                position += new Vector2(randX, randY) * Random.Range(generationMin, generationMax);
                */
                position = new Vector2(Random.Range(0, 50), Random.Range(0, 50));
            } while(VerifyDistance(position, x));
            generatedObjects[x].transform.position = position;
        }
    }

    void GenerateConnections()
    {
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            for(int y = 0; y < generatedObjects.Count; ++y)
            {
                if(x != y)
                {
                    if( (generatedObjects[x].transform.position - generatedObjects[y].transform.position).magnitude < connectionRadius)
                    {
                        if(x < y)
                        {
                            //Debug.Log("adding "+ generatedObjects[x] + " and " + generatedObjects[y]);
                            generatedObjects[x].GetComponent<Planet>().connections.Add(generatedObjects[y].GetComponent<Planet>());
                            generatedObjects[y].GetComponent<Planet>().connections.Add(generatedObjects[x].GetComponent<Planet>());
                        }
                        GameObject temp = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, gameObject.transform);
                        LineRenderer lr = temp.GetComponent<LineRenderer>();
                        Vector3[] pos = new[] { generatedObjects[x].transform.position, generatedObjects[y].transform.position };
                        lr.SetPositions(pos);
                    }
                }
            }
        }
    }

    void CenterCamera()
    {
        float maxDist = float.MinValue;
        Vector3 midpoint = Vector3.zero;
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            for(int y = 0; y < generatedObjects.Count; ++y)
            {
                if((generatedObjects[x].transform.transform.position - generatedObjects[y].transform.position).magnitude > maxDist)
                {
                    maxDist = (generatedObjects[x].transform.transform.position - generatedObjects[y].transform.position).magnitude;
                    midpoint = (generatedObjects[x].transform.transform.position + generatedObjects[y].transform.position) / 2;
                }
            }
        }
        //Debug.Log(maxDist);
        CameraController.instance.point = midpoint;
        CameraController.instance.offset = new Vector3(0f, 0f, maxDist * -1f);
    }

    bool VerifyDistance(Vector3 positionToCheck, int index)
    {
        float closestPlanet = float.MaxValue;
        float distance;
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            distance = (generatedObjects[x].transform.position - positionToCheck).magnitude;
            if(x != index)
            {
                if( distance < planetsTooClose )
                {
                    Debug.Log("planets too close");
                    return false;
                }
                if(distance < closestPlanet)
                {
                    closestPlanet = distance;
                }
            }
        }
        //Debug.Log(closestPlanet > connectionRadius);
        return closestPlanet > connectionRadius; 
    }

    bool TooManyLegs()
    {
        int twoCount = 0;
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            if(generatedObjects[x].GetComponent<Planet>().connections.Count <= 2)
            {
                //Debug.Log(generatedObjects[x].name + " is a leg");
                twoCount++;
            }
        }
        //Debug.Log(twoCount);
        //Debug.Log(twoCount >= legMax);
        return twoCount >= legMax;
    }

    public void RegenerateMap()
    {
        do
        {
            foreach (Transform child in gameObject.transform) 
            {
                Destroy(child.gameObject);
            }
            GeneratePlanets();
            GenerateConnections();
            CenterCamera();
        } while(TooManyLegs());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RegenerateMap();
        }
    }
}


[CustomEditor(typeof(GenerationScript))]
public class GenerationScriptEditor : Editor 
{
    public override void OnInspectorGUI()
	{
		GenerationScript t = target as GenerationScript;
		DrawDefaultInspector();
        if(GUILayout.Button("Regen"))
        {
            t.RegenerateMap();
        }
	}
}
