using UnityEngine;
using GamepadInput;

public class PlayerInputPad : MonoBehaviour
{
    GamePad.Index thisPadNumber_ = GamePad.Index.Any;
    bool isOldLeftTriggerPush_ = false;

    public GamePad.Index PadNumber
    {
        get { return thisPadNumber_; }
        set { thisPadNumber_ = value; }
    }

    public float GetTrigger(GamePad.Trigger argTrigger)
    {
        return GamePad.GetTrigger(argTrigger, thisPadNumber_);
    }

    /// <summary>
    /// 押した瞬間のレフトトリガーの最深入力の瞬間を取る
    /// </summary>
    /// <param name="argTrigger"></param>
    /// <returns></returns>
    public bool GetLeftTriggerPushed()
    {
        bool isLeftTriggerPush = (GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, thisPadNumber_) >= 1.0f) ? true : false;

        if (isOldLeftTriggerPush_)
        {// さっきまで入力されていた
            isLeftTriggerPush = false;
        }
        else
        {// 入力されている瞬間
            isOldLeftTriggerPush_ = isLeftTriggerPush;
        }

        if((GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, thisPadNumber_) <= 0.0f))
        {// 入力無し(検知できない)
            isOldLeftTriggerPush_ = false;
        }

        return isLeftTriggerPush;
    }

    public Vector2 GetAxis(GamePad.Axis argStick)
    {
        return GamePad.GetAxis(argStick, thisPadNumber_);
    }

    public bool GetButtonDown(GamePad.Button argButton)
    {
        return GamePad.GetButtonDown(argButton, thisPadNumber_);
    }

}
