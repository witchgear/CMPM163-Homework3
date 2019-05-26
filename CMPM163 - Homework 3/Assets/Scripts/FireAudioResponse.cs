using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudioResponse : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // consolidate spectral data to 8 partitions (1 partition for each fire)
        int numPartitions = 1;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2;

        for (int i = 0; i < numDisplayedBins; i++) 
        {
            if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData [i] / (512  / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for(int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = 0.5f + aveMag[i] * 100;
            if (aveMag[i] > 100) 
            {
                aveMag[i] = 100;
            }
            Debug.Log(aveMag[i]);
        }

        // Map the magnitude to the fire based on name
        if (gameObject.name == "Fire") 
        {
            // ParticleSystem ps = GetComponent<ParticleSystem>();
            // var main = ps.main;
            // main.startSize = 10 + aveMag[0];
            
            transform.localScale = new Vector3 (1, 1, aveMag[0]);
        } 
        for (int i = 1; i < numPartitions; i++) 
        {
            if (gameObject.name == "Fire (" + i + ")") 
            {
                

                // transform.localScale = new Vector3 (aveMag[i], aveMag[i], aveMag[i]);
            }
        }
    }
}
