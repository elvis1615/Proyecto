using UnityEngine;
using System.Collections;

public class contron : MonoBehaviour {
    Rigidbody2D R;
    public float velocidad;
    public Transform aux;
   

    // Use this for initialization
    void Start () {
        R = GetComponent<Rigidbody2D>();
        R.velocity = new Vector2(velocidad, R.velocity.y);
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {

        float a = Input.GetAxis("Horizontal");

        if (Input.GetMouseButton(0)) {
            R.velocity = new Vector3(velocidad, R.velocity.y,0);
            R.AddForce(new Vector2(0,500));            
        }

        R.velocity = new Vector2(a * velocidad, R.velocity.y);
       
        
    }




}
