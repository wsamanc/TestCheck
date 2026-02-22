using System;

namespace TestCheck.Helpers
{
    public static class RandomDataHelper
    {
        private static readonly Random random = new Random();

        public static string GenerateName()
        {
            return $"Apple MacBook Pro {random.Next(1000, 9999)}";
        }

        public static int GenerateYear()
        {
            return random.Next(2018, 2026);
        }

        public static double GeneratePrice()
        {
            return Math.Round(random.NextDouble() * 2000 + 1000, 2);
        }

        public static string GenerateCpuModel()
        {
            return $"Intel Core i{random.Next(5, 10)}";
        }

        public static string GenerateDiskSize()
        {
            int[] sizes = { 256, 512, 1024, 2048 };
            return $"{sizes[random.Next(sizes.Length)]} GB";
        }

        public static string GenerateColor()
        {
            string[] colors = { "Silver", "Space Gray", "Black" };
            return colors[random.Next(colors.Length)];
        }
    }
}