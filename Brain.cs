using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public GameObject end;
    int DNALenght = 2;
    public DNA dna;
    public GameObject eyes;
    bool seewall = true;
    Vector3 start;
    public float distanceTravelled = 0.0f;
    public float distanceToTarget=Mathf.Infinity;
    bool alive = true;
    public void Init()
    {
        // 0 - forward
        //1 - turn
        dna = new DNA(DNALenght, 360);
        start = this.transform.position;
        end = GameObject.Find("EndPoint");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="dead")
        {
            distanceTravelled = 0;
            distanceToTarget = Mathf.Infinity;
            alive = false;
        }
    }	
	void Update () {
		if(!alive)
        {
            return;
        }
        seewall = false;
        RaycastHit hit;
        if (Physics.SphereCast(eyes.transform.position,0.1f, eyes.transform.forward ,out hit,0.5f))
        {
            if(hit.collider.gameObject.tag=="wall")
            {
                seewall = true;
            }
        }
	}
    private void FixedUpdate()
    {
        if(!alive)
        {
            return;
        }
        //h - turn
        float h = 0;
        //v - forward
        float v = dna.getGene(0);
        if(seewall)
        {
            h = dna.getGene(1);
        }
        this.transform.Translate(0, 0, v * 0.0001f);
        this.transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(start, this.transform.position);
        //distanceToTarget = Vector3.Distance(this.transform.position,end.transform.position);
    }
}
