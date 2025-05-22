/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.116
 *UnityVersion:   2018.4.24f1
 *Date:           2020-11-29
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;

namespace WooTimer
{
    public abstract class TimerContextBase : ITimerContext, IPoolObject
    {
        //public string guid { get; private set; } = Guid.NewGuid().ToString();
        public ITimerContext Bind<T>(T data)
        {
            bind = data;
            return this;
        }

        public T GetBind<T>()
        {
            if (bind == null)
                return default;
            try
            {
                return (T)bind;
            }
            catch (Exception)
            {

                return default;
            }
        }
        object bind;
        internal bool isDone { get; private set; }
        internal bool canceled { get; private set; }
        public bool valid { get; set; }

        public string id { get; private set; }
        public object owner { get; private set; }

        internal TimerScheduler scheduler;

        //public bool valid { get; internal set; }
        private Action<ITimerContext> onComplete;
        private Action<ITimerContext> onCancel;
        private Action<ITimerContext> onBegin;
        private TimerAction onTick;
        protected float timeScale { get; private set; }
        internal void OnComplete(Action<ITimerContext> action) => onComplete += action;
        internal void OnCancel(Action<ITimerContext> action) => onCancel += action;
        internal void OnTick(TimerAction action) => onTick += action;
        internal void OnBegin(Action<ITimerContext> action) => onBegin += action;

        protected void InvokeBegin()
        {
            onBegin?.Invoke(this);
        }
        protected void InvokeTick(float time, float delta)
        {
            onTick?.Invoke(time, delta);

        }

 
        protected virtual void Reset()
        {
            bind = null;
            id = string.Empty;
            owner = null;
            onBegin = null;
            onCancel = null;
            onComplete = null;
            onTick = null;

            isDone = false;
            canceled = false;
            timeScale = 1;



        }

        protected virtual void StopChildren() { }
        private void _Cancel(bool invoke)
        {
            if (!((IPoolObject)this).valid || canceled || isDone) return;
            if (canceled) return;
            canceled = true;

            if (invoke)
                onCancel?.Invoke(this);
            StopChildren();
            TimeEx.Cycle(this);
        }
        internal void Stop() => _Cancel(false);
        internal void Cancel() => _Cancel(true);
        internal void SetId(string id)
        {
            if (!((IPoolObject)this).valid) return;
            this.id = id;
        }
        internal void SetOwner(object owner)
        {
            if (!((IPoolObject)this).valid) return;
            this.owner = owner;
        }
        protected void Complete()
        {
            if (isDone) return;
            isDone = true;
            onComplete?.Invoke(this);
            TimeEx.Cycle(this);

        }


        public virtual void SetTimeScale(float timeScale)
        {
            if (!((IPoolObject)this).valid) return;
            this.timeScale = timeScale;
        }

        public abstract void Pause();

        public abstract void UnPause();






        void IPoolObject.OnGet()
        {
           
            Reset();
        }

        void IPoolObject.OnSet()
        {
            Reset();
        }


    }

}
