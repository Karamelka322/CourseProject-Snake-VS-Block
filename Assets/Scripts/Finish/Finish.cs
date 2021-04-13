using UnityEngine;

public class Finish : LoadingScene
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out SnakeHead sakeHead))
        {
            LoadScene("Main");
        }
    }
}
