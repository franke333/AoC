class Monkey:
    monkeys = {}
    mult_div_test=1
    def __init__(self,data):
        self.items = []
        self.name = int(data[0].split()[1][:-1])
        for item in data[1].replace(',','').split()[2:]:
            self.items.append(int(item))
        v1,op,v2 = data[2].split()[3:]
        if op == '+':
            self.operation = lambda old : old + (old if v2 == 'old' else int(v2))
        else:
            self.operation = lambda old : old * (old if v2 == 'old' else int(v2))
        self.div_test = int(data[3].split()[3])
        self.monkey_true = int(data[4].split()[5])
        self.monkey_false = int(data[5].split()[5])
        self.inspected = 0

        Monkey.mult_div_test *= self.div_test
        Monkey.monkeys[self.name] = self
    def TakeTurn(self,divide_by):
        for item in self.items:
            item = (self.operation(item)//divide_by)%Monkey.mult_div_test
            Monkey.monkeys[self.monkey_true if item % self.div_test == 0 \
                else self.monkey_false].items.append(item)
            self.inspected += 1
        self.items = []

# for part in [part1,part2]
for rounds,divide in [(20,3),(10_000,1)]:
    data = []
    for l in open("inputs/input11.txt"):
        data.append(l.lstrip())
        if(len(data) == 7):
            Monkey(data)
            data = []
    for _ in range(rounds):
        for monkey in Monkey.monkeys.values():
            monkey.TakeTurn(divide)

    inspections = [x.inspected for x in Monkey.monkeys.values()]
    inspections.sort()
    print(inspections[-1]*inspections[-2])
    Monkey.monkeys = {}
    Monkey.mult_div_test = 1