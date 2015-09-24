// Connect.cpp : Implementation of CConnect

#include "stdafx.h"
#include "Connect.h"


// CConnect

STDMETHODIMP CConnect::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* const arr[] = 
	{
		&IID_IConnect
	};

	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}
