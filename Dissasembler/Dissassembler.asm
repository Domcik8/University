;2013.12.08															;CORRECT JUMPS + ADDRESS ; correct c/r and case_f
.model small
	Input_Buffer_Length equ 16
	Output_Buffer_Length equ 71										;21 + 50
	Code_Output_Buffer_Length equ 50
.stack 100h
.data
;**********************************************************************************************
;OPC Table (Operation Code Table)
;**********************************************************************************************
OPC00 db "ADD            ", 0Fh
OPC01 db "ADD            ", 0Fh
OPC02 db "ADD            ", 0Fh
OPC03 db "ADD            ", 0Fh
OPC04 db "ADD            ", 8
OPC05 db "ADD            ", 8
OPC06 db "PUSH           ", 4
OPC07 db "POP            ", 4
OPC08 db "OR             ", 0Fh
OPC09 db "OR             ", 0Fh
OPC0A db "OR             ", 0Fh
OPC0B db "OR             ", 0Fh
OPC0C db "OR             ", 8
OPC0D db "OR             ", 8
OPC0E db "PUSH           ", 4
OPC0F db "POP            ", 4
OPC10 db "ADC            ", 0Fh
OPC11 db "ADC            ", 0Fh
OPC12 db "ADC            ", 0Fh
OPC13 db "ADC            ", 0Fh
OPC14 db "ADC            ", 8
OPC15 db "ADC            ", 8
OPC16 db "PUSH           ", 4
OPC17 db "POP            ", 4
OPC18 db "SBB            ", 0Fh
OPC19 db "SBB            ", 0Fh
OPC1A db "SBB            ", 0Fh
OPC1B db "SBB            ", 0Fh
OPC1C db "SBB            ", 8
OPC1D db "SBB            ", 8
OPC1E db "PUSH           ", 4
OPC1F db "POP            ", 4
OPC20 db "AND            ", 0Fh
OPC21 db "AND            ", 0Fh
OPC22 db "AND            ", 0Fh
OPC23 db "AND            ", 0Fh
OPC24 db "AND            ", 8
OPC25 db "AND            ", 8
OPC26 db "PREFIKSAS      ", "P"
OPC27 db "DAA            ", 1
OPC28 db "SUB            ", 0Fh
OPC29 db "SUB            ", 0Fh
OPC2A db "SUB            ", 0Fh
OPC2B db "SUB            ", 0Fh
OPC2C db "SUB            ", 8
OPC2D db "SUB            ", 8
OPC2E db "PREFIKSAS      ", "P"
OPC2F db "DAS            ", 1
OPC30 db "XOR            ", 0Fh
OPC31 db "XOR            ", 0Fh
OPC32 db "XOR            ", 0Fh
OPC33 db "XOR            ", 0Fh
OPC34 db "XOR            ", 8
OPC35 db "XOR            ", 8
OPC36 db "PREFIKSAS      ", "P"
OPC37 db "AAA            ", 1
OPC38 db "CMP            ", 0Fh
OPC39 db "CMP            ", 0Fh
OPC3A db "CMP            ", 0Fh
OPC3B db "CMP            ", 0Fh
OPC3C db "CMP            ", 8
OPC3D db "CMP            ", 8
OPC3E db "PREFIKSAS      ", "P"
OPC3F db "AAS            ", 1
OPC40 db "INC            ", 5
OPC41 db "INC            ", 5
OPC42 db "INC            ", 5
OPC43 db "INC            ", 5
OPC44 db "INC            ", 5
OPC45 db "INC            ", 5
OPC46 db "INC            ", 5
OPC47 db "INC            ", 5
OPC48 db "DEC            ", 5
OPC49 db "DEC            ", 5
OPC4A db "DEC            ", 5
OPC4B db "DEC            ", 5
OPC4C db "DEC            ", 5
OPC4D db "DEC            ", 5
OPC4E db "DEC            ", 5
OPC4F db "DEC            ", 5
OPC50 db "PUSH           ", 5
OPC51 db "PUSH           ", 5
OPC52 db "PUSH           ", 5
OPC53 db "PUSH           ", 5
OPC54 db "PUSH           ", 5
OPC55 db "PUSH           ", 5
OPC56 db "PUSH           ", 5
OPC57 db "PUSH           ", 5
OPC58 db "POP            ", 5
OPC59 db "POP            ", 5
OPC5A db "POP            ", 5
OPC5B db "POP            ", 5
OPC5C db "POP            ", 5
OPC5D db "POP            ", 5
OPC5E db "POP            ", 5
OPC5F db "POP            ", 5
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
OPC70 db "JO             ", 0Ch
OPC71 db "JNO            ", 0Ch
OPC72 db "JNAE           ", 0Ch
OPC73 db "JAE            ", 0Ch
OPC74 db "JE             ", 0Ch
OPC75 db "JNE            ", 0Ch
OPC76 db "JBE            ", 0Ch
OPC77 db "JA             ", 0Ch
OPC78 db "JS             ", 0Ch
OPC79 db "JNS            ", 0Ch
OPC7A db "JP             ", 0Ch
OPC7B db "JNP            ", 0Ch
OPC7C db "JL             ", 0Ch
OPC7D db "JGE            ", 0Ch
OPC7E db "JLE            ", 0Ch
OPC7F db "JG             ", 0Ch
OPC80 db "TWO BYTES      ", 0
OPC81 db "TWO BYTES      ", 0
OPC82 db "TWO BYTES      ", 0
OPC83 db "TWO BYTES      ", 0
OPC84 db "TEST           ", 0
OPC85 db "TEST           ", 0
OPC86 db "XCHG           ", 0
OPC87 db "XCHG           ", 0
OPC88 db "MOV            ", 0Fh
OPC89 db "MOV            ", 0Fh
OPC8A db "MOV            ", 0Fh
OPC8B db "MOV            ", 0Fh
OPC8C db "MOV            ", 00
OPC8D db "LEA            ", 0
OPC8E db "MOV            ", 00
OPC8F db "POP            ", 0
OPC90 db "NOP            ", 1
OPC91 db "XCHG           ", 6
OPC92 db "XCHG           ", 6
OPC93 db "XCHG           ", 6
OPC94 db "XCHG           ", 6
OPC95 db "XCHG           ", 6
OPC96 db "XCHG           ", 6
OPC97 db "XCHG           ", 6
OPC98 db "CBW            ", 1 
OPC99 db "CWD            ", 1
OPC9A db "CALL           ", 0Eh
OPC9B db "WAIT           ", 1
OPC9C db "PUSHF          ", 1
OPC9D db "POPF           ", 1
OPC9E db "SAHF           ", 1
OPC9F db "LAHF           ", 1
OPCA0 db "MOV            ", 0Bh
OPCA1 db "MOV            ", 0Bh
OPCA2 db "MOV            ", 0Bh
OPCA3 db "MOV            ", 0Bh
OPCA4 db "MOVSB          ", 1
OPCA5 db "MOVSB          ", 1
OPCA6 db "CMPSB          ", 1
OPCA7 db "CMPSB          ", 1
OPCA8 db "TEST           ", 8
OPCA9 db "TEST           ", 8
OPCAA db "STOSB          ", 1
OPCAB db "STOSB          ", 1
OPCAC db "LODSB          ", 1
OPCAD db "LODSB          ", 1
OPCAE db "SCASB          ", 1
OPCAF db "SCASB          ", 1
OPCB0 db "MOV            ", 0Dh
OPCB1 db "MOV            ", 0Dh
OPCB2 db "MOV            ", 0Dh
OPCB3 db "MOV            ", 0Dh
OPCB4 db "MOV            ", 0Dh
OPCB5 db "MOV            ", 0Dh
OPCB6 db "MOV            ", 0Dh
OPCB7 db "MOV            ", 0Dh
OPCB8 db "MOV            ", 0Dh
OPCB9 db "MOV            ", 0Dh
OPCBA db "MOV            ", 0Dh
OPCBB db "MOV            ", 0Dh
OPCBC db "MOV            ", 0Dh
OPCBD db "MOV            ", 0Dh
OPCBE db "MOV            ", 0Dh
OPCBF db "MOV            ", 0Dh
OPCC0 db "UNKNOWN        ", 0
OPCC1 db "UNKNOWN        ", 0
OPCC2 db "RET            ", 7
OPCC3 db "RET            ", 1
OPCC4 db "LES            ", 0
OPCC5 db "LDS            ", 0
OPCC6 db "MOV            ", 0
OPCC7 db "MOV            ", 0
OPCC8 db "UNKNOWN        ", 0
OPCC9 db "UNKNOWN        ", 0
OPCCA db "RETF           ", 7
OPCCB db "RETF           ", 1
OPCCC db "INT            ", "3"
OPCCD db "INT            ", 2
OPCCE db "INTO           ", 1
OPCCF db "IRET           ", 1
OPCD0 db "TWO BYTES      ", 0
OPCD1 db "TWO BYTES      ", 0
OPCD2 db "TWO BYTES      ", 0
OPCD3 db "TWO BYTES      ", 0
OPCD4 db "AAM            ", 3
OPCD5 db "AAD            ", 3
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
OPCE0 db "LOOPNE         ", 0Ch
OPCE1 db "LOOPE          ", 0Ch
OPCE2 db "LOOP           ", 0Ch
OPCE3 db "JCXZ           ", 0Ch
OPCE4 db "IN             ", 9
OPCE5 db "IN             ", 9
OPCE6 db "OUT            ", 9
OPCE7 db "OUT            ", 9
OPCE8 db "CALL           ", 7
OPCE9 db "JMP            ", 7
OPCEA db "JMP            ", 0Eh
OPCEB db "JMP            ", 0Ch
OPCEC db "IN             ", 0Ah
OPCED db "IN             ", 0Ah
OPCEE db "OUT            ", 0Ah
OPCEF db "OUT            ", 0Ah
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
	Sr db "XS"																								;Holds Segment Register
	Prefix db "Ds 0"							;1 - Prefix Was Changed	0 - Was Not Changed					;Holds Segment Register As Prefix
	Address dw 0FFh								;Address Of First OPC will be 100h
	W db 0										;1 - Word Operation 0 - Byte Operation
	D db 0										;1 - Reg <- R/M 0 - R/M <- Reg
	Mod_ db 0																								;Hold Mod
	Reg db "XX"																								;Holds Register
	RM db "XX+XX+    " 																						;Holds Register Or EA (Effective Address)
	Offset_16Bit db "XX"																					;Holds Offset's First 8 Bits
	Extra_W db 0																							;Used In Case_F
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
Return_And_Pop_Di:
	Pop Di
	Ret
