# 6502 Test Data

This directory contains a set of test data for the 6502 and 65C02 processors. The tests are designed to validate the behavior of the processor's opcodes and their interactions with memory.

The tests were originally created by Thomas Harte and are under the MIT license. The full repo is available at https://github.com/SingleStepTests/65x02.

That repository provides 10,000 tests per opcode. I have reduced that to 100 and only included the documented opcodes. All tests assume a full 64kb of RAM is mapped to the processor.

Sample test:

	{
		"name": "b1 28 b5",
		"initial": {
			"pc": 59082,
			"s": 39,
			"a": 57,
			"x": 33,
			"y": 174,
			"p": 96,
			"ram": [
				[59082, 177],
				[59083, 40],
				[59084, 181],
				[40, 160],
				[41, 233],
				[59982, 119]
			]
		},
		"final": {
			"pc": 59084,
			"s": 39,
			"a": 119,
			"x": 33,
			"y": 174,
			"p": 96,
			"ram": [
				[40, 160],
				[41, 233],
				[59082, 177],
				[59083, 40],
				[59084, 181],
				[59982, 119]
			]
		},
		"cycles": [
			[59082, 177, "read"],
			[59083, 40, "read"],
			[40, 160, "read"],
			[41, 233, "read"],
			[59083, 40, "read"],
			[59982, 119, "read"]
		]
	}

`name` is provided for human consumption and has no formal meaning.

`initial` is the initial state of the processor; `ram` contains a list of values to store in memory prior to execution, each one in the form `[address, value]`.

`final` is the state of the processor and relevant memory contents after execution.

`cycles` provides a cycle-by-cycle breakdown of bus activity in the form `[address, value, type]` where `type` is either `read` or `write`.
