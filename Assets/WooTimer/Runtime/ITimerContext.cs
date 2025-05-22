/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.116
 *UnityVersion:   2018.4.24f1
 *Date:           2020-11-29
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
namespace WooTimer
{
    public delegate bool TimerFunc(float time, float delta);
    public delegate void TimerAction(float time, float delta);


    public interface ITimerContext
    {
        string id { get; }
        ITimerContext Bind<T>(T data);
        T GetBind<T>();
        //bool isDone { get; }
        //bool canceled { get; }

    }

}
