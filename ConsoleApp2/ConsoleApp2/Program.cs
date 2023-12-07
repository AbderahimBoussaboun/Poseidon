using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp2 {
    class Program
    {
        static void Main()
        {
            // Read your text document into a string
            string textDocument = File.ReadAllText("configuracion_qsqbalalc03_qsqbalalc04_29062023.txt", Encoding.UTF8);


            // Parse text document and create instances of classes
            /*List<Irule> iruleList = ParseIrules(textDocument);
            List<Monitor> monitorList = ParseMonitors(textDocument);
            List<Node> nodeList = ParseNodes(textDocument);

            List<Virtual> virtualList = ParseVirtuals(textDocument);*/
            //Console.WriteLine($"Contenido del archivo: {textDocument}");

            List<Pool> poolList = ParsePools(textDocument);

            // Now you have lists of parsed objects that you can use in your program
        }

        #region ParseIrules
        /*
        static List<Irule> ParseIrules(string text)
        {
            List<Irule> irules = new List<Irule>();

            // Use regex to extract data for Irule class
            Regex iruleRegex = new Regex(@"YourIruleRegexPattern");
            MatchCollection matches = iruleRegex.Matches(text);

            foreach (Match match in matches)
            {
                Irule irule = new Irule
                {
                    // Set Irule properties based on regex captures
                    Redirect = match.Groups["Redirect"].Value,
                    //RuleId = Guid.Parse(match.Groups["RuleId"].Value),
                    // Set other properties...
                };

                irules.Add(irule);
            }

            return irules;
        }
        #endregion

        #region ParseMonitors
        static List<Irule> ParseMonitors(string text)
        {
            List<Irule> irules = new List<Irule>();

            // Use regex to extract data for Irule class
            Regex iruleRegex = new Regex(@"YourIruleRegexPattern");
            MatchCollection matches = iruleRegex.Matches(text);

            foreach (Match match in matches)
            {
                Irule irule = new Irule
                {
                    // Set Irule properties based on regex captures
                    Redirect = match.Groups["Redirect"].Value,
                    RuleId = Guid.Parse(match.Groups["RuleId"].Value),
                    // Set other properties...
                };

                irules.Add(irule);
            }

            return irules;
        }
        #endregion

        #region ParseNodes
        static List<Node> ParseNodes(string text)
        {
            List<Node> nodes = new List<Node>();

            // Use regex to extract data for Irule class
            Regex nodeRegex = new Regex(@"YourIruleRegexPattern");
            MatchCollection matches = nodeRegex.Matches(text);

            foreach (Match match in matches)
            {
                Node node = new Node
                {
                    // Set Irule properties based on regex captures
                    IP = match.Groups["Redirect"].Value,
                    Port = Guid.Parse(match.Groups["RuleId"].Value),
                    Description =
                        // Set other properties...
                };

                nodes.Add(node);
            }

            return nodes;
        }
        */
        #endregion

        #region ParsePools
        static List<Pool> ParsePools(string text)
        {
            List<Pool> pools = new List<Pool>();

            // Use regex to extract data for Pool class
            Regex poolRegex = new Regex(@"ltm pool\s+/\w+\/([\w\d._-]+)\s+{\s+(?:description\s+""([^""]*)""|\S+)?\s+(?:load-balancing-mode\s+([^\s]+))?\s+(?:members\s+{(?:[^}]+}\s*)*})?\s+(?:monitor\s+(\S+))?", RegexOptions.Singleline);

            MatchCollection poolMatches = poolRegex.Matches(text);

            // Imprime el número de coincidencias
            Console.WriteLine($"Número de coincidencias: {poolMatches.Count}");

            // Itera a través de las coincidencias e imprime la información
            for (int i = 0; i < poolMatches.Count; i++)
            {
                Match match = poolMatches[i];
                Console.WriteLine($"Coincidencia {i + 1}:");
                Console.WriteLine($"Nombre del pool: {match.Groups[1].Value}");
                Console.WriteLine($"Description: {match.Groups[2].Value}");
                Console.WriteLine($"Load Balancing Mode: {match.Groups[3].Value}");
                Console.WriteLine($"Nombre del monitor: {match.Groups[4].Value}");
            }
            /*foreach (Match poolMatch in poolMatches)
            {
                Pool pool = new Pool
                {
                    Name = match.Groups[1].Value
                    Description = match.Groups[2].Value,
                    BalancerType = match.Groups[3].Value,
                    // Other common properties...

                    // MonitorId and Monitor properties
                    MonitorId = ExtractGuidValue(poolMatch.Groups["Body"].Value, "monitor"),
                    Monitor = null // Initialize to null; set it later if MonitorId is not null
                };

                // Handle members
                pool.Members = ParseMembers(poolMatch.Groups["Body"].Value);

                pools.Add(pool);
            }*/

            return pools;
        }

        #endregion

        #region ParseVirtuals
/*
static List<Irule> ParseVirtuals(string text)
{
    List<Irule> irules = new List<Irule>();

    // Use regex to extract data for Irule class
    Regex iruleRegex = new Regex(@"YourIruleRegexPattern");
    MatchCollection matches = iruleRegex.Matches(text);

    foreach (Match match in matches)
    {
        Irule irule = new Irule
        {
            // Set Irule properties based on regex captures
            Redirect = match.Groups["Redirect"].Value,
            RuleId = Guid.Parse(match.Groups["RuleId"].Value),
            // Set other properties...
        };

        irules.Add(irule);
    }

    return irules;
}
*/
#endregion

}
}
