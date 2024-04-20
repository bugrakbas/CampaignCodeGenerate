using System;

namespace ExampleNamespace
{
    class Program
    {
        static List<string> codeList = new List<string>();

        static Dictionary<string, string> campaignIdAndCodes = new Dictionary<string, string>()
        {
          {"UK", ""},
          {"USA", ""},
          {"India", ""}
        };


        static void Main(string[] args)
        {
            var code = new CodeGenerator().GenerateCode();

            CheckCode(code);
        }
        static void CheckCode(string code)
        {
            int len = code.Length;
            if (len != 8)
            {
                Console.WriteLine("Kod 10 karakter uzunluğunda olmalıdır.");
                return;
            }

            codeList.Add(code);

            Console.WriteLine($"Kod {code} doğrulandı.");
        }

        public Dictionary<string,string> GetCampaignIdAndCodes()
        {
            return campaignIdAndCodes;
        }

        public void AddCodeToCampaignId(string campaignId,string codes)
        {
            foreach (var code in codes.Split(","))
            {
                if (!codeList.Contains(code))
                {
                    Console.WriteLine($"Kampanya kodu {code} geçersizdir.");
                }
                else
                {
                    campaignIdAndCodes[campaignId] = codes;
                    Console.WriteLine($"Kampanya kodu {code} doğrulandı.");
                }
            }
        }
        public class CodeGenerator
        {
            public string GenerateCode()
            {
                var codeStrings = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                Random random = new Random();
                string code = "";
                for (int i = 0; i < 8; i++)
                {
                    code += codeStrings[random.Next(0, codeStrings.Length)];
                }

                return code;
            }
        }
    }
}