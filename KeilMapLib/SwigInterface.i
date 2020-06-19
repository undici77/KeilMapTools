/* File : SwigInterface.i */

%begin %{
#include "stdafx.h"
%}
%module(directors="1") KeilMapLibCs
%{
#include "KeilMapLibStructs.h"
#include "SwigInterface.h"
%}

%include "std_vector.i"
%include "std_string.i"

%feature("director") SwigInterface;

%include "KeilMapLibStructs.h"
%include "SwigInterface.h"


