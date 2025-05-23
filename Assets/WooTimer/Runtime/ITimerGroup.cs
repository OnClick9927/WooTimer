﻿/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.116
 *UnityVersion:   2018.4.24f1
 *Date:           2020-11-29
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
namespace WooTimer
{
    public interface ITimerGroup : ITimerContext
    {
        ITimerGroup NewContext(System.Func<ITimerContext> func);
        ITimerGroup Run();
    }

}
