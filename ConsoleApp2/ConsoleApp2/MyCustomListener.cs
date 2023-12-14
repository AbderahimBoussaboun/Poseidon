using System;
using System.Text.RegularExpressions;
using ConsoleApp2;  // Ajusta según tus archivos generados por ANTLR

public class MyCustomListener : BigIPConfigParserBaseListener
{
    private int recordCount = 0;

    public override void EnterRecord(BigIPConfigParser.RecordContext context)
    {
        string recordType = context.recordStart().TYPE().GetText();

        string recordContent = context.recordContent().GetText();

        switch (recordType)
        {
            case "node":
                ProcessNodeRecord(recordContent, context.recordStart().RECORD_POST().GetText());
                break;
            case "pool":
                ProcessPoolRecord(recordContent, context.recordStart().RECORD_POST().GetText());
                break;
            case "rule":
                ProcessRuleRecord(recordContent, context.recordStart().RECORD_POST().GetText());
                break;
            case "virtual":
                ProcessVirtualRecord(recordContent, context.recordStart().RECORD_POST().GetText());
                break;
            default:
                Console.WriteLine("Tipo de registro desconocido: " + recordType);
                break;
        }

  
    }
    private void ProcessNodeRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'node' aquí...
        string addressPattern = @"address\s*([^\r\n]+)";
        var matches = Regex.Matches(recordContent, addressPattern);

        string descriptionPattern = @"description\s*((\""[^\""]*\"")|(\S+))";
        var descriptionMatch = Regex.Match(recordContent, descriptionPattern);

        Console.WriteLine("Nombre del nodo:" + recordPost);
        foreach (Match addressMatch in matches)
        {
            if (addressMatch.Success)
            {
                string addressValue = addressMatch.Groups[1].Value;
                Console.WriteLine($"IP del nodo: {addressValue}");
            }
        }

        if (descriptionMatch.Success)
        {
            string descriptionValue = descriptionMatch.Groups[1].Value;
            Console.WriteLine($"Descripción del nodo: {descriptionValue}");
        }
    }

    private void ProcessPoolRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'pool' aquí...
        Console.WriteLine("Nombre del pool:" + recordPost);
    }

    private void ProcessRuleRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'rule' aquí...
    }

    private void ProcessVirtualRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'virtual' aquí...
    }
}