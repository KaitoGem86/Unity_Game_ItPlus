using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject thisObject;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 position = transform.position;
        //position.x = target.transform.position.x;
        //position.y = target.transform.position.y + 1; 
        //transform.position = position;
        FollowCharacter();
    }

    void FollowCharacter()
    {
        Vector3 position = transform.position;
        position.x = target.transform.position.x;
        if(!isBackground())
            position.y = target.transform.position.y + 1;
        transform.position = position;
    }

    bool isBackground()
    {
        if(thisObject.tag == "Background")
            return true;
        return false;
    }

}
