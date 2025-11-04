using System;
using System.Reflection;
using UnityEngine;

public class DIContainer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _monoBehaviour;

    private void Awake()
    {
        foreach (var monoBahaviour in _monoBehaviour)
        {
            Inject(monoBahaviour);
            Debug.Log($"<color=yellow>Inject</color>: <color=#FF6666><b>{monoBahaviour.GetType()}</b></color>");       
        }
    }

    public void Inject(object monoBahaviour)
    {
        Type type = monoBahaviour.GetType();

        var methodsInfo = type.GetMethods(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy
        );

        foreach (var methodInfo in methodsInfo)
        {
            if (methodInfo.IsDefined(typeof(InjectAttribute)))
            {
                var parametersInfo = methodInfo.GetParameters();
                var arguments = new object[parametersInfo.Length];

                for (int i = 0; i < parametersInfo.Length; i++)
                {
                    Type argumentType = parametersInfo[i].ParameterType;
                    var argument = ServiceLocator.GetService(argumentType);
                    arguments[i] = argument;
                }

                methodInfo.Invoke(monoBahaviour, arguments);
            }
        }
    }
}