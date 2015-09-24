select max(StockQuoteId), Symbol from StockQuotes a
where EXISTS (
select Symbol
from StockQuotes
where Symbol = a.Symbol
group by Symbol
having count(*) > 1) group by symbol order by 2, 1;


select TOP 3 *
from (select Name, Symbol, cast([Last] as numeric(18,2)) as [Last] from StockQuotes where ISNUMERIC([Last]) = 1) a order by  CASE 
    WHEN ISNUMERIC([Last]) = 1 THEN cast([Last] as numeric(18,2)) 
    ELSE 9999999 -- or something huge
  END desc;

select cast([Last] as numeric(18,2)) as [Last1], * from (select TOP 3 * from StockQuotes where [Last] != 'N/A' order by 1 desc) a;

select * from StockQuotes where [Last] != 'N/A' order by [Last] desc;

select TOP 3 *
from (select Name, Symbol, cast([Last] as numeric(18,2)) as [Last] from StockQuotes where ISNUMERIC([Last]) = 1) a order by [Last] desc;

select top 3 * from StockQuotes order by StockQuoteId;

delete from StockQuotes where StockQuoteId = 5;

select * from (select TOP 3 Name, Symbol, cast([Last] as numeric(18,2)) as [Last] from StockQuotes where ISNUMERIC([Last]) = 1  order by [Last] desc) a
union all 
select * from (select TOP 3 Name, Symbol, cast([Last] as numeric(18,2)) as [Last] from StockQuotes where ISNUMERIC([Last]) = 1  order by [Last]) b;

SELECT * INTO XLImport3 FROM OPENDATASOURCE('Microsoft.ACE.OLEDB.12.0',
'Data Source=I:\My Projects\Exam70-483\djia30.xlsx;Extended Properties=Excel 12.0')...[companylist$];

SELECT * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',  'Excel 12.0;I:\My Projects\Exam70-483\djia30.xlsx', 
    'SELECT * FROM [companylist$]')

sp_configure 'show advanced options', 1;
RECONFIGURE;
sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO

insert into StockQuotes (Symbol, Name, [Last], Change, PercentageChange, Low, High, PreviousClose)
select * from XLImport3;

select count(*) from StockQuotes


delete StockQuotes
where StockQuoteId in (select Id from ((select max(StockQuoteId) as Id, Symbol from StockQuotes a
where EXISTS (
select Symbol
from StockQuotes
where Symbol = a.Symbol
group by Symbol
having count(*) > 1) group by symbol) )j);

select *
from StockQuotes 
where SUBSTRING(last, 0, 1) in ('1', '2');

select round(StockQuoteId, 4), SUBSTRING(Name, len(Name) -1, 1) from StockQuotes;