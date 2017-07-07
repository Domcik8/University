;2013.12.03
.model small
	Input_Buffer_Length equ 16
	Output_Buffer_Length equ 71										;21 + 50
	Code_Output_Buffer_Length equ 50
.stack 100h
.data
;**********************************************************************************************
;OPC Table (Operation Code Table)
;**********************************************************************************************
OPC00 db "ADD            ", 0
OPC01 db "ADD            ", 0
OPC02 db "ADD            ", 0
OPC03 db "ADD            ", 0
OPC04 db "ADD            ", 00
OPC05 db "ADD            ", 00
OPC06 db "PUSH           ", 0
OPC07 db "POP            ", 0
OPC08 db "OR             ", 0
OPC09 db "OR             ", 0
OPC0A db "OR             ", 0
OPC0B db "OR             ", 0
OPC0C db "OR             ", 00
OPC0D db "OR             ", 00
OPC0E db "PUSH           ", 0
OPC0F db "POP            ", 0
OPC10 db "ADC            ", 0
OPC11 db "ADC            ", 0
OPC12 db "ADC            ", 0
OPC13 db "ADC            ", 0
OPC14 db "ADC            ", 00
OPC15 db "ADC            ", 00
OPC16 db "PUSH           ", 0
OPC17 db "POP            ", 0
OPC18 db "SBB            ", 0
OPC19 db "SBB            ", 0
OPC1A db "SBB            ", 0
OPC1B db "SBB            ", 0
OPC1C db "SBB            ", 00
OPC1D db "SBB            ", 00
OPC1E db "PUSH           ", 0
OPC1F db "POP            ", 0
OPC20 db "AND            ", 0
OPC21 db "AND            ", 0
OPC22 db "AND            ", 0
OPC23 db "AND            ", 0
OPC24 db "AND            ", 00
OPC25 db "AND            ", 00
OPC26 db "PREFIKSAS      ", "P"
OPC27 db "DAA            ", 1
OPC28 db "SUB            ", 0
OPC29 db "SUB            ", 0
OPC2A db "SUB            ", 0
OPC2B db "SUB            ", 0
OPC2C db "SUB            ", 00
OPC2D db "SUB            ", 00
OPC2E db "PREFIKSAS      ", "P"
OPC2F db "DAS            ", 1
OPC30 db "XOR            ", 0
OPC31 db "XOR            ", 0
OPC32 db "XOR            ", 0
OPC33 db "XOR            ", 0
OPC34 db "XOR            ", 00
OPC35 db "XOR            ", 00
OPC36 db "PREFIKSAS      ", "P"
OPC37 db "AAA            ", 1
OPC38 db "CMP            ", 0
OPC39 db "CMP            ", 0
OPC3A db "CMP            ", 0
OPC3B db "CMP            ", 0
OPC3C db "CMP            ", 00
OPC3D db "CMP            ", 00
OPC3E db "PREFIKSAS      ", "P"
OPC3F db "AAS            ", 1
OPC40 db "INC            ", 0
OPC41 db "INC            ", 0
OPC42 db "INC            ", 0
OPC43 db "INC            ", 0
OPC44 db "INC            ", 0
OPC45 db "INC            ", 0
OPC46 db "INC            ", 0
OPC47 db "INC            ", 0
OPC48 db "DEC            ", 0
OPC49 db "DEC            ", 0
OPC4A db "DEC            ", 0
OPC4B db "DEC            ", 0
OPC4C db "DEC            ", 0
OPC4D db "DEC            ", 0
OPC4E db "DEC            ", 0
OPC4F db "DEC            ", 0
OPC50 db "PUSH           ", 0
OPC51 db "PUSH           ", 0
OPC52 db "PUSH           ", 0
OPC53 db "PUSH           ", 0
OPC54 db "PUSH           ", 0
OPC55 db "PUSH           ", 0
OPC56 db "PUSH           ", 0
OPC57 db "PUSH           ", 0
OPC58 db "POP            ", 0
OPC59 db "POP            ", 0
OPC5A db "POP            ", 0
OPC5B db "POP            ", 0
OPC5C db "POP            ", 0
OPC5D db "POP            ", 0
OPC5E db "POP            ", 0
OPC5F db "POP            ", 0
OPC60 db "UNKNOWN        ", 0
OPC61 db "UNKNOWN        ", 0
OPC62 db "UNKNOWN        ", 0
OPC63 db "UNKNOWN        ", 0
OPC64 db "UNKNOWN        ", 0
OPC65 db "UNKNOWN        ", 0
OPC66 db "UNKNOWN        ", 0
OPC67 db "UNKNOWN        ", 0
OPC68 db "UNKNOWN        ", 0
OPC69 db "UNKNOWN        ", 0
OPC6A db "UNKNOWN        ", 0
OPC6B db "UNKNOWN        ", 0
OPC6C db "UNKNOWN        ", 0
OPC6D db "UNKNOWN        ", 0
OPC6E db "UNKNOWN        ", 0
OPC6F db "UNKNOWN        ", 0
OPC70 db "JO             ", 0
OPC71 db "JNO            ", 0
OPC72 db "JNAE           ", 0
OPC73 db "JAE            ", 0
OPC74 db "JE             ", 0
OPC75 db "JNE            ", 0
OPC76 db "JBE            ", 0
OPC77 db "JA             ", 0
OPC78 db "JS             ", 0
OPC79 db "JNS            ", 0
OPC7A db "JP             ", 0
OPC7B db "JNP            ", 0
OPC7C db "JL             ", 0
OPC7D db "JGE            ", 0
OPC7E db "JLE            ", 0
OPC7F db "JG             ", 0
OPC80 db "TWO BYTES      ", 0
OPC81 db "TWO BYTES      ", 0
OPC82 db "TWO BYTES      ", 0
OPC83 db "TWO BYTES      ", 0
OPC84 db "TEST           ", 0
OPC85 db "TEST           ", 0
OPC86 db "XCHG           ", 0
OPC87 db "XCHG           ", 0
OPC88 db "MOV            ", 0
OPC89 db "MOV            ", 0
OPC8A db "MOV            ", 0
OPC8B db "MOV            ", 0
OPC8C db "MOV            ", 00
OPC8D db "LEA            ", 0
OPC8E db "MOV            ", 00
OPC8F db "POP            ", 0
OPC90 db "NOP            ", 1
OPC91 db "XCHG           ", 0
OPC92 db "XCHG           ", 0
OPC93 db "XCHG           ", 0
OPC94 db "XCHG           ", 0
OPC95 db "XCHG           ", 0
OPC96 db "XCHG           ", 0
OPC97 db "XCHG           ", 0
OPC98 db "CBW            ", 1 
OPC99 db "CWD            ", 1
OPC9A db "CALL           ", 0
OPC9B db "WAIT           ", 1
OPC9C db "PUSHF          ", 1
OPC9D db "POPF           ", 1
OPC9E db "SAHF           ", 1
OPC9F db "LAHF           ", 1
OPCA0 db "MOV            ", 0
OPCA1 db "MOV            ", 0
OPCA2 db "MOV            ", 00
OPCA3 db "MOV            ", 00
OPCA4 db "MOVSB          ", 0
OPCA5 db "MOVSB          ", 0
OPCA6 db "CMPSB          ", 0
OPCA7 db "CMPSB          ", 0
OPCA8 db "TEST           ", 0
OPCA9 db "TEST           ", 0
OPCAA db "STOSB          ", 0
OPCAB db "STOSB          ", 0
OPCAC db "LODSB          ", 0
OPCAD db "LODSB          ", 0
OPCAE db "SCASB          ", 0
OPCAF db "SCASB          ", 0
OPCB0 db "MOV            ", 0
OPCB1 db "MOV            ", 0
OPCB2 db "MOV            ", 0
OPCB3 db "MOV            ", 0
OPCB4 db "MOV            ", 0
OPCB5 db "MOV            ", 0
OPCB6 db "MOV            ", 0
OPCB7 db "MOV            ", 0
OPCB8 db "MOV            ", 0
OPCB9 db "MOV            ", 0
OPCBA db "MOV            ", 0
OPCBB db "MOV            ", 0
OPCBC db "MOV            ", 0
OPCBD db "MOV            ", 0
OPCBE db "MOV            ", 0
OPCBF db "MOV            ", 0
OPCC0 db "UNKNOWN        ", 0
OPCC1 db "UNKNOWN        ", 0
OPCC2 db "RET            ", 0
OPCC3 db "RET            ", 1
OPCC4 db "LES            ", 0
OPCC5 db "LDS            ", 0
OPCC6 db "MOV            ", 0
OPCC7 db "MOV            ", 0
OPCC8 db "UNKNOWN        ", 0
OPCC9 db "UNKNOWN        ", 0
OPCCA db "RETF           ", 0
OPCCB db "RETF           ", 1
OPCCC db "INT            ", "3"
OPCCD db "INT            ", 2
OPCCE db "INTO           ", 1
OPCCF db "IRET           ", 1
OPCD0 db "TWO BYTES      ", 0
OPCD1 db "TWO BYTES      ", 0
OPCD2 db "TWO BYTES      ", 0
OPCD3 db "TWO BYTES      ", 0
OPCD4 db "AAM            ", 0
OPCD5 db "AAD            ", 0
OPCD6 db "UNKNOWN        ", 0
OPCD7 db "XLAT           ", 1
OPCD8 db "ESC            ", 0
OPCD9 db "ESC            ", 0
OPCDA db "ESC            ", 0
OPCDB db "ESC            ", 0
OPCDC db "ESC            ", 0
OPCDD db "ESC            ", 0
OPCDE db "ESC            ", 0
OPCDF db "ESC            ", 0
OPCE0 db "LOOPNE         ", 0
OPCE1 db "LOOPE          ", 0
OPCE2 db "LOOP           ", 0
OPCE3 db "JCXZ           ", 0
OPCE4 db "IN             ", 0
OPCE5 db "IN             ", 0
OPCE6 db "OUT            ", 0
OPCE7 db "OUT            ", 0
OPCE8 db "CALL           ", 0
OPCE9 db "JMP            ", 0
OPCEA db "JMP            ", 0
OPCEB db "JMP            ", 0
OPCEC db "IN             ", 0
OPCED db "IN             ", 0
OPCEE db "OUT            ", 0
OPCEF db "OUT            ", 0
OPCF0 db "LOCK           ", 1
OPCF1 db "UNKNOWN        ", 0
OPCF2 db "REPNZ          ", 1
OPCF3 db "REP            ", 1
OPCF4 db "HLT            ", 1
OPCF5 db "CMC            ", 1
OPCF6 db "TWO BYTES      ", 0
OPCF7 db "TWO BYTES      ", 0
OPCF8 db "CLC            ", 1
OPCF9 db "STC            ", 1
OPCFA db "CLI            ", 1
OPCFB db "STI            ", 1
OPCFC db "CLD            ", 1
OPCFD db "STD            ", 1
OPCFE db "TWO BYTES      ", 0
OPCFF db "TWO BYTES      ", 0
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
	Input_Buffer db Input_Buffer_Length dup (0)
	Data_Output_Buffer db "0000:	              	"														;Holds Address And Operation Code In Hex
	Code_Output_Buffer db Code_Output_Buffer_Length dup (0)													;Holds Operation Code
	Output_Buffer db Output_Buffer_Length dup (0)															;Used To Minimalimaze Writing To File
	Not_Recognised_Buffer db "db 0x  "
	Prefix db "Ds 0"							;1 - Prefix Was Changed	0 - Was Not Changed					;Holds Segment Register 
	Address dw 0FFh								;Address Of First OPC will be 100h
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
	Mov Cx, Ds									;Make Ds And Es Into One Segment
	Mov Es, Cx									;Used In Fill_Code_Output_Buffer
