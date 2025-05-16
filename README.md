# WooTimer
好用的计时器 运行时+编辑器
## upm https://github.com/OnClick9927/WooTimer.git#upm

``` csharp
    //延时+API
     var delay = TimeEx.Delay(1, (time, delta) => { Debug.Log("Do Sth"); })
          .OnBegin((context) => { Debug.Log("OnBegin"); })
          .OnCancel((context) => { Debug.Log("OnCancel"); })
          .OnComplete((context) => { Debug.Log("OnComplete"); })
          .OnTick((time, delta) => { Debug.Log("OnTick"); })
          .SetTimeScale(2f)
          .SetId("Test_ID")
          .AddTo(this);
     delay.Pause();
     delay.UnPause();
     delay.Stop();
     delay.Cancel();
     // 多次调用+await 支持
     await TimeEx.Tick(1, 10, (time, delta) => { Debug.Log("Do Sth"); }).AddTo(this);
     Debug.Log("Do Sth");

     // 条件等待
     await TimeEx.While((time, delta) => { return Time.time < 5; }, 1f).AddTo(this);
     // 串行 计时器
     await TimeEx.Sequence()
         .NewContext(() => TimeEx.Delay(1, (time, delta) => { Debug.Log("Do Sth"); }))
         .NewContext(() => TimeEx.Delay(1, (time, delta) => { Debug.Log("Do Sth"); }))
         .AddTo(this);
     //并行 计时器
     await TimeEx.Parallel()
     .NewContext(() => TimeEx.Delay(1, (time, delta) => { Debug.Log("Do Sth"); }))
     .NewContext(() => TimeEx.Delay(1, (time, delta) => { Debug.Log("Do Sth"); }))
     .AddTo(this);
      // 杀掉 所有  用 AddTo 绑定到 this 的计时器
     this.KillTimers();
```
[更多](https://github.com/OnClick9927/WooTimer/blob/main/Assets/WooTimer/Runtime/TimeEx.cs)
