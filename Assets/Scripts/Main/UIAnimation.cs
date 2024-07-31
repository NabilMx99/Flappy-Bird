using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    private float _maxY = 1.5f;
    private float _minY = 0.8f;
    public bool isGoingUp;

    void Start()
    {

        isGoingUp = true;

    }

    void Update()
    {

        AnimateUI();

    }

    void AnimateUI()
    {

       if(isGoingUp)
       {

        transform.Translate(Vector2.up * Time.deltaTime);

        if(transform.position.y >= _maxY)
        {
       
           isGoingUp = false;

        }  

       } else {

        transform.Translate(Vector2.down * Time.deltaTime);

        if(transform.position.y <= _minY)
        {

          isGoingUp = true;

        }  
       }
    }
}

