//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class REMOVED_SYMBOL_VECTOR : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<REMOVED_SYMBOL_FIELD>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal REMOVED_SYMBOL_VECTOR(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(REMOVED_SYMBOL_VECTOR obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~REMOVED_SYMBOL_VECTOR() {
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
          KeilMapLibPINVOKE.delete_REMOVED_SYMBOL_VECTOR(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public REMOVED_SYMBOL_VECTOR(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (REMOVED_SYMBOL_FIELD element in c) {
      this.Add(element);
    }
  }

  public REMOVED_SYMBOL_VECTOR(global::System.Collections.Generic.IEnumerable<REMOVED_SYMBOL_FIELD> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (REMOVED_SYMBOL_FIELD element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public REMOVED_SYMBOL_FIELD this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(REMOVED_SYMBOL_FIELD[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(REMOVED_SYMBOL_FIELD[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, REMOVED_SYMBOL_FIELD[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public REMOVED_SYMBOL_FIELD[] ToArray() {
    REMOVED_SYMBOL_FIELD[] array = new REMOVED_SYMBOL_FIELD[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<REMOVED_SYMBOL_FIELD> global::System.Collections.Generic.IEnumerable<REMOVED_SYMBOL_FIELD>.GetEnumerator() {
    return new REMOVED_SYMBOL_VECTOREnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new REMOVED_SYMBOL_VECTOREnumerator(this);
  }

  public REMOVED_SYMBOL_VECTOREnumerator GetEnumerator() {
    return new REMOVED_SYMBOL_VECTOREnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class REMOVED_SYMBOL_VECTOREnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<REMOVED_SYMBOL_FIELD>
  {
    private REMOVED_SYMBOL_VECTOR collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public REMOVED_SYMBOL_VECTOREnumerator(REMOVED_SYMBOL_VECTOR collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public REMOVED_SYMBOL_FIELD Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (REMOVED_SYMBOL_FIELD)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
  }

  public void Clear() {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Clear(swigCPtr);
  }

  public void Add(REMOVED_SYMBOL_FIELD x) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Add(swigCPtr, REMOVED_SYMBOL_FIELD.getCPtr(x));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_reserve(swigCPtr, n);
  }

  public REMOVED_SYMBOL_VECTOR() : this(KeilMapLibPINVOKE.new_REMOVED_SYMBOL_VECTOR__SWIG_0(), true) {
  }

  public REMOVED_SYMBOL_VECTOR(REMOVED_SYMBOL_VECTOR other) : this(KeilMapLibPINVOKE.new_REMOVED_SYMBOL_VECTOR__SWIG_1(REMOVED_SYMBOL_VECTOR.getCPtr(other)), true) {
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public REMOVED_SYMBOL_VECTOR(int capacity) : this(KeilMapLibPINVOKE.new_REMOVED_SYMBOL_VECTOR__SWIG_2(capacity), true) {
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  private REMOVED_SYMBOL_FIELD getitemcopy(int index) {
    REMOVED_SYMBOL_FIELD ret = new REMOVED_SYMBOL_FIELD(KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_getitemcopy(swigCPtr, index), true);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private REMOVED_SYMBOL_FIELD getitem(int index) {
    REMOVED_SYMBOL_FIELD ret = new REMOVED_SYMBOL_FIELD(KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_getitem(swigCPtr, index), false);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, REMOVED_SYMBOL_FIELD val) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_setitem(swigCPtr, index, REMOVED_SYMBOL_FIELD.getCPtr(val));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(REMOVED_SYMBOL_VECTOR values) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_AddRange(swigCPtr, REMOVED_SYMBOL_VECTOR.getCPtr(values));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public REMOVED_SYMBOL_VECTOR GetRange(int index, int count) {
    global::System.IntPtr cPtr = KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_GetRange(swigCPtr, index, count);
    REMOVED_SYMBOL_VECTOR ret = (cPtr == global::System.IntPtr.Zero) ? null : new REMOVED_SYMBOL_VECTOR(cPtr, true);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, REMOVED_SYMBOL_FIELD x) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Insert(swigCPtr, index, REMOVED_SYMBOL_FIELD.getCPtr(x));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, REMOVED_SYMBOL_VECTOR values) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_InsertRange(swigCPtr, index, REMOVED_SYMBOL_VECTOR.getCPtr(values));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_RemoveAt(swigCPtr, index);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_RemoveRange(swigCPtr, index, count);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public static REMOVED_SYMBOL_VECTOR Repeat(REMOVED_SYMBOL_FIELD value, int count) {
    global::System.IntPtr cPtr = KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Repeat(REMOVED_SYMBOL_FIELD.getCPtr(value), count);
    REMOVED_SYMBOL_VECTOR ret = (cPtr == global::System.IntPtr.Zero) ? null : new REMOVED_SYMBOL_VECTOR(cPtr, true);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_Reverse__SWIG_1(swigCPtr, index, count);
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, REMOVED_SYMBOL_VECTOR values) {
    KeilMapLibPINVOKE.REMOVED_SYMBOL_VECTOR_SetRange(swigCPtr, index, REMOVED_SYMBOL_VECTOR.getCPtr(values));
    if (KeilMapLibPINVOKE.SWIGPendingException.Pending) throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
  }

}
