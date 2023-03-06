using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ThreatRadius : MonoBehaviour
{

    private AudioListener listener;
    public GameObject polo1;
    public GameObject polo2;
    public GameObject background;

    public AudioClip[] threatRadiusSounds;

    public float[] threatRadius;


    // Start is called before the first frame update
    void Start()
    {
        listener = this.GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis1 = XZDistance(transform.position, polo1.transform.position);
        float dis2 = XZDistance(transform.position, polo2.transform.position);

        if (dis1 > threatRadius[0] && dis2 > threatRadius[0]) {
            // To far away so should play the background sound
            polo1.GetComponent<AudioSource>().Stop();
            polo2.GetComponent<AudioSource>().Stop();
            background.GetComponent<AudioSource>().mute = false;
            Debug.Log("Background Sound");
        } else {
            background.GetComponent<AudioSource>().mute = true;
            if (dis1 < dis2 ) {
                // polo1.GetComponent<AudioSource>().mute = false;
                polo2.GetComponent<AudioSource>().Stop();
                makeThreatRadiusSound(polo1, dis1);
            } else if (dis1 >= dis2) {
                polo1.GetComponent<AudioSource>().Stop();
                // polo2.GetComponent<AudioSource>().mute = false;
                makeThreatRadiusSound(polo2, dis2);
            }
        }
    }

    void makeThreatRadiusSound(GameObject polo, float distance) {
        
        if (distance < threatRadius[2]) {
            // Polo in the closest ring
            Debug.Log("polo in the 3 ring");
            if (polo.GetComponent<AudioSource>().clip != threatRadiusSounds[2]) {
                polo.GetComponent<AudioSource>().Stop();
                polo.GetComponent<AudioSource>().clip = threatRadiusSounds[2];
                polo.GetComponent<AudioSource>().Play();
            }
        } else if (distance < threatRadius[1]){
            // Polo in the second closest ring
            Debug.Log("polo in the 2 ring");
            if (polo.GetComponent<AudioSource>().clip != threatRadiusSounds[1]) {
                polo.GetComponent<AudioSource>().Stop();
                polo.GetComponent<AudioSource>().clip = threatRadiusSounds[1];
                polo.GetComponent<AudioSource>().Play();
            }

        } else {
            // Polo in the furthest ring
            Debug.Log("polo in the 1 ring");
            if (!polo.GetComponent<AudioSource>().isPlaying || polo.GetComponent<AudioSource>().clip != threatRadiusSounds[0]) {
                polo.GetComponent<AudioSource>().Stop();
                polo.GetComponent<AudioSource>().clip = threatRadiusSounds[0];
                polo.GetComponent<AudioSource>().Play();
            }

        }
    }

    float XZDistance(Vector3 v1, Vector3 v2) {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.z - v2.z) * (v1.z - v2.z));
    }
}
