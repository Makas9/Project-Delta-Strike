using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {
    public static SceneFader Instance;

	public Image img;
	public AnimationCurve curve;

    private void Start()
    {
        Instance = this;
		StartCoroutine(FadeIn());
	}

	public void FadeTo (string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

    /// <summary>
    /// Load next level.
    /// </summary>
    public void NextLevel()
    {
        int level = int.Parse(SceneManager.GetActiveScene().name) + 1;
        FadeTo(level.ToString());
    }

    public void RestartLevel()
    {
        int level = int.Parse(SceneManager.GetActiveScene().name);
        FadeTo(level.ToString());
    }

    public void PreviousLevel()
    {
        int level = int.Parse(SceneManager.GetActiveScene().name)-1;
        if (level == 0)
        {
            FadeTo("MainMenu");
        }
        else
        {
            FadeTo(level.ToString());
        }
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
