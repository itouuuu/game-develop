using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MyEventTriggerExpantion 
{
    public static void AddOnPointerEnter(this EventTrigger trigger,Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }

    public static void AddOnPointerExit(this EventTrigger trigger, Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }

    public static void AddOnPointerUp(this EventTrigger trigger, Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }

    public static void AddOnPointerDown(this EventTrigger trigger, Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }
    public static void AddOnSelected(this EventTrigger trigger, Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Select;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }

    public static void AddOnDeselected(this EventTrigger trigger, Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Deselect;
        entry.callback.AddListener((data) => { action.Invoke(data); });
        trigger.triggers.Add(entry);
    }
}
