/* File : KeilMapLibInterface.i */

%begin
%{
	#include "stdafx.h"
%}

%module(directors = "1") KeilMapLib

%{
	#include "KeilMapLibStructs.h"
	#include "KeilMapLibClient.h"
%}

%include "std_vector.i"
%include "std_string.i"
%include "KeilMapLibStructs.h"
%include "KeilMapLibClient.h"

namespace std
{
	%template(CROSS_REFERENCE_VECTOR)                      vector<CROSS_REFERENCE_FIELD>;
	%template(FUNCTION_POINTER_VECTOR)                     vector<FUNCTION_POINTER_FIELD>;
	%template(GLOBAL_SYMBOL_VECTOR)                        vector<GLOBAL_SYMBOL_FIELD>;
	%template(IMAGE_COMPONENT_SIZE_VECTOR)                 vector<IMAGE_COMPONENT_SIZE_FIELD>;
	%template(LOCAL_SYMBOL_VECTOR)                         vector<LOCAL_SYMBOL_FIELD>;
	%template(MAXIMUM_STACK_USAGE_VECTOR)                  vector<MAXIMUM_STACK_USAGE_FIELD>;
	%template(MEMORY_MAP_IMAGE_OBJECT_VECTOR)              vector<MEMORY_MAP_IMAGE_OBJECT_FIELD>;
	%template(MEMORY_MAP_IMAGE_EXECUTION_REGION_VECTOR)    vector<MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD>;
	%template(MEMORY_MAP_IMAGE_LOAD_REGION_VECTOR)         vector<MEMORY_MAP_IMAGE_LOAD_REGION_FIELD>;
	%template(MUTUALLY_RECURSIVE_VECTOR)                   vector<MUTUALLY_RECURSIVE_FIELD>;
	%template(REMOVED_SYMBOL_VECTOR)                       vector<REMOVED_SYMBOL_FIELD>;
	%template(STACK_USAGE_VECTOR)                          vector<STACK_USAGE_FIELD>;
}

