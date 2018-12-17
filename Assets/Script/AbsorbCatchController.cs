using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbCatchController : MonoBehaviour {
	public GameObject spirit;

	int[] leftRight = new int[] {1,-1};
	int[] upDown = new int[] {1,-1};

	GameObject[] spirits = new GameObject[4];
	// Use this for initialization
	void Start () {
		StartCoroutine(generatorspirits(4, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		//user can catch the souls by pressing U, I, J, K which are coresponding to 4 locations
		bool UKeyPressed = Input.GetKeyDown(KeyCode.U);
		bool JKeyPressed = Input.GetKeyDown(KeyCode.J);
		bool KKeyPressed = Input.GetKeyDown(KeyCode.K);
		bool IKeyPressed = Input.GetKeyDown(KeyCode.I);

        bool catched = false;
		if (UKeyPressed) catched = checkIfSpiritIsOnTheBoader(2);
		if (JKeyPressed) catched = checkIfSpiritIsOnTheBoader(3);
		if (KKeyPressed) catched = checkIfSpiritIsOnTheBoader(1);
		if (IKeyPressed) catched = checkIfSpiritIsOnTheBoader(0);

        if (catched)
        Debug.Log(catched);
	}

	IEnumerator generatorspirits(int times, float timeStep)
	{
		yield return 0.5;
		for (int i = 0; i < times; i++)
		{
			yield return new WaitForSeconds(timeStep);
			var b = Instantiate(spirit) as GameObject;
			b.transform.SetParent(this.transform);
			b.transform.localPosition = new Vector2(0, 0);
			b.name = "spirit";

			var rb = b.GetComponent<Rigidbody2D>();
			int x = leftRight[Random.Range(0,2)];
			int y = upDown[Random.Range(0,2)];

			// 2---0
			// |   |
			// 3---1
			//store the spirit in spirit array
			if (x == 1 && y == 1)
				spirits[0] = b;
			else if (x == 1 && y == -1)
				spirits[1] = b;
			else if (x == -1 && y == 1)
				spirits[2] = b;
			else if (x == -1 && y == -1)
				spirits[3] = b;

			rb.velocity = new Vector2(x * 0.9f,y * 0.9f);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "spirit")
		{
			Destroy(other.gameObject);
		}
	}


    bool checkIfSpiritIsOnTheBoader(int direction)
    {
        var spirit = spirits[direction];
        bool isOnBoader = false;

        if (spirit)
        {
            var spiritCollider = spirit.GetComponent<Collider2D>();

            isOnBoader = spiritCollider.IsTouching(GetComponent<Collider2D>());
        }

        return isOnBoader;
    }
}