;**********************************************************************************************
Open_Output_File:																						;NOT BEING USED	
	Mov Al, 1									;Open For Writing Only									;NOT BEING USED	
	Mov Dx, Offset Output_File																			;NOT BEING USED	
	Int 21h																								;NOT BEING USED	
	Mov Ah, 3Dh									;Open File Using Handle									;NOT BEING USED	
	Jc Jump_To_Error_4																					;NOT BEING USED	
	mov [Output_File_Handle], Ax																		;NOT BEING USED	
	Ret																									;NOT BEING USED	
;**********************************************************************************************			;NOT BEING USED	
Jump_To_Error_4:																						;NOT BEING USED	
	Jmp Error_4																							;NOT BEING USED	
;**********************************************************************************************
Read_From_Input_File:
	Push Dx
	Push Bx
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
	Pop Bx
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
	Call Clean_Data_Output_Buffer_1				;Cleans Data_Output_Buffer From Old Bytes
	Call Read_Byte								;Reads Byte, Increases Address, Puts Byte To Data_Output_Buffer
	Call Set_Prefix_To_Default					;Sets Prefix To Ds And Changes Prefix To 0 (Was Not Changed)
	Call Set_W_To_Default						;Sets W To 0 
	Call Fill_Address_To_Buffer					;Fills Data_Output_Buffer With Address Of Current Operation
	Call Case_Check_1							;Dx Holds 2 Bytes
	Jmp Main_Function
