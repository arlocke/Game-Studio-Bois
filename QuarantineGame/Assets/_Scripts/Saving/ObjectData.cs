using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public float[] position;
    public float[] rotation;
    public bool destroyed = false;

    public ObjectData(ObjectSaver obj)
    {
        position = new float[3];
        rotation = new float[3];
        position[0] = obj.transform.position.x;
        position[1] = obj.transform.position.y;
        position[2] = obj.transform.position.z;
        rotation[0] = obj.transform.eulerAngles.x;
        rotation[1] = obj.transform.eulerAngles.y;
        rotation[2] = obj.transform.eulerAngles.z;
    }

    public ObjectData()
    {
        destroyed = true;
    }
}
