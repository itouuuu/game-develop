using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DispDescriptionOnHover : MonoBehaviour
{
   
    [SerializeField] private MessageStackDescription _messageStackDescription;
   //[SerializeField] private VSModeLobbyUI _lobbyUI;
   [SerializeField] private EventTrigger _eventTrigger;
    //[SerializeField, TextArea(1, 5)] public string _dispMessage= "����������abcdeABCDE";
    [SerializeField, TextArea(1, 5)] public string DispMessage = "����������abcdeABCDE";



    // Start is called before the first frame update
    void Start()
    {
        if (!_messageStackDescription) 
        {
            GameObject temp = GameObject.Find("Description");
            _messageStackDescription=temp.GetComponent<MessageStackDescription>();
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
        _messageStackDescription.DispNewDescription(this.gameObject, DispMessage);
    }
    public void OnPointerExit(BaseEventData baseEventData) 
    {
        _messageStackDescription.DeleteDescription(this.gameObject);
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
            _messageStackDescription = descriptionObj.GetComponent<MessageStackDescription>();
        }

        

        _eventTrigger = GetComponent<EventTrigger>();
        
    }
}