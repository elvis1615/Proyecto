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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up,10);
        Quaternion rot = Quaternion.identity;
        if (ray)
        {
            Debug.DrawRay(transform.position, -transform.up, Color.blue, 0.1f);
            rot = Quaternion.FromToRotation(transform.up, ray.normal) * transform.rotation;

        }

        else if(ray.distance>0.5) {
            rot = Quaternion.Euler(0,0,0);
        }

       transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 0.1f);
        
    }




}
