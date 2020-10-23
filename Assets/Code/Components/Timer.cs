using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime;
    public float StartTime { get => startTime; set => startTime = value; }

    [SerializeField] private float count;
    public float Count { get => count; set => count = value; }

    [SerializeField] private bool finished;
    public bool Finished { get => finished; set => finished = value; }

    [SerializeField] private bool loopTimer;
    public bool LoopTimer { get => loopTimer; set => loopTimer = value; }

    //public OnTimerFinished onTimerFinished;

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        count = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
    }
    public void RunTimer()
    {
        count -= Time.deltaTime;

        if (count <= 0)
        {
            count = 0;
            finished = true;

            if (loopTimer)
                ResetTimer();
        }
        else
        {
            finished = false;
        }
    }
    public int GetIntTime()
    {
        return (int)Count;
    }
    public void ResetTimer()
    {
        count = startTime;
        finished = false;
    }
    public void ResetTimer(float newCount)
    {
        count = newCount;
    }
}
