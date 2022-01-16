using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractClass
{

    protected abstract void AbstractMethod();

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}

public interface IMyInterface1
{
    void Method1();
}

public interface IMyInterface2
{
    void Method2();
}

public interface IMyInterfaceP
{
    void MethodP(int p);
}

public class DerivedClass : AbstractClass, IMyInterface1, IMyInterface2, IMyInterfaceP
{
    public void Method1()
    {

    }

    public void Method2()
    {

    }

    public void MethodP(int p)
    {

    }

    protected override void AbstractMethod()
    {
    
    }

    private void Deneme()
    {
        Start();
        Update();
    }
}
