using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerExitHandler,IPointerEnterHandler
{
    public static JoysticButton instance;

    private void Awake()
    {
        instance = this;
    }

    public bool pressed;
    public bool isPressed;

    //bool canPress = true;

    private void Update()
    {
        //if()
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        // Debug.Log(canPress);
        /*   if (PlayerController.instance.grounded)
           {
               pressed = true;
               //canPress = false;
               StartCoroutine(PressWithCooldown());
           }
       */
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        isPressed = true;
        /*    Debug.Log(canPress);
            if (canPress)
            {
                canPress = false;
                StartCoroutine(PressWithCooldown());
            }
        */
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    //   pressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }


    IEnumerator PressWithCooldown()
    {
        pressed = true;
        yield return new WaitForSeconds(0.1f);
       // canPress = true;

    }

}
