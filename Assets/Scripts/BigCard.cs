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

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
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
    }
}
