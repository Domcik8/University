;2013.11.30
.model small
	Input_Buffer_Length equ 16
	Output_Buffer_Length equ 255
.stack 100h
.data
;**********************************************************************************************
;OPC Table (Operation Code Table)
;**********************************************************************************************
OPC00 db "ADD           $", 0
OPC01 db "ADD           $", 0
OPC02 db "ADD           $", 0
OPC03 db "ADD           $", 0
OPC04 db "ADD           $", 00
OPC05 db "ADD           $", 00
OPC06 db "PUSH          $", 0
OPC07 db "POP           $", 0
OPC08 db "OR            $", 0
OPC09 db "OR            $", 0
OPC0A db "OR            $", 0
OPC0B db "OR            $", 0
OPC0C db "OR            $", 00
OPC0D db "OR            $", 00
OPC0E db "PUSH          $", 0
OPC0F db "POP           $", 0
OPC10 db "ADC           $", 0
OPC11 db "ADC           $", 0
OPC12 db "ADC           $", 0
OPC13 db "ADC           $", 0
OPC14 db "ADC           $", 00
OPC15 db "ADC           $", 00
OPC16 db "PUSH          $", 0
OPC17 db "POP           $", 0
OPC18 db "SBB           $", 0
OPC19 db "SBB           $", 0
OPC1A db "SBB           $", 0
OPC1B db "SBB           $", 0
OPC1C db "SBB           $", 00
OPC1D db "SBB           $", 00
OPC1E db "PUSH          $", 0
OPC1F db "POP           $", 0
OPC20 db "AND           $", 0
OPC21 db "AND           $", 0
OPC22 db "AND           $", 0
OPC23 db "AND           $", 0
OPC24 db "AND           $", 00
OPC25 db "AND           $", 00
OPC26 db "PREFIKSAS     $", "P"
OPC27 db "DAA           $", 000
OPC28 db "SUB           $", 0
OPC29 db "SUB           $", 0
OPC2A db "SUB           $", 0
OPC2B db "SUB           $", 0
OPC2C db "SUB           $", 00
OPC2D db "SUB           $", 00
OPC2E db "PREFIKSAS     $", "P"
OPC2F db "DAS           $", 000
OPC30 db "XOR           $", 0
OPC31 db "XOR           $", 0
OPC32 db "XOR           $", 0
OPC33 db "XOR           $", 0
OPC34 db "XOR           $", 00
OPC35 db "XOR           $", 00
OPC36 db "PREFIKSAS     $", "P"
OPC37 db "AAA           $", 000
OPC38 db "CMP           $", 0
OPC39 db "CMP           $", 0
OPC3A db "CMP           $", 0
OPC3B db "CMP           $", 0
OPC3C db "CMP           $", 00
OPC3D db "CMP           $", 00
OPC3E db "PREFIKSAS     $", "P"
OPC3F db "AAS           $", 000            
OPC40 db "INC           $", 0
OPC41 db "INC           $", 0
OPC42 db "INC           $", 0
OPC43 db "INC           $", 0
OPC44 db "INC           $", 0
OPC45 db "INC           $", 0
OPC46 db "INC           $", 0
OPC47 db "INC           $", 0
OPC48 db "DEC           $", 0
OPC49 db "DEC           $", 0
OPC4A db "DEC           $", 0
OPC4B db "DEC           $", 0
OPC4C db "DEC           $", 0
OPC4D db "DEC           $", 0
OPC4E db "DEC           $", 0
OPC4F db "DEC           $", 0
OPC50 db "PUSH          $", 0
OPC51 db "PUSH          $", 0
OPC52 db "PUSH          $", 0
OPC53 db "PUSH          $", 0
OPC54 db "PUSH          $", 0
OPC55 db "PUSH          $", 0
OPC56 db "PUSH          $", 0
OPC57 db "PUSH          $", 0
OPC58 db "POP           $", 0
OPC59 db "POP           $", 0
OPC5A db "POP           $", 0
OPC5B db "POP           $", 0
OPC5C db "POP           $", 0
OPC5D db "POP           $", 0
OPC5E db "POP           $", 0
OPC5F db "POP           $", 0
OPC60 db "UNKNOWN       $", 0
OPC61 db "UNKNOWN       $", 0
OPC62 db "UNKNOWN       $", 0
OPC63 db "UNKNOWN       $", 0
OPC64 db "UNKNOWN       $", 0
OPC65 db "UNKNOWN       $", 0
OPC66 db "UNKNOWN       $", 0
OPC67 db "UNKNOWN       $", 0
OPC68 db "UNKNOWN       $", 0
OPC69 db "UNKNOWN       $", 0
OPC6A db "UNKNOWN       $", 0
OPC6B db "UNKNOWN       $", 0
OPC6C db "UNKNOWN       $", 0
OPC6D db "UNKNOWN       $", 0
OPC6E db "UNKNOWN       $", 0
OPC6F db "UNKNOWN       $", 0
OPC70 db "JO            $", 0
OPC71 db "JNO           $", 0
OPC72 db "JNAE          $", 0
OPC73 db "JAE           $", 0
OPC74 db "JE            $", 0
OPC75 db "JNE           $", 0
OPC76 db "JBE           $", 0
OPC77 db "JA            $", 0
OPC78 db "JS            $", 0
OPC79 db "JNS           $", 0
OPC7A db "JP            $", 0
OPC7B db "JNP           $", 0
OPC7C db "JL            $", 0
OPC7D db "JGE           $", 0
OPC7E db "JLE           $", 0
OPC7F db "JG            $", 0
OPC80 db "TWO BYTES     $", 0
OPC81 db "TWO BYTES     $", 0
OPC82 db "TWO BYTES     $", 0
OPC83 db "TWO BYTES     $", 0
OPC84 db "TEST          $", 0
OPC85 db "TEST          $", 0
OPC86 db "XCHG          $", 0
OPC87 db "XCHG          $", 0
OPC88 db "MOV           $", 0
OPC89 db "MOV           $", 0
OPC8A db "MOV           $", 0
OPC8B db "MOV           $", 0
OPC8C db "MOV           $", 00
OPC8D db "LEA           $", 0
OPC8E db "MOV           $", 00
OPC8F db "POP           $", 0
OPC90 db "NOP           $", 0
OPC91 db "XCHG          $", 0
OPC92 db "XCHG          $", 0
OPC93 db "XCHG          $", 0
OPC94 db "XCHG          $", 0
OPC95 db "XCHG          $", 0
OPC96 db "XCHG          $", 0
OPC97 db "XCHG          $", 0
OPC98 db "CBW           $", 0 
OPC99 db "CWD           $", 0
OPC9A db "CALL          $", 0
OPC9B db "WAIT          $", 0
OPC9C db "PUSHF         $", 0
OPC9D db "POPF          $", 0
OPC9E db "SAHF          $", 0
OPC9F db "LAHF          $", 0
OPCA0 db "MOV           $", 0
OPCA1 db "MOV           $", 0
OPCA2 db "MOV           $", 00
OPCA3 db "MOV           $", 00
OPCA4 db "MOVSB         $", 0
OPCA5 db "MOVSB         $", 0
OPCA6 db "CMPSB         $", 0
OPCA7 db "CMPSB         $", 0
OPCA8 db "TEST          $", 0
OPCA9 db "TEST          $", 0
OPCAA db "STOSB         $", 0
OPCAB db "STOSB         $", 0
OPCAC db "LODSB         $", 0
OPCAD db "LODSB         $", 0
OPCAE db "SCASB         $", 0
OPCAF db "SCASB         $", 0
OPCB0 db "MOV           $", 0
OPCB1 db "MOV           $", 0
OPCB2 db "MOV           $", 0
OPCB3 db "MOV           $", 0
OPCB4 db "MOV           $", 0
OPCB5 db "MOV           $", 0
OPCB6 db "MOV           $", 0
OPCB7 db "MOV           $", 0
OPCB8 db "MOV           $", 0
OPCB9 db "MOV           $", 0
OPCBA db "MOV           $", 0
OPCBB db "MOV           $", 0
OPCBC db "MOV           $", 0
OPCBD db "MOV           $", 0
OPCBE db "MOV           $", 0
OPCBF db "MOV           $", 0
OPCC0 db "UNKNOWN       $", 0
OPCC1 db "UNKNOWN       $", 0
OPCC2 db "RET           $", 0
OPCC3 db "RET           $", 00
OPCC4 db "LES           $", 0
OPCC5 db "LDS           $", 0
OPCC6 db "MOV           $", 0
OPCC7 db "MOV           $", 0
OPCC8 db "UNKNOWN       $", 0
OPCC9 db "UNKNOWN       $", 0
OPCCA db "RETF          $", 0
OPCCB db "RETF          $", 00
OPCCC db "INT           $", "3"
OPCCD db "INT           $", 0
OPCCE db "INTO          $", 0
OPCCF db "IRET          $", 0
OPCD0 db "TWO BYTES     $", 0
OPCD1 db "TWO BYTES     $", 0
OPCD2 db "TWO BYTES     $", 0
OPCD3 db "TWO BYTES     $", 0
OPCD4 db "AAM           $", 0
OPCD5 db "AAD           $", 0
OPCD6 db "UNKNOWN       $", 0
OPCD7 db "XLAT          $", 0
OPCD8 db "ESC           $", 0
OPCD9 db "ESC           $", 0
OPCDA db "ESC           $", 0
OPCDB db "ESC           $", 0
OPCDC db "ESC           $", 0
OPCDD db "ESC           $", 0
OPCDE db "ESC           $", 0
OPCDF db "ESC           $", 0
OPCE0 db "LOOPNE        $", 0
OPCE1 db "LOOPE         $", 0
OPCE2 db "LOOP          $", 0
OPCE3 db "JCXZ          $", 0
OPCE4 db "IN            $", 0
OPCE5 db "IN            $", 0
OPCE6 db "OUT           $", 0
OPCE7 db "OUT           $", 0
OPCE8 db "CALL          $", 0
OPCE9 db "JMP           $", 0
OPCEA db "JMP           $", 0
OPCEB db "JMP           $", 0
OPCEC db "IN            $", 0
OPCED db "IN            $", 0
OPCEE db "OUT           $", 0
OPCEF db "OUT           $", 0
OPCF0 db "LOCK          $", 0
OPCF1 db "UNKNOWN       $", 0
OPCF2 db "REPNZ         $", 0
OPCF3 db "REP           $", 0
OPCF4 db "HLT           $", 0
OPCF5 db "CMC           $", 0
OPCF6 db "TWO BYTES     $", 0
OPCF7 db "TWO BYTES     $", 0
OPCF8 db "CLC           $", 000
OPCF9 db "STC           $", 000
OPCFA db "CLI           $", 000
OPCFB db "STI           $", 000
OPCFC db "CLD           $", 000
OPCFD db "STD           $", 000
OPCFE db "TWO BYTES     $", 0
OPCFF db "TWO BYTES     $", 0
;**********************************************************************************************
	Help_Message_1 db 10, 13, "Dominik Gabriel Lisovski", 10, 13
	Help_Message_2 db "Vilnius University, Faculty of Mathematics and Informatics", 10, 13
	Help_Message_3 db "Software Engineering", 10, 13
	Help_Message_4 db "6th group ", 10, 13, "Disassembler", 10, 13, 10, 13
	Error_Message_1 db "You have to enter input and output files in parameters", 13, 10, "$"
	Error_Message_2 db "Could not open reading file", 13, 10, "$"
	Error_Message_3 db "Could not create writing file ", 13, 10, "$"
	Error_Message_4 db "Could not open writing file", 13, 10, "$"											;Not Being Used
	Error_Message_5 db "Could not read from input file  ", 13, 10, "$"
	Error_Message_6 db "Could not write to output file ", 13, 10, "$"
	Error_Message_7 db " ", 13, 10, "$"
	Error_Message_8 db " ", 13, 10, "$"
	Test_Message db "It is a test message", 13, 10, "$"
	Input_File db 15 dup (0)
	Output_File db 15 dup (0)
	Input_File_Handle dw 0
	Output_File_Handle dw 0
	Input_Buffer db Input_Buffer_Length dup (?)
	Output_Buffer db Output_Buffer_Length dup (?)
