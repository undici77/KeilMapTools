// This file is part of KeilRtxStackSize.
//
// KeilRtxStackSize is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilRtxStackSize is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilRtxStackSize.  If not, see <https://www.gnu.org/licenses/>.

#pragma once

/******************************************************************************/

#include "Errors.h"

/******************************************************************************/

#define UNDEFINED               -2

/******************************************************************************/

#define _DEF_CAT_2(a, b)                a b
#define DEF_CAT_2(a, b)                 _DEF_CAT_2(a, b)

#define _DEF_CAT_3(a, b, c)             a b c
#define DEF_CAT_3(a, b, c)              _DEF_CAT_3(a, b, c)

#define _DEF_CAT_4(a, b, c, d)          a b c d
#define DEF_CAT_4(a, b, c, d)           _DEF_CAT_4(a, b, c, d)

#define _DEF_CAT_5(a, b, c, d, e)       a b c d e
#define DEF_CAT_5(a, b, c, d, e)        _DEF_CAT_5(a, b, c, d, e)

/*********************************************************************************************************************/

#define ARRAY_SIZE(a)               (sizeof(a) / sizeof(a[0]))
#define ROUND(x, type)              ((type) (((x) >= 0) ? ((x) + 0.5f) : ((x) - 0.5f)))

#define RDIV_UU(a, b)               (((a) + ((b) / 2)) / (b))
#define RDIV_SU(a, b)               ((((a) >= 0) ? ((a) + ((b) / 2)) : ((a) - ((b) / 2))) / (b))

#define RUPDIV_UU(a, b)             (((a) + (b) - 1) / (b))

#define ABS(a)                      (((a) >= 0) ? (a) : -(a))

#define MIN(a, b)                   (((a) < (b)) ? (a) : (b))
#define MAX(a, b)                   (((a) > (b)) ? (a) : (b))
#define INTERNAL(a, b1, b2)         (((b1) <= (a)) && ((a) <= (b2)))

/*********************************************************************************************************************/

#define SWAP_64(x)     x = \
                           ((((x) & 0xff00000000000000) >> 56)  | (((x) & 0x00ff000000000000) >> 40) | \
                            (((x) & 0x0000ff0000000000) >> 24)  | (((x) & 0x000000ff00000000) >>  8) | \
                            (((x) & 0x00000000ff000000) <<  8)  | (((x) & 0x0000000000ff0000) << 24) | \
                            (((x) & 0x000000000000ff00) << 40 ) | (((x) & 0x00000000000000ff) << 56))

#define _SWAP_64(x)  ((((x) & 0xff00000000000000) >> 56)  | (((x) & 0x00ff000000000000) >> 40) | \
                      (((x) & 0x0000ff0000000000) >> 24)  | (((x) & 0x000000ff00000000) >>  8) | \
                      (((x) & 0x00000000ff000000) <<  8)  | (((x) & 0x0000000000ff0000) << 24) | \
                      (((x) & 0x000000000000ff00) << 40 ) | (((x) & 0x00000000000000ff) << 56))

#define SWAP_32(x)     x = \
                           ((((x) & 0xFF000000) >> 24) | (((x) & 0x00FF0000) >>  8) | \
                            (((x) & 0x0000FF00) <<  8) | (((x) & 0x000000FF) << 24))

#define _SWAP_32(x)    ((((x) & 0xFF000000) >> 24) | (((x) & 0x00FF0000) >>  8) | \
                        (((x) & 0x0000FF00) <<  8) | (((x) & 0x000000FF) << 24))

#define SWAP_16(x)     x = \
                           ((((x) >> 8) & 0xFF) | (((x) & 0xFF) << 8))

#define _SWAP_16(x)    ((((x) >> 8) & 0xFF) | (((x) & 0xFF) << 8))

/*********************************************************************************************************************/

#define BITS_IN_UINT8  8
typedef union
{
	uint8_t value;
	struct
	{
		uint8_t bit_00 : 1;
		uint8_t bit_01 : 1;
		uint8_t bit_02 : 1;
		uint8_t bit_03 : 1;
		uint8_t bit_04 : 1;
		uint8_t bit_05 : 1;
		uint8_t bit_06 : 1;
		uint8_t bit_07 : 1;
	} fields;
} UINT8_U;

