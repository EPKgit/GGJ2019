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
    public float generationMax;
    public float generationMin;
    public float planetsTooClose;
    public float connectionRadius;

    private List<GameObject> generatedObjects;

    IEnumerator Start()
    {
        yield return new WaitUntil( () => CameraController.instance != null);
        GeneratePlanets();
        GenerateConnections();
        CenterCamera();
    }

    void GeneratePlanets()
    {
        generatedObjects = new List<GameObject>();
        Vector2 position;
        for(int x = 0; x < numberOfPlanetsToGen; ++x)
        {
            generatedObjects.Add(Instantiate(planetPrefabs[0], Vector3.zero, Quaternion.identity, gameObject.transform));
            do
            {
                position = (x == 0) ? Vector3.zero : generatedObjects[x - 1].transform.position;
                position += Random.insideUnitCircle * Random.Range(generationMin, generationMax);
            } while(TooClose(position));
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
        Debug.Log(maxDist);
        CameraController.instance.point = midpoint;
        CameraController.instance.offset = new Vector3(0f, 0f, maxDist / -1.5f);
    }

    bool TooClose(Vector3 positionToCheck)
    {
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            if((generatedObjects[x].transform.position - positionToCheck).magnitude < planetsTooClose)
            {
                return true;
            }
        }
        return false; 
    }

    public void RegnerateMap()
    {
        foreach (Transform child in gameObject.transform) 
        {
            Destroy(child.gameObject);
        }
        GeneratePlanets();
        GenerateConnections();
        CenterCamera();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RegnerateMap();
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
            t.RegnerateMap();
        }
	}
}
