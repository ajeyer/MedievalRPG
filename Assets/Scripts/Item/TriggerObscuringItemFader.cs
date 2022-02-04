using UnityEngine;

public class TriggerObscuringItemFader : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get Object thats been collided with, and then get all the obsucring item fader components on it and its children - then trigger the fade out
       
        
        ObscuringItemFader[] obscuringItemFader = collision.GetComponentsInChildren<ObscuringItemFader>();

        if (obscuringItemFader.Length > 0)
        {
            for (int i = 0; i<obscuringItemFader.Length; i++)
            {
                obscuringItemFader[i].FadeOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Get Object thats been collided with, and then get all the obsucring item fader components on it and its children - then trigger the fade in

        ObscuringItemFader[] obscuringItemFader = collision.GetComponentsInChildren<ObscuringItemFader>();

        if (obscuringItemFader.Length > 0)
        {
            for (int i = 0; i < obscuringItemFader.Length; i++)
            {
                obscuringItemFader[i].FadeIn();
            }
        }
    }


}
