.Model small
.Stack 100h
.Data
	VP db 10, 13, "Dominik Lisovski", 10, 13
	VU db "Vilniaus Universitetas, Matematikos ir informatikos fakultetas", 10, 13
	Spc db "Programø Sistemos", 10, 13
    Gr db "6 grupe", 10, 13, "28 programa", 10, 13 
	Pr db "Zaidimas atspiek skaiciu", 10, 13, "$"
	Msg1 db 10, 13, "Sveiki!", 10, 13 
	Msg2 db "Atspekite maksimaliai 3-zenkli skaiciu!", 10, 13, "$"
	Msg3 db "Sveikiname!", 10, 13, "Jus atspejote skaiciu!", "$"
	Er db "Turite ivesti maksimaliai 3-zenkli skaiciu", 13, 10, "$" 
	NL db 13, 10, "$"
	Dsk db "Didesnis", 10, 13, "$"
	Msk db "Mazesnis", 10, 13, "$"
	max db 255
	count db 0
	BUFFER db 255 dup("$")
.Code
Start:
	Mov dx, @data
	Mov ds, dx
	Mov Bx, 81h		;i BX ikeliam 81h reiksme, nes programos parametrai saugomi adresu ES:0081h

Check:
	Mov Ax, Es:[Bx] ;nuskaitom pirmus du parametro baitus
	Inc Bx       
	CMP AL, 13		;Enter
	Je Program
	Cmp Al, 20h		;Space
	Je Check
	Cmp Ax, "?/"
	Jne Program
	Mov Ax, Es:[Bx]
	Cmp Ah, 13
	Je Help
	Jmp Program
	
Help:
	Mov Ah, 09
	Mov Dx, offset VP
	int 21h
	Jmp Exit
	
Program:
	Mov Ah, 09h
	Mov Dx, offset Msg1 ;Lea Dx, Msg1		
	int 21h

Randomizer2013: ;Sugeneruoja random skaiciu.
	MOV Ah, 2CH                   ; CH = hour (0-23) CL = minutes (0-59) DH = seconds (0-59) DL = hundredths (0-99)
    INT 21H    
	Mov Bx, 0
	Mov Ax, 0
	Add Bl, Dl
	Add Bl, Cl
	Add Al, Dh
	Add Ch, 1
	Inc Al
	Div Ch
	Add Bl, Al
	Add Bl, Ah
	Mov Di, Bx

Bandymas:	;Lygina Jusu skaiciu su random skaiciumi
	Mov Ah, 0Ah
	Mov Dx, offset max
	int 21h
	Mov Ah, 9
	Mov Dx, offset NL
	Int 21h
	Mov Al, count
	Cmp Al, 3
	Ja Erroras
	Cmp Al, 1
	Jb Erroras
	MOV SI, offset Max ;i SI registra ikeliam bufferio pradzios adresa
	Add Si, 2
	;prie SI pridedam 2, nes pirmas baitas skirtas bufferio talpai, o antras saugo kiek simboliu buvo nuskaityta
	Mov Ch, 0
	Mov Cl, count
	Xor Ax, Ax
	Xor Dx, Dx
	Mov Bl, 10
	Call Skaicius
	Cmp Di, Ax
	Je Sveikiname
	Ja Didesnis
	Jb Mazesnis
	
Erroras:
	Mov Ah, 9
	Mov Dx, offset Er
	Int 21h
	Jmp Bandymas
	
Skaicius: ;Pakeiciamia simboliu i skaiciu
	Mov DL, [DS:SI] ;i DL ikeliam pirma baita is bufferio
	Inc Si
	Cmp Dl, 48 ;Lyginama su 0
	Jb Erroras
	Cmp Dl, 57 ;Lygina su 9
	Ja Erroras
	Mul Bl
	Sub Dl, 30h
	Add Al, Dl	
	Loop Skaicius
	Ret
	
Didesnis:
	Mov Ah, 9
	Lea Dx, Dsk
	Int 21h
	Jmp Bandymas

Mazesnis:
	Mov Ah, 9
	Lea Dx, Msk
	Int 21h
	Jmp Bandymas

Sveikiname:
	Mov Ah, 9
	Mov Dx, offset Msg3
	Int 21h

Exit: 
	Mov ah, 4Ch
	Mov al, 0
	Int 21h
	
End Start