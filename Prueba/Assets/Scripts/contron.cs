using UnityEngine;
using System.Collections;

public class contron : MonoBehaviour {
    Rigidbody2D R;
    public float velocidad;
   

    // Use this for initialization
    void Start () {
        R = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {

        float a = Input.GetAxis("Horizontal");

        R.velocity = new Vector2(a * velocidad, R.velocity.y);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up);
        Quaternion rot = Quaternion.identity;
        if (ray) {
            Debug.DrawRay(transform.position, -transform.up, Color.blue, 0.1f);
            rot = Quaternion.FromToRotation(transform.up,ray.normal) * transform.rotation;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 0.1f);

    }




}
