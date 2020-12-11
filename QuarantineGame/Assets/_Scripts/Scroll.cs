using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float startX;
    public float endX;
    public float speed;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;

        if(pos.x < endX)
        {
            pos.x = startX;
        }
        transform.position = pos;
        // GetComponent<Renderer>().material.mainTextureOffset = new Vector2((Time.time * speed) % 1, 0f);
    }
}