;	Call Open_Output_File																					;Not Being Used But Works
	Call Read_From_Input_File					;Used Once To Fill Buffer									;Also Used In Main_Function
												;It Also Converts Read Bytes To Hex From ASCII
	Jmp Main_Function																						;Needs Remake
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
Open_Output_File:																						;NOT BEING USED	
	Mov Ah, 3Dh									;Open File Using Handle									;NOT BEING USED	
	Mov Al, 1									;Open For Writing Only									;NOT BEING USED	
	Mov Dx, Offset Output_File																			;NOT BEING USED	
	Int 21h																								;NOT BEING USED	
	Jc Jump_To_Error_4																					;NOT BEING USED	
	mov [Output_File_Handle], Ax																		;NOT BEING USED	
	Ret																									;NOT BEING USED	
;**********************************************************************************************			;NOT BEING USED	
Jump_To_Error_4:																						;NOT BEING USED	
	Jmp Error_4																							;NOT BEING USED	
;**********************************************************************************************
Read_From_Input_File:
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Push Dx
	Mov Ah, 3Fh 								;Read From File or Device Using Handle
	Mov Bx, Input_File_Handle					;Input File Handle
	Mov Cx, Input_Buffer_Length					;Input_Buffer_Length Equ 16
	Mov Dx, Offset Input_Buffer					;Read To 
	Int 21h										;Ax Now Holds Amount Of Bytes Read
	Jc Jump_To_Error_5
	Cmp Ax, 0
	Je Jump_To_Close_Input_And_Output_Files
	Mov Cx, Ax									;Cx Used To Show Length Of Buffer Read
	Mov Si, Offset Input_Buffer					;Put Buffer Start To Si
	Pop Dx
	ret
