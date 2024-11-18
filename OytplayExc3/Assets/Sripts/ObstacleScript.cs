using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void Awake()
    {
        //used to format obstacles with a tag.
            //probably a better way to do this but this approach was used as obstacles can have more data added this way.
        this.tag = "Obstacle";
    }
}