;**********************************************************************************************
;Checking If We Need To Read From File Again
;**********************************************************************************************
Check_Buffer:
	Push Di
	Mov Di, Offset Input_Buffer
	Add Di, Cx
	Cmp Si, Di
	Jne Return_And_Pop_Di
	Cmp Cx, Input_Buffer_Length
	Jne Jump_To_Close_Input_And_Output_Files
	Call Read_From_Input_File
	Pop Di
	Ret
;**********************************************************************************************
;Checking All Possible Cases
;**********************************************************************************************
Case_Check_1:									;Bp - Holds Current Case Number
	Mov Di, Offset OPC00						;Ds = Es So Not Needed To Push/Pop
	Mov Ax, 16									;Each OPC Has 16 Symbols
	Mul Dh
	Add Di, Ax
	Add Di, 15									;Now Bi Points To Case of OPC(Bx)
	Mov Byte Ptr Bp, [Ds:Di]
	Shl Bp, 8
	ShR Bp, 8
	Cmp Bp, '3'
	Je Case_Int_3
	Cmp Bp, 1
	Je Case_1
	Cmp Bp, 2
	Je Case_2
	Jmp Case_Check_2
;**********************************************************************************************	
Case_Int_3:										;1100 1100
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
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
Case_1:											;XXXX XXXX													;For Example INTO
	Push Si										;1010 XXXW													;For Example MOVSB
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************
Case_2: 										;1100 1101 Number(0-FF)										;Interrupts	
	Call Read_Byte
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Convert_ASCII_To_Hex_1 
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Mov [Ds:Di], Ax
	Add Di, 2
	Mov Byte Ptr [Ds:Di], 'h'
	Inc Di
	Add Cx, 3
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si		
	Ret
;**********************************************************************************************
Case_Check_2:
	Cmp Bp, 3
	Je Case_3
	Cmp Bp, 4
	Je Case_4
	Cmp Bp, 5
	Je Case_5
	Jmp Case_Check_3
;**********************************************************************************************
Case_3:											;XXXX XXXX 0000 1010										;AAM / AAD								
;Second Byte Has To Be 0Ah ASK!!!!!!!!!@!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	Push Cx
	Call Read_Byte
	Push Si
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Si
	Pop Cx
	Ret
;**********************************************************************************************
Case_4:											;XXXS RXXX													;Push / Pop
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Check_Sr_1
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Push Cx
	Mov Si, Offset Sr
	Mov Cx, 2
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]	
	Pop Cx
	Add Cx, 2
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si	
	Ret
;**********************************************************************************************
Case_5:											;010X XREG													;INC / DEC / PUSH / POP
	Push Cx
	Push Si
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Push Dx
	Shl Dh, 5
	Shr Dh, 5
	Push Di
	Mov Di, Offset W
	Mov Byte Ptr [Ds:Di], 1 
	Pop Di
	Call Check_Reg_1
	Pop Dx
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Push Cx
	Mov Si, Offset Reg
	Mov Cx, 2
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]	
	Pop Cx
	Add Cx, 2
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Si
	Pop Cx
	Ret
;**********************************************************************************************
Case_Check_3:
	Cmp Bp, 6
	Je Case_6
	Cmp Bp, 7
	Je Case_7
	Jmp Case_Check_4
;**********************************************************************************************	
Case_6:											;1001 0REG														;XCHG
	Push Cx
	Push Si
	Mov Si, Offset W
	Mov Byte Ptr [Ds:Si], 1
	Pop Si
	Push Si
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Push Dx
	Shl Dh, 5
	Shr Dh, 5
	Call Check_Reg_1
	Pop Dx
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Push Cx
	Mov Si, Offset Reg
	Mov Cx, 2
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]	
	Pop Cx
	Add Cx, 2
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Mov [Ds:Di], 'XA'
	Add Di, 2
	Add Cx, 4
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Si
	Pop Cx
	Ret
