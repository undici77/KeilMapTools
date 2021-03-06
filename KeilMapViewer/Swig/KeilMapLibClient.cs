//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class KeilMapLibClient : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal KeilMapLibClient(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(KeilMapLibClient obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~KeilMapLibClient() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          KeilMapLibPINVOKE.delete_KeilMapLibClient(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public KeilMapLibClient() : this(KeilMapLibPINVOKE.new_KeilMapLibClient(), true) {
  }

  public bool ReadFile(string file_path) {
    bool ret = KeilMapLibPINVOKE.KeilMapLibClient_ReadFile(swigCPtr, file_path);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CROSS_REFERENCE_VECTOR GetCrossReference() {
    CROSS_REFERENCE_VECTOR ret = new CROSS_REFERENCE_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetCrossReference(swigCPtr), true);
    return ret;
  }

  public FUNCTION_POINTER_VECTOR GetFunctionPointer() {
    FUNCTION_POINTER_VECTOR ret = new FUNCTION_POINTER_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetFunctionPointer(swigCPtr), true);
    return ret;
  }

  public GLOBAL_SYMBOL_VECTOR GetGlobalSymbols() {
    GLOBAL_SYMBOL_VECTOR ret = new GLOBAL_SYMBOL_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetGlobalSymbols(swigCPtr), true);
    return ret;
  }

  public IMAGE_COMPONENT_SIZE_VECTOR GetImageComponentSize() {
    IMAGE_COMPONENT_SIZE_VECTOR ret = new IMAGE_COMPONENT_SIZE_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetImageComponentSize(swigCPtr), true);
    return ret;
  }

  public IMAGE_SIZE_DATA GetImageSize() {
    IMAGE_SIZE_DATA ret = new IMAGE_SIZE_DATA(KeilMapLibPINVOKE.KeilMapLibClient_GetImageSize(swigCPtr), true);
    return ret;
  }

  public LOCAL_SYMBOL_VECTOR GetLocalSymbols() {
    LOCAL_SYMBOL_VECTOR ret = new LOCAL_SYMBOL_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetLocalSymbols(swigCPtr), true);
    return ret;
  }

  public MAXIMUM_STACK_USAGE_VECTOR GetMaximumStackUsage() {
    MAXIMUM_STACK_USAGE_VECTOR ret = new MAXIMUM_STACK_USAGE_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetMaximumStackUsage(swigCPtr), true);
    return ret;
  }

  public MEMORY_MAP_IMAGE GetMemoryMapImage() {
    MEMORY_MAP_IMAGE ret = new MEMORY_MAP_IMAGE(KeilMapLibPINVOKE.KeilMapLibClient_GetMemoryMapImage(swigCPtr), true);
    return ret;
  }

  public MUTUALLY_RECURSIVE_VECTOR GetMutualRecursive() {
    MUTUALLY_RECURSIVE_VECTOR ret = new MUTUALLY_RECURSIVE_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetMutualRecursive(swigCPtr), true);
    return ret;
  }

  public REMOVED_SYMBOL_VECTOR GetRemovedSymbols() {
    REMOVED_SYMBOL_VECTOR ret = new REMOVED_SYMBOL_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetRemovedSymbols(swigCPtr), true);
    return ret;
  }

  public STACK_USAGE_VECTOR GetStackUsage() {
    STACK_USAGE_VECTOR ret = new STACK_USAGE_VECTOR(KeilMapLibPINVOKE.KeilMapLibClient_GetStackUsage(swigCPtr), true);
    return ret;
  }

}
