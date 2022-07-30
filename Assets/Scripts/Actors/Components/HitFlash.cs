using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public Color flashColor = Color.white;
    public float flashDuration = 0.25f;

    private SpriteRenderer sr;
    private Material mat;
    private IEnumerator flashCoroutine;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
        flashColor = Color.white;
        flashDuration = 0.25f;
    }

	public void Flash(){
		if (flashCoroutine != null)
		    	StopCoroutine(flashCoroutine);
		
		flashCoroutine = DoFlash();
        StartCoroutine(flashCoroutine);
    }

    private IEnumerator DoFlash()
    {
        float lerpTime = 0;

        while (lerpTime < flashDuration)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / flashDuration;

            SetFlashAmount(Globals.EaseOutSine(1f - perc, 0f, 1f, 1f));
            yield return null;
        }
        SetFlashAmount(0);
    }

    private void SetFlashAmount(float flashAmount)
    {
        mat.SetFloat("_FlashAmount", flashAmount);
    }
}