;**********************************************************************************************
Case_7:                  						;1100 X010 ILB IHB	(Immediate Low Byte / Hight Byte)			;RET / RETF
	Push Si										;1110 100X SLB SHB  (Offset Low Byte / High Byte)				;CALL / Jmp (Short Direct)
	Push Cx
	Mov Si, Offset W
	Mov Byte Ptr [Ds:Si], 1
	Pop Cx
	Pop Si
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Immediate_Byte_1
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************
Case_Check_4:
	Cmp Bp, 8
	Je Case_8
	Cmp Bp, 9
	Je Case_9
	Jmp Case_Check_5
;**********************************************************************************************	
Case_8:											;XXXX XXXW ILB [IHB] 											;For Example Test
	Push Si
	Push Cx
	Call Check_W_1
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Mov [Ds:Di], 'XA'
	Add Di, 2
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Add Cx, 4
	Call Immediate_Byte_1
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************
Case_9:											;1110 01XW Port 												;In / Out (Port)
	Call Read_Byte
	Push Si
	Push Cx
	Call Check_W_1
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Convert_ASCII_To_Hex_1 
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Cmp W, 1
	Je Case_9_W_1
Case_9_W_0:
	Mov [Ds:Di], 'lA'
	jmp Case_9_2
Case_9_W_1:
	Mov [Ds:Di], 'XA'
Case_9_2:
	Add Di, 2
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Mov [Ds:Di], Ax
	Add Di, 2
	Add Cx, 6
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si		
	Ret
;**********************************************************************************************
Case_Check_5:
	Cmp Bp, 0Ah
	Je Case_A
	Cmp Bp, 0Bh
	Je Case_B
	Jmp Case_Check_6
;**********************************************************************************************	
Case_A:											;1110 11XW														;In / Out (Dx Port)
	Push Si
	Push Cx
	Call Check_W_1
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Cmp W, 1
	Je Case_A_W_1
Case_A_W_0:
	Mov [Ds:Di], 'lA'
	jmp Case_A_2
Case_A_W_1:
	Mov [Ds:Di], 'XA'
Case_A_2:
	Add Di, 2
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Mov [Ds:Di], 'xD'
	Add Di, 2
	Add Cx, 6
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si		
	Ret
;**********************************************************************************************
Case_B:											;1010 00XW ALB AHB (Address Low Byte / High Byte)				;Mov Ax <-> Memory
	Push Si
	Push Cx
	Call Check_W_1
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Push Dx
	Shr Dx, 2
	Mov Dh, 0
	SHL DX, 1
	Cmp Dh, 1
	Je Case_To_Mem
Case_From_Mem:
	Pop Dx
	Call Case_B_2
	Call Case_B_4
	Push Di
	Mov Di, Offset W
	Mov Byte Ptr [Ds:Di], 1
	Pop Di
	Mov Byte Ptr [Ds:Di], '['
	Inc Di
	Call Immediate_Byte_1
	Mov Byte Ptr [Ds:Di], ']'
	Inc Di
	Add Cx, 2
	Call Check_W_1
	Jmp Case_B_5
Case_To_Mem:
	Pop Dx
	Push Di
	Mov Di, Offset W
	Mov Byte Ptr [Ds:Di], 1
	Pop Di
	Mov Byte Ptr [Ds:Di], '['
	Inc Di
	Call Immediate_Byte_1
	Mov Byte Ptr [Ds:Di], ']'
	Inc Di
	Add Cx, 2
	Call Check_W_1
	Call Case_B_4
	Call Case_B_2
	Jmp Case_B_5
Case_B_2:
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Cmp W, 1
	Je Case_B_W_1
Case_B_W_0:
	Mov [Ds:Di], 'lA'
	Jmp Case_B_3
Case_B_W_1:
	Mov [Ds:Di], 'XA'
Case_B_3:
	Add Di, 2
	Add Cx, 2
	Ret
Case_B_4:
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Add Cx, 2
	Ret
Case_B_5:
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;*********************************************************************************************
Case_Check_6:
	Cmp Bp, 0Ch
	Je Case_C
	Cmp Bp, 0Dh
	Je Case_D
	Jmp Case_Check_7
;**********************************************************************************************	
Case_C:
	Call Read_Byte								;0111 XXXX Offset (0-FF)										;J... (For Example Ja)
	Push Si
	Push Cx
	Call Check_Offset_16Bit
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Convert_ASCII_To_Hex_1 
	Push Si
	Mov Si, Offset Offset_16Bit
	Mov Di, Offset Code_Output_Buffer
	Add Di, Cx
	Push Cx
	Push Si
	Push Di
	Mov Cx, 2
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]	
	Pop Di
	Pop Si
	Pop Cx
	Add Di, 2
	Mov [Ds:Di], Ax
	Add Di, 2
	Add Cx, 4
	Pop Si
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si		
	Ret
