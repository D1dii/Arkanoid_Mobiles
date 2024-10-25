using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;
    private Image image;
    public int brickLife = 1;
    public Sprite greenSprite;
    public Sprite blueSprite;
    public Sprite redSprite;
    public GameObject powerUpPrefab;
    public bool isDead = false;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        image = GetComponent<Image>();
        Debug.Log("Start Brick");
        if (brickLife == 3)
        {
            image.sprite = redSprite;
        }
        else if (brickLife == 2)
        {
            image.sprite = blueSprite;
        }
        else if (brickLife == 1)
        {
            image.sprite = greenSprite;
        }
        else if(brickLife == 0)
        {
            boxCollider2D.enabled = false;
            image.enabled = false;
            isDead = true;
        }
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.BrickFX();
        brickLife--;
        if (brickLife == 3)
        {
            image.sprite = redSprite;
        }
        else if (brickLife == 2)
        {
            image.sprite = blueSprite;
        }
        else if (brickLife == 1)
        {
            image.sprite = greenSprite;
        }
        else if (brickLife == 0)
        {
            
            int powerUpDrop = Random.Range(1, 100);

            if(powerUpDrop <= 20)
            {
                var powerUp = Instantiate(powerUpPrefab);
                powerUp.transform.SetParent(transform.parent.parent.transform, false);
                powerUp.transform.position = transform.position;
            }

            boxCollider2D.enabled = false;
            image.enabled = false;
            UIController.instance.AddPoints();
        }
        
    }
}
