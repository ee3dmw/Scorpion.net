namespace Scorpion.CommandProcessor
{
    public interface IHandlerAttribute<T> : IHandleAttribute
    {
        void BeforeHandler(T command);
        void AfterHandler(T command);
    }

    public interface IHandleAttribute
    {
        int Order { get; set; }
    }
}
