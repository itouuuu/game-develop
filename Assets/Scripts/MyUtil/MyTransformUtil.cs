using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTransformUtil
{
    public static T FindAndGetComponent<T>(this Transform transform, string n)
    {
        T retValue = default(T);
        Transform childTransform=transform.Find(n);
        if (childTransform) 
        {
            retValue= childTransform.gameObject.GetComponent<T>();
        }
        return retValue;
    }
}
