# 65C02 Instructions

| Byte | Mnemonic | Bytes | Description |
|------|----------|-------|-------------|
| 0x00 | BRK  | 0 | BReaK instruction |
| 0x01 | ORA (zp,x) | 2 | "OR" memory with Accumulator |
| 0x04 | TSB zp | 2 | Test and Set memory Bit |
| 0x05 | ORA zp | 2 | "OR" memory with Accumulator |
| 0x06 | ASL zp | 2 | Arithmetic Shift one bit Left, memory or accumulator |
| 0x07 | RMB0 zp | 2 | Reset Memory Bit |
| 0x08 | PHP  | 0 | PusH Processor status on stack |
| 0x09 | ORA # | 2 | "OR" memory with Accumulator |
| 0x0A | ASL A | 1 | Arithmetic Shift one bit Left, memory or accumulator |
| 0x0C | TSB a | 3 | Test and Set memory Bit |
| 0x0D | ORA a | 3 | "OR" memory with Accumulator |
| 0x0E | ASL a | 3 | Arithmetic Shift one bit Left, memory or accumulator |
| 0x0F | BBR0 r | 1 | Branch on Bit Reset |
| 0x10 | BPL r | 1 | Branch if result PLus (Pn=0) |
| 0x11 | ORA (zp),y | 2 | "OR" memory with Accumulator |
| 0x12 | ORA (zp) | 2 | "OR" memory with Accumulator |
| 0x14 | TRB zp | 2 | Test and Reset memory Bit |
| 0x15 | ORA zp,x | 2 | "OR" memory with Accumulator |
| 0x16 | ASL zp,x | 2 | Arithmetic Shift one bit Left, memory or accumulator |
| 0x17 | RMB1 zp | 2 | Reset Memory Bit |
| 0x18 | CLC  | 0 | CLear Cary flag |
| 0x19 | ORA a,y | 3 | "OR" memory with Accumulator |
| 0x1A | INC A | 1 | INCrement memory or accumulate by one |
| 0x1C | TRB a | 3 | Test and Reset memory Bit |
| 0x1D | ORA a,x | 3 | "OR" memory with Accumulator |
| 0x1E | ASL a,x | 3 | Arithmetic Shift one bit Left, memory or accumulator |
| 0x1F | BBR1 r | 1 | Branch on Bit Reset |
| 0x20 | JSR a | 3 | Jump to new location Saving Return (Jump to SubRoutine) |
| 0x21 | AND (zp,x) | 2 | "AND" memory with accumulator |
| 0x24 | BIT zp | 2 | BIt Test |
| 0x25 | AND zp | 2 | "AND" memory with accumulator |
| 0x26 | ROL zp | 2 | ROtate one bit Left memory or accumulator |
| 0x27 | RMB2 zp | 2 | Reset Memory Bit |
| 0x28 | PLP  | 0 | PuLl Processor status from stack |
| 0x29 | AND # | 2 | "AND" memory with accumulator |
| 0x2A | ROL A | 1 | ROtate one bit Left memory or accumulator |
| 0x2C | BIT a | 3 | BIt Test |
| 0x2D | AND a | 3 | "AND" memory with accumulator |
| 0x2E | ROL a | 3 | ROtate one bit Left memory or accumulator |
| 0x2F | BBR2 r | 1 | Branch on Bit Reset |
| 0x30 | BMI r | 1 | Branch if result MInus (Pn=1) |
| 0x31 | AND (zp),y | 2 | "AND" memory with accumulator |
| 0x32 | AND (zp) | 2 | "AND" memory with accumulator |
| 0x34 | BIT zp,x | 2 | BIt Test |
| 0x35 | AND zp,x | 2 | "AND" memory with accumulator |
| 0x36 | ROL zp,x | 2 | ROtate one bit Left memory or accumulator |
| 0x37 | RMB3 zp | 2 | Reset Memory Bit |
| 0x38 | SEC  | 0 | SEt Carry |
| 0x39 | AND a,y | 3 | "AND" memory with accumulator |
| 0x3A | DEC A | 1 | DECrement memory or accumulate by one |
| 0x3C | BIT a,x | 3 | BIt Test |
| 0x3D | AND a,x | 3 | "AND" memory with accumulator |
| 0x3E | ROL a,x | 3 | ROtate one bit Left memory or accumulator |
| 0x3F | BBR3 r | 1 | Branch on Bit Reset |
| 0x40 | RTI  | 0 | ReTurn from Interrupt |
| 0x41 | EOR (zp,x) | 2 | "Exclusive OR" memory with accumulate |
| 0x45 | EOR zp | 2 | "Exclusive OR" memory with accumulate |
| 0x46 | LSR zp | 2 | Logical Shift one bit Right memory or accumulator |
| 0x47 | RMB4 zp | 2 | Reset Memory Bit |
| 0x48 | PHA  | 0 | PusH Accumulator on stack |
| 0x49 | EOR # | 2 | "Exclusive OR" memory with accumulate |
| 0x4A | LSR A | 1 | Logical Shift one bit Right memory or accumulator |
| 0x4C | JMP a | 3 | JuMP to new location |
| 0x4D | EOR a | 3 | "Exclusive OR" memory with accumulate |
| 0x4E | LSR a | 3 | Logical Shift one bit Right memory or accumulator |
| 0x4F | BBR4 r | 1 | Branch on Bit Reset |
| 0x50 | BVC r | 1 | Branch on oVerflow Clear (Pv=0) |
| 0x51 | EOR (zp),y | 2 | "Exclusive OR" memory with accumulate |
| 0x52 | EOR (zp) | 2 | "Exclusive OR" memory with accumulate |
| 0x55 | EOR zp,x | 2 | "Exclusive OR" memory with accumulate |
| 0x56 | LSR zp,x | 2 | Logical Shift one bit Right memory or accumulator |
| 0x57 | RMB5 zp | 2 | Reset Memory Bit |
| 0x58 | CLI  | 0 | CLear Interrupt disable bit |
| 0x59 | EOR a,y | 3 | "Exclusive OR" memory with accumulate |
| 0x5A | PHY  | 0 | PusH Y register on stack |
| 0x5D | EOR a,x | 3 | "Exclusive OR" memory with accumulate |
| 0x5E | LSR a,x | 3 | Logical Shift one bit Right memory or accumulator |
| 0x5F | BBR5 r | 1 | Branch on Bit Reset |
| 0x60 | RTS  | 0 | ReTurn from Subroutine |
| 0x61 | ADC (zp,x) | 2 | ADd memory to accumulator with Carry |
| 0x64 | STZ zp | 2 | STore Zero in memory |
| 0x65 | ADC zp | 2 | ADd memory to accumulator with Carry |
| 0x66 | ROR zp | 2 | ROtate one bit Right memory or accumulator |
| 0x67 | RMB6 zp | 2 | Reset Memory Bit |
| 0x68 | PLA  | 0 | PuLl Accumulator from stack |
| 0x69 | ADC # | 2 | ADd memory to accumulator with Carry |
| 0x6A | ROR A | 1 | ROtate one bit Right memory or accumulator |
| 0x6C | JMP (a) | 3 | JuMP to new location |
| 0x6D | ADC a | 3 | ADd memory to accumulator with Carry |
| 0x6E | ROR a | 3 | ROtate one bit Right memory or accumulator |
| 0x6F | BBR6 r | 1 | Branch on Bit Reset |
| 0x70 | BVS r | 1 | Branch on oVerflow Set (Pv=1) |
| 0x71 | ADC (zp),y | 2 | ADd memory to accumulator with Carry |
| 0x72 | ADC (zp) | 2 | ADd memory to accumulator with Carry |
| 0x74 | STZ zp,x | 2 | STore Zero in memory |
| 0x75 | ADC zp,x | 2 | ADd memory to accumulator with Carry |
| 0x76 | ROR zp,x | 2 | ROtate one bit Right memory or accumulator |
| 0x77 | RMB7 zp | 2 | Reset Memory Bit |
| 0x78 | SEI  | 0 | SEt Interrupt disable status |
| 0x79 | ADC a,y | 3 | ADd memory to accumulator with Carry |
| 0x7A | PLY  | 0 | PuLl Y register from stack |
| 0x7C | JMP (a,x) | 3 | JuMP to new location |
| 0x7D | ADC a,x | 3 | ADd memory to accumulator with Carry |
| 0x7E | ROR a,x | 3 | ROtate one bit Right memory or accumulator |
| 0x7F | BBR7 r | 1 | Branch on Bit Reset |
| 0x80 | BRA r | 1 | BRanch Always |
| 0x81 | STA (zp,x) | 2 | STore Accumulator in memory |
| 0x84 | STY zp | 2 | STore the Y register in memory |
| 0x85 | STA zp | 2 | STore Accumulator in memory |
| 0x86 | STX zp | 2 | STore the X register in memory |
| 0x87 | SMB0 zp | 2 | Set Memory Bit |
| 0x88 | DEY  | 0 | DEcrement Y by one |
| 0x89 | BIT # | 2 | BIt Test |
| 0x8A | TXA  | 0 | Transfer the X register to the Accumulator |
| 0x8C | STY a | 3 | STore the Y register in memory |
| 0x8D | STA a | 3 | STore Accumulator in memory |
| 0x8E | STX a | 3 | STore the X register in memory |
| 0x8F | BBS0 r | 1 | Branch of Bit Set |
| 0x90 | BCC r | 1 | Branch on Carry Clear (Pc=0) |
| 0x91 | STA (zp),y | 2 | STore Accumulator in memory |
| 0x92 | STA (zp) | 2 | STore Accumulator in memory |
| 0x94 | STY zp,x | 2 | STore the Y register in memory |
| 0x95 | STA zp,x | 2 | STore Accumulator in memory |
| 0x96 | STX zp,y | 2 | STore the X register in memory |
| 0x97 | SMB1 zp | 2 | Set Memory Bit |
| 0x98 | TYA  | 0 | Transfer Y register to the Accumulator |
| 0x99 | STA a,y | 3 | STore Accumulator in memory |
| 0x9A | TXS  | 0 | Transfer the X register to the Stack pointer register |
| 0x9C | STZ a | 3 | STore Zero in memory |
| 0x9D | STA a,x | 3 | STore Accumulator in memory |
| 0x9E | STZ a,x | 3 | STore Zero in memory |
| 0x9F | BBS1 r | 1 | Branch of Bit Set |
| 0xA0 | LDY # | 2 | LoaD the Y register with memory |
| 0xA1 | LDA (zp,x) | 2 | LoaD Accumulator with memory |
| 0xA2 | LDX # | 2 | LoaD the X register with memory |
| 0xA4 | LDY zp | 2 | LoaD the Y register with memory |
| 0xA5 | LDA zp | 2 | LoaD Accumulator with memory |
| 0xA6 | LDX zp | 2 | LoaD the X register with memory |
| 0xA7 | SMB2 zp | 2 | Set Memory Bit |
| 0xA8 | TAY  | 0 | Transfer the Accumulator to the Y register |
| 0xA9 | LDA # | 2 | LoaD Accumulator with memory |
| 0xAA | TAX  | 0 | Transfer the Accumulator to the X register |
| 0xAC | LDY a | 3 | LoaD the Y register with memory |
| 0xAD | LDA a | 3 | LoaD Accumulator with memory |
| 0xAE | LDX a | 3 | LoaD the X register with memory |
| 0xAF | BBS2 r | 1 | Branch of Bit Set |
| 0xB0 | BCS r | 1 | Branch on Carry Set (Pc=1) |
| 0xB1 | LDA (zp),y | 2 | LoaD Accumulator with memory |
| 0xB2 | LDA (zp) | 2 | LoaD Accumulator with memory |
| 0xB4 | LDY zp,x | 2 | LoaD the Y register with memory |
| 0xB5 | LDA zp,y | 2 | LoaD Accumulator with memory |
| 0xB6 | LDX zp,y | 2 | LoaD the X register with memory |
| 0xB7 | SMB3 zp | 2 | Set Memory Bit |
| 0xB8 | CLV  | 0 | CLear oVerflow flag |
| 0xB9 | LDA a,y | 3 | LoaD Accumulator with memory |
| 0xBA | TSX  | 0 | Transfer the Stack pointer to the X register |
| 0xBC | LDY a,x | 3 | LoaD the Y register with memory |
| 0xBD | LDA a,x | 3 | LoaD Accumulator with memory |
| 0xBE | LDX a,y | 3 | LoaD the X register with memory |
| 0xBF | BBS3 r | 1 | Branch of Bit Set |
| 0xC0 | CPY # | 2 | ComPare memory and Y register |
| 0xC1 | CMP (zp,x) | 2 | CoMPare memory and accumulator |
| 0xC4 | CPY zp | 2 | ComPare memory and Y register |
| 0xC5 | CMP zp | 2 | CoMPare memory and accumulator |
| 0xC6 | DEC zp | 2 | DECrement memory or accumulate by one |
| 0xC7 | SMB4 zp | 2 | Set Memory Bit |
| 0xC8 | INY  | 0 | INcrement Y register by one |
| 0xC9 | CMP # | 2 | CoMPare memory and accumulator |
| 0xCA | DEX  | 0 | DEcrement X by one |
| 0xCB | WAI  | 0 | WAit for Interrupt |
| 0xCC | CPY a | 3 | ComPare memory and Y register |
| 0xCD | CMP a | 3 | CoMPare memory and accumulator |
| 0xCE | DEC a | 3 | DECrement memory or accumulate by one |
| 0xCF | BBS4 r | 1 | Branch of Bit Set |
| 0xD0 | BNE r | 1 | Branch if Not Equal (Pz=0) |
| 0xD1 | CMP (zp),y | 2 | CoMPare memory and accumulator |
| 0xD2 | CMP (zp) | 2 | CoMPare memory and accumulator |
| 0xD5 | CMP zp,x | 2 | CoMPare memory and accumulator |
| 0xD6 | DEC zp,x | 2 | DECrement memory or accumulate by one |
| 0xD7 | SMB5 zp | 2 | Set Memory Bit |
| 0xD8 | CLD  | 0 | CLear Decimal mode |
| 0xD9 | CMP a,y | 3 | CoMPare memory and accumulator |
| 0xDA | PHX  | 0 | PusH X register on stack |
| 0xDB | STP  | 0 | SToP mode |
| 0xDD | CMP a,x | 3 | CoMPare memory and accumulator |
| 0xDE | DEC a,x | 3 | DECrement memory or accumulate by one |
| 0xDF | BBS5 r | 1 | Branch of Bit Set |
| 0xE0 | CPX # | 2 | ComPare memory and X register |
| 0xE1 | SBC (zp,x) | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xE4 | CPX zp | 2 | ComPare memory and X register |
| 0xE5 | SBC zp | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xE6 | INC zp | 2 | INCrement memory or accumulate by one |
| 0xE7 | SMB6 zp | 2 | Set Memory Bit |
| 0xE8 | INX  | 0 | INcrement X register by one |
| 0xE9 | SBC # | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xEA | NOP  | 0 | No OPeration |
| 0xEC | CPX a | 3 | ComPare memory and X register |
| 0xED | SBC a | 3 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xEE | INC a | 3 | INCrement memory or accumulate by one |
| 0xEF | BBS6 r | 1 | Branch of Bit Set |
| 0xF0 | BEQ r | 1 | Branch if EQual (Pz=1) |
| 0xF1 | SBC (zp),y | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xF2 | SBC (zp) | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xF5 | SBC zp,x | 2 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xF6 | INC zp,x | 2 | INCrement memory or accumulate by one |
| 0xF7 | SMB7 zp | 2 | Set Memory Bit |
| 0xF8 | SED  | 0 | SEt Decimal mode |
| 0xF9 | SBC a,y | 3 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xFA | PLX  | 0 | PuLl X register from stack |
| 0xFD | SBC a,x | 3 | SuBtract memory from accumulator with borrow (Carry bit) |
| 0xFE | INC a,x | 3 | INCrement memory or accumulate by one |
| 0xFF | BBS7 r | 1 | Branch of Bit Set |