;**********************************************************************************************
;In Whole Program Registers Mainly Hold
;Di = Size Of Unused Bytes In Input Buffer
;**********************************************************************************************
.code
Start:
	Mov Bx, @Data
	Mov DS, Bx
	Mov Bx, 81h
	Xor Di, Di
	Xor Si, Si
	Call Check_Parameters_1																					;Works
	Call Open_Input_File																					;Works
	Call Create_Output_File																					;Works
;	Call Open_Output_File																					;Not Being Used But Works
	Call Read_From_Input_File					;Used Once To Fill Buffer									;Also Used In Main_Function
	Jmp Main_Function																						;
;	Call Write_To_Output_File					;At The Moment Used Only For Tests							;Not Being Used But Works
;	Jmp Close_Input_And_Output_Files		 	;Close All files And Exit									;Used In Main_Function
;**********************************************************************************************
;Working With Parameters
;**********************************************************************************************
Check_Parameters_0:
	Inc Bx
;**********************************************************************************************
Check_Parameters_1:
	Mov Ax, [Es:Bx]
	Cmp Al, 0Dh									;Enter Symbol
	Jne Check_Parameters_2
	Jmp Error_1
;**********************************************************************************************
Check_Parameters_2:
	Cmp Al, 20h									;Space Symbol
	Je Check_Parameters_0
	Cmp Ax, '?/'
	Je Help_Check_0
	Jmp Form_Input_File_1
