using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wind : MonoBehaviour 
{
	public float minWindForce = 0.5f;
	public float maxWindForce = 4.5f;
	private float windForce;
	public bool rightWindSource;
	public Text windForceText;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			if(rightWindSource)
				other.GetComponent<Ball>().WindReflect(-windForce);
			else
				other.GetComponent<Ball>().WindReflect(windForce);
		}
	}

	public void ChangeWindSource()
	{
		float randomWindSideChanger;
		windForce = Random.Range(minWindForce, maxWindForce);
		randomWindSideChanger = Random.Range(0f, 1f);

		if(randomWindSideChanger > 0.5f)
			rightWindSource = true;
		else
			rightWindSource = false;
		windForceText.text = windForce.ToString("##.#");
	}
}
