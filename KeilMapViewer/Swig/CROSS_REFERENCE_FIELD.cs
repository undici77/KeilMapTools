//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class CROSS_REFERENCE_FIELD : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CROSS_REFERENCE_FIELD(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CROSS_REFERENCE_FIELD obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CROSS_REFERENCE_FIELD() {
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
          KeilMapLibPINVOKE.delete_CROSS_REFERENCE_FIELD(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string module {
    set {
      KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_module_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_module_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string module_reference {
    set {
      KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_module_reference_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_module_reference_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string reference_qualifier {
    set {
      KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_reference_qualifier_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_reference_qualifier_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string symbol {
    set {
      KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_symbol_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.CROSS_REFERENCE_FIELD_symbol_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public CROSS_REFERENCE_FIELD() : this(KeilMapLibPINVOKE.new_CROSS_REFERENCE_FIELD(), true) {
  }

}
