using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void OnEnable(){
        print("Hello World from OnEnable Method!");
    }

    public Transform myObject;
    // Start is called before the first frame update
    void Start()
    {
        print("Hello World from Start Method!");
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Hello World from Update Method!");
    }

    void Awake() {
        print("Hello World from Awake Method!");
    } 

}
