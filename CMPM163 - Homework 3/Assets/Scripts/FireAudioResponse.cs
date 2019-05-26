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
        int numPartitions = 8;
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
            aveMag[i] = 1.0f + aveMag[i] * 100;
            if (aveMag[i] > 100) 
            {
                aveMag[i] = 100;
            }
        }

        // Map the magnitude to the fire based on name
        if (gameObject.name == "Fire") 
        {
            float scale = 1 + (aveMag[0] - 1) / 3;
            if(scale > 1.75f) 
            { 
                scale = 1.75f; 
            }
            transform.localScale = new Vector3(1, 1, scale);
        } 
        for (int i = 1; i < numPartitions; i++) 
        {
            if (gameObject.name == "Fire (" + i + ")") 
            {
                float scale = 1 + (aveMag[i] - 1);
                if(i < numPartitions / 2)
                {
                    scale = 1 + (aveMag[i] - 1) / 2;
                }
                if(scale > 1.75f) 
                { 
                   scale = 1.75f; 
                }
                transform.localScale = new Vector3(1, 1, scale);
            }
        }
    }
}
