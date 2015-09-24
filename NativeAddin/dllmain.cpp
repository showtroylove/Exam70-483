// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "NativeAddin_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CNativeAddinModule _AtlModule;

class CNativeAddinApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CNativeAddinApp, CWinApp)
END_MESSAGE_MAP()

CNativeAddinApp theApp;

BOOL CNativeAddinApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CNativeAddinApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
