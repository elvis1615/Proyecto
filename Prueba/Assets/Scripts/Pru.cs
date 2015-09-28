using UnityEngine;
using System.Collections;

public class Pru : MonoBehaviour {
    public float fuerzaSalto;
    public float velocidad;
    Rigidbody f;
	// Use this for initialization
	void Start () {
        f = GetComponent<Rigidbody>();
        f.velocity = new Vector3(velocidad, f.velocity.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0)) {
           f.velocity = new Vector2(f.velocity.x, fuerzaSalto);
            f.AddForce(new Vector2(0, fuerzaSalto));
        }

    }

}
