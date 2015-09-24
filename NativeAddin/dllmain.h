// dllmain.h : Declaration of module class.

class CNativeAddinModule : public ATL::CAtlDllModuleT< CNativeAddinModule >
{
public :
	DECLARE_LIBID(LIBID_NativeAddinLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_NATIVEADDIN, "{65FD1183-38C3-48E0-A8A8-2C7FCFD25A17}")
};

extern class CNativeAddinModule _AtlModule;
