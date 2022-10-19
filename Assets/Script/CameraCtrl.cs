using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        if(this.transform.position.x<0)
        {
            transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
        }
    }
}
