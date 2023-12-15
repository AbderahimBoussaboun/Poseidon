using System;
using System.Text.RegularExpressions;
using ConsoleApp2;  // Ajusta según tus archivos generados por ANTLR

public class MyCustomListener : BigIPConfigParserBaseListener
{
    public override void EnterRecord(BigIPConfigParser.RecordContext context)
    {
        string recordType = context.recordStart().TYPE().GetText();

        string recordContent = context.recordContent().GetText();

        switch (recordType)
        {
            case "node":
                ProcessNodeRecord(recordContent, context.recordStart().RECORD_POST().GetText().Trim());
                break;
            case "pool":
                ProcessPoolRecord(recordContent, context.recordStart().RECORD_POST().GetText().Trim());
                break;
            case "rule":
                ProcessRuleRecord(recordContent, context.recordStart().RECORD_POST().GetText().Trim());
                break;
            case "virtual":
                ProcessVirtualRecord(recordContent, context.recordStart().RECORD_POST().GetText().Trim());
                break;
            case "monitor":
                ProcessMonitorRecord(recordContent, context.recordStart().RECORD_POST().GetText().Trim());
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
        var addressMatch = Regex.Match(recordContent, addressPattern);
        string addressValue = addressMatch.Success ? addressMatch.Groups[1].Value : null;

        string descriptionPattern = @"description\s*((\""[^\""]*\"")|(\S+))";
        var descriptionMatch = Regex.Match(recordContent, descriptionPattern);
        string descriptionValue = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : null;
    }

    private void ProcessPoolRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'pool' aquí...
        string descriptionPattern = @"description\s*((\""[^\""]*\"")|(\S+))";
        var descriptionMatch = Regex.Match(recordContent, descriptionPattern);
        string descriptionValue = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : null;

        string loadBalancingModePattern = @"load-balancing-mode\s*([^\r\n]+)";
        var loadBalancingModeMatch = Regex.Match(recordContent, loadBalancingModePattern);
        string loadBalancingModeValue = loadBalancingModeMatch.Success ? loadBalancingModeMatch.Groups[1].Value : null;

        string membersPattern = @"members\s*{([^}]|(?<=})[^}])*}";
        var membersMatch = Regex.Match(recordContent, membersPattern);
        string membersValue = membersMatch.Success ? membersMatch.Groups[1].Value : null;

        string memberPattern = @"/.+\s*{";
        var memberMatches = Regex.Matches(membersValue, memberPattern);
        foreach (Match memberMatch in memberMatches)
        {
            string memberName = memberMatch.Value.Trim().TrimEnd('{').Trim();
            // Dividir el nombre del miembro y el puerto
            string[] memberParts = memberName.Split(':');
            string memberNameValue = memberParts[0];
            string memberPortValue = memberParts.Length > 1 ? memberParts[1] : null;
            // Procesar memberNameValue y memberPortValue aquí...
        }

        string monitorPattern = @"monitor\s*([^\r\n]+)";
        var monitorMatch = Regex.Match(recordContent, monitorPattern);
        string monitorValue = monitorMatch.Success ? monitorMatch.Groups[1].Value : null;
    }

        private void ProcessRuleRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'rule' aquí...
        Console.WriteLine("Nombre del rule:" + recordPost);

        string ifBlockPattern = @"if\s*{([^}]|(?<=})[^}]*)}\s*{\s*(.+?)\s*}";
        var ifMatches = System.Text.RegularExpressions.Regex.Matches(recordContent, ifBlockPattern);

        foreach (Match match in ifMatches)
        {
            string conditionValue = match.Groups[1].Value;
            string redirectValue = match.Groups[2].Value;
            // Procesar conditionValue y redirectValue aquí...
        }
    }

    private void ProcessVirtualRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'virtual' aquí...
        Console.WriteLine("Nombre del virtual:" + recordPost);

        string poolPattern = @"pool\s*([^\r\n]+)";
        var poolMatch = Regex.Match(recordContent, poolPattern);
        string poolValue = poolMatch.Success ? poolMatch.Groups[1].Value : null;
    }
    private void ProcessMonitorRecord(string recordContent, string recordPost)
    {
        // ...procesar contenido del registro 'monitor' aquí...
        Console.WriteLine("Nombre del monitor:" + recordPost);

        string adaptivePattern = @"adaptive\s*([^\r\n]+)";
        var adaptiveMatch = Regex.Match(recordContent, adaptivePattern);
        string adaptiveValue = adaptiveMatch.Success ? adaptiveMatch.Groups[1].Value : null;

        string cipherlistPattern = @"cipherlist\s*([^\r\n]+)";
        var cipherlistMatch = Regex.Match(recordContent, cipherlistPattern);
        string cipherlistValue = cipherlistMatch.Success ? cipherlistMatch.Groups[1].Value : null;

        string compatibilityPattern = @"compatibility\s*([^\r\n]+)";
        var compatibilityMatch = Regex.Match(recordContent, compatibilityPattern);
        string compatibilityValue = compatibilityMatch.Success ? compatibilityMatch.Groups[1].Value : null;

        string debugPattern = @"debug\s*([^\r\n]+)";
        var debugMatch = Regex.Match(recordContent, debugPattern);
        string debugValue = debugMatch.Success ? debugMatch.Groups[1].Value : null;

        string defaultsFromPattern = @"defaults-from\s*([^\r\n]+)";
        var defaultsFromMatch = Regex.Match(recordContent, defaultsFromPattern);
        string defaultsFromValue = defaultsFromMatch.Success ? defaultsFromMatch.Groups[1].Value : null;

        string descriptionPattern = @"description\s*([^\r\n]+)";
        var descriptionMatch = Regex.Match(recordContent, descriptionPattern);
        string descriptionValue = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : null;

        string destinationPattern = @"destination\s*([^\r\n]+)";
        var destinationMatch = Regex.Match(recordContent, destinationPattern);
        string destinationValue = destinationMatch.Success ? destinationMatch.Groups[1].Value : null;

        string getPattern = @"get\s*([^\r\n]+)";
        var getMatch = Regex.Match(recordContent, getPattern);
        string getValue = getMatch.Success ? getMatch.Groups[1].Value : null;

        string intervalPattern = @"interval\s*([^\r\n]+)";
        var intervalMatch = Regex.Match(recordContent, intervalPattern);
        string intervalValue = intervalMatch.Success ? intervalMatch.Groups[1].Value : null;

        string ipDscpPattern = @"ip-dscp\s*([^\r\n]+)";
        var ipDscpMatch = Regex.Match(recordContent, ipDscpPattern);
        string ipDscpValue = ipDscpMatch.Success ? ipDscpMatch.Groups[1].Value : null;

        string passwordPattern = @"password\s*([^\r\n]+)";
        var passwordMatch = Regex.Match(recordContent, passwordPattern);
        string passwordValue = passwordMatch.Success ? passwordMatch.Groups[1].Value : null;

        string serverPattern = @"server\s*([^\r\n]+)";
        var serverMatch = Regex.Match(recordContent, serverPattern);
        string serverValue = serverMatch.Success ? serverMatch.Groups[1].Value : null;

        string servicePattern = @"service\s*([^\r\n]+)";
        var serviceMatch = Regex.Match(recordContent, servicePattern);
        string serviceValue = serviceMatch.Success ? serviceMatch.Groups[1].Value : null;

        string recvPattern = @"recv\s*([^\r\n]+)";
        var recvMatch = Regex.Match(recordContent, recvPattern);
        string recvValue = recvMatch.Success ? recvMatch.Groups[1].Value : null;

        string recvDisablePattern = @"recv-disable\s*([^\r\n]+)";
        var recvDisableMatch = Regex.Match(recordContent, recvDisablePattern);
        string recvDisableValue = recvDisableMatch.Success ? recvDisableMatch.Groups[1].Value : null;

        string reversePattern = @"reverse\s*([^\r\n]+)";
        var reverseMatch = Regex.Match(recordContent, reversePattern);
        string reverseValue = reverseMatch.Success ? reverseMatch.Groups[1].Value : null;

        string sendPattern = @"send\s*([^\r\n]+)";
        var sendMatch = Regex.Match(recordContent, sendPattern);
        string sendValue = sendMatch.Success ? sendMatch.Groups[1].Value : null;

        string sslProfilePattern = @"ssl-profile\s*([^\r\n]+)";
        var sslProfileMatch = Regex.Match(recordContent, sslProfilePattern);
        string sslProfileValue = sslProfileMatch.Success ? sslProfileMatch.Groups[1].Value : null;

        string timeUntilUpPattern = @"time-until-up\s*([^\r\n]+)";
        var timeUntilUpMatch = Regex.Match(recordContent, timeUntilUpPattern);
        string timeUntilUpValue = timeUntilUpMatch.Success ? timeUntilUpMatch.Groups[1].Value : null;

        string timeoutPattern = @"timeout\s*([^\r\n]+)";
        var timeoutMatch = Regex.Match(recordContent, timeoutPattern);
        string timeoutValue = timeoutMatch.Success ? timeoutMatch.Groups[1].Value : null;

        string usernamePattern = @"username\s*([^\r\n]+)";
        var usernameMatch = Regex.Match(recordContent, usernamePattern);
        string usernameValue = usernameMatch.Success ? usernameMatch.Groups[1].Value : null;
    }
}