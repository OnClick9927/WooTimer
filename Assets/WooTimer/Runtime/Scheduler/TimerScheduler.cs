﻿/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.116
 *UnityVersion:   2018.4.24f1
 *Date:           2020-11-29
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System.Collections.Generic;
using System;

namespace WooTimer
{
    class TimerScheduler
    {
        private List<TimerContext> timers;
        private List<ITimerGroup> groups;


        private Dictionary<Type, ISimpleObjectPool> contextPools;

        public TimerScheduler()
        {
            contextPools = new Dictionary<Type, ISimpleObjectPool>();
            timers = new List<TimerContext>();
            groups = new List<ITimerGroup>();
        }

        public ITimerGroup Sequence()
        {
            var group = _NewTimerContext<TimerSequence>();

            groups.Add(group);
            return group;
        }

        public ITimerGroup Parallel()
        {
            var group = _NewTimerContext<TimerParallel>();
            groups.Add(group);
            return group;
        }

        public T _NewTimerContext<T>() where T : TimerContextBase, new()
        {
            Type type = typeof(T);
            ISimpleObjectPool pool = null;
            if (!contextPools.TryGetValue(type, out pool))
            {
                pool = new SimpleObjectPool<T>();
                contextPools.Add(type, pool);
            }
            var simple = pool as SimpleObjectPool<T>;
            var cls = simple.Get();
            cls.scheduler = this;
            return cls;
        }
        public T NewContext<T>() where T : TimerContext, new() => _NewTimerContext<T>();
        public void Cycle(ITimerContext context)
        {
            var type = context.GetType();
            if (context is TimerContext)
                timers.Remove(context as TimerContext);
            if (context is ITimerGroup)
                groups.Remove(context as ITimerGroup);

            ISimpleObjectPool _pool = null;
            if (contextPools.TryGetValue(type, out _pool))
                _pool.SetObject(context);
        }

        public ITimerContext RunTimerContext(TimerContext context)
        {
            context.UnPause();
            timers.Add(context);
            return context;
        }
        public void KillTimers()
        {
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                var timer = groups[i];
                timer.Stop();
            }
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                timer.Stop();
            }
        }
        public void KillTimers(object obj)
        {
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                var timer = groups[i];
                if ((timer as TimerContextBase).owner == obj)
                    timer.Stop();
            }
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                if (timer.owner == obj)
                    timer.Stop();
            }
        }

        internal void Update()
        {
            float deltaTime = TimeEx.GetDeltaTime();
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                timer.Update(deltaTime);
            }


        }
    }

}
