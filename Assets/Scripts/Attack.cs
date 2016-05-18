using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    private GameController gc;

    void Start() {
        gc = GameController.Instance;
    }

    void Update() {

	
		if (gc.throwing()) {
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else{
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
		Handheld.Vibrate();

        if (other.CompareTag("Enemy")) {

			gc.Catch(other.gameObject);
			Destroy(other.gameObject);
			Destroy(this.gameObject);
            gc.DesFloat ();
        }
        else {
            Destroy(this.gameObject);
            gc.Miss();
            gc.combo = 0;
            gc.DesFloat();
            Debug.Log("Missed");
        }
    }
}

