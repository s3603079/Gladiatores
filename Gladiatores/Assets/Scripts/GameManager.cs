using GamepadInput;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

    public GamePad.Index oneIndex;
    public GamePad.Index twoIndex;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
    }

    void Update()
    {
    }

}
