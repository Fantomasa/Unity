using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
public class Ads : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("61889",false);

		Advertisement.Show ();
	}
	
	IEnumerable ShowAdOnLoad()
	{
		while (!Advertisement.IsReady())
			yield return null;

		Advertisement.Show ();
	}
}
