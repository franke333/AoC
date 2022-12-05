import copy

inp = list()
commands = []
towersCount = -1
for l in open('inputs/input05.txt'):
    l = l[:-1]
    if towersCount == -1:
        if l[1] != '1':
            inp.append(l[1::4])
        else:
            towersCount = int(l.split()[-1])
    elif l!="":
            commands.append([int(x) for x in l.split()[1::2]])
towers = []
for _ in range(towersCount):
    towers.append([])
inp.reverse()
for t in inp:
    for i,c in enumerate(t):
        if c != ' ':
            towers[i].append(c)
towers2 = copy.deepcopy(towers)
#part 1
for howMany,fromT,toT in commands:
    for _ in range(howMany):
        towers[toT-1].append(towers[fromT-1].pop())
#part 2
for howMany,fromT,toT in commands:
    towers2[toT-1].extend(towers2[fromT-1][-howMany:])
    towers2[fromT-1] = towers2[fromT-1][:-howMany]

print("".join([t.pop() for t in towers]))
print("".join([t.pop() for t in towers2]))