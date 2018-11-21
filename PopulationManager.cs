using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject botPre;
    public GameObject startingpos;
    public GameObject endingpos;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trial = 5 ;
    int generation = 1;
    // Use this for initialization
    GUIStyle gui = new GUIStyle();
    void OnGUI()
    {
        gui.fontSize = 25;
        gui.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", gui);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, gui);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), gui);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, gui);
        GUI.EndGroup();
    }
	void Start () {
		for(int i=0;i<populationSize;i++)
        {
            GameObject b = Instantiate(botPre, startingpos.transform.position, this.transform.rotation);
            b.GetComponent<Brain>().Init();
            population.Add(b);
        }
	}
    private GameObject Breed(GameObject p1,GameObject p2)
    {
        GameObject child = Instantiate(botPre, startingpos.transform.position, this.transform.rotation);
        Brain b = child.GetComponent<Brain>();
        // Mutation probability 1 /  population size
        if(Random.Range(0,populationSize)==1)
        {
            b.Init();
            b.dna.Mutate();
        }
        else
        {
            b.Init();
            b.dna.Combine(p1.GetComponent<Brain>().dna, p2.GetComponent<Brain>().dna);
        }
        return child;
    }
    private void  BreedNewPop()
    {

        List<GameObject> sorted = population.OrderBy(o => o.GetComponent<Brain>().distanceTravelled).ToList();
        //List<GameObject> sorted = population.OrderByDescending(o => o.GetComponent<Brain>().distanceToTarget).ToList();
        population.Clear();
        for(int i=(int)(sorted.Count/2.0f)-1;i<sorted.Count-1;i++)
        {
            population.Add(Breed(sorted[i], sorted[i + 1]));
            population.Add(Breed(sorted[i + 1], sorted[i]));
        }
        for(int i=0;i<sorted.Count;i++)
        {
            Destroy(sorted[i]);
        }
        ++generation;
    }
	
	void Update () {
        elapsed += Time.deltaTime;
        if(elapsed>=trial)
        {
            BreedNewPop();
            elapsed = 0;
        }
		
	}
}