;**********************************************************************************************
Case_D:											;1011 WReg bojb [bovb]											;MOV registras <- Immediate Operand
	Push Si
	Push Cx
	Push Dx
	Shr Dx, 3
	Call Check_W_1
	Pop Dx
	Push Dx
	Shr Dx, 3
	Mov Dh, 0
	Shl Dx, 3
	Call Check_Reg_1
	Pop Dx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Push Cx
	Push Si
	Push Di
	Mov Cx, 2
	Mov Si, Offset Reg
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]
	Pop Di
	Pop Si
	Pop Cx
	Add Di, 2
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Add Cx, 4
	Call Immediate_Byte_1
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************	
Case_Check_7:
	Cmp Bp, 0Eh
	Je Case_E
	Cmp Bp, 0Fh
	Je Case_F	
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
Case_E: 										;1XXX 1010 ALB AHB SRLB SRHB 									;Jmp / Call (Long Direct)
	Push Si										;(Immediate Operand Pointing To SR Low Byte / High Byte)
	Push Cx
	Mov Si, Offset W
	Mov Byte Ptr [Ds:Si], 1
	Pop Cx
	Pop Si
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Call Immediate_Byte_1
	Call Immediate_Byte_1
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si
	Ret
;**********************************************************************************************	
Case_F:											;X0XX X0DW Mod Reg R/M [Offset]									;For Example Add Reg <-> Memory
;Cx - Input_Buffer Length 						;Comes From Read_From_Input_File
;Dl - Current Byte In Data_Output_Buffer		;Comes From Byte_To_Data_Output_Buffer
;Dh - First Byte Read							;Read In Main_Function			
;Si - Points To Input_Buffers Current Byte 		;Comes From Read_From_Input_File
;Di - Points To Case Number						;Comse From Case_Check_1
	Mov Bl, W
	Mov Extra_W, Bl
	Mov Bx, 0
	Call Check_D_1
	Call Check_W_1
	Call Read_Byte
	Call Check_Mod
	Push Dx
	Shr Dx, 6
	Mov Dh, 0
	Shl Dx, 3
	Call Check_Reg_1
	Pop Dx
	Call Check_Rm_1
	Push Si
	Push Cx
	Call Set_Si_To_Code_Name
	Call Fill_Code_Output_Buffer
	Cmp D, 1
	Je Case_F_D_1_1
;***************************************
Case_F_D_0_1:
	Mov Bp, 0 
	Call Case_F_Fill_Reg
	Call Case_F_Fill_Comma
	Cmp Mod_, 3
	Jne Case_F_Mod_Not_11_1	
Case_F_D_0_2:	
	Call Case_F_Fill_Mem_Or_Reg_1
	Cmp Mod_, 3
	Jne Case_F_Mod_Not_11_2
Case_F_D_0_3:
	Jmp Case_F_2
;***************************************	
Case_F_D_1_1:
	Mov Bp, 1
	Cmp Mod_, 3
	Jne Case_F_Mod_Not_11_1
Case_F_D_1_2:
	Call Case_F_Fill_Mem_Or_Reg_1
	Cmp Mod_, 3
	Jne Case_F_Mod_Not_11_2
Case_F_D_1_3:	
	Call Case_F_Fill_Comma	
	Call Case_F_Fill_Reg
	Jmp Case_F_2
;*************************************** 
Case_F_Mod_Not_11_1:
	Mov Byte Ptr [Ds:Di], '['
	Inc Di
	Inc Cx
	Cmp Bp, 0
	Je Case_F_D_0_2
	Jmp Case_F_D_1_2
;***************************************
Case_F_Mod_Not_11_2:
	Mov Byte Ptr [Ds:Di], ']'
	Inc Di
	Inc Cx
	Cmp Bp, 0
	Je Case_F_D_0_3
	Jmp Case_F_D_1_3
;***************************************
Case_F_Fill_Reg:
	Push Di
	Push Si
	Push Cx
	Mov Si, Offset Reg
	Mov Cx, 2
	Rep Movsb
	Pop Cx
	Pop Si
	Pop Di
	Add Di, 2
	Add Cx, 2	
	Ret
;***************************************
Case_F_Fill_Comma:
	Mov [Ds:Di], ' ,'
	Add Di, 2
	Add Cx, 2
	Ret
;***************************************
Case_F_Fill_Mem_Or_Reg_1:
	Cmp Bh, 1
	Je Mod_00_Rm_110
Case_F_Fill_Mem_Or_Reg_2:	
	Mov Bl, Bh
	Mov Bh, 0
	Push Si
	Push Cx
	Mov Si, Offset Rm
	Mov Cx, Bx
	Rep Movsb
	Pop Cx
	Pop Si
	Add Si, Bx
	Add Cx, Bx	
	Ret	
;*************************************** ;CHECK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
Mod_00_Rm_110:
	Push SI
	Mov Si, Offset W
	Mov Byte Ptr[Ds:Si], 1
	Pop Si
	Pop Dx
	;Mov Helper, Dx
	Call Immediate_Byte_1
	Mov Bx, Cx
	Pop Si
	Pop Cx
	Pop Dx
	Call Check_W_1
	Push Si
	Push Cx
	Mov Cx, Bx
	;Push Helper
	Ret
Case_F_2:
	Call Add_Enter_To_Code_Output_Buffer
	Call Fill_Output_Buffer
	Call Write_To_Output_File
	Pop Cx
	Pop Si	
	Ret
;**********************************************************************************************	
;**********************************************************************************************	
;**********************************************************************************************
Check_Offset_16Bit:								;Checks Dh And Adds '00' / 'FF' To Offset_16Bit
	Push Dx
	Push Di
	Shr Dh, 7
	Mov Di, Offset Offset_16Bit
	Cmp Dh, 1
	Je Offset_FFXX
