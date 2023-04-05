using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCard : CardGroup
{
    public List<Image> imageLevel;
    public CanvasGroup pauseCardContent;
    public void SetPauseCard(Sprite sprite, string name, int level){
        pauseCardContent.alpha = 1;
        image.sprite = sprite;
        image.color = Color.white;
        title.text = name;
        for (int i = 0; i < level; i++)
        {
            imageLevel[i].color = Color.white;
        }
    }
    public void SetPauseCard(int level){
        imageLevel[level-1].color = Color.white;
    }
}
