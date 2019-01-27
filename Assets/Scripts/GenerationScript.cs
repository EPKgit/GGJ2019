using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphGeneration;

public class GenerationScript : MonoBehaviour
{
    public GameObject[] planetPrefabs;
    public GameObject lineRenderer;
    public LayerMask planetLayer;
    public bool old;
    /*
    public float numberOfPlanetsToGen;
    public float generationMin;
    public float generationMax;
    public float planetsTooClose;
    public float connectionRadius;
    public float legMax;
    */
    
    public float numPlanets;
    public float xStep;
    public float yStep;
    public float xRandRange;
    public float yRandRange;
    public float buildChance;
    
    private GraphGenerator.Graph graph;
    private List<GameObject> generatedObjects;
    private List<GameObject> toGen;
    private Dictionary<GraphGenerator.BuildingNode, GameObject> dict;

    IEnumerator Start()
    {
        yield return new WaitUntil( () => CameraController.instance != null);
        /*
        generatedObjects = new List<GameObject>();
        foreach (Transform child in gameObject.transform) 
        {
            generatedObjects.Add(child.gameObject);
        }
        GenerateConnections();
        CenterCamera();
        */
        if(old)
        {
            OldGenerateConnections();
        }
        else
        {
            RegenerateMap();
        }
    }
    /*
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
                position = (x == 0) ? Vector3.zero : generatedObjects[x - 1].transform.position;
                float randX = Random.Range(-1, 1);
                float randY = (1 - (randX * randX)) * ((Random.Range(0, 1) == 0) ? -1 : 1);
                position += new Vector2(randX, randY) * Random.Range(generationMin, generationMax);
            } while(VerifyDistance(position, x));
            generatedObjects[x].transform.position = position;
        }
    }
    */
    void OldGenerateConnections()
    {
        generatedObjects = new List<GameObject>();
        foreach(Transform t in gameObject.transform)
        {
            generatedObjects.Add(t.gameObject);
        }
        for(int x = 0; x < generatedObjects.Count; ++x)
        {
            for(int y = 0; y < generatedObjects.Count; ++y)
            {
                if(x != y)
                {
                    if( (generatedObjects[x].transform.position - generatedObjects[y].transform.position).magnitude < 7)
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
    
    
    void GeneratePlanets()
    {
        toGen = new List<GameObject>(planetPrefabs);
        generatedObjects = new List<GameObject>();
        dict = new Dictionary<GraphGenerator.BuildingNode, GameObject>();
        foreach(GraphGenerator.BuildingNode node in graph.Nodes)
        {
            GameObject planetToGen = toGen[Random.Range(0, toGen.Count)];
            toGen.Remove(planetToGen);
            if(toGen.Count == 0)
            {
                toGen.AddRange(planetPrefabs);
            }
            GameObject temp = Instantiate(planetToGen, new Vector3(node.xPos, node.yPos, 0f), Quaternion.identity, transform);
            generatedObjects.Add(temp);
            dict.Add(node, temp);
        }
    }

    void GenerateConnections()
    {
        foreach(GraphGenerator.BuildingNode node in graph.Nodes)
        {
            foreach(GraphGenerator.BuildingNode other in node.AdjacentNodes)
            {
                dict[node].GetComponent<Planet>().connections.Add(dict[other].GetComponent<Planet>());
                dict[other].GetComponent<Planet>().connections.Add(dict[node].GetComponent<Planet>());
                GameObject temp = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, gameObject.transform);
                LineRenderer lr = temp.GetComponent<LineRenderer>();
                Vector3[] pos = new[] { dict[node].transform.position, dict[other].transform.position };
                lr.SetPositions(pos);
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

    /*
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
    */

    public void RegenerateMap()
    {
        foreach (Transform child in gameObject.transform) 
        {
            Destroy(child.gameObject);
        }
        int xy = (int)Mathf.Floor(Mathf.Sqrt(numPlanets));
        graph = GraphGenerator.Build(xy, xy, xStep, yStep, xRandRange, yRandRange, buildChance);
        GeneratePlanets();
        GenerateConnections();
        //CenterCamera();
        GameManager.instance.setPlanets(generatedObjects);
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
