using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// All credits goes to Alan Zucconi, published July 22nd 2015.
[InitializeOnLoad]
[CustomEditor(typeof(SnapToGrid), true)]
[CanEditMultipleObjects]
public class SnapToGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SnapToGrid actor = target as SnapToGrid;
        if (actor.snapToGrid)
            actor.transform.localPosition = RoundTransform(actor.transform.localPosition, actor.snapValue);

        if (actor.sizeToGrid)
            actor.transform.localScale = RoundTransform(actor.transform.localScale, actor.sizeValue);

    }

    private Vector3 RoundTransform(Vector3 v, float snapValue)
    {
        return new Vector3(
            snapValue * Mathf.Round(v.x / snapValue),
            snapValue * Mathf.Round(v.y / snapValue),
            snapValue * Mathf.Round(v.z / snapValue)
            );
    }

}