Offset_00XX:
	Mov [Ds:Di], '00'
	Jmp Check_Offset_16Bit_2
Offset_FFXX:
	Mov [Ds:Di], 'FF'
Check_Offset_16Bit_2:
	Pop Di
	Pop Dx
	Ret
;**********************************************************************************************
Immediate_Byte_1:								;Fills Code_Output_Buffer With Immediate_Byte
;Di - Points To First Space After Code_Output_Buffer
;Cx - Holds Lenght Of  Code_Output_Buffer
;MUST Hold CX, Si From Main In Stack !!!
	Pop Ax
	Mov Bx, Cx
	Pop Cx
	Pop Si
	Push Ax
	Push Bx
	Call Read_Byte
	Call Convert_ASCII_To_Hex_1
	Mov Bp, Ax
	Cmp W, 1
	Je Immmediate_Byte_2
	Pop Bx
	Pop Ax
	Push Si
	Push Cx
	Mov Cx, Bx
	Mov [Ds:Di], Bp
	Add Di, 2
	Add Cx, 2
	Jmp Immmediate_Byte_3
Immmediate_Byte_2:	
	Call Read_Byte
	Call Convert_ASCII_To_Hex_1
	Mov [Ds:Di], Ax
	Add Di, 2
	Mov [Ds:Di], Bp
	Add Di, 2
	Pop Bx
	Pop Ax
	Push Si
	Push Cx
	Mov Cx, Bx
	Add Cx, 4	
Immmediate_Byte_3:
	Push Ax
	Ret
;**********************************************************************************************
;**********************************************************************************************
;**********************************************************************************************
Check_D_1:
	Push Dx
	Push Si
	Shr Dx, 2
	Mov Dh, 0
	Shl Dx, 1
	Mov Si, Offset D
	Cmp Dh, 1
	Je D_1
D_0:
	Mov Byte Ptr [Ds:Si], 0
	Jmp Check_D_2
D_1:
	Mov Byte Ptr [Ds:Si], 1
Check_D_2:
	Pop Si
	Pop Dx
	Ret
;**********************************************************************************************
Check_W_1:
	Push Dx
	Push Si
	Shr Dx, 1
	Mov Dh, 0
	Shl Dx, 1
	Mov Si, Offset W
	Cmp Dh, 1
	Je W_1
W_0:
	Mov Byte Ptr [Ds:Si], 0
	Jmp Check_W_2
W_1:
	Mov Byte Ptr [Ds:Si], 1
Check_W_2:
	Pop Si
	Pop Dx
	Ret
;**********************************************************************************************
;**********************************************************************************************
Check_Mod:
	Push Dx
	Push Di
	Shr Dh, 6
	Mov Di, Offset Mod_
	Mov [Ds:Di], Dh
	Pop Di
	Pop Dx
	Ret
;**********************************************************************************************
Check_Reg_1:									;Dh MUST Hold Reg (0000 0REG)									;UPGRADABLE FUNCTION! (A lot Shorter With DS Strings)
	Push Si
	Mov Dl, 0									;0 - Dh Was Not Subbed; 1 - Dh Was Subbed
Check_Reg_2_Reg:
	Cmp Dh, 0
	Je Register_Ax_Reg
	Cmp Dh, 1
	Je Register_Cx_Reg
	Cmp Dh, 2
	Je Register_Dx_Reg
	Cmp Dh, 3
	Je Register_Bx_Reg
	Sub Dh, 4
	Mov Dl, 1
	Jmp Check_Reg_2_Reg
;***************************************
Register_Ax_Reg:
	Mov Si, Offset Reg
	Mov Byte Ptr [Ds:Si], 'A'
	Call Cmp_W_Reg
	Jmp Check_Reg_One_Byte_Reg
Register_Cx_Reg:
	Mov Si, Offset Reg
	Mov Byte Ptr [Ds:Si], 'C'
	Call Cmp_W_Reg
	Jmp Check_Reg_One_Byte_Reg
Register_Dx_Reg:
	Mov Si, Offset Reg
	Mov Byte Ptr [Ds:Si], 'D'
	Call Cmp_W_Reg
	Jmp Check_Reg_One_Byte_Reg
Register_Bx_Reg:
	Mov Si, Offset Reg
	Mov Byte Ptr [Ds:Si], 'B'
	Call Cmp_W_Reg
	Jmp Check_Reg_One_Byte_Reg
;***************************************
Cmp_W_Reg:
	Inc Si
	Cmp W, 1
	Je Check_Reg_Two_Byte_Reg
	Ret
;***************************************
Check_Reg_One_Byte_Reg:
	Cmp Dl, 1
	Je Register_Xh_Reg
;***************************************
Register_Xl_Reg:
	Mov Byte Ptr[Ds:Si], 'l' 
	Jmp Check_Reg_4_Reg
Register_Xh_Reg:
	Mov Byte Ptr [Ds:Si], 'h'
	Jmp Check_Reg_4_Reg
;***************************************
Check_Reg_Two_Byte_Reg:
	Add Sp, 2
	Cmp Dl, 1
	Je Check_Reg_3_Reg
	Mov Byte Ptr [Ds:Si], 'X'
	Jmp Check_Reg_4_Reg
;***************************************
Check_Reg_3_Reg:
	Cmp Dh, 0
	Je Register_Sp_Reg
	Cmp Dh, 1
	Je Register_Bp_Reg
	Cmp Dh, 2
	Je Register_Si_Reg
	Cmp Dh, 3
	Je Register_Di_Reg
