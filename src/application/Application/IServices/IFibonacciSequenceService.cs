namespace Application.IServices;
public interface IFibonacciSequenceService
{
    public Task<long> GetFibbonacciNumberAsync(int n);
}