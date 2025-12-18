namespace AsianFoodDelivery.Core.Observers.Interfaces;

public interface IOrderSubject
{
    void Attach(IOrderObserver observer);
    void Detach(IOrderObserver observer);
    void Notify();
}