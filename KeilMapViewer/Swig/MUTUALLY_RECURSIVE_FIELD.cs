//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MUTUALLY_RECURSIVE_FIELD : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal MUTUALLY_RECURSIVE_FIELD(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MUTUALLY_RECURSIVE_FIELD obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~MUTUALLY_RECURSIVE_FIELD() {
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
          KeilMapLibPINVOKE.delete_MUTUALLY_RECURSIVE_FIELD(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string function {
    set {
      KeilMapLibPINVOKE.MUTUALLY_RECURSIVE_FIELD_function_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.MUTUALLY_RECURSIVE_FIELD_function_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string caller {
    set {
      KeilMapLibPINVOKE.MUTUALLY_RECURSIVE_FIELD_caller_set(swigCPtr, value);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = KeilMapLibPINVOKE.MUTUALLY_RECURSIVE_FIELD_caller_get(swigCPtr);
      if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public MUTUALLY_RECURSIVE_FIELD() : this(KeilMapLibPINVOKE.new_MUTUALLY_RECURSIVE_FIELD(), true) {
  }

}