;***************************************
Register_Sp_Reg:
	Mov Si, Offset Reg
	Mov [Ds:Si], 'pS'
	Jmp Check_Reg_4_Reg
Register_Bp_Reg:
	Mov Si, Offset Reg
	Mov [Ds:Si], 'pB'
	Jmp Check_Reg_4_Reg
Register_Si_Reg:
	Mov Si, Offset Reg
	Mov [Ds:Si], 'iS'
	Jmp Check_Reg_4_Reg
Register_Di_Reg:
	Mov Si, Offset Reg
	Mov [Ds:Si], 'iD'
	Jmp Check_Reg_4_Reg
;***************************************
Check_Reg_4_Reg:
	Pop Si
	Ret
;**********************************************************************************************
Check_RM_1:									;Reads R/m From Dh And Puts It To Rm
;Bh Holds Lenght Of Rm, If dh = 1 then it needs Immediate Address
	Push Dx;
	Push Si;
	Shr Dx, 3
	Mov Dh, 0
	Shl Dx, 3
	Mov Si, Offset Mod_
	Mov Bl, [Ds:Si]
	Cmp Bl, 0
	Je Jump_To_Mod_0
	Cmp Bl, 3
	Je Jump_To_Mod_11_1
;***************************************
Mod_01_10:
	Cmp Dh, 1
	Je Rm_001_2
	Cmp Dh, 2
	Je Rm_010_2
	Cmp Dh, 3
	Je Rm_011_2
	Cmp Dh, 4
	Je Rm_100_2
	Cmp Dh, 5
	Je Rm_101_2
	Cmp Dh, 6
	Je Rm_110_2
	Cmp Dh, 7
	Je Rm_111_2	
Rm_000_2:
	Call Memory_Bx
	Inc Si
	Call Memory_Si
	Inc Si
	Mov Bh, 6
	Jmp Fill_Offset_16Bit
Rm_001_2:
	Call Memory_Bx
	Inc Si
	Call Memory_Di
	Inc Si
	Mov Bh, 6
	Jmp Fill_Offset_16Bit
Rm_010_2:
	Call Memory_Bp
	Call Memory_Si
	Inc Si
	Mov Bh, 6
	Jmp Fill_Offset_16Bit
Rm_011_2:
	Call Memory_Bp
	Call Memory_Di
	Inc Si
	Mov Bh, 6
	Jmp Fill_Offset_16Bit
Jump_To_Mod_0:
	Jmp Mod_0
;***************************************
Jump_To_Mod_11_1:
	Jmp Mod_11_1
;***************************************
Rm_100_2:
	Mov Si, Offset Rm
	Call Memory_Si
	Mov Bh, 2
	Jmp Fill_Offset_16Bit
Rm_101_2:
	Mov Si, Offset Rm
	Call Memory_Di
	Mov Bh, 2
	Jmp Fill_Offset_16Bit
Rm_110_2:
	Call Memory_Bp
	Mov Bh, 3
	Jmp Fill_Offset_16Bit
Rm_111_2:
	Call Memory_BX
	Inc Si
	Mov Bh, 3
	Jmp Fill_Offset_16Bit
;***************************************
Fill_Offset_16Bit:
	Pop Si
	Call Read_Byte
	Call Convert_ASCII_To_Hex_1
	Push Si
	Call Check_Offset_16Bit
	Push Di
	Mov Di, Si
	Mov Si, Offset Offset_16Bit
	Push Cx
	Push Di
	Mov Cx, 2
	Rep Movsb									;Copies Cx Bytes From [Ds:Si] To [Es:Di]	
	Pop Di
	Pop Cx
	Mov Si, Di
	Add Si, 2
	Mov [Ds:Si], Ax
	Add Bh, 4
	Pop Di
	Jmp Check_RM_2
;***************************************
Mod_0:
	Cmp Dh, 1
	Je Rm_001_1
	Cmp Dh, 2
	Je Rm_010_1
	Cmp Dh, 3
	Je Rm_011_1
	Cmp Dh, 4
	Je Rm_100_1
	Cmp Dh, 5
	Je Rm_101_1
	Cmp Dh, 6
	Je Rm_110_1
	Cmp Dh, 7
	Je Rm_111_1
Rm_000_1:
	Call Memory_Bx
	Inc Si
	Call Memory_Si
	Mov Bh, 5
	Jmp Check_RM_2
Rm_001_1:
	Call Memory_Bx
	Inc Si
	Call Memory_Di
	Mov Bh, 5
	Jmp Check_RM_2
Rm_010_1:
	Call Memory_Bp
	Call Memory_Si
	Mov Bh, 5
	Jmp Check_RM_2
Rm_011_1:
	Call Memory_Bp
	Call Memory_Di
	Mov Bh, 5
	Jmp Check_RM_2
Rm_100_1:
	Mov Si, Offset Rm
	Call Memory_Si
	Mov Bh, 2
	Jmp Check_RM_2
Rm_101_1:
	Mov Si, Offset Rm
	Call Memory_Di
	Mov Bh, 2
	Jmp Check_RM_2
Rm_110_1:
	Mov Dh, 1	
	Jmp Check_RM_2
Rm_111_1:
	Call Memory_BX
	Mov Bh, 2
	Jmp Check_RM_2
