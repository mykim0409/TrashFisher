using UnityEngine;
using System.Collections;

public class Maneuver : MonoBehaviour {

    public Sprite[] popup;

    private float evadeTime { set; get; }
    private float targetManeuver;
    private int lv;
    private Boundary bound;
    private float x, y;

    private GameController gc;
	private int identity;
	public Sprite sp;
    public AudioClip ac;

    void Start() {
        gc = GameController.Instance;
        lv = gc.lv;
        bound = gc.b;
        x = Random.Range(bound.xMin, bound.xMax);
        y = Random.Range(bound.yMin, bound.yMax);

        transform.position = new Vector3(x, y);
        evadeTime = Random.Range(1.0f, 3.0f);
		identity = Random.Range(0, popup.Length);
		sp = popup[identity];
        StartCoroutine(RandomSet());
    }

    void Update() {
        iTween.MoveUpdate(this.gameObject,
            iTween.Hash("x", targetManeuver,
            "looptype", iTween.LoopType.pingPong
            , "time", evadeTime)
            );
    }

    IEnumerator RandomSet() {
            while (true) {
            targetManeuver = Mathf.Clamp(Random.Range(x - lv, x + lv), bound.xMin, bound.xMax);
            yield return new WaitForSeconds(evadeTime*1.5f);
        }
    }
}