;**********************************************************************************************
Jump_To_Error_5:
	Jmp Error_5
;**********************************************************************************************
Write_To_Output_File:
;Cx MUST Hold Number Of Bytes To Write
	Mov Dx, Offset Output_Buffer
	Mov Ah, 40h
	Mov Bx, Output_File_Handle
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
Read_Byte:										;Reads Byte, Increases Address, Puts Byte To Data_Output_Buffer
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Call Check_Buffer
	Mov Dh, [Ds:Si]								;Put First Byte To Dh
	Inc Si	
	Inc Address
	Call Byte_To_Data_Output_Buffer
	Ret
;**********************************************************************************************
Main_Function:
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Mov Dl, 6									;Address Inside Data_Output_Buffer Of First Byte
	Call Read_Byte								;Reads Byte, Increases Address, Puts Byte To Data_Output_Buffer
	Call Set_Prefix_To_Default					;Sets Prefix To Ds And Changes Prefix To 0 (Was Not Changed)	
	Call Fill_Address_To_Buffer					;Fills Data_Output_Buffer With Address Of Current Operation
	Call Case_Check								;Dx Holds 2 Bytes
	Jmp Main_Function
;**********************************************************************************************
;Checking If We Need To Read From File Again
;**********************************************************************************************
Check_Buffer:
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Mov Di, Offset Input_Buffer
	Add Di, Cx
	Cmp Si, Di
	Jne Return
	Cmp Cx, Input_Buffer_Length
	Jne Jump_To_Close_Input_And_Output_Files
	Call Read_From_Input_File
	Ret
