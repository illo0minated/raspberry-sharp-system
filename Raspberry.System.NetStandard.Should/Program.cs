using System;

namespace Raspberry.System.NetStandard.Should
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var board = Board.Current;

            if (!board.IsRaspberryPi)
                Console.WriteLine("System is not a Raspberry Pi");
            else
            {
                Console.WriteLine("Raspberry Pi running on {0} processor", board.Processor);
                Console.WriteLine("Firmware rev{0}, board model {1} ({2})", board.Firmware, board.Model, board.Model.GetDisplayName() ?? "Unknown");
                Console.WriteLine();
                Console.WriteLine("Serial number: {0}", board.SerialNumber);
            }
        }
    }
}
