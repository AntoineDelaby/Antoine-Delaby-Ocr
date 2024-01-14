using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tesseract; 
using System.Threading.Tasks;

namespace Antoine.Delaby.Ocr.csproj;    

public class Ocr
{
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
    public List<OcrResult> Read(IList<byte[]> images)
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        
        using var engine = new TesseractEngine(Path.Combine(executingPath,
            @"tessdata"), "fra", EngineMode.Default);
        
        // Création d'une liste de tâches et celle des résultats
        List<Task> tasks = new List<Task>();
        List<OcrResult> results = new List<OcrResult>();

        foreach (var image in images)
        {
            // Pour chaque image, on crée une tâche
            var task = Task.Run(() =>
            {
                // Résultat de la lecture
                OcrResult res = new OcrResult();
                
                using var pix = Pix.LoadFromMemory(image);
                var test = engine.Process(pix);
                res.Text = test.GetText();
                res.Confidence = test.GetMeanConfidence();
                
                results.Add(res);
            });
        }
        
        // On attend la fin de toutes les tâches pour renvoyer le résultat
        Task.WaitAll(tasks.ToArray());
        return results;
    } 
}