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
        yield return new WaitUntil( () => GameManager.instance.player != null);
        foreach(GameObject g in allCards)
        {
            GameObject temp = Instantiate(g, Vector3.one * 9999, Quaternion.identity, transform);
            GameObject temp2 = Instantiate(temp.GetComponent<HalfCard>().prefabCard, Vector3.one * 9999, Quaternion.identity, transform);
            temp2.GetComponent<Card>().halfCard = temp;
            temp.GetComponent<HalfCard>().card = temp2.GetComponent<Card>();
            cards.Add(temp);
            if(temp.GetComponent<HalfCard>().card.starterCard)
            {
                GameManager.instance.player.playerHand.Add(temp2.GetComponent<Card>());
            }
        }
    }
}
