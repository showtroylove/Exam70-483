// dllmain.h : Declaration of module class.

class COutlookStockQuoteModule : public ATL::CAtlDllModuleT< COutlookStockQuoteModule >
{
public :
	DECLARE_LIBID(LIBID_OutlookStockQuoteLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_OUTLOOKSTOCKQUOTE, "{CE8AAFFB-1790-4F5C-B8C4-AE3EDD77B743}")
};

extern class COutlookStockQuoteModule _AtlModule;
