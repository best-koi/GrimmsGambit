using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Timer
{
    /// Timer class
    /// 
    /// Represents timer 
    ///  - Run on Update to use properly
    ///  - Can make it loop
    ///  - Can reset on full or not time
    /// 
    /// </summary>
    #region Private Fields
    
    // The timer interval
    [SerializeField]
    private float m_TimerInterval = 0;
    public float TimerInterval
    {
        get
        {
            return m_TimerInterval;
        }
        set
        {
            m_TimerInterval = value;
        }
    }

    // The current time of the timer
    // - When negative, the timer never stops
    [SerializeField]
    private float m_CurrentTime = 0;
    public float CurrentTime
    {
        get
        {
            return m_CurrentTime;
        }
    }

    #endregion

    // Timer constructor
    public Timer(float loopIntevral, bool setTimeToInterval)
    {
        m_TimerInterval = loopIntevral;
        m_CurrentTime = 0;

        ResetTimer(setTimeToInterval);
    }

    // Updates the timer in real-time
    //
    // - Run on Update method
    // - bool willLoop --> True for looping timer, False for standard timer
    public void UpdateTimer(bool willLoop)
    {
        if (m_CurrentTime > 0)
        {
            m_CurrentTime -= Time.deltaTime;
            if (m_CurrentTime < 0)
                m_CurrentTime = 0;
        }
        else if (willLoop)
            m_CurrentTime = m_TimerInterval;
    }

    // True if the current time is 0
    public bool TimeRanOut()
    {
        return m_CurrentTime == 0;
    }

    // Resets the timer 
    // - bool setTimerToInterval --> True for current time = timer interval, False for current time = 0
    public void ResetTimer(bool setTimeToInterval)
    {
        if (m_CurrentTime < 0 || Mathf.Abs(m_TimerInterval) < 0.001f)
            return;

        if (setTimeToInterval)
            m_CurrentTime = m_TimerInterval;
        else
            m_CurrentTime = 0;
    }

    // Resets the timer with a new timer interval
    // - bool setTimerToInterval --> True for current time = timer interval, False for current time = 0
    // - float neewInterval --> new timer interval
    public void ResetTimer(bool setTimerToInterval, float newInterval)
    {
        m_TimerInterval = newInterval;
        ResetTimer(setTimerToInterval);
    }

    // Returns the time ratio (1 for full time, 0 for empty time)
    public float GetCurrentTimeRatio()
    {
        return m_CurrentTime / m_TimerInterval;
    }
}
