using Mos6502Emu.Core.Memory;
using Mos6502Emu.Tests.Extensions;

namespace Mos6502Emu.Tests.Memory
{
    public class MmuTests
    {
        Mmu _mmu;

        [SetUp]
        public void Setup()
        {
            _mmu = new Mmu();
        }

        [Test]
        public void CanLoadProgramAtDefaultAddress()
        {
            _mmu.LoadProgram("Test.prg").ShouldBeTrue();

            _mmu[0x0200].ShouldBe(0xA9);
            _mmu[0x0201].ShouldBe(0xDE);
            _mmu[0x0202].ShouldBe(0x69);
            _mmu[0x0203].ShouldBe(0x2A);
            _mmu[0x0204].ShouldBe(0x85);
            _mmu[0x0205].ShouldBe(0x00);
            _mmu[0x0206].ShouldBe(0xC6);
            _mmu[0x0207].ShouldBe(0x00);
            _mmu[0x0208].ShouldBe(0x60);
        }

        [Test]
        public void CanLoadProgramAtSpecifiedAddress()
        {
            _mmu.LoadProgram("Test.prg", 0x8000).ShouldBeTrue();

            _mmu[0x8000].ShouldBe(0xA9);
            _mmu[0x8001].ShouldBe(0xDE);
            _mmu[0x8002].ShouldBe(0x69);
            _mmu[0x8003].ShouldBe(0x2A);
            _mmu[0x8004].ShouldBe(0x85);
            _mmu[0x8005].ShouldBe(0x00);
            _mmu[0x8006].ShouldBe(0xC6);
            _mmu[0x8007].ShouldBe(0x00);
            _mmu[0x8008].ShouldBe(0x60);
        }

        [Test]
        public void CanReadAndWriteByte()
        {
            _mmu[0x0200] = 0x3A;

            _mmu[0x0200].ShouldBe(0x3A);
        }

        [TestCase(-1)]
        [TestCase(0x10000)]
        public void ReadFromMemoryOutOfRangeThrowsException(int addr)
        {
            Action act = () => { byte _ = _mmu[addr]; };
            act.ShouldThrow<IndexOutOfRangeException>();
        }

        [TestCase(-1)]
        [TestCase(0x10000)]
        public void WriteToMemoryOutOfRangeThrowsException(int addr)
        {
            Action act = () => _mmu[addr] = 0x3A;
            act.ShouldThrow<IndexOutOfRangeException>();
        }
    }
}
