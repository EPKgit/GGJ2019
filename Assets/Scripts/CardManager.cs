using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public int numCard = 75; //3 * number of plannets

    public GameObject[] allCards;

    public GameObject bigCard;

    private List<GameObject> cards;

    private Dictionary<Card.Suit, List<Card>> distroQueues;
    private List<Card.Suit> distroKeys;

    IEnumerator Start()
    {
        cards = new List<GameObject>();
        distroQueues = new Dictionary<Card.Suit, List<Card>>();
        distroKeys = new List<Card.Suit>();
        yield return new WaitUntil( () => GameManager.instance != null);
        yield return new WaitUntil( () => GameManager.instance.player != null);
        foreach(GameObject g in allCards)
        {
            GameObject temp = Instantiate(g, Vector3.one * 9999, Quaternion.identity, transform);
            GameObject temp2 = Instantiate(temp.GetComponent<HalfCard>().prefabCard, Vector3.one * 9999, Quaternion.identity, transform);
            temp2.GetComponent<Card>().halfCard = temp;
            temp.GetComponent<HalfCard>().card = temp2.GetComponent<Card>();
            cards.Add(temp);
            Card.Suit suit = temp.GetComponent<HalfCard>().card.cardSuit;
            if(temp.GetComponent<HalfCard>().card.starterCard)
            {
                GameManager.instance.player.playerHand.Add(temp2.GetComponent<Card>());
            }
            else
            {
                if(!distroQueues.ContainsKey(suit)){
                    distroQueues[suit] = new List<Card>();
                    distroKeys.Add(suit);
                }
                if(Random.Range(0,1)>0.5){
                    distroQueues[suit].Insert(0, temp.GetComponent<HalfCard>().card);
                }else{
                    distroQueues[suit].Add(temp.GetComponent<HalfCard>().card);
                }
            }
            
        }
        foreach(GameObject go in GameManager.instance.planetList){
            Planet p = go.GetComponent<Planet>();
            p.AddCards(DistributeCards(3, p.Suit));
        }
    }

    public Card[] DistributeCards(int i, Card.Suit suitReq){
        Card[] r = new Card[i];
        for(int j = 0; j < i; j++){
            // break out if we are out of cards
            if(distroKeys.Count <= 0){
                break;
            }
            Card.Suit s = distroKeys[(int)Random.Range(0,distroKeys.Count)];
            if(j == 0 && distroKeys.Contains(suitReq)){
                s = suitReq;
            }
            List<Card> q = distroQueues[s];
            Debug.Log(i+" "+  (q.Count-1));
            r[j] = q[q.Count-1];
            q.RemoveAt(q.Count-1);
            //handle running out of cards
            if(q.Count <= 0){
                distroKeys.Remove(s);
            }
        }
        return r;
    }

}
