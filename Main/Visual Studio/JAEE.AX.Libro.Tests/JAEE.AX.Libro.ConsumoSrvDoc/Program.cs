using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JAEE.AX.Libro.WSRef.JAEEServiciosDocumento;

namespace JAEE.AX.Libro.ConsumoSrvDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            // El contexto indica a AX algunos aspectos de la conexión
            CallContext context = new CallContext();
            context.Company = "USMF";

            // Cabecera del diario
            AxdEntity_LedgerJournalTable journalHeader = new AxdEntity_LedgerJournalTable();
            journalHeader.JournalName = "GenJrn";

            // Línea de diario. Repetir para cada línea
            AxdEntity_LedgerJournalTrans journalLine = new AxdEntity_LedgerJournalTrans();
            journalLine.Company = context.Company;

            // BEGIN. Número de cuenta
            AxdType_MultiTypeAccount account = new AxdType_MultiTypeAccount();
            account.Account = "130100";
            account.DisplayValue = "130100-001-024";
            account.Values = new AxdType_DimensionAttributeValue[2];
            // Segmentos. Dimensiones financieras.
            AxdType_DimensionAttributeValue dav1 = new AxdType_DimensionAttributeValue();
            dav1.Name = "BusinessUnit";
            dav1.Value = "001";
            account.Values[0] = dav1;
            AxdType_DimensionAttributeValue dav2 = new AxdType_DimensionAttributeValue();
            dav2.Name = "Department";
            dav2.Value = "024";
            account.Values[1] = dav2;
            // END. Número de cuenta

            // BEGIN. Cuenta de contrapartida.
            AxdType_MultiTypeAccount offsetAccount = new AxdType_MultiTypeAccount();
            offsetAccount.Account = "110110";
            offsetAccount.DisplayValue = "110110-001-024";
            offsetAccount.Values = new AxdType_DimensionAttributeValue[2];
            // Reutilizo las dimensiones para simplificar
            offsetAccount.Values[0] = dav1;
            offsetAccount.Values[1] = dav2;
            // END. Cuenta de contrapartida.

            journalLine.AccountType = AxdEnum_LedgerJournalACType.Ledger;
            journalLine.AccountTypeSpecified = true;
            journalLine.LedgerDimension = account;
            journalLine.OffsetAccountType = AxdEnum_LedgerJournalACType.Ledger;
            journalLine.OffsetAccountTypeSpecified = true;
            journalLine.OffsetLedgerDimension = offsetAccount;
            journalLine.AmountCurDebit = 120;
            journalLine.AmountCurDebitSpecified = true;

            // Agregar la línea a la cabecera
            journalHeader.LedgerJournalTrans = new AxdEntity_LedgerJournalTrans[1];
            journalHeader.LedgerJournalTrans[0] = journalLine;

            // Entidad que representa el contrato de datos del servicio
            AxdJAEELedgerJournal journal = new AxdJAEELedgerJournal();
            journal.LedgerJournalTable = new AxdEntity_LedgerJournalTable[1];
            journal.LedgerJournalTable[0] = journalHeader;

            // Llamada al servicio
            JAEELedgerJournalServiceClient client = new JAEELedgerJournalServiceClient();
            client.create(context, journal);
            }
            catch
            {
                throw;
            }
        }
    }
}
