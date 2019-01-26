using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   void Start()
   {
       if(instance != null)
       {
           Destroy(gameObject);
       }
       instance = this;
       DontDestroyOnLoad(gameObject);
   }

   void Update()
   {
       if(Input.GetKeyDown(KeyCode.Mouse0))
       {
           Debug.Log("click");
       }
   }

   public void InputMove()
   {

   }
}
