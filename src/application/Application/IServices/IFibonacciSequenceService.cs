using System.Numerics;

namespace Application.IServices;
public interface IFibonacciSequenceService
{
    public Task<string> GetFibbonacciNumberAsync(uint n);
}