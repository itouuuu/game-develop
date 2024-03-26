using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DispDescriptionOnHover : MonoBehaviour
{

    [SerializeField] public MessageStackDescription messageStackDescription;
    
   //[SerializeField] private VSModeLobbyUI _lobbyUI;
   [SerializeField] private EventTrigger _eventTrigger;
    //[SerializeField, TextArea(1, 5)] public string _dispMessage= "‚ ‚¢‚¤‚¦‚¨abcdeABCDE";
    [SerializeField, TextArea(1, 5)] public string DispMessage = "‚ ‚¢‚¤‚¦‚¨abcdeABCDE";



    // Start is called before the first frame update
    void Start()
    {
            if (!messageStackDescription)
            {
                MessageStackDescription[] comps = FindObjectsByType<MessageStackDescription>(FindObjectsSortMode.None);
                if (comps.Length > 0)
                {

                messageStackDescription = comps[0];
                }
            }
            _eventTrigger.AddOnPointerEnter(OnPointerEnter);
        _eventTrigger.AddOnPointerExit(OnPointerExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(BaseEventData baseEventData) 
    {
        messageStackDescription.DispNewDescription(this.gameObject, DispMessage);
    }
    public void OnPointerExit(BaseEventData baseEventData) 
    {
        messageStackDescription.DeleteDescription(this.gameObject);
    }



    private void Reset()
    {
        /*
        GameObject lobbyUIObj=GameObject.Find("UIM");
        if (lobbyUIObj ) 
        {
            _lobbyUI=lobbyUIObj.GetComponent<VSModeLobbyUI>();
        }
        */

        GameObject descriptionObj = GameObject.Find("Description");
        if (descriptionObj)
        {
            messageStackDescription = descriptionObj.GetComponent<MessageStackDescription>();
        }

        

        _eventTrigger = GetComponent<EventTrigger>();
        
    }
}
