//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class STACK_USAGE_VECTOR : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<STACK_USAGE_FIELD>
{
	private global::System.Runtime.InteropServices.HandleRef swigCPtr;
	protected bool swigCMemOwn;

	internal STACK_USAGE_VECTOR(global::System.IntPtr cPtr, bool cMemoryOwn)
	{
		swigCMemOwn = cMemoryOwn;
		swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
	}

	internal static global::System.Runtime.InteropServices.HandleRef getCPtr(STACK_USAGE_VECTOR obj)
	{
		return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
	}

	~STACK_USAGE_VECTOR()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);
		global::System.GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		lock (this)
		{
			if (swigCPtr.Handle != global::System.IntPtr.Zero)
			{
				if (swigCMemOwn)
				{
					swigCMemOwn = false;
					KeilMapLibPINVOKE.delete_STACK_USAGE_VECTOR(swigCPtr);
				}
				swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
			}
		}
	}

	public STACK_USAGE_VECTOR(global::System.Collections.IEnumerable c) : this()
	{
		if (c == null)
		{
			throw new global::System.ArgumentNullException("c");
		}
		foreach (STACK_USAGE_FIELD element in c)
		{
			this.Add(element);
		}
	}

	public STACK_USAGE_VECTOR(global::System.Collections.Generic.IEnumerable<STACK_USAGE_FIELD> c) : this()
	{
		if (c == null)
		{
			throw new global::System.ArgumentNullException("c");
		}
		foreach (STACK_USAGE_FIELD element in c)
		{
			this.Add(element);
		}
	}

	public bool IsFixedSize
	{
		get
		{
			return false;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	public STACK_USAGE_FIELD this[int index]
	{
		get
		{
			return getitem(index);
		}
		set
		{
			setitem(index, value);
		}
	}

	public int Capacity
	{
		get
		{
			return (int)capacity();
		}
		set
		{
			if (value < size())
			{
				throw new global::System.ArgumentOutOfRangeException("Capacity");
			}
			reserve((uint)value);
		}
	}

	public int Count
	{
		get
		{
			return (int)size();
		}
	}

	public bool IsSynchronized
	{
		get
		{
			return false;
		}
	}

	public void CopyTo(STACK_USAGE_FIELD[] array)
	{
		CopyTo(0, array, 0, this.Count);
	}

	public void CopyTo(STACK_USAGE_FIELD[] array, int arrayIndex)
	{
		CopyTo(0, array, arrayIndex, this.Count);
	}

	public void CopyTo(int index, STACK_USAGE_FIELD[] array, int arrayIndex, int count)
	{
		if (array == null)
		{
			throw new global::System.ArgumentNullException("array");
		}
		if (index < 0)
		{
			throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
		}
		if (arrayIndex < 0)
		{
			throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
		}
		if (count < 0)
		{
			throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
		}
		if (array.Rank > 1)
		{
			throw new global::System.ArgumentException("Multi dimensional array.", "array");
		}
		if (index + count > this.Count || arrayIndex + count > array.Length)
		{
			throw new global::System.ArgumentException("Number of elements to copy is too large.");
		}
		for (int i = 0; i < count; i++)
		{
			array.SetValue(getitemcopy(index + i), arrayIndex + i);
		}
	}

	public STACK_USAGE_FIELD[] ToArray()
	{
		STACK_USAGE_FIELD[] array = new STACK_USAGE_FIELD[this.Count];
		this.CopyTo(array);
		return array;
	}

	global::System.Collections.Generic.IEnumerator<STACK_USAGE_FIELD> global::System.Collections.Generic.IEnumerable<STACK_USAGE_FIELD>.GetEnumerator()
	{
		return new STACK_USAGE_VECTOREnumerator(this);
	}

	global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
	{
		return new STACK_USAGE_VECTOREnumerator(this);
	}

	public STACK_USAGE_VECTOREnumerator GetEnumerator()
	{
		return new STACK_USAGE_VECTOREnumerator(this);
	}

	// Type-safe enumerator
	/// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
	/// whenever the collection is modified. This has been done for changes in the size of the
	/// collection but not when one of the elements of the collection is modified as it is a bit
	/// tricky to detect unmanaged code that modifies the collection under our feet.
	public sealed class STACK_USAGE_VECTOREnumerator : global::System.Collections.IEnumerator
		, global::System.Collections.Generic.IEnumerator<STACK_USAGE_FIELD>
	{
		private STACK_USAGE_VECTOR collectionRef;
		private int currentIndex;
		private object currentObject;
		private int currentSize;

		public STACK_USAGE_VECTOREnumerator(STACK_USAGE_VECTOR collection)
		{
			collectionRef = collection;
			currentIndex = -1;
			currentObject = null;
			currentSize = collectionRef.Count;
		}

		// Type-safe iterator Current
		public STACK_USAGE_FIELD Current
		{
			get
			{
				if (currentIndex == -1)
				{
					throw new global::System.InvalidOperationException("Enumeration not started.");
				}
				if (currentIndex > currentSize - 1)
				{
					throw new global::System.InvalidOperationException("Enumeration finished.");
				}
				if (currentObject == null)
				{
					throw new global::System.InvalidOperationException("Collection modified.");
				}
				return (STACK_USAGE_FIELD)currentObject;
			}
		}

		// Type-unsafe IEnumerator.Current
		object global::System.Collections.IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public bool MoveNext()
		{
			int size = collectionRef.Count;
			bool moveOkay = (currentIndex + 1 < size) && (size == currentSize);
			if (moveOkay)
			{
				currentIndex++;
				currentObject = collectionRef[currentIndex];
			}
			else
			{
				currentObject = null;
			}
			return moveOkay;
		}

		public void Reset()
		{
			currentIndex = -1;
			currentObject = null;
			if (collectionRef.Count != currentSize)
			{
				throw new global::System.InvalidOperationException("Collection modified.");
			}
		}

		public void Dispose()
		{
			currentIndex = -1;
			currentObject = null;
		}
	}

	public void Clear()
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Clear(swigCPtr);
	}

	public void Add(STACK_USAGE_FIELD x)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Add(swigCPtr, STACK_USAGE_FIELD.getCPtr(x));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	private uint size()
	{
		uint ret = KeilMapLibPINVOKE.STACK_USAGE_VECTOR_size(swigCPtr);
		return ret;
	}

	private uint capacity()
	{
		uint ret = KeilMapLibPINVOKE.STACK_USAGE_VECTOR_capacity(swigCPtr);
		return ret;
	}

	private void reserve(uint n)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_reserve(swigCPtr, n);
	}

	public STACK_USAGE_VECTOR() : this(KeilMapLibPINVOKE.new_STACK_USAGE_VECTOR__SWIG_0(), true)
	{
	}

	public STACK_USAGE_VECTOR(STACK_USAGE_VECTOR other) : this(KeilMapLibPINVOKE.new_STACK_USAGE_VECTOR__SWIG_1(STACK_USAGE_VECTOR.getCPtr(other)), true)
	{
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public STACK_USAGE_VECTOR(int capacity) : this(KeilMapLibPINVOKE.new_STACK_USAGE_VECTOR__SWIG_2(capacity), true)
	{
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	private STACK_USAGE_FIELD getitemcopy(int index)
	{
		STACK_USAGE_FIELD ret = new STACK_USAGE_FIELD(KeilMapLibPINVOKE.STACK_USAGE_VECTOR_getitemcopy(swigCPtr, index), true);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
		return ret;
	}

	private STACK_USAGE_FIELD getitem(int index)
	{
		STACK_USAGE_FIELD ret = new STACK_USAGE_FIELD(KeilMapLibPINVOKE.STACK_USAGE_VECTOR_getitem(swigCPtr, index), false);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
		return ret;
	}

	private void setitem(int index, STACK_USAGE_FIELD val)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_setitem(swigCPtr, index, STACK_USAGE_FIELD.getCPtr(val));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public void AddRange(STACK_USAGE_VECTOR values)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_AddRange(swigCPtr, STACK_USAGE_VECTOR.getCPtr(values));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public STACK_USAGE_VECTOR GetRange(int index, int count)
	{
		global::System.IntPtr cPtr = KeilMapLibPINVOKE.STACK_USAGE_VECTOR_GetRange(swigCPtr, index, count);
		STACK_USAGE_VECTOR ret = (cPtr == global::System.IntPtr.Zero) ? null : new STACK_USAGE_VECTOR(cPtr, true);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
		return ret;
	}

	public void Insert(int index, STACK_USAGE_FIELD x)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Insert(swigCPtr, index, STACK_USAGE_FIELD.getCPtr(x));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public void InsertRange(int index, STACK_USAGE_VECTOR values)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_InsertRange(swigCPtr, index, STACK_USAGE_VECTOR.getCPtr(values));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public void RemoveAt(int index)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_RemoveAt(swigCPtr, index);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public void RemoveRange(int index, int count)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_RemoveRange(swigCPtr, index, count);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public static STACK_USAGE_VECTOR Repeat(STACK_USAGE_FIELD value, int count)
	{
		global::System.IntPtr cPtr = KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Repeat(STACK_USAGE_FIELD.getCPtr(value), count);
		STACK_USAGE_VECTOR ret = (cPtr == global::System.IntPtr.Zero) ? null : new STACK_USAGE_VECTOR(cPtr, true);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
		return ret;
	}

	public void Reverse()
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Reverse__SWIG_0(swigCPtr);
	}

	public void Reverse(int index, int count)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_Reverse__SWIG_1(swigCPtr, index, count);
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

	public void SetRange(int index, STACK_USAGE_VECTOR values)
	{
		KeilMapLibPINVOKE.STACK_USAGE_VECTOR_SetRange(swigCPtr, index, STACK_USAGE_VECTOR.getCPtr(values));
		if (KeilMapLibPINVOKE.SWIGPendingException.Pending)
		{
			throw KeilMapLibPINVOKE.SWIGPendingException.Retrieve();
		}
	}

}
