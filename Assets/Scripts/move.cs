using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
    public float speed;
    public Vector3 v;
    public Transform play;
    
    private float temp = 7f;
    private GameController gc;

    void Start() {
        gc = GameController.Instance;
        play = GameObject.Find("Player").transform;
        speed = 5f;
        //찌를 화면의 끝까지 보내기 위한 함수
        v =  this.gameObject.transform.up * 23f + play.transform.position;
        if (v.x > temp + play.position.x || v.x < -temp + play.position.x) {
			v.y = Mathf.Abs(v.y * temp / (v.x - play.transform.position.x));
			v.x = Mathf.Clamp(v.x, -temp + play.position.x, temp + play.position.x);
            
        }
        iTween.MoveTo(this.gameObject, 
            iTween.Hash("x",v.x,
            "y", v.y ,
            "oncomplete", "Back", 
            "time", 1f));
    }
    
    void Back(){
        iTween.MoveTo(this.gameObject, 
            iTween.Hash("x",play.position.x,
            "y",play.position.y,
            "oncomplete","Des",
            "time",speed,"easetype",
            iTween.EaseType.linear));
    }
    void Des() {
        Destroy(this.gameObject);
        gc.DesFloat();
    }

    
}
