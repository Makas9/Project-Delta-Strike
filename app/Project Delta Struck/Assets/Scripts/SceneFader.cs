using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {
    public static SceneFader Instance;

	public Image img;
	public AnimationCurve curve;

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo (string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

	IEnumerator FadeIn ()
	{
        float t = curve.keys[curve.keys.Length - 1].value;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color (0f, 0f, 0f, a);
			yield return null;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < curve.keys[curve.keys.Length-1].value)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return null;
		}

		SceneManager.LoadScene(scene);
	}


    public void Quit()
    {
        Application.Quit();
    }
}