Memory_Bx:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'xB'
	Add Si, 2
	Ret
Memory_Bp:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'pB'
	Add Si, 3
	Ret
Memory_Si:
	Mov [Ds:Si], 'iS'
	Add Si, 2
	Ret	
Memory_Di:
	Mov [Ds:Si], 'iD'
	Add Si, 2
	Ret
;***************************************
Mod_11_1:
	Mov Dl, 0									;0 - Dh Was Not Subbed; 1 - Dh Was Subbed	
Mod_11_2:																										;UPGRADABLE FUNCTION! (A lot Shorter With DS Strings)
	Cmp Dh, 0
	Je Register_Ax_Rm
	Cmp Dh, 1
	Je Register_Cx_Rm
	Cmp Dh, 2
	Je Register_Dx_Rm
	Cmp Dh, 3
	Je Register_Bx_Rm
	Sub Dh, 4
	Mov Dl, 1
	Jmp Mod_11_2
Register_Ax_Rm:
	Mov Si, Offset Rm
	Mov Byte Ptr [Ds:Si], 'A'
	Call Cmp_W_Rm
	Jmp Check_Reg_One_Byte_Rm
Register_Cx_Rm:
	Mov Si, Offset Rm
	Mov Byte Ptr [Ds:Si], 'C'
	Call Cmp_W_Rm
	Jmp Check_Reg_One_Byte_Rm
Register_Dx_Rm:
	Mov Si, Offset Rm
	Mov Byte Ptr [Ds:Si], 'D'
	Call Cmp_W_Rm
	Jmp Check_Reg_One_Byte_Rm
Register_Bx_Rm:
	Mov Si, Offset Rm
	Mov Byte Ptr [Ds:Si], 'B'
	Call Cmp_W_Rm
	Jmp Check_Reg_One_Byte_Rm
Cmp_W_Rm:
	Inc Si
	Cmp W, 1
	Je Check_Reg_Two_Byte_Rm
	Ret
Check_Reg_One_Byte_Rm:
	Cmp Dl, 1
	Je Register_Xh_Rm
Register_Xl_Rm:
	Mov Byte Ptr[Ds:Si], 'l' 
	Jmp Inc_Bh
Register_Xh_Rm:
	Mov Byte Ptr [Ds:Si], 'h'
	Jmp Inc_Bh
Check_Reg_Two_Byte_Rm:
	Add Sp, 2
	Cmp Dl, 1
	Je Check_Reg_3_Rm
	Mov Byte Ptr [Ds:Si], 'X'
	Jmp Inc_Bh
Check_Reg_3_Rm:
	Cmp Dh, 0
	Je Register_Sp_Rm
	Cmp Dh, 1
	Je Register_Bp_Rm
	Cmp Dh, 2
	Je Register_Si_Rm
	Cmp Dh, 3
	Je Register_Di_Rm
Register_Sp_Rm:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'pS'
	Jmp Inc_Bh
Register_Bp_Rm:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'pB'
	Jmp Inc_Bh
Register_Si_Rm:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'iS'
	Jmp Inc_Bh
Register_Di_Rm:
	Mov Si, Offset Rm
	Mov [Ds:Si], 'iD'
Inc_Bh:
	Mov Bh, 2
Check_RM_2:
	Pop Si
	Pop Dx
	Ret
;**********************************************************************************************
Check_Sr_1:										;XXXS RXXX
	Push Dx
	Push Si
	Shl Dh, 3
	Shr Dh, 6
	Cmp Dh, 0
	Je Segment_ES
	Cmp Dh, 1
	Je Segment_CS
	Cmp Dh, 2
	Je Segment_SS
	Jmp Segment_DS
Segment_ES:	
	Lea Si, Sr
	Mov Byte Ptr [Ds:Si], 'E'
	Jmp Check_Sr_2
Segment_CS:
	Lea Si, Sr
	Mov Byte Ptr [Ds:Si], 'C'
	Jmp Check_Sr_2
Segment_SS:
	Lea Si, Sr
	Mov Byte Ptr [Ds:Si], 'S'
	Jmp Check_Sr_2
Segment_DS:
	Lea Si, Sr
	Mov Byte Ptr [Ds:Si], 'D'
	Jmp Check_Sr_2
Check_Sr_2:
	Pop Si
	Pop Dx
	Ret
;**********************************************************************************************
Set_Si_To_Code_Name:
	Mov Si, Di
	Sub Si, 15
	Mov Cx, 15
	Ret
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
Add_Enter_To_Code_Output_Buffer:				;Di MUST Point To Place For Enter
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
Clean_Data_Output_Buffer_1:
	Push Si
	Push Cx
	Lea Si, Data_Output_Buffer
	Add Si, 8									;We Do Not Need To Clean First Byte, Because All Commands Are Atleast One Byte Long
	Mov Cx, 12
Clean_Data_Output_Buffer_2:
	Mov Byte Ptr [Ds:Si], ' '
	Inc Si
	Loop Clean_Data_Output_Buffer_2
	Pop Cx
	Pop Si
	Ret
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
Byte_To_Data_Output_Buffer:						;Puts Byte To Data_Output_Buffer
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
	Add Dl, 2
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
Set_W_To_Default:
	Push Si
	Mov Si, Offset W
	Mov Byte Ptr [Ds:Si], 0
	Pop Si
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