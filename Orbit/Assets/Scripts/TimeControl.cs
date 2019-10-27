using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField]
    private float slowFactor = 0.1f;
    [SerializeField]
    private float slowPeriod = 1f;

    public void SlowTime()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = slowFactor * 0.02f;
    }
    void Update()
    {
        Time.timeScale += (1f / slowPeriod) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
