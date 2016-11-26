using UnityEngine;
using System.Collections;

public class FixParticleEffectToLayer : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        Renderer renderer = ps.GetComponent<Renderer>();
        renderer.sortingLayerName = "Player";
        renderer.sortingOrder = -1; //prevents the particels to spawn above the player (because its in the same Layer!)
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
