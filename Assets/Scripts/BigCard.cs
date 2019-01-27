using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigCard : MonoBehaviour
{
    public static BigCard instance;

    public Text name;
    public Text effect;
    public Image art;
    public Image frame;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        frame = transform.GetChild(1).GetComponent<Image>();
        art = transform.GetChild(2).GetComponent<Image>();
        name = transform.GetChild(4).GetComponent<Text>();
        effect = transform.GetChild(3).GetComponent<Text>();
    }

    public void setNewBigCard(Card c)
    {
        if(c == null)
        {
            return;
        }
        name.text = c.cardName;
        effect.text = c.cardAbilityText;
        art.sprite = c.cardImage;
        Debug.Log(c.cardSuit);
        switch(c.cardSuit)
        {
            case Card.Suit.DIP: 
                frame.color = Color.blue;
                break;
            case Card.Suit.FOOD: 
                frame.color = Color.green;
                break;
            case Card.Suit.MACHINE: 
                frame.color = Color.red;
                break;
            case Card.Suit.TOY: 
                frame.color = Color.magenta;//new Color(164f, 122f, 211f);
                break;
            case Card.Suit.VALUABLE: 
                frame.color = Color.yellow;
                break;
        }
    }
}