;**********************************************************************************************
Help_Check_0:
	Inc Bx
;**********************************************************************************************
Help_Check_1:
	Inc Bx
	Mov Ax, [Es:Bx]
	Cmp Al, 20h									;Space Symbol
	Je Help_Check_1
	Cmp Al, 0Dh									;Enter Symbol
	Je Help_Check_2
	Jmp Error_1
;**********************************************************************************************
Help_Check_2:
	Mov Dx, offset Help_Message_1
	Jmp Print_Message
;**********************************************************************************************
Form_Input_File_1:
	Mov Ax, [Es:Bx]
	Cmp Al, 20h									;Space Symbol
	Je Check_Spaces
	Cmp Al, 0Dh									;Enter Symbol
	Jne Form_Input_File_2
	Jmp Error_1
;**********************************************************************************************
Check_Spaces:
	Cmp Ah, 20h									;Space Symbol
	Jne Form_Output_File_1
	Inc Bx
	Mov Ax, [Es:Bx]
	Jmp Check_Spaces
;**********************************************************************************************
Form_Input_File_2:
	Mov [Input_File + Si], Al
	Inc Si
	Inc Bx
	Jmp Form_Input_File_1
;**********************************************************************************************
Form_Output_File_1:
	Inc Bx
	Mov Ax, [Es:Bx]
	Cmp Al, 20h									;Space Symbol
	Jne Form_Output_File_2
	Jmp Check_Parameters_3
