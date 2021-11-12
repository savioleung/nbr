using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class findball : MonoBehaviour {
	public GameObject myatk;
	private GameObject nearObj;
	GameObject targetObj = null; 
	public Text endui;
	Vector2 v;
	Vector3 target;
	Rigidbody2D body;
	private float atkTime = 0; 
	float reloadtime = 0;
	public float HP,ATK,DEF,SPD,ATS,RSP;

	void Awake()
	{

		body = GetComponent<Rigidbody2D> ();
		nearObj = serchTag(gameObject, "ball");
		HP = Random.Range (1, 100);
		ATK = Random.Range (1, 100);
		DEF = Random.Range (1, 100);
		SPD = Random.Range (0.5f, 5.0f);
		ATS = Random.Range (0.1f, 3.0f);
		RSP = Random.Range (0.1f, 3.0f);
		if (ATS>= 1.7f&& RSP>=1.1f) {
			ATK*=2;
		}
		myatk.SetActive (false);

	}
	void Start () {
			nearObj	 = serchTag(gameObject, "ball");

	}
	void Update () {

		attacking();
		FindingBall();

		if (HP <= 0)
		{
			Destroy(this.gameObject);
		}
	}
	//攻撃
	void attacking()
	{
		//atk

		atkTime += 1 * Time.deltaTime;
		if (atkTime >= ATS)
		{
			myatk.SetActive(true);
			reloadtime += 1 * Time.deltaTime;

			if (reloadtime >= RSP)
			{
				if (ATS <= 1.0f) { ATS -= 0.03f; }
				myatk.SetActive(false);
				reloadtime = 0;
				atkTime = 0;
			}
		}
	}
	//移動
	void FindingBall()
	{
		if (nearObj == null)
		{
			nearObj = serchTag(gameObject, "ball");
			if (nearObj == null)
			{
				nearObj = this.gameObject;
			}
		}
		target = nearObj.transform.position;
		Vector3 norTar = (target - transform.position).normalized;
		float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;

		Quaternion rotation = new Quaternion();
		rotation.eulerAngles = new Vector3(0, 0, angle - 90);
		transform.rotation = rotation;

		//move
		v.x = Mathf.Cos(Mathf.Deg2Rad * angle) * SPD;
		v.y = Mathf.Sin(Mathf.Deg2Rad * angle) * SPD;
		body.velocity = v;

	}

	//タグを探して、一番近いオブジェクトを返す
	GameObject serchTag(GameObject nowObj, string tagName)
	{
		float tmpDis = 0;
		float nearDis = 0;
		GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
		GameObject[] oballs = new GameObject[balls.Length - 1];
		int index = 0;

		for (int i = 0; i < balls.Length; i++)
		{
			if (balls[i] == this.gameObject)
				continue;

			oballs[index] = balls[i];
			index++;
		}
		if (oballs != null)
		{
			foreach (GameObject obs in oballs)
			{
				tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);
				if (nearDis == 0 || nearDis > tmpDis)
				{
					nearDis = tmpDis;
					targetObj = obs;
				}

			}
		}
		return targetObj;

	}
	//HP計算
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "atk") {
			Debug.Log ("hit");

			float damg=other.gameObject.GetComponentInParent<findball> ().ATK -this.DEF;
			if (damg <= 1) {
				damg = 1;
				DEF -= 5;
			}
			HP -= damg;
		}
	}
}
