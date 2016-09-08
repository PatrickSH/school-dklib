using System.Collections.Generic;

namespace EnergiDKLib
{
    internal interface UsageDatabaseOperationInterface
    {
        string addUsage(string type, string usage, string date);

        List<string[]> getAllUsage(string dateFrom = null, string dateTo = null);

        List<string[]> getPeriodUsage(string dateFrom, string dateTo);
    }
}