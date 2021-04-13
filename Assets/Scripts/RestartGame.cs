public class RestartGame : LoadingScene
{
    private Snake _snake;

    private void Awake()
    {
        _snake = FindObjectOfType<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdated += OnSizeUpdated;
    }

    private void OnDisable()
    {
        _snake.SizeUpdated -= OnSizeUpdated;        
    }

    private void OnSizeUpdated(int size)
    {
        if(size == 0)
        {
            LoadScene("Main");
        }
    }
}
