using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public GameObject player;
	// Update is called once per frame
	void Update () {
        Vector3 v = player.transform.position;
        v.y = 12.5f; v.z = -10;
        this.transform.position = v;
	}
}
