/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.116
 *UnityVersion:   2018.4.24f1
 *Date:           2020-11-29
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;

namespace WooTimer
{
    [UnityEngine.AddComponentMenu("")]
    class TimerScheduler_Runtime : MonoBehaviour
    {
        public TimerScheduler scheduler;
        private static TimerScheduler_Runtime ins;

        public static TimerScheduler_Runtime Instance
        {
            get
            {
                if (!Application.isPlaying) return null;
                if (ins == null)
                {
                    ins = new GameObject("Timer").AddComponent<TimerScheduler_Runtime>();
                    DontDestroyOnLoad(ins.gameObject);
                }
                return ins;
            }
        }
        private void Awake()
        {
            
            scheduler = new TimerScheduler();
        }

        private void Update()
        {
            scheduler.Update();
        }
        protected  void OnDestroy()
        {
            scheduler.KillTimers();
        }


    }

}
