using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA  {

    List<int> genes = new List<int>();
    int dnalength = 0;
    int max = 0;
    public DNA(int dnalength,int max)
    {
        this.dnalength = dnalength;
        this.max = max;
        Randomize();
    }
    private void Randomize()
    {
        genes.Clear();
        for(int i=0;i<dnalength;i++)
        {
            genes.Add(Random.Range(0, max));
        }
    }
    public void setInt(int pos,int val)
    {
        genes[pos] = val;
    }
    public void Combine(DNA d1,DNA d2)
    {
        for(int i=0;i<dnalength;i++)
        {
            if(i<dnalength/2)
            {
                int c = d1.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = d2.genes[i];
                genes[i] = c;
            }
        }
    }
    public void Mutate()
    {
        genes[Random.Range(0, dnalength)] = Random.Range(0, max);
    }
    public int getGene(int pos)
    {
        return genes[pos];
    }
}
