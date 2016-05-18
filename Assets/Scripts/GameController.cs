using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Boundary {
	public float xMin, xMax;
	public float yMin, yMax;
}

public class GameController : MonoBehaviour {

	private static GameController instance = null;
	public static GameController Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType(typeof(GameController)) as GameController;
			}

			if (instance == null) {
				GameObject obj = new GameObject("GameController");
				instance = obj.AddComponent(typeof(GameController)) as GameController;
			}
			return instance;
		}
	} 

	public int lv;
	public int total;
	public float prob;
	Vector3[] dir;
	public Boundary b;
	public GameObject goFish;
	public GameObject goTrash;
	//public Canvas canvas;
	public GameObject popUp;
	public Animator anim;
	//public bool fish;
	public bool fish_throw;
	private Vector2 range;
	public Button.ButtonClickedEvent be1;

	public Button btn1;
	public Button btn2;
	public Button.ButtonClickedEvent be2;
	//private bool item1 = false;
	//private bool item2 = false;

	public int combo { get; set; }
	public int hungryPoint { set;  get ; }
	//public int cur { set; get; }
	public bool isShot { set; get; }
	public bool isTouch { get; set; }
	public bool fall = false;
    
	private List<GameObject> collection;
	public Slider slider;

	//수정
	public GameObject player;
	public bool btn1Flag;
	public bool btn2Flag;

    public Text text;
    public Sprite miss;
    public GameObject gameOver;
    public GameObject gameClear;
    public int clearCondition;

    public AudioClip missClip;

    void Start() {
		Init();

		StartCoroutine(RandomGenerate());
	}

	void Update() {
		HpController();
		if (0 >= clearCondition) {
            gameClear.SetActive(true);
            int pl = PlayerPrefs.GetInt("player_level");
            PlayerPrefs.SetInt("player_level", pl + 1);
            Time.timeScale = 0;
            GetComponent<AudioSource>().Stop();
		}
		if (hungryPoint < 0) {
            gameOver.SetActive(true);
            Time.timeScale = 0;
            GetComponent<AudioSource>().Stop();
        }
		if (Input.touchCount>0) {

			isTouch = true;
			IniFloat ();
		}
		else {
			isTouch = false;
			isShot = false;
			anim.SetBool("isEnabledUI", true);
		}
		if (hungryPoint < 40f) {
			btn1.interactable = false;
			btn2.interactable = false;
		}
		//else {
		else {
			if (btn1Flag == false) {
				btn1.interactable = true;
			}
			if (btn2Flag == false) {
				btn2.interactable = true;
			}
		}
		for (int i = 4; i >= 1; i--)
		{
			dir[i] = dir[i - 1];
		}
		dir[0] = Input.acceleration;
	}

	void Init() {
		//수정 Canvas내의 오브젝트 interactable 설정
		//be1 = new Button.ButtonClickedEvent();
		//be1.AddListener(Item1F);
		//btn1.onClick = be1;
		collection = new List<GameObject>(total);

		//be2 = new Button.ButtonClickedEvent();
		//be2.AddListener(Item2F);
		//btn2.onClick = be2;
		//
		//fish = false;
		fish_throw = false;

		hungryPoint = PlayerPrefs.GetInt("player_hp",100);
		range.x = 0; range.y = 1;
		//prob = 0.8f;
		//cur = 0;
		combo = 0;
		isShot = false;
		isTouch = false;
		for (int i = 0; i < total; i++) {
			Generate();
		}
		Input.gyro.enabled = true;
		dir = new Vector3[10];
		for(int i=0; i<5; i++) {
			dir[i] = Input.gyro.userAcceleration;
		}
		slider.wholeNumbers = true;
		slider.minValue = 0f;
		slider.maxValue = 100f;
		slider.value = 50f;
        
        text.text = "X " + clearCondition;
    }

	IEnumerator RandomGenerate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.0f, 5.0f));
			if (total > collection.Count) {
				Generate();
			}
		}
	}
	public void Item1F() {
		hungryPoint -= 20;
		StartCoroutine(Item1Routine());
	}
	IEnumerator Item1Routine() {
		btn1Flag = true;
		total *= 2;
		collection.Capacity = total;
		for (; collection.Count < total;) {
			Generate();
		}
		yield return new WaitForSeconds(60f);
		btn2Flag = false;
		total /= 2;
		for(;collection.Count > total;){
			Destroy(collection.ToArray()[0]);
			collection.RemoveAt(0);
		}
		collection.Capacity = total;
	}
	//수정
	public void Item2F() {
		hungryPoint -= 20;
		StartCoroutine(Item2Routine());
	}
	IEnumerator Item2Routine() {
		btn2Flag = true;
		yield return new WaitForSeconds(60f);
		btn2Flag = false;
	}
	//
	void Generate() {
		float rnd = Random.Range(range.x, range.y);
		GameObject tmp;
		if (rnd > prob) {
			tmp = Instantiate(goFish);
		}
		else {
			tmp = Instantiate(goTrash);
		}
		collection.Add(tmp);
	}
	//
	public void DesFloat() {
		anim.SetBool("isEnabledUI", true);
		//canvas.enabled = true;
		isShot = false;
		fall = false;

		fish_throw = false;
	}
	public void IniFloat() {
		anim.SetBool("isEnabledUI", false);
		//canvas.enabled = false;
		isShot = true;

	}
	public bool fishing(){
		
		bool fish = false;

		 if ((dir[0].z - dir[4].z) < -0.7 && fall==false && Input.gyro.rotationRateUnbiased.x<-90){

			fall = true;	
			Debug.Log ("던짐");
			fish = true;

		}
		return fish;
	}
	public bool throwing(){
		
		//bool fish = false;

		if ((dir [0].z - dir [4].z) > 0.7 && fall == true && Input.gyro.rotationRateUnbiased.x >90) {
			Debug.Log ("당김");
		
			fish_throw = true;

			//fall = false;
		}

		return fish_throw;

	}

	public void Catch(GameObject other) {
		Maneuver m = other.GetComponent<Maneuver>();
        StartCoroutine(audPlay(m.ac));
		StartCoroutine(PopUpRoutine(m.sp));
		combo++;
		collection.Remove(other);
        if (other.name.Contains("Trash")) {
            Debug.Log("clearCondition");
            clearCondition--;
            text.text = "X " + clearCondition;
        }
        else {
            hungryPoint += 10;
        }

    }

    public IEnumerator audPlay(AudioClip aud) {
        Debug.Log("aud");
        GameObject go = new GameObject();
        go.AddComponent<AudioSource>();
        go.GetComponent<AudioSource>().clip = aud;
        go.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(go);
    }
    public void Miss() {
        StartCoroutine(audPlay(missClip ));
        StartCoroutine(PopUpRoutine(miss));
    }

    IEnumerator PopUpRoutine(Sprite image) {
		GameObject go = new GameObject();
		go.transform.SetParent(popUp.transform);
		Debug.Log(popUp.transform);
		Image i = go.AddComponent<Image>();
        Vector2 size = new Vector2(image.texture.width, image.texture.height);
        float ratio;
        if (size.y > size.x) {
            ratio = size.x / size.y;
            size = new Vector2(ratio * 500, 500);
        }
        else {
            ratio = size.y / size.x;
            size = new Vector2(500, ratio * 500);
        }

        i.sprite = image;
		i.rectTransform.localPosition = new Vector3(0, 0, 0);
		i.rectTransform.localScale = new Vector3(1, 1, 1);
        i.rectTransform.sizeDelta = size;
		i.color = new Vector4(1, 1, 1, 1);
		yield return new WaitForSeconds(1f);
		Destroy(go.gameObject);
	}
    
	void HpController() {
        hungryPoint = hungryPoint > 100 ? 100 : hungryPoint;
		slider.value = hungryPoint;
		slider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, (float)hungryPoint / 100f);
	}
}
