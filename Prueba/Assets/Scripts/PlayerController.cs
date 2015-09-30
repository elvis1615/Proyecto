using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    PlayerScript playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

	}
	
	// Update is called once per frame
	void Update () {
				keyboardInput();
	}


    void keyboardInput()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
               playerScript.MoveRight();
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
          
             playerScript.MoveLeft();
            
        }
        else
        {
              playerScript.StayIdle();
        }

        // we can only jump whilst grounded
        if (Input.GetKeyDown(KeyCode.Space))
        {
                playerScript.DoJump();
    
        }
    }

}
