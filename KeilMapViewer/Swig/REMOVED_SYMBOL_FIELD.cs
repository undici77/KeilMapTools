//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class REMOVED_SYMBOL_FIELD : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal REMOVED_SYMBOL_FIELD(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(REMOVED_SYMBOL_FIELD obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~REMOVED_SYMBOL_FIELD() {
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
          KeilMapLibPINVOKE.delete_REMOVED_SYMBOL_FIELD(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string module {
    set {
      KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_module_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_module_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string symbol {
    set {
      KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_symbol_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_symbol_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string size {
    set {
      KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_size_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.REMOVED_SYMBOL_FIELD_size_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public REMOVED_SYMBOL_FIELD() : this(KeilMapLibPINVOKE.new_REMOVED_SYMBOL_FIELD(), true) {
  }

}
