using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ReviewWeekApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Boxing Match Simulator!");

            // Read the JSON data from the file
            string jsonFilePath = "C:\\Users\\KDots\\OneDrive\\Documents\\GitHub\\ReviewWeekApp\\DataBin\\Data.json";

            while (true) // Run indefinitely until the user decides to exit
            {
                try
                {
                    // Read the JSON content and deserialize it into a list of Boxer objects
                    List<Boxer> boxers = ReadBoxersFromJson(jsonFilePath);

                    // making sure there are at least two boxers available
                    if (boxers.Count < 2)
                    {
                        Console.WriteLine("There are not enough boxers for a match.");
                        return;
                    }

                    // Letting  the user choose two boxers for the match
                    Boxer boxer1 = SelectBoxer(boxers);
                    Boxer boxer2 = SelectBoxer(boxers);

                    // Simulate the match
                    Console.WriteLine($"Match: {boxer1.Name} vs. {boxer2.Name}");
                    SimulateMatch(boxer1, boxer2);

                    // Ask the user if they want to run another simulation
                    Console.Write("Do you want to run another simulation? (Y/N): ");
                    string response = Console.ReadLine();
                    if (response?.Trim().ToUpper() != "Y")
                    {
                        break; // Exit the loop if the user doesn't want to run another simulation
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Data.json file not found.");
                    return;
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    Console.WriteLine($"Error parsing JSON: {ex.Message}");
                    return;
                }
            }
        }

        private static List<Boxer> ReadBoxersFromJson(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Boxer>>(jsonContent);
        }

        private static Boxer SelectBoxer(List<Boxer> boxers)
        {
            Console.WriteLine("Select a boxer for the match:");

            for (int i = 0; i < boxers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {boxers[i].Name}");
            }

            int selection;
            do
            {
                Console.Write("Enter the number of the boxer: ");
            } while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > boxers.Count);

            return boxers[selection - 1];
        }

        private static void SimulateMatch(Boxer boxer1, Boxer boxer2)
        {
            double koRate1 = double.Parse(boxer1.KoRate.TrimEnd('%'));
            double koRate2 = double.Parse(boxer2.KoRate.TrimEnd('%'));
            // Simulate the match logic here
            Console.WriteLine($"Boxer 1: {boxer1.Name} (KO Rate: {boxer1.KoRate})");
            Console.WriteLine($"Boxer 2: {boxer2.Name} (KO Rate: {boxer2.KoRate})");

            //win logic 
            if (koRate1 > koRate2)
            {
                Console.WriteLine($"{boxer1.Name} wins!");
            }
            else if (koRate2 > koRate1)
            {
                Console.WriteLine($"{boxer2.Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }
    }

    //class properties for the boxers
    public class Boxer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }

        [JsonProperty("draws")]
        public int Draws { get; set; }

        [JsonProperty("ko_rate")]
        public string KoRate { get; set; }

        [JsonProperty("stance")]
        public string Stance { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("reach")]
        public string Reach { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