;**********************************************************************************************
;Checking All Possible Cases
;**********************************************************************************************
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
Case_Check:										;Bp - Holds Current Case Number
	Mov Di, Offset OPC00						;Ds = Es So Not Needed To Push/Pop
	Mov Ax, 16									;Each OPC Has 16 Symbols
	Mul Dh
	Add Di, Ax
	Add Di, 15									;Now Bi Points To Case of OPC(Bx)
	Mov Byte Ptr Bp, [Ds:Di]
	Shl Bp, 8
	ShR Bp, 8
	Cmp Bp, '3'
	Je Case_Int_23
	Cmp Bp, 1
	Je Case_1
	Cmp Case, 2
	Je Case_2
	
;**********************************************************************************************
Case_Not_Recognised:
	Push Si
	Push Cx
	Call Convert_ASCII_To_Hex_1					;Brings Back Dh in Ax As ASCII (Al - The Quotient, Ah - Remainer)
	Call Fill_Not_Recognised_Buffer				;Fill Not_Recognised_Buffer With Not Recognised Byte
	MOV Si, Offset Not_Recognised_Buffer
	MOV Cx, 7
	Call Fill_Code_Output_Buffer
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************	
Case_Int_23:	;copied
	Push Si
	Push Cx
	Mov Si, Di
	Sub Si, 15
	Mov Cx, 15
	Call Fill_Code_Output_Buffer
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Mov Byte Ptr [Ds:Di], '3'
	Inc Di
	Inc Cx
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si	
	Ret
;**********************************************************************************************
Case_1:
	Push Si
	Push Cx
	Mov Si, Di
	Sub Si, 15
	Mov Cx, 15
	Call Fill_Code_Output_Buffer
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************
Case_2: 					;Copied
	Push Si
	Push Cx
	Mov Si, Di
	Sub Si, 15
	Mov Cx, 15
	Call Fill_Code_Output_Buffer
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Mov Byte Ptr [Ds:Di], '3'
	Inc Di
	Inc Cx
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si		
	Ret
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
Convert_ASCII_To_Hex_1:							;Converts Dh As ASCII to Ax As HEX (Al - The Quotient, Ah - Remainer)	(Example: Dh = B4 -> AH = 34, Al= 42)
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Xor Ax, Ax
	Mov Al, Dh
	Mov Bl, 10h
	Div Bl
