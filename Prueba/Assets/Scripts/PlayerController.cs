using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text text;
    Rigidbody2D Jugador;
    public float velocidad;
    public float VelocidadRotacion;
    float respaldo;
	// Use this for initialization
	void Start () {
        respaldo = velocidad;
        Jugador = GetComponent<Rigidbody2D>();
        text.text = "ola";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        float x = Input.acceleration.x*4/*Input.GetAxis("Horizontal")*/;
        float YS = Input.acceleration.y + 0.5f;
        float y;
        if (YS < 2.1)
        {
            y = (YS) * 6 /*Input.GetAxis("Vertical")*/;
        }
        else 
        {
            y = 2.1f* 6f;
        }
        
        
    Vector2 movement = new Vector2(x, y);
    Jugador.velocity = movement * velocidad * Time.deltaTime;

    if(x!=0 || y!=0)
    {
        float angulo = Mathf.Atan2(-x, y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag=="Correr")
        {
        StartCoroutine(Acelerar());
        Destroy(other.gameObject,0.05f);
        text.text = "ke ase";
        }
        

        
    }

    IEnumerator Acelerar() 
    {
        velocidad = velocidad + 400;
        for (int i = 0; i < 10;i++ )
        {
            
            if (i == 9)
            {
                velocidad = respaldo;
                StopCoroutine(Acelerar());
            }
            yield return new WaitForSeconds(0.7f);
        }

    }
}
