using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random : MonoBehaviour {
	public InputField min,max;
	int minn, maxn,n2;
	public GameObject uui,b2,ball,balltextp,stat;
	Text balltextc;
	// Use this for initialization
	void Start () {
		stat.SetActive (true);
		uui.SetActive (false);
		b2.SetActive(false);
	}
	

	//ボールを生成
	public void randomm()
	{
		minn =int.Parse(min.text);
		maxn = int.Parse(max.text);
		n2 = maxn - minn+1;
			for (int i = 0; i < n2; i++) {
			
			balltextc = balltextp.GetComponentInChildren<Text> ();
			balltextc.text = minn.ToString();
			Instantiate(ball,new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (-4.0f, 4.0f),0),Quaternion.identity);
			minn++;
		}
		Debug.Log (n2);
		uui.SetActive (false);
		b2.SetActive(true);
	}
	public void stop()
	{	GameObject[] balll = GameObject.FindGameObjectsWithTag ("ball");
		for(int i=0; i< balll.Length; i++)
		{
			Destroy(balll[i]);
		}
		uui.SetActive (true);
		b2.SetActive(false);
	}
	public void OnstartButton(){
		stat.SetActive (false);
		uui.SetActive (true);
		b2.SetActive(false);
	}
}
