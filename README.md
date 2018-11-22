# TransactionData

## Problem

You are designing a web application that processes account transaction data to calculate tax figures for a client tax return. We are interested in the data input/manipulation stage of the process which is achieved through an upload feature.
The transaction data has 4 columns: Account (text), Description (text), Currency Code (string) and Amount (decimal). The format of the data will be in Excel (xlsx) and each individual file could contain up to 100k rows. Overall, the system is expected to cope with millions of records.

## Requirement

To develop a small web application that does the following:
-	Has a clean interface;
-	Allow the user to select a file;
-	Allow the user to upload the content of the current file to a database;
-	Each line of data should be validated before it is uploaded – all fields are required, Currency Code must be a valid ISO 4217 currency code and Amount must be a valid number;
-	On completion, the number of lines imported and the details of any lines ignored due to failed validation should be shown;
-	Allow user to see all the transaction data in the system;
-	If time allows, allow user to remove or edit the transaction data;

___

## Getting Started

1. First run the following script to create the database in SQLServer;

   - [Script to create Database in SQLServer.](https://github.com/yirernet/TransactionData/blob/master/TransactionData.DAL/SQLScript/CreateDB_Script.sql).

2. Modified the ConnectionString in the web.config file accordingly.
3. The Excel file must be a xslx file.
4. There are two appsettings in the web.config;
   - 'ExcelFilePath' to provide the path that contains the xslx files to upload.
   - 'NeedValidExcelColumnNames' 
     - If true,  will validate the first row looking for the column names. If not found it will throw and exception a it will not upload the data into the database.
     - if fasle, will also validate the first row looking for the column names. If not found it will display an error message but it will upload the data into the database.


The solution has 3 projects;

1. Web Project - Web Forms application with three pages.
   - Home welcome page.
   - Transactions page, this page allows the user to see all the transaction data in the system. Also the user can remove and edit this transaction data.
   - Upload page, this page allows the user to upload the content of an excel (xlsx) file to a database.
2. Core project - Here we have core business logic.
3. Data Access project - This is the data access logic.

Iso loaded from json data file through iso data provider (could be changed to any iso provider)

Unit tests are in project TransactionData.Core.Test

Project written on .NET Framework and VS 2017.

The EXCEL file should be in format:
```
Account|Description|Currency Code|Amount
```

___

If you get this error running the app "Could not find a part of the path … bin\roslyn\csc.exe"

Add this code in your .csproj file:

```
<Target Name="CopyRoslynFiles" AfterTargets="AfterBuild" Condition="!$(Disable_CopyWebApplication) And '$(OutDir)' != '$(OutputPath)'">
    <ItemGroup>
      <RoslynFiles Include="$(CscToolPath)\*" />
    </ItemGroup>
    <MakeDir Directories="$(WebProjectOutputDir)\bin\roslyn" />
    <Copy SourceFiles="@(RoslynFiles)" DestinationFolder="$(WebProjectOutputDir)\bin\roslyn" SkipUnchangedFiles="true" Retries="$(CopyRetryCount)" RetryDelayMilliseconds="$(CopyRetryDelayMilliseconds)" />
</Target>
```
