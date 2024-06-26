using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(EventTrigger))]
public class DispDescriptionOnSelected : MonoBehaviour
{
    [SerializeField] private MessageStackDescription _messageStackDescription;
    //[SerializeField] private VSModeLobbyUI _lobbyUI;
    [SerializeField] private EventTrigger _eventTrigger;
    //[SerializeField, TextArea(1, 5)] public string _dispMessage= "あいうえおabcdeABCDE";
    [SerializeField, TextArea(1, 5)] public string DispMessage = "あいうえおabcdeABCDE";



    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger.AddOnSelected(OnSelect);
        _eventTrigger.AddOnDeselected(OnDeselect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect(BaseEventData baseEventData)
    {
        _messageStackDescription.DispNewDescription(this.gameObject, DispMessage);
    }
    public void OnDeselect(BaseEventData baseEventData)
    {
        _messageStackDescription.DeleteDescription(this.gameObject);
    }



    private void Reset()
    {
        /*
        GameObject lobbyUIObj = GameObject.Find("UIM");
        if (lobbyUIObj)
        {
            _lobbyUI = lobbyUIObj.GetComponent<VSModeLobbyUI>();
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
