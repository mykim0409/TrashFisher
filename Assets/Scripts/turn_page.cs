using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class turn_page : MonoBehaviour {

	public Sprite CurrentSprite; 
	public Sprite NextSprite; 
	public float speed = 100f;
	public Sprite NextNextSprite; 
	public float positionX;
	public float positionY;
	private SpriteRenderer spriteRenderer; public float speedX;


	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
		spriteRenderer.sprite = CurrentSprite; 
		positionX = this.transform.position.x;
		positionY = this.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		
		int count = Input.touchCount;
		if( count == 0 ) return; //할 일이 없다.
		//실제touch처리
	
		for( int i = 0 ; i < count ; i++ ){
			Touch touch = Input.GetTouch(i);

			if(touch.phase == TouchPhase.Ended && spriteRenderer.sprite==CurrentSprite  )    // 터치하고 움직이믄 발생한다.
		{
			Debug.Log ("1");
				this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			spriteRenderer.sprite = NextSprite; 
				continue;
		}
			else if(touch.phase == TouchPhase.Ended && spriteRenderer.sprite==NextSprite)    // 터치하고 움직이믄 발생한다.
		{

			Debug.Log ("2");
				this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			spriteRenderer.sprite = NextNextSprite; 
				continue;
		}

			else if(touch.phase == TouchPhase.Ended && spriteRenderer.sprite==NextNextSprite)    // 터치하고 움직이믄 발생한다.
		{

			Debug.Log ("3");
					SceneManager.LoadScene ("play01");
					
				}
				}
	
	}}
