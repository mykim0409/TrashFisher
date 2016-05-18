using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class map_to_stage : MonoBehaviour {
 
	public Button but1;
	public Button but2;
	public Button but3;
	public Button but4;
	public Button but5;
	//private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		but1.enabled = false;
		but2.enabled = false;
		but3.enabled = false;
		but4.enabled = false;
		but5.enabled = false;
        //PlayerPrefs.SetInt ("player_level", 1);
        //PlayerPrefs.Save ();
        //		spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
        //		spriteRenderer.sprite = CurrentSprite;
        int level = PlayerPrefs.GetInt ("player_level",1);
		Debug.Log (level);
		switch (level) {
		case 1:
			but1.enabled = true;
			break;
		case 2:
			but1.enabled = true;
			but2.enabled = true;
			break;
		case 3:
			but1.enabled = true;
			but2.enabled = true;
			but3.enabled = true;
			break;
		case 4:
			but1.enabled = true;
			but2.enabled = true;
			but3.enabled = true;
			but4.enabled = true;
			break;
		case 5:
			but1.enabled = true;
			but2.enabled = true;
			but3.enabled = true;
			but4.enabled = true;
			but5.enabled = true;
			break;
			
		}
		//		spriteRenderersp.enabled = false;
			}

	public void OnGUI1(){

		Debug.Log ("push1");
		PlayerPrefs.SetInt ("player_level", 1);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("scene2");
		//씬이동시에 연결이 되지않았다는 에러가 발생할 수 있음 그럴경우 file-buildsettings에서 씬들을 모두 add해주면 해결된다
	}

	public void OnGUI2(){

		Debug.Log ("push2");
		PlayerPrefs.SetInt ("player_level", 2);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("play02");
		//씬이동시에 연결이 되지않았다는 에러가 발생할 수 있음 그럴경우 file-buildsettings에서 씬들을 모두 add해주면 해결된다
	}
	public void OnGUI3(){

		Debug.Log ("push3");
		PlayerPrefs.SetInt ("player_level", 3);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("play03");
		//씬이동시에 연결이 되지않았다는 에러가 발생할 수 있음 그럴경우 file-buildsettings에서 씬들을 모두 add해주면 해결된다
	}
	public void OnGUI4(){

		Debug.Log ("push4");
		PlayerPrefs.SetInt ("player_level", 4);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("play04");
		//씬이동시에 연결이 되지않았다는 에러가 발생할 수 있음 그럴경우 file-buildsettings에서 씬들을 모두 add해주면 해결된다
	}
	public void OnGUI5(){

		Debug.Log ("push5");
		PlayerPrefs.SetInt ("player_level", 5);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("scene5");
		//씬이동시에 연결이 되지않았다는 에러가 발생할 수 있음 그럴경우 file-buildsettings에서 씬들을 모두 add해주면 해결된다
	}

}