#define BITS_IN_UINT16  16
typedef union
{
	uint16_t value;
	struct
	{
		uint16_t bit_00 : 1;
		uint16_t bit_01 : 1;
		uint16_t bit_02 : 1;
		uint16_t bit_03 : 1;
		uint16_t bit_04 : 1;
		uint16_t bit_05 : 1;
		uint16_t bit_06 : 1;
		uint16_t bit_07 : 1;
		uint16_t bit_08 : 1;
		uint16_t bit_09 : 1;
		uint16_t bit_10 : 1;
		uint16_t bit_11 : 1;
		uint16_t bit_12 : 1;
		uint16_t bit_13 : 1;
		uint16_t bit_14 : 1;
		uint16_t bit_15 : 1;
	} fields;
} UINT16_U;

#define BITS_IN_UINT  32
typedef union
{
	uint32_t value;
	struct
	{
		uint32_t bit_00 : 1;
		uint32_t bit_01 : 1;
		uint32_t bit_02 : 1;
		uint32_t bit_03 : 1;
		uint32_t bit_04 : 1;
		uint32_t bit_05 : 1;
		uint32_t bit_06 : 1;
		uint32_t bit_07 : 1;
		uint32_t bit_08 : 1;
		uint32_t bit_09 : 1;
		uint32_t bit_10 : 1;
		uint32_t bit_11 : 1;
		uint32_t bit_12 : 1;
		uint32_t bit_13 : 1;
		uint32_t bit_14 : 1;
		uint32_t bit_15 : 1;
		uint32_t bit_16 : 1;
		uint32_t bit_17 : 1;
		uint32_t bit_18 : 1;
		uint32_t bit_19 : 1;
		uint32_t bit_20 : 1;
		uint32_t bit_21 : 1;
		uint32_t bit_22 : 1;
		uint32_t bit_23 : 1;
		uint32_t bit_24 : 1;
		uint32_t bit_25 : 1;
		uint32_t bit_26 : 1;
		uint32_t bit_27 : 1;
		uint32_t bit_28 : 1;
		uint32_t bit_29 : 1;
		uint32_t bit_30 : 1;
		uint32_t bit_31 : 1;
	} fields;
} UINT32_U;

#define BITS_IN_UINT64  64
typedef union
{
	uint64_t value;
	struct
	{
		uint64_t bit_00 : 1;
		uint64_t bit_01 : 1;
		uint64_t bit_02 : 1;
		uint64_t bit_03 : 1;
		uint64_t bit_04 : 1;
		uint64_t bit_05 : 1;
		uint64_t bit_06 : 1;
		uint64_t bit_07 : 1;
		uint64_t bit_08 : 1;
		uint64_t bit_09 : 1;
		uint64_t bit_10 : 1;
		uint64_t bit_11 : 1;
		uint64_t bit_12 : 1;
		uint64_t bit_13 : 1;
		uint64_t bit_14 : 1;
		uint64_t bit_15 : 1;
		uint64_t bit_16 : 1;
		uint64_t bit_17 : 1;
		uint64_t bit_18 : 1;
		uint64_t bit_19 : 1;
		uint64_t bit_20 : 1;
		uint64_t bit_21 : 1;
		uint64_t bit_22 : 1;
		uint64_t bit_23 : 1;
		uint64_t bit_24 : 1;
		uint64_t bit_25 : 1;
		uint64_t bit_26 : 1;
		uint64_t bit_27 : 1;
		uint64_t bit_28 : 1;
		uint64_t bit_29 : 1;
		uint64_t bit_30 : 1;
		uint64_t bit_31 : 1;
		uint64_t bit_32 : 1;
		uint64_t bit_33 : 1;
		uint64_t bit_34 : 1;
		uint64_t bit_35 : 1;
		uint64_t bit_36 : 1;
		uint64_t bit_37 : 1;
		uint64_t bit_38 : 1;
		uint64_t bit_39 : 1;
		uint64_t bit_40 : 1;
		uint64_t bit_41 : 1;
		uint64_t bit_42 : 1;
		uint64_t bit_43 : 1;
		uint64_t bit_44 : 1;
		uint64_t bit_45 : 1;
		uint64_t bit_46 : 1;
		uint64_t bit_47 : 1;
		uint64_t bit_48 : 1;
		uint64_t bit_49 : 1;
		uint64_t bit_50 : 1;
		uint64_t bit_51 : 1;
		uint64_t bit_52 : 1;
		uint64_t bit_53 : 1;
		uint64_t bit_54 : 1;
		uint64_t bit_55 : 1;
		uint64_t bit_56 : 1;
		uint64_t bit_57 : 1;
		uint64_t bit_58 : 1;
		uint64_t bit_59 : 1;
		uint64_t bit_60 : 1;
		uint64_t bit_61 : 1;
		uint64_t bit_62 : 1;
		uint64_t bit_63 : 1;
	} fields;
} UINT64_U;
