// See https://aka.ms/new-console-template for more information

namespace Antoine.Delaby.Ocr.Console

{
    class PrintProgram
    {
        public static void Main(string[] args)
        {
            csproj.Ocr ocr = new csproj.Ocr();
            IList<byte[]> filesBytes = new List<byte[]>();

            // Lecture des octets
            foreach (var arg in args)
            {
                filesBytes.Add(File.ReadAllBytes(arg));
            }

            // Utilisation de ma bibliothèque
            var ocrResults = ocr.Read(filesBytes);
        
            // Affichage des résultats
            foreach (var ocrResult in ocrResults)
            {
                System.Console.WriteLine($"Confidence :{ocrResult.Confidence}");
                System.Console.WriteLine($"Text :{ocrResult.Text}");
            }
        }
    }
}