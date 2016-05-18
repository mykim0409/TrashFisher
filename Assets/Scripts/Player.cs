using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  //  public float shotSpeed;
  //  private float shotDelay;

    public GameObject shotSpawn;
    public GameObject shot;
    public GameController gc;

    private Vector3 rotateDir;
  //  private Vector3 trans;
    private float horizontal;

    // Use this for initialization
    void Start() {
		Input.gyro.enabled = true;
        gc = GameController.Instance;
        //shotDelay = shotSpeed;
        rotateDir = new Vector3(0, 0, 90f);
    //    trans = Vector3.zero;
    }

    void Update() {
        //if (shotSpeed < (shotDelay += Time.deltaTime)) {
		if (gc.isTouch == false && gc.fall==false) {
			float moveHorizontal = Input.acceleration.x;
			//this.transform.Translate(Input.acceleration.x,0,0);
			//this.transform.position= new Vector3(Input.gyro.rotationRateUnbiased.z,0.0f, 0.0f);
            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
			GetComponent<Rigidbody2D> ().velocity = movement*25f;

            GetComponent<Rigidbody2D>().position = new Vector2(
                    Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -12.5f, 12.5f),
                    0.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (gc.isTouch) {
            if (shotSpawn.gameObject.transform.position.y < 0.3f) {
                rotateDir.z = rotateDir.z > 0 ? -90f : 90f;
            }
			GetComponent<Rigidbody2D> ().velocity =Vector2.zero;
            iTween.RotateUpdate(this.gameObject, rotateDir, 3.5f);
			if (gc.fishing()&&gc.isShot) {
                gc.hungryPoint -= 4;
				GameObject temp = Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
				if (gc.btn2Flag) {
					//Vector3 v = temp.transform.localScale;
					temp.transform.localScale *= 2;
				}
                //shotDelay = 0;
            }
        }
    }
}
