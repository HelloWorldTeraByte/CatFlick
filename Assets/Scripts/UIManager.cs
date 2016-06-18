using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Animator flyHitTextAnimator;
	public Image rightWindSourceImage;
	public Image leftWindSourceImage;

	public void FadeOutFlyHitText()
	{
		flyHitTextAnimator.SetTrigger("bFageOutTrig");
	}

	public void ChangeWindSignToRight(bool right)
	{
		if(right)
		{
			rightWindSourceImage.enabled = true;
			leftWindSourceImage.enabled = false;
		}
		else
		{
			leftWindSourceImage.enabled = true;
			rightWindSourceImage.enabled = false;
		}
	}
}
