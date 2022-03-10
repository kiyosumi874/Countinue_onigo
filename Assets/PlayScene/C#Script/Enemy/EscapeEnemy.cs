using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeEnemy : Enemy
{
    private ForwardSearch mForwardSearch;
    // Start is called before the first frame update
    void Start()
    {
        mForwardSearch = this.gameObject.GetComponent<ForwardSearch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
