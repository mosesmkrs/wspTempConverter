using System.ServiceModel;

[ServiceContract]
public interface ITemperatureService
{
    [OperationContract]
    double FahrenheitToCelsius(double fahrenheit);

    [OperationContract]
    double CelsiusToFahrenheit(double celsius);
}

public class TemperatureService : ITemperatureService
{
    public double FahrenheitToCelsius(double fahrenheit)
    {
        return Math.Round((fahrenheit - 32) * 5 / 9, 2);
    }

    public double CelsiusToFahrenheit(double celsius)
    {
        return Math.Round((celsius * 9 / 5) + 32, 2);
    }
}