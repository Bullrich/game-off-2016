using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// By @JavierBullrich

public class TierAnimation : MonoBehaviour {
    public Sprite[] arrowSprites;
    Image img, parentImg;
    public bool mainMenu;
    bool canCopyColor;
    private void Start()
    {
        img = GetComponent<Image>();
        if (mainMenu)
        {
            parentImg = transform.parent.GetComponent<Image>();
            StartCoroutine(CopyColor());
        }
    }
    IEnumerator CopyColor()
    {
        yield return new WaitForSeconds(1f);
        canCopyColor = true;
    }

    float changeTime = 0;
    public void Update()
    {
        if (canCopyColor)
        img.color = new Color(255, 255, 255, parentImg.color.a);
        if (changeTime < 1)
            img.sprite = arrowSprites[0];
        else if (changeTime < 2)
            img.sprite = arrowSprites[1];
        else
            changeTime = 0;
        changeTime += Time.deltaTime;
    }

}
