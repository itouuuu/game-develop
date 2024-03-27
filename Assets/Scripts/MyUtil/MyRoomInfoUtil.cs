using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MyPhotonHashTableExpantion
{
    public static T GetCastValue<T>(this ExitGames.Client.Photon.Hashtable hashtable,  string key,T defaultValue) 
    {
        T retValue = defaultValue;
        if (hashtable.ContainsKey(key) && hashtable[key] is T ) 
        {
            retValue = (T)hashtable[key];
        }
        return retValue;
    }
}
