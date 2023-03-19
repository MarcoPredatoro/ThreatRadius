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
            SetMute(polo1, new bool[]{true, true, true});
            SetMute(polo2, new bool[]{true, true, true});
            background.GetComponent<AudioSource>().mute = fal​se;
            Debug.Log("Background Sound");
        } else {
            background.GetComponent<AudioSource>().mute = true;
            if (dis1 < dis2 ) {
                SetMute(polo2, new bool[]{true, true, true});
                makeThreatRadiusSound(polo1, dis1);
            } else if (dis1 >= dis2) {
                SetMute(polo1, new bool[]{true, true, true});
                makeThreatRadiusSound(polo2, dis2);
            }
        }
    }

    void makeThreatRadiusSound(GameObject polo, float distance) {
        
        if (distance < threatRadius[2]) {
            // Polo in the closest ring
            Debug.Log("polo in the 3 ring");
            SetMute(polo, new bool[]{false, false, false});
        } else if (distance < threatRadius[1]){
            // Polo in the second closest ring
            Debug.Log("polo in the 2 ring");
            SetMute(polo, new bool[]{false, false, true});

        } else {
            // Polo in the furthest ring
            Debug.Log("polo in the 1 ring");
            SetMute(polo, new bool[]{false, t​rue, true});

        }
    }

    void SetMute(GameObject polo, bool[] isMuted) {
        // Sets whether the child of the polo's audio sources are muted or not based on the bool array
        for (int i = 0 ; i < isMuted.Length; i++){
            polo.transform.GetChild(i).GetComponent<AudioSource>().mute = isMuted[i];
        }
    }

    float XZDistance(Vector3 v1, Vector3 v2) {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.z - v2.z) * (v1.z - v2.z));
    }
}
