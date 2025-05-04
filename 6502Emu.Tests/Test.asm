; Simple test program for my 6502 emulator
  LDA #$DE  ; Load immediate value DE into A
  ADC #$2A  ; Add immediate value 2A to A
  STA $00   ; Store A into memory address 00
  DEC $00   ; Decrement the value at memory address 00
  RTS       ; Return from subroutine
