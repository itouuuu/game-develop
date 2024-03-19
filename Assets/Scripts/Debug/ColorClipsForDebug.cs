using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ColorClipMemos",menuName ="ScriptabeleObjects/Debug/ColorClip")]
public class ColorClipsForDebug : ScriptableObject
{
    [System.Serializable]
    private class ColorMemo 
    {
        [SerializeField]string _memo;
        [SerializeField] Color _color;
    }

    [SerializeField] List<ColorMemo> _colorMemos;
}