Convert_ASCII_To_Hex_2:
	Mov Bl, Al
	Call Convert_ASCII_To_Hex_3
	Mov Al, Bl
	Mov Bl, Ah
	Call Convert_ASCII_To_Hex_3
	Mov Ah, Bl
	Ret
Convert_ASCII_To_Hex_3:
	Cmp Bl, 10
	Jb Convert_ASCII_To_Hex_4
	Add Bl, 37h
	Ret
Convert_ASCII_To_Hex_4:
	Add Bl, 30h
	Ret
;**********************************************************************************************
Fill_Code_Output_Buffer:						;Si MUST Point To "FROM" Buffer							;Cx MUST Hold How Many Bytes To Copy
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Push Cx
	MOV Di, Offset Code_Output_Buffer
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]
	Pop Cx
	Ret
;**********************************************************************************************
Add_Enter_To_Code_Output_Buffer:
	Mov [Ds:Di], 0A0Dh							;Mov 10, 13 (New Line) (In Memory 13, 10)
	Add Cx, 2
	Ret
;**********************************************************************************************
Fill_Not_Recognised_Buffer:						;Fill Not_Recognised_Buffer With Not Recognised Byte
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	;Not_Recognised_Buffer db "db 0x  "
	Mov Si, Offset Not_Recognised_Buffer
	Add Si, 5
	Mov [DS:Si], Al
	Inc Si
	Mov [DS:Si], Ah
	Ret
;**********************************************************************************************
Fill_Output_Buffer:								;Cx - MUST Hold Code_Output_Buffer Length
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Push Si
	Push Cx
	Mov Si, Offset Data_Output_Buffer
	Mov Cx, 21									;Number Of Bytes In Data_Output_Buffer
	Mov Di, Offset Output_Buffer
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]
	Pop Cx
	Push Cx										;Length Of Code_Output_Buffer
	Mov Si, Offset Code_Output_Buffer
	Rep Movsb
	Pop CX
	Add Cx, 21
	Pop Si
	Ret
;**********************************************************************************************
;**********************************************************************************************
Fill_Address_To_Buffer:							;Moves All 4 Half-Bytes To Dh And Calls Convert_ASCII_To_Hex_1				;UPGRADABLE FUNCTION!
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Push Si
	Push Dx
	Mov Si, Offset Data_Output_Buffer
	Mov Dx, Address
	Call Convert_ASCII_To_Hex_1					;Converts Dh As ASCII to Ax As HEX (Al - The Quotient, Ah - Remainer)	(Example: Dh = B4 -> AH = 34, Al= 42)
	Mov [Ds:Si], Al
	Inc Si
	Mov [Ds:Si], Ah
	Inc Si
	Mov Dh, Dl
	Call Convert_ASCII_To_Hex_1
	Mov [Ds:Si], Al
	Inc Si
	Mov [Ds:Si], Ah
	Pop Dx
	Pop Si
	Ret
;**********************************************************************************************
Byte_To_Data_Output_Buffer:					;Reads Byte, Increases Address, Puts Byte To Data_Output_Buffer
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Call Convert_ASCII_To_Hex_1
	Push Si
	Push Dx
	Mov Si, Offset Data_Output_Buffer
	Xor Dh, Dh
	Add Si, Dx																		;ASK HOW TO ADD SI, DL !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	Mov [Ds:Si], Al
	Inc Si
	Mov [Ds:Si], Ah
	Pop Dx
	Add Di, 2
	Pop Si
	Ret
;**********************************************************************************************
Convert_ASCII_To_Decimal_1:
;Bl - ASCII Symbol '0'-'9' Or 'A'-'F'
	Cmp Bl, 3Ah									;Symbol After '9' (':')
	Jb Convert_ASCII_To_Decimal_2
	Sub Bl, 37h
	Ret
Convert_ASCII_To_Decimal_2:
	Sub Bl, 30h
	Ret
;**********************************************************************************************
Set_Prefix_To_Default:
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
	Push Si
	Mov Si, Offset Prefix
	Mov Byte Ptr[Ds:Si], 'D'
	Add Si, 3
	Mov Byte Ptr[Ds:Si], '0'
	Pop Si
	Ret
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