using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] allCards;

    public GameObject bigCard;

    private List<GameObject> cards;

    void Start()
    {
        foreach(GameObject g in allCards)
        {
            GameObject temp = Instantiate(g, Vector3.one * 9999, Quaternion.identity);
            cards.Add(temp);
            //if(temp.GetComponent<HalfCard>().card.)
        }
    }
}
