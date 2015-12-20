using UnityEngine;

namespace LGame.LBehaviour
{

    /******
     * 
     * 
     *  unity 脚本的生命周期
     * 
     *  行为组件基类，所以行为都需要继承它
     *
     * 
     */

    public abstract class ALBehaviour : CLBehaviour, ILBehaviour
    {
        public virtual void Awake()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void FixedUpdate()
        {
            OnFixedUpdate(Time.deltaTime);
        }

        public virtual void OnFixedUpdate(float fixedTime)
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime);
        }

        public virtual void OnLateUpdate(float deltaTime)
        {
        }

        public virtual void OnGUI()
        {
        }

        public virtual void OnApplicationPause()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnClear()
        {

        }

        public virtual void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        public virtual void OnDestroy()
        {
            OnClear();
        }

        public virtual void OnApplicationQuit()
        {

        }
    }

}