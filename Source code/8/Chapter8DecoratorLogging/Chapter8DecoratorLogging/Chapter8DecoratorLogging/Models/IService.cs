namespace Chapter8DecoratorLogging.Models;

public interface IService
{
    string PerformOperation();
}

public class ConcreteService : IService
{
    public string PerformOperation()
    {
        return "Operation Performed";
    }
}


public abstract class ServiceDecorator : IService
{
    protected IService _service;

    public ServiceDecorator(IService service)
    {
        _service = service;
    }

    public virtual string PerformOperation()
    {
        return _service.PerformOperation();
    }
}


public class LoggingDecorator : ServiceDecorator
{
    public LoggingDecorator(IService service) : base(service) { }
    public override string PerformOperation()
    {
        Log("Operation started.");
        var result = base.PerformOperation();
        Log("Operation ended.");
        return result;
    }
    private void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}
