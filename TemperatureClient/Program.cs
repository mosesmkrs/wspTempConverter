using System;
using System.ServiceModel;

[ServiceContract]
public interface ITemperatureService
{
    [OperationContract]
    double FahrenheitToCelsius(double fahrenheit);

    [OperationContract]
    double CelsiusToFahrenheit(double celsius);
}

class Program
{
    static void Main(string[] args)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress("http://localhost:5201/TemperatureService.asmx");
        var channelFactory = new ChannelFactory<ITemperatureService>(binding, endpoint);
        var serviceClient = channelFactory.CreateChannel();

        Console.WriteLine("Temperature Converter Client");
        Console.WriteLine("---------------------------");

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Fahrenheit to Celsius");
            Console.WriteLine("2. Celsius to Fahrenheit");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice (1-3): ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        ConvertFahrenheitToCelsius(serviceClient);
                        break;
                    case "2":
                        ConvertCelsiusToFahrenheit(serviceClient);
                        break;
                    case "3":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please ensure the SOAP service is running.");
            }
        }
    }

    static void ConvertFahrenheitToCelsius(ITemperatureService client)
    {
        Console.Write("Enter temperature in Fahrenheit: ");
        if (double.TryParse(Console.ReadLine(), out double fahrenheit))
        {
            double celsius = client.FahrenheitToCelsius(fahrenheit);
            Console.WriteLine($"{fahrenheit}°F = {celsius:0.##}°C");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    static void ConvertCelsiusToFahrenheit(ITemperatureService client)
    {
        Console.Write("Enter temperature in Celsius: ");
        if (double.TryParse(Console.ReadLine(), out double celsius))
        {
            double fahrenheit = client.CelsiusToFahrenheit(celsius);
            Console.WriteLine($"{celsius}°C = {fahrenheit:0.##}°F");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}