;**********************************************************************************************
Form_Output_File_2:
	Cmp Al, 0Dh									;Enter Symbol
	Je Return
	Mov [Output_File + Di], Al
	Inc Di
	Jmp Form_Output_File_1
;**********************************************************************************************
Check_Parameters_3:
	Mov Ax, [Es:Bx]
	Cmp Al, 0Dh									;Enter Symbol
	Je Return
	Inc Bx
	Cmp Al, 20h									;Space Symbol
	Je Check_Parameters_3
	Jmp Error_1
;**********************************************************************************************
Open_Input_File:
	Mov Ah, 3Dh									;Open File Using Handle
	Mov Al, 0                                   ;Open For Reading Only
	Mov Dx, Offset Input_File
	Int 21h
	Jc Jump_To_Error_2
	mov [Input_File_Handle], Ax
	Ret
;**********************************************************************************************
Jump_To_Error_2:
	Jmp Error_2
;**********************************************************************************************
Create_Output_File:																									
	Mov Ah, 3Ch									;Create File Using Handle
	Mov Cx, 0									;No Attributes
	Mov Dx, Offset Output_File
	Int 21h
	Jc Jump_To_Error_3
	Mov [Output_File_Handle], Ax
	Ret
;**********************************************************************************************
Jump_To_Error_3:
	Jmp Error_3
;**********************************************************************************************
Return:
	Ret
;**********************************************************************************************
Open_Output_File:																									;NOT BEING USED	
	Mov Ah, 3Dh									;Open File Using Handle												;NOT BEING USED	
	Mov Al, 1									;Open For Writing Only												;NOT BEING USED	
	Mov Dx, Offset Output_File																						;NOT BEING USED	
	Int 21h																											;NOT BEING USED	
	Jc Jump_To_Error_4																								;NOT BEING USED	
	mov [Output_File_Handle], Ax																					;NOT BEING USED	
	Ret																												;NOT BEING USED	
