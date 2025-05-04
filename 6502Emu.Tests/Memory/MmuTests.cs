using Mos6502Emu.Core.Memory;

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
            _mmu.LoadProgram("Test.prg").Should().BeTrue();

            _mmu[0x8000].Should().Be(0xA9);
            _mmu[0x8001].Should().Be(0xDE);
            _mmu[0x8002].Should().Be(0x69);
            _mmu[0x8003].Should().Be(0x2A);
            _mmu[0x8004].Should().Be(0x85);
            _mmu[0x8005].Should().Be(0x00);
            _mmu[0x8006].Should().Be(0xC6);
            _mmu[0x8007].Should().Be(0x00);
            _mmu[0x8008].Should().Be(0x60);
        }

        [Test]
        public void CanLoadProgramAtSpecifiedAddress()
        {
            _mmu.LoadProgram("Test.prg", 0x0200).Should().BeTrue();

            _mmu[0x0200].Should().Be(0xA9);
            _mmu[0x0201].Should().Be(0xDE);
            _mmu[0x0202].Should().Be(0x69);
            _mmu[0x0203].Should().Be(0x2A);
            _mmu[0x0204].Should().Be(0x85);
            _mmu[0x0205].Should().Be(0x00);
            _mmu[0x0206].Should().Be(0xC6);
            _mmu[0x0207].Should().Be(0x00);
            _mmu[0x0208].Should().Be(0x60);
        }

        [Test]
        public void CanReadAndWriteByte()
        {
            _mmu[0x0200] = 0x3A;

            _mmu[0x0200].Should().Be(0x3A);
        }

        [TestCase(-1)]
        [TestCase(0x10000)]
        public void ReadFromMemoryOutOfRangeThrowsException(int addr)
        {
            Action act = () => { byte _ = _mmu[addr]; };
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [TestCase(-1)]
        [TestCase(0x10000)]
        public void WriteToMemoryOutOfRangeThrowsException(int addr)
        {
            Action act = () => _mmu[addr] = 0x3A;
            act.Should().Throw<IndexOutOfRangeException>();
        }
    }
}
