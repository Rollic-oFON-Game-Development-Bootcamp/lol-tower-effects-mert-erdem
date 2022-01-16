using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    public int CurrentLevel = 5;
    private int myHealth = 100;
    private bool isAlarmActive = false;

    public delegate int VoidDelegate(int a, float b);

    private UnityAction<int, float> OnLowHealth;

    // Start is called before the first frame update
    void Start()
    {
        MyClass c1 = new MyClass();
        c1.ClassInteger = 1;
        MyClass.si = 1;
        c1.Print();

        MyClass c2 = new MyClass();
        c2.Print();

        MySingleton sng = MySingleton.Instance;
        MyObserver.LowHealth += ActivateAlarm;
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActivateAlarm()
    {

    }

    private void GetHit(int damage)
    {
        myHealth -= damage;
        if (myHealth <= 20)
        {
            MyObserver.LowHealth?.Invoke();
        }
    }
}

public class MyClass
{
    public static int si = 0;

    public int LazyInteger { get; set; }

    public MyClass()
    {
        MyObserver.LowHealth += OnLowHealth;
    }

    ~MyClass()
    {
        MyObserver.LowHealth -= OnLowHealth;
    }

    private void OnLowHealth()
    {

    }

    private int i = 0;
    public int ClassInteger
    {
        get
        {
            return i;
        }
        set
        {
            if (value >= 0)
            {
                i = value;
            }
        }
    }
    public int CoolInteger
    {
        get => i;
        set => i = value >= 0 ? value : i;
    }

    public void Print()
    {
        Debug.Log($"si = {si}");
        Debug.Log($" i = {i}");
        MySingleton sng = MySingleton.Instance;

    }
}