;**********************************************************************************************
Jump_To_Error_4:																									;NOT BEING USED	
	Jmp Error_4																										;NOT BEING USED	
;**********************************************************************************************
Read_From_Input_File:
	Mov Ah, 3Fh 								;Read From File or Device Using Handle
	Mov Bx, Input_File_Handle					;Input File Handle
	Mov Cx, Input_Buffer_Length						;Input_Buffer_Length Equ 16
	Mov Dx, Offset Input_Buffer					;Read To 
	Int 21h										;Ax Now Holds Amount Of Bytes Read
	Jc Jump_To_Error_5
	Mov Di, Ax									;Di Used To Check Amount Of Bytes Unread
	Mov Cx, Ax									;Cx Used To Show Length Of Buffer Read
	Mov Si, Offset Input_Buffer					;Put Buffer Start To Si
	ret
;**********************************************************************************************
Jump_To_Error_5:
	Jmp Error_5
;**********************************************************************************************
Write_To_Output_File:
;*************************Cx Should Hold Number Of Bytes To Write******************************
	Mov Ah, 40h
	Mov Bx, Output_File_Handle
	Mov Dx, Offset Test_Message
	Mov Cx, 22
	Int 21h
	Jc Jump_To_Error_6
	Ret
;**********************************************************************************************
Jump_To_Error_6:
	Jmp Error_6
;**********************************************************************************************
Jump_To_Close_Input_And_Output_Files:
	Jmp Close_Input_And_Output_Files		 	;Close All files And Exit
;**********************************************************************************************
Main_Function:
	Call Check_Buffer
;	Inc Si 										;Buffer Byte 1 Returns The Number Of Chars							;NOT NEEDED
	Mov Dh, [Ds:Si]								;Put First Byte To Dh
	Inc Si
	Dec Di
	Call Check_Buffer							;Check If Buffer Did Not End
	Mov Dl, [Ds:Si]								;Put Second Byte To Dl
	Inc Si
	Dec Di
	Call Case_Check								;Dx Holds 2 Bytes
;	Call Write_To_Output_File
	Jmp Main_Function
;**********************************************************************************************
;Checking If We Need To Read From File Again
;**********************************************************************************************
Check_Buffer:
	Cmp Di, 0
	Jne Return
	Cmp Cx, Input_Buffer_Length
	Jne Jump_To_Close_Input_And_Output_Files
	Call Read_From_Input_File
	Ret
;**********************************************************************************************
;Checking All Possible Cases
;**********************************************************************************************
Case_Check:
	Mov Bx, Offset OPC00
	Mov Ax, 16
	Mul Dx
	Add Bx, Ax
	Add Bx, 15
	
	;If No Case Found Then Do Case_0
;**********************************************************************************************
Case_0:
	Call Write_To_Output_File
	Ret
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;Error Messages
;**********************************************************************************************
Error_1:
	Mov Dx, Offset Error_Message_1
	Jmp Print_Message
;**********************************************************************************************
Error_2:
	Mov Dx, Offset Error_Message_2
	Jmp Print_Message
;**********************************************************************************************
Error_3:
	Mov Dx, Offset Error_Message_3
	Jmp Print_Message
;**********************************************************************************************
Error_4:
	Mov Dx, Offset Error_Message_4
	Jmp Print_Message
;**********************************************************************************************
Error_5:
	Mov Dx, Offset Error_Message_5
	Jmp Print_Message
;**********************************************************************************************
Error_6:
	Mov Dx, Offset Error_Message_6
	Jmp Print_Message
;**********************************************************************************************
Print_Message:
	Mov ah, 9
	Int 21h
;**********************************************************************************************
Close_Input_And_Output_Files:
	Mov Ah, 3Eh
	Mov Bx, Offset Input_File_Handle
	Int 21h
	Mov Ah, 3Eh
	Mov Bx, Offset Output_File_Handle
	Int 21h
;**********************************************************************************************
Exit:
	Mov ax, 4C00h
	int 21h
;**********************************************************************************************
end Start