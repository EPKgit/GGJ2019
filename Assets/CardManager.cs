using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] allCards;

    public GameObject bigCard;

    private List<GameObject> cards;

    IEnumerator Start()
    {
        cards = new List<GameObject>();
        yield return new WaitUntil( () => GameManager.instance != null);
        foreach(GameObject g in allCards)
        {
            GameObject temp = Instantiate(g, Vector3.one * 9999, Quaternion.identity, transform);
            temp.GetComponent<HalfCard>().card.halfCard = temp;
            cards.Add(temp);
            if(temp.GetComponent<HalfCard>().card.starterCard)
            {
                GameManager.instance.player.playerHand.Add(temp.GetComponent<HalfCard>().card);
            }
        }
    }
}